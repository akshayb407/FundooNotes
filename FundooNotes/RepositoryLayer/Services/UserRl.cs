using CommonLayer.Models;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace RepositoryLayer.Services
{
    public class UserRl : IUserRL
    {
     public readonly UserContext ucontext;

        public readonly IConfiguration Iconfiguration;
        public UserRl(UserContext ucontext, IConfiguration Iconfiguration)
        {
            this.ucontext = ucontext;
            this.Iconfiguration = Iconfiguration;
        }

        public UserEntity Registration(UserRegistration user)
        {
            try
            {
                UserEntity entity = new UserEntity();
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Email = user.Email;
                entity.Password = user.Password;
                ucontext.Users.Add(entity);
                int result = this.ucontext.SaveChanges();
                if(result > 0)
                {
                    return entity;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
           
        }
        public string LoginUser(LoginUser loginUser)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity = this.ucontext.Users.FirstOrDefault(x => x.Email == loginUser.Email && x.Password == loginUser.Password);
                var id = userEntity.UserId;
                if (userEntity != null)
                {
                    var token = GenerateJWTToken(id);
                    return token;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string GenerateJWTToken(long  userid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Iconfiguration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", userid.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }  
}
