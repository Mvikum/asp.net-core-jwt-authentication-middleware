using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentService.Middlewares.helpers.Requests;
using StudentService.Middlewares.helpers.Response;
using StudentService.Services;

namespace StudentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("authenticate")]
        public BaseResponse Authenticate(AuthenticateRequest request)
        {
            return authService.Authenticate(request);
        }
    }
}
