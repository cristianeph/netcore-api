using Microsoft.AspNetCore.Mvc;
using RetoBackendApp.Models;
using RetoBackendApp.Services;

namespace RetoBackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            var user = _authenticationService.Authenticate(model.UserName, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Credenciales incorrectas" });
            }
            else
            {
                return  Ok(user);
            }
        }
    }
}
