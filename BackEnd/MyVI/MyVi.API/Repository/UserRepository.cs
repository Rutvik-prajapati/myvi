using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyVi.API.Authentication;
using MyVi.API.Entities;
using MyVi.API.IRepository;
using MyVi.API.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUser
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public UserRepository(MyVIDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration) : base(context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        // Login Users
        public async Task<TokenModel> LoginUser(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);

            var userRoles = await userManager.GetRolesAsync(user);

            if (user == null || !await userManager.CheckPasswordAsync(user, password))
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                //expires: DateTime.Now.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new TokenModel() { Token = new JwtSecurityTokenHandler().WriteToken(token), ValidTo = token.ValidTo };

        }

        // Register User
        public async Task<IdentityResult> RegisterUser(RegisterModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }

            if (result.Succeeded)
            {
                context.User.Add(new User()
                {
                    ContactNo = model.PhoneNumber,
                    Email = model.Email,
                    Password = user.PasswordHash,
                    UserName = model.Username,
                    IsActive = true,
                    IsDeleted = false,
                    OnCreated = DateTime.Now,
                    OnUpdated = DateTime.Now
                });

                context.SaveChanges();
            }

            return result;
        }

        // Get information of a particular user based on username
        public UserModel FindName(string name)
        {
            var registeredUser = context.User.Where(x => x.IsDeleted == false)
                .Select(x => new UserModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    Email = x.Email,
                    ContactNo = x.ContactNo
                }).SingleOrDefault(x => x.UserName == name);
            return registeredUser;
        }

        public UserModel FindContact(string contact)
        {
            var registeredUser = context.User.Where(x => x.IsDeleted == false)
                .Select(x => new UserModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    Email = x.Email,
                    ContactNo = x.ContactNo
                }).SingleOrDefault(x => x.ContactNo == contact);
            return registeredUser;
        }
        public IEnumerable GetAllUsers()
        {
            var users = context.User.Where(x => x.IsDeleted == false)
                .Select(x => new UserModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    Email = x.Email,
                    ContactNo = x.ContactNo
                }).ToList();

            return users;
        }

        public UserModel FindId(int id)
        {
            var registeredUser = context.User.Where(x => x.IsDeleted == false)
                .Select(x => new UserModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    Email = x.Email,
                    ContactNo = x.ContactNo
                }).SingleOrDefault(x => x.Id == id);
            return registeredUser;
        }

        public override void Delete(int id)
        {
            var registeredUser = context.User.Where(x => x.Id == id).FirstOrDefault();
            registeredUser.IsDeleted = true;
            registeredUser.OnUpdated = DateTime.Now;
            context.SaveChanges();
        }

        public bool CheckUserExist(int userId)
        {
            var isExist = false;
            isExist = context.User.Any(x => x.Id == userId);
            return isExist;
        }
    }
}
