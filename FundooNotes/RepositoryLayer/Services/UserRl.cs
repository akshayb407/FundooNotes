using CommonLayer.Models;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRl : IUserRL
    {
     public readonly Context context;
        public UserEntity Registration(UserRegistration user)
        {
            try
            {
                UserEntity entity = new UserEntity();
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Email = user.Email;
                entity.Password = user.Password;
                context.Add(entity);
                int result = this.context.SaveChanges();
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
    }
}
