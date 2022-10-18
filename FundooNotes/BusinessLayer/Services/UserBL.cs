using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL:IUserBL
    {
        IUserRL iuserrl;

        public UserBL(IUserRL userrl)
        {
            this.iuserrl = userrl;
        }
        public UserEntity UserRegistration(UserRegistration user)
        {
            try
            {
                return this.iuserrl.Registration(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
