using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vivigest_backend.Application.Interfaces.IAuth;
using Vivigest_backend.Domain.Entities;

namespace Vivigest_backend.Infrastructure.Authentication
{
    public class JwtProvider: IJwtProvider
    {
        private readonly IConfiguration _configuration;

        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(User user)
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var secureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Encrypt algorithm
            // We are using HMACSHA256
            var credentials = new SigningCredentials(secureKey, SecurityAlgorithms.HmacSha256);


            // We add public data to the payload
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.IdUser.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Person.Names.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Person.Email),
            };

            if (user.UserRols != null)
            {
                foreach (var userRol in user.UserRols)
                {
                    claims.Append(new Claim(ClaimTypes.Role, userRol.Rol.NameRol));
                }
            }

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
