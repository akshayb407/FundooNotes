using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public UserEntity Registration(UserRegistration userRegistration);

        public string LoginUser(LoginModel loginModel);


    }

}
