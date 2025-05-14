using DevIntel.Application.Interfaces;
using DevIntel.Domain.Entities;
using DevIntel.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;

        public JwtTokenGenerator(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()), // Use ClaimTypes.Role instead
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                                       issuer: _settings.Issuer,
                                       audience: _settings.Audience,
                                       claims: claims,
                                       expires: DateTime.Now.AddMinutes(_settings.DurationInMinutes),
                                       signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
