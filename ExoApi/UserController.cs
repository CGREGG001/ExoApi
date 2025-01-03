using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExoApi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public UserServices UserServices;

        public UserController(UserServices userServices)
        {
            UserServices = userServices;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public IActionResult GetAllUsers()
        {
            List<User> users = UserServices.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("getById/{id:int}")]
        [AllowAnonymous]
        public IActionResult GetUserById(int id)
        {
            User user = UserServices.GetUserById(id);
            return Ok(user);
        }

        [HttpDelete("delete/{id:int}")]
        [AllowAnonymous]
        public IActionResult DeleteUser(int id)
        {
            UserServices.DeleteUser(id);
            return Ok();
        }

        [HttpPatch("update/{id:int}")]
        [AllowAnonymous]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            UserServices.UpdateUser(id, user);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginUser([FromBody] User user)
        {
            User? newUser = UserServices.Login(user.Email);
            return Ok(newUser);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult RegisterUser([FromBody] User user)
        {
            UserServices.Register(user);
            return Ok();
        }

    }
}
