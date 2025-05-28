using System.ComponentModel.DataAnnotations;

namespace StudentService.DTOs.Requests
{
    public class CreateStudentRequest
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string dob { get; set; }
        [Required]
        public string gender { get; set; }
    }
}
