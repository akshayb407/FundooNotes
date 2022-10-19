using CommonLayer.Models;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRl : IUserRL
    {
     public readonly UserContext ucontext;
        public UserRl(UserContext ucontext)
        {
            this.ucontext = ucontext;
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
        public UserEntity LoginUser(LoginUser loginUser)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity = this.ucontext.Users.FirstOrDefault(x => x.Email == loginUser.Email && x.Password == loginUser.Password);
                if (userEntity == null)
                {
                    return null;
                }
                return userEntity;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
