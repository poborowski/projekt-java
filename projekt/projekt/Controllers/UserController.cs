using Microsoft.AspNetCore.Mvc;

namespace projekt.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IAccountService _service;
        public UserController(IAccountService service)
        {
            _service = service;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _service.RegisterUser(dto);
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            string token = _service.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
