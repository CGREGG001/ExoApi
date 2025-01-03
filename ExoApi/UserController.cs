using Microsoft.AspNetCore.Mvc;

namespace ExoApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserServices UserServices;

        public UserController(UserServices userServices)
        {
            UserServices = userServices;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            List<User> users = UserServices.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            User user = UserServices.GetUserById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            UserServices.DeleteUser(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            UserServices.UpdateUser(id, user);
            return Ok();
        }

        [HttpPost]
        public IActionResult LoginUser([FromBody] User user)
        {

            User? newUser = UserServices.Login(user.Email);
            return Ok(newUser);
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] User user)
        {
            UserServices.Register(user);
            return Ok();
        }

    }
}
