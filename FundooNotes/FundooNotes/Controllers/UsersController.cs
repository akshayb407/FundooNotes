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
    }
}
