﻿using CommonLayer.Models;
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
        public bool Pinned(long NoteID, long userId);
        public bool Trashed(long NoteID, long userId);
        public bool Archieved(long NoteID, long userId);
        public UserNotes ColorNote(long NoteId, string color);
        bool Trashed(long userId);
        bool Archieved(long userId);
    }
}