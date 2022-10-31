using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
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
        public List<UserNotes> GetNote(long NotesId)
        {
            try
            {
                return noteRL.GetNote(NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<UserNotes> GetAllNote()
        {
            try
            {
                return noteRL.GetAllNote();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserNotes IsPinORNot(long noteid)
        {
            try
            {
                return this.noteRL.IsPinORNot(noteid);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public UserNotes IstrashORNot(long noteid)
        {
            try
            {
                return this.noteRL.IstrashORNot(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserNotes IsArchiveORNot(long noteid)
        {
            try
            {
                return this.noteRL.IsArchiveORNot(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserNotes Color(long noteid, string color)
        {
            try
            {
                return this.noteRL.Color(noteid, color);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
