using StudentService.DTOs.Requests;
using StudentService.Middlewares.helpers.Response;
using StudentService.Middlewares.helpers;
using StudentService.Models;
using StudentService.Middlewares.helpers.utils;

namespace StudentService.Services
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext context;
        public LoginService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public BaseResponse CreateLogin(CreateLoginRequest request)
        {
            BaseResponse response;

            try
            {
                LoginDetailModel newLogger = new LoginDetailModel();

                newLogger.user_id = request.user_id;
                newLogger.token = request.token;

                using (context)
                {
                    context.Add(newLogger);
                    context.SaveChanges();
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully Created the new Login" }
                };
                return response;

            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error : " + ex.Message }
                };

                return response;
            }
        }
    }
}
