using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class NotesController : ControllerBase
    {
        private readonly INoteBL noteBL;
        private IConfiguration config;
        private UserContext Context;
        private readonly IDistributedCache distributedcache;
        private readonly IMemoryCache memoryCache;
        public NotesController(INoteBL noteBL, IConfiguration config, UserContext Context, IDistributedCache distributedcache, IMemoryCache memoryCache)
        {
            this.noteBL = noteBL;
            this.config = config;
            this.Context = Context;
            this.distributedcache = distributedcache;
            this.memoryCache = memoryCache;

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
        [Authorize]
        [HttpGet("GetNote")]
        public IEnumerable<UserNotes> GetNote(long NotesId)
        {
            try
            {
                return noteBL.GetNote(NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("AllNotes")]
        public IEnumerable<UserNotes> GetAllNote()
        {
            try
            {
                return noteBL.GetAllNote();
            }
            catch (Exception)
            {
                throw;
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
        [Authorize]
        [HttpPut("Pin")]
        public IActionResult Ispinornot(long noteid)
        {
            try
            {
                var result = noteBL.IsPinORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new { message = "Note unPinned ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Note Pinned Successfully" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("Trash")]
        public IActionResult Istrashornot(long noteid)
        {
            try
            {
                var result = noteBL.IstrashORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new { message = "Note Restored ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Note is in trash" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("Archive")]
        public IActionResult IsArchiveORNot(long noteid)
        {
            try
            {
                var result = noteBL.IsArchiveORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new { message = "Note Unarchived ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Note Archived Successfully" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("Color")]
        public IActionResult Color(long noteid, string color)
        {
            try
            {
                var result = noteBL.Color(noteid, color);
                if (result != null)
                {
                    return this.Ok(new { message = "Color is changed ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Unable to change color" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("Upload")]
        public IActionResult UploadImage(long noteid, IFormFile img)
        {
            try
            {
                var result = noteBL.UploadImage(noteid, img);
                if (result != null)
                {
                    return this.Ok(new { message = "uploaded ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Not uploaded" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("RedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NodeList";
            string serializedNotesList;
            var NotesList = new List<UserNotes>();
            var redisNotesList = await distributedcache.GetAsync(cacheKey);
            //var redisNotesList = distributedcache.Get(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<UserNotes>>(serializedNotesList);
            }
            else
            {

                //NotesList = await Context.Notes.ToListAsync();
                NotesList = (List<UserNotes>)noteBL.GetAllNote();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var option = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedcache.SetAsync(cacheKey, redisNotesList, option);

            }
            return this.Ok(NotesList);
        }

    }
}