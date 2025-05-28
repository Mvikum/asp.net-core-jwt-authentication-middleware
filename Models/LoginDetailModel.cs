using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models
{
    [Table("login_detail")]
    public class LoginDetailModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public long user_id { get; set; }

        public string token { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime updated_at { get; set; }
    }
}
