using Microsoft.AspNetCore.Identity;
using MyVi.API.Authentication;
using MyVi.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.IRepository
{
    public interface IAdmin
    {
        Task<IdentityResult> RegisterAdmin(RegisterModel model);
        public void CreateAdmin(RegisterModel model);
        public Admin FindName(string name);
        public Admin FindContact(string contact);
    }
}
