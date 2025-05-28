using StudentService.Middlewares.helpers.Requests;
using StudentService.Middlewares.helpers.Response;

namespace StudentService.Services
{
    public interface IAuthService
    {
        BaseResponse Authenticate(AuthenticateRequest request);

    }
}
