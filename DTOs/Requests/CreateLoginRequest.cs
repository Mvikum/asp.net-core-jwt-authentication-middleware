using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.DTOs.Requests
{
    public class CreateLoginRequest
    {
        public long user_id { get; set; }

        public string token { get; set; }
    }
}
