using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserBL userBL;
       
        public UsersController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]

        public IActionResult AddUser(UserRegistration userRegistration)
        {
            try
            {
                var reg = this.userBL.Registration(userRegistration);
                if (reg != null)
                {
                    return this.Ok(new { Success = true, message = $"Registration sucessful for {userRegistration.Email}" });
                }
                else
                {
                    return this.BadRequest(new { Sucess = false, message = $"Registration failed for {userRegistration.Email}" });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginUser loginUser)
        {
            try
            {
                var Login = this.userBL.LoginUser(loginUser);
                if (Login != null)
                {
                    return this.Ok(new { success = true, message = $"login successful for {loginUser.Email}" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = $"login failed for {loginUser.Email}" });

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
