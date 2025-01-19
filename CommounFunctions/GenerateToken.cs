using NexGen.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NexGen.CommonFunction
{
    public class GenerateToken
    {
        public static string GetToken(Admin admin, IConfiguration configuration)
        {
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim("Id",admin.Id.ToString()),
                new Claim("User_Name",admin.Name),
                new Claim("Email", admin.Email),
                new Claim("Phone", admin.Phone)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                Claims,
                expires: DateTime.UtcNow.AddDays(10),
                signingCredentials: signIn
                );
            var Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }
    }
}
