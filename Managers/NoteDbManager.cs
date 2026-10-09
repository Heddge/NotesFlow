using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using NotesFlow.Objects;

namespace NotesFlow.Managers
{
    /// <summary>
    /// Менеджер общения NoteContainer (UI) и SQLite.
    /// </summary>
    public class NoteDbManager
    {
        IDbContextFactory<NoteDbContext> _contextFactory;
        public NoteDbManager(IDbContextFactory<NoteDbContext> context)
        {
            _contextFactory = context;
        }

        public bool AddNote(Note n)
        {
            using var factory = _contextFactory.CreateDbContext();
            factory.Notes.Add(n);
            return factory.SaveChanges() == 1;
        }

        public List<Note> GetNotes()
        {
            List<Note> result = new List<Note>();
            using var factory = _contextFactory.CreateDbContext();
            foreach (var note in factory.Notes.ToList())
                result.Add(note);

            return result;
        }

        public bool UpdateNote(Note n)
        {
            using var factory = _contextFactory.CreateDbContext();
            factory.Notes.Update(n);
            return factory.SaveChanges() == 1;
        }

        public bool DeleteNote(Note n)
        {
            using var factory = _contextFactory.CreateDbContext();
            factory.Notes.Remove(n);
            return factory.SaveChanges() == 1;
        }
    }
}
