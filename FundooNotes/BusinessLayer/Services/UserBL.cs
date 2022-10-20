using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL iuserrl;

        public UserBL(IUserRL userrl)
        {
            this.iuserrl = userrl;
        }

        public UserEntity Registration(UserRegistration userRegistration)
        {
            try
            {
                return this.iuserrl.Registration(userRegistration);
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
                return this.iuserrl.LoginUser(loginUser);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateJWTToken(long emailid)
        {
            try
            {
                return iuserrl.GenerateJWTToken(emailid);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}