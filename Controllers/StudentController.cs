using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentService.DTOs.Requests;
using StudentService.Middlewares.helpers.Response;
using StudentService.Services;

namespace StudentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpPost("save")]
        public BaseResponse CreateStudent(CreateStudentRequest request)
        {
            return studentService.CreateStudent(request);
        }

        [HttpGet("list")]
        public BaseResponse StudentList()
        {
            return studentService.StudentList();
        }

        [HttpPost("{id}")]
        public BaseResponse GetStudentById(int id)
        {
            return studentService.GetStudentById(id);
        }

        [HttpPut("{id}")]
        public BaseResponse UpdateStudentById(int id, UpdateStudentRequest request)
        {
            return studentService.UpdateStudentById(id, request);
        }

        [HttpDelete("{id}")]
        public BaseResponse DeleteStudentById(int id)
        {
            return studentService.DeleteStudentById(id);
        }
    }
}
