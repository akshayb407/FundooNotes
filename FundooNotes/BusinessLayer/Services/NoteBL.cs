using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public UserNotes AddNote(NoteModel notes, long userid)
        {
            try
            {
               return this.noteRL.AddNote(notes, userid);
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
                return noteRL.DeleteNote(NoteId);
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
                return noteRL.UpdateNote(noteModel, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<UserNotes> GetNotebyUserId(long userId)
        {
            try
            {                
               return noteRL.GetNotebyUserId(userId);
               
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
                return noteRL.Archieved(userId);
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
                return noteRL.Trashed(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Pinned(long NoteID, long userId)
        {
            throw new NotImplementedException();
        }

        public UserNotes ColorNote(long NoteId, string color)
        {
            throw new NotImplementedException();
        }
    }
}
