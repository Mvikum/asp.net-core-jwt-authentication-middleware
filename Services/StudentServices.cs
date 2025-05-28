using Microsoft.AspNetCore.Http.HttpResults;
using StudentService.DTOs;
using StudentService.DTOs.Requests;
using StudentService.Middlewares.helpers;
using StudentService.Middlewares.helpers.Response;
using StudentService.Models;

namespace StudentService.Services
{
    public class StudentServices : IStudentService
    {
        private readonly ApplicationDbContext context;
        public StudentServices(ApplicationDbContext applicationDbContext)
        {
            this.context = applicationDbContext;
        }

        public BaseResponse CreateStudent(CreateStudentRequest request)
        {
            BaseResponse response;

            try
            {
                StudentModel newStudent = new StudentModel();
                newStudent.name = request.name;
                newStudent.password = Supports.GenerateMD5(request.password);
                newStudent.email = request.email;
                newStudent.dob = request.dob;
                newStudent.gender = request.gender;

                using (context)
                {
                    context.Add(newStudent);
                    context.SaveChanges();
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully Created the new student" }
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

        public BaseResponse StudentList()
        {
            BaseResponse response;

            try
            {
                List<StudentDTO> students = new List<StudentDTO>();

              
                    context.Students.ToList().ForEach(student => students.Add(new StudentDTO
                    {
                        id = student.id,
                        name = student.name,
                        password = student.password,
                        email = student.email,
                        dob = student.dob,
                        gender = student.gender
                    }));
                

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { students }
                };
                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "internal server error " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponse GetStudentById(int id)
        {
            BaseResponse response;

            try
            {
                StudentDTO student = new StudentDTO();

                using (context)
                {
                    StudentModel filteredStudent = context.Students.Where(student => student.id == id).FirstOrDefault();

                    if (filteredStudent != null)
                    {
                        student.id = filteredStudent.id;
                        student.name = filteredStudent.name;
                        student.password = filteredStudent.password;
                        student.email = filteredStudent.email;
                        student.dob = filteredStudent.dob;
                        student.gender = filteredStudent.gender;
                    }
                    else
                    {
                        student = null;
                    }
                }
                if (student != null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { student }
                    };
                }
                else
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status404NotFound,
                        data = new { message = "No Student found" }
                    };
                }
                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Interal Server Error" + ex.Message }
                };

                return response;
            }
        }

        public BaseResponse UpdateStudentById(int id, UpdateStudentRequest request)
        {
            BaseResponse response;

            try
            {
                using (context)
                {
                    StudentModel filteredStudent = context.Students.Where(student => student.id == id).FirstOrDefault();

                    if (filteredStudent != null)
                    {
                        filteredStudent.name = request.name;
                        filteredStudent.password = request.password;
                        filteredStudent.email = request.email;
                        filteredStudent.dob = request.dob;
                        filteredStudent.gender = request.gender;

                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Student Update Successfully" }

                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No student found" }
                        };

                    }
                }
                return response;
            }

            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server error : " + ex.Message }
                };
                return response;
            }
        }

        public BaseResponse DeleteStudentById(int id)
        {
            BaseResponse response;
            try
            {
                using (context)
                {
                    StudentModel studentToDelete = context.Students.Where(student => student.id == id).FirstOrDefault();
                    if (studentToDelete != null)
                    {
                        context.Students.Remove(studentToDelete);
                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Student deleted successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No student found" }
                        };
                    }
                    return response;

                }
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error..." + ex.Message }
                };
                return response;
            }

        }
    }
}