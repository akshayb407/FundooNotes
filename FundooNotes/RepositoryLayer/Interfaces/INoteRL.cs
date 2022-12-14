using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public UserNotes AddNote(NoteModel node, long UserId);
        public UserNotes DeleteNote(long NoteId);
        public UserNotes UpdateNote(NoteModel noteModel, long NoteId);
        public List<UserNotes> GetNotebyUserId(long userId);
        public List<UserNotes> GetNote(long NoteId);
        public List<UserNotes> GetAllNote();
        public UserNotes IsPinORNot(long noteid);
        public UserNotes IstrashORNot(long noteid);
        public UserNotes IsArchiveORNot(long noteid);
        public UserNotes Color(long noteid, string color);
        public UserNotes UploadImage(long noteid, IFormFile img);

    }
}
