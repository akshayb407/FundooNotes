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

        public List<UserNotes> GetNote(long NoteId)
        {
            try
            {
                var Note = ucontext.Notes.Where(x => x.NoteID == NoteId).FirstOrDefault();

                if (Note != null)
                {
                    return ucontext.Notes.Where(list => list.NoteID == NoteId).ToList();
                }

                return null;

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
                var Note = ucontext.Notes.FirstOrDefault();

                if (Note != null)
                {
                    return ucontext.Notes.ToList();
                }

                return null;

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
                UserNotes result = this.ucontext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (result.IsPin == true)
                {
                    result.IsPin = false;
                    this.ucontext.SaveChanges();
                    return result;
                }
                result.IsPin = true;
                this.ucontext.SaveChanges();
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserNotes IstrashORNot(long noteid)
        {
            try
            {
                UserNotes result = this.ucontext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (result.IsTrash == true)
                {
                    result.IsTrash = false;
                    this.ucontext.SaveChanges();
                    return result;
                }
                result.IsTrash = true;
                this.ucontext.SaveChanges();
                return null;
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
                UserNotes result = this.ucontext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (result.IsArchive == true)
                {
                    result.IsArchive = false;
                    this.ucontext.SaveChanges();
                    return result;
                }
                result.IsArchive = true;
                this.ucontext.SaveChanges();
                return null;
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
                UserNotes note = this.ucontext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (note.Color != null)
                {
                    note.Color = color;
                    this.ucontext.SaveChanges();
                    return note;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
