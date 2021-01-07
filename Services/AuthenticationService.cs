using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RetoBackendApp.models;
using RetoBackendApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RetoBackendApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;
        public AuthenticationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        private List<User> users = new List<User>()
        {
            new User{UserId = 1, FirstName = "Cristian", LastName = "Peralta", UserName = "cristianeph", Password = "123456"}
        };

        public User Authenticate(string userName, string password)
        {
            var user = users.SingleOrDefault(x => x.UserName == userName && x.Password == password);
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.UserId.ToString()),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Version, "V3.1"),
                    }),
                    Expires = DateTime.Now.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.Password = null;
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
