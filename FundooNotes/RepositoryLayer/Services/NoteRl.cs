using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
   public class NoteRl : INoteRL
    {
        public readonly UserContext ucontext;
        private readonly IConfiguration Config;

        public NoteRl(UserContext context, IConfiguration Config)
        {
            this.ucontext = context;
            this.Config = Config;
        }
        public UserNotes AddNote(NoteModel notes, long userid)
        {
            try
            {
                UserNotes noteEntity = new UserNotes();
                noteEntity.Title = notes.Title;
                noteEntity.Note = notes.Note;
                noteEntity.Color = notes.Color;
                noteEntity.Image = notes.Image;
                noteEntity.IsArchive = notes.IsArchive;
                noteEntity.IsPin = notes.IsPin;
                noteEntity.IsTrash = notes.IsTrash;
                noteEntity.userid = userid;
               
                this.ucontext.Notes.Add(noteEntity);
                int result = this.ucontext.SaveChanges();

                if (result > 0)
                {
                    return noteEntity;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserNotes DeleteNote(long NoteId)
        {
            try
            {
                var deleteNote = ucontext.Notes.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (deleteNote != null)
                {
                   ucontext.Notes.Remove(deleteNote);
                   ucontext.SaveChanges();
                    return deleteNote;
                }

                return null;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserNotes UpdateNote(NoteModel noteModel, long NoteId)
        {
            try
            {
                var update = ucontext.Notes.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (update != null)
                {
                    update.Title = noteModel.Title;
                    update.Note = noteModel.Note;
                    update.IsArchive = noteModel.IsArchive;
                    update.Color = noteModel.Color;
                    update.Image = noteModel.Image;
                    update.IsPin = noteModel.IsPin;
                    update.IsTrash = noteModel.IsTrash;
                    ucontext.Notes.Update(update);
                    ucontext.SaveChanges();
                    return update;

                }


                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Pinned(long NoteID, long userId)
        {
            try
            {
                var result = ucontext.Notes.Where(r => r.userid == userId && r.NoteID == NoteID).FirstOrDefault();

                result.IsPin = !result.IsPin;
                ucontext.SaveChanges();
                return result.IsPin;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trashed(long NoteID, long userId)
        {
            try
            {
                var result = ucontext.Notes.Where(r => r.userid == userId && r.NoteID == NoteID).FirstOrDefault();

                result.IsTrash = !result.IsTrash;
                ucontext.SaveChanges();
                return result.IsTrash;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Archieved(long NoteID, long userId)
        {
            try
            {
                var result = ucontext.Notes.Where(r => r.userid == userId && r.NoteID == NoteID).FirstOrDefault();
                result.IsArchive = !result.IsArchive;
                ucontext.SaveChanges();
                return result.IsArchive;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserNotes ColorNote(long NoteId, string color)
        {
            var result = ucontext.Notes.Where(r => r.NoteID == NoteId).FirstOrDefault();
            if (result != null)
            {

                result.Color = color;
                ucontext.Notes.Update(result);
                ucontext.SaveChanges();
                return result;

            }
            else
            {
                return null;
            }
        }

        public List<UserNotes> GetNotebyUserId(long userId)
        {
            try
            {
                var Note = ucontext.Notes.Where(x => x.userid == userId).FirstOrDefault();
                if (Note != null)
                {
                    return ucontext.Notes.Where(list => list.userid == userId).ToList();
                }

                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Trashed(long userId)
        {
            throw new NotImplementedException();
        }

        public bool Archieved(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
