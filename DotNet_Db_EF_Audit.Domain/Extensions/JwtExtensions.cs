using DotNet_Db_EF_Audit.Domain.Configuration;
using DotNet_Db_EF_Audit.Domain.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNet_Db_EF_Audit.Domain.Extensions
{
    public static class JwtExtensions
    {
        public static string GenerateJwtToken(UserDto user, AuthConfiguration auth)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(auth.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("userid", user.Id.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: auth.Issuer,
                audience: auth.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
