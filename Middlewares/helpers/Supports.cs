using System.Security.Cryptography;
using System.Text;

namespace StudentService.Middlewares.helpers
{
    public class Supports
    {
        public static string GenerateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes); 

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}
