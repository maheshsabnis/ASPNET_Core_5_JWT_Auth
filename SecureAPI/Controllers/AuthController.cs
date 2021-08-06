using Microsoft.AspNetCore.Mvc;
using SecureAPI.Services;
using SharedModels.Models;
using System.Threading.Tasks;

namespace SecureAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AuthService authenticationService;
        public AuthController(AuthService authenticationService)
        {
            this.authenticationService = authenticationService;
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                var IsCreated = await authenticationService.RegisterUserAsync(user);
                if (IsCreated == false)
                {
                    return Conflict("The User Already Present");
                }
                var ResponseData = new ResponseData()
                {
                    Message = $"{user.Email} User Created Successfully"
                };
                return Ok(ResponseData);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser inputModel)
        {
            if (ModelState.IsValid)
            {
                var token = await authenticationService.AuthenticateUserAsync(inputModel);
                if (token == null)
                {
                    return Unauthorized("The Authentication Failed");
                }
                var ResponseData = new ResponseData()
                {
                    Message = token
                };

                return Ok(ResponseData);
            }
            return BadRequest(ModelState);
        }
    }
}
