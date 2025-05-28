using StudentService.DTOs.Requests;
using StudentService.Middlewares.helpers.Response;

namespace StudentService.Services
{
    public interface IStudentService
    {
        BaseResponse CreateStudent(CreateStudentRequest request);

        BaseResponse StudentList();

        BaseResponse GetStudentById(int id);

        BaseResponse UpdateStudentById(int id,UpdateStudentRequest request);

        public BaseResponse DeleteStudentById(int id);

    }
}
