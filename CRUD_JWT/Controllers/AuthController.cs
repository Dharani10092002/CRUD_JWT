using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD_JWT.DTO;
using CRUD_JWT.Services;

namespace CRUD_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService _authser)
        {        
            _authService = _authser;
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register(RegisterRequest reg_req)
        {
            bool result = await _authService.Register(reg_req);

            if(result)
            {                 
                return Ok("User Register Successfully");
            }
            else
            {
                return BadRequest("User Already Exist");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest log_req)
        {
            var result = await _authService.Login(log_req);

            if(result == null)
            {
                return Unauthorized("Invalid Email or Password");
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
