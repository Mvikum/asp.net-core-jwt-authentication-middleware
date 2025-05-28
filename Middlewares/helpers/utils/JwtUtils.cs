using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentService.Middlewares.helpers.utils
{
    public static class JwtUtils
    {
        static string secret = "3hO4Lash4CzZfk0Ga6yQhd4820BRGTAu";

        public static string GenerateJwtToken(StudentModel student)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secret);

            //token claims
            List<Claim> claims = new List<Claim>
            {
                new Claim("user_id",student.id.ToString()),
                new Claim("name",student.name)
             };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);

        }

        public static bool ValidateJwtToken(string jwt)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(secret);

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
                JwtSecurityToken validatedJWT = (JwtSecurityToken)validatedToken;

                int userId = int.Parse(validatedJWT.Claims.First(claim => claim.Type == "user_id").Value);

                using(ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    StudentModel? student = dbContext.Students.FirstOrDefault(s => s.id == userId);

                    if(student == null)
                    {
                        return false;

                    }
                    else
                    {
                        LoginDetailModel loginDetail = dbContext.LoginDetails.Where(Id => Id.user_id == userId).First();

                        if(loginDetail.token != jwt)
                        {
                            return false;

                        }
                        else
                        {
                            return true;
                        }

                    }
                }
            }
            catch (Exception ex) {
                return false;
            }
        }


    }
}
