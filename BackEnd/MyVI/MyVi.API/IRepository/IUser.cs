using Microsoft.AspNetCore.Identity;
using MyVi.API.Authentication;
//using MyVi.API.Entities;
using MyVi.API.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IUser : IGenericInterface<ApplicationUser>
    {
        Task<IdentityResult> RegisterUser(RegisterModel model);
        Task<TokenModel> LoginUser(string userName, string password);
        public UserModel FindName(string name);
        public UserModel FindContact(string contact);
        public IEnumerable GetAllUsers();
        public UserModel FindId(int id);
        public bool CheckUserExist(int userId);
    }
}
