﻿using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public UserEntity Registration(UserRegistration user);

        public string LoginUser(LoginUser loginUser);

        public string GenerateJWTToken(long userid);
    }
}
