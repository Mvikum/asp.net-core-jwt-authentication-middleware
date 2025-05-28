using Microsoft.EntityFrameworkCore;
using StudentService.DTOs;
using StudentService.Middlewares.helpers;
using StudentService.Middlewares.helpers.Requests;
using StudentService.Middlewares.helpers.Response;
using StudentService.Middlewares.helpers.utils;
using StudentService.Models;

namespace StudentService.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext dbContext;

        public AuthService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public BaseResponse Authenticate(AuthenticateRequest request)
        {
            try
            {
                StudentModel? student = dbContext.Students.Where(u => u.name == request.username).FirstOrDefault();
                if (student == null)
                {
                    return new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Invalid username or password"));
                }

                string md5Password = Supports.GenerateMD5(request.password);
                if (student.password == md5Password)
                {
                    string jwt = JwtUtils.GenerateJwtToken(student);

                    LoginDetailModel? loginDetail = dbContext.LoginDetails.Where(Id => Id.user_id == student.id).FirstOrDefault();

                    if (loginDetail == null)
                    {
                        loginDetail = new LoginDetailModel();
                        loginDetail.user_id = student.id;
                        loginDetail.token = jwt;

                        dbContext.LoginDetails.Add(loginDetail);
                    }
                    else
                    {
                        loginDetail.token = jwt;
                    }
                    dbContext.SaveChanges();
                    return new BaseResponse(StatusCodes.Status200OK, new { token = jwt });
                }
                else
                {
                    return new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Invalid username or password"));
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse { status_code = StatusCodes.Status500InternalServerError, data = new MessageDTO(ex.Message) };
            }

        

        }
    }
}
