using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyVi.API.IRepository;
using MyVi.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVi.API.Controllers
{
    [Route("api/myvi/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUser users;

        public UsersController(IUser context)
        {
            users = context;
        }

        // GET: api/myvi/Users/GetUsers
        [Authorize]
        [HttpGet]
        [Route("GetUsers")]
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            return Ok(users.GetAllUsers());
        }

        // GET: api/myvi/Users/GetUserById
        [Authorize]
        [HttpGet]
        [Route("GetUserById")]
        public ActionResult<IEnumerable<UserModel>> GetUserById(int Id)
        {
            if (Id <= 0)
                return BadRequest ("Invalid UserId");
            
            var user = users.FindId(Id);
            
            if (user == null)
            {
                NotFound(user);
            }
            
            return Ok(user);
        }

        // GET: api/myvi/Users/GetUserByName
        [Authorize]
        [HttpGet]
        [Route("GetUserByName")]
        public ActionResult<IEnumerable<UserModel>> GetUserByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest ("Invalid Username");

            var user = users.FindName(userName);
            
            if (user == null)
            {
                NotFound(user);
            }
            
            return Ok(user);
        }

        // GET: api/myvi/Users/GetUserByContact
        [Authorize]
        [HttpGet]
        [Route("GetUserByContact")]
        public ActionResult<IEnumerable<UserModel>> GetUserByContact(string contact)
        {
            if (string.IsNullOrEmpty(contact))
                return BadRequest("Invalid contact no.");

            var user = users.FindContact(contact);

            if (user == null)
            {
                NotFound(user);
            }

            return Ok(user);
        }

        // GET: api/myvi/Users/DeleteUserById
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteUserById")]
        public ActionResult<IEnumerable<UserModel>> DeleteUserById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid UserId");

            users.Delete(id);

            return Ok(new { Status = "Success", Message = "Successfully Delete User" });
        }
    }
}
