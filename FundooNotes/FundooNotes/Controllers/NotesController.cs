using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL noteBL;
        public NotesController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddNotes(NoteModel addnote)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "UserId").Value);
                var result = noteBL.AddNote(addnote, userid);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Note Added Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to add note" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long NoteId)
        {
            try
            {
                var delete = noteBL.DeleteNote(NoteId);
                if (delete != null)
                {
                    return this.Ok(new { Success = true, message = "Notes Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes Deleted Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("GetNoteByUserId")]
        public IActionResult GetNoteByUserID(long userID)
        {
            try
            {
                //long note = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "userID").Value);
                List<UserNotes> result = noteBL.GetNotebyUserId(userID);
                if (result == null)
                {
                    return this.Ok(new { Success = false, message = " Note not Available" });
                }
                else
                {
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, message = " Note got Successfully", data = result });
                    }
                    return this.BadRequest(new { Success = false, message = " error occured" });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNote(NoteModel noteModel, long NoteId)
        {
            try
            {
                var result = noteBL.UpdateNote(noteModel, NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Notes Found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
      
    }
}
