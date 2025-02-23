using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebDevCrafts.DbConnections;
using WebDevCrafts.Helpers;
using WebDevCrafts.Models.Entities;
using WebDevCrafts.Services;

namespace WebDevCrafts.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly UserService userService;

        public UserController(AppDbContext db)
        {
            this.db = db;
            this.userService = new UserService(db);
        }

        [HttpGet]
        [Route("api/getusers")]
        public IActionResult GetUsers()
        {
            var data = userService.GetAllUsers();
            return Ok(data);
        }

        [HttpPost]
        [Route("api/addusers")]
        public IActionResult AddUser(UserModel user)
        {
            var userAdded = userService.AddUser(user);
            if (!userAdded)
            {
                return BadRequest("User already exist");
            }
            return Ok("User created successfully");
        }

        [HttpDelete]
        [Route("api/deleteuser/{userId}")]
        public IActionResult DeleteUser(string userId)
        {
            var userDeleted = userService.RemoveUser(userId);
            if (!userDeleted)
            {
                return BadRequest("User doesn't exist");
            }
            return Ok("User deleted successfully");
        }
    }
}
