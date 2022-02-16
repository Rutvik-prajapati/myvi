using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MyVi.API.Authentication;
using MyVi.API.Entities;
using MyVi.API.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Repository
{
    public class AdminRepository : GenericRepository<ApplicationUser>, IAdmin
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AdminRepository(MyVIDBContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration) : base(context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        //To Register admin
        public async Task<IdentityResult> RegisterAdmin(RegisterModel model)
        {
            ApplicationUser admin = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber
            };
            var result = await userManager.CreateAsync(admin, model.Password);

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(admin, UserRoles.Admin);
            }

            context.Admin.Add(new Admin()
            {
                ContactNo = model.PhoneNumber,
                Email = model.Email,
                Password = admin.PasswordHash,
                UserName = model.Username,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });

            context.SaveChanges();

            return result;
        }

        //Map admin details to Admins table in BookMyShowDB from Users table in BookMyShowAuthenticationAPIDB
        public void CreateAdmin(RegisterModel model)
        {
            context.Admin.Add(new Admin()
            {
                ContactNo = model.PhoneNumber,
                Email = model.Email,
                Password = model.Password,
                UserName = model.Username,
                IsActive = true,
                IsDeleted = false,
                OnCreated = DateTime.Now,
                OnUpdated = DateTime.Now
            });

            context.SaveChanges();
        }

        //Return Admin information based on username
        public Admin FindName(string name)
        {
            var registeredAdmin = context.Admin.SingleOrDefault(x => x.UserName == name);
            return registeredAdmin;
        }

        public Admin FindContact(string contact)
        {
            var registeredAdmin = context.Admin.SingleOrDefault(x => x.ContactNo == contact);
            return registeredAdmin;
        }
    }
}
