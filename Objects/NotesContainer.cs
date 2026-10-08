using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NotesFlow.Managers;

namespace NotesFlow.Objects
{
    public class NotesContainer
    {
        private List<Note> notes = new List<Note>();
        private NoteDbManager _noteDbManager;

        public NotesContainer(NoteDbManager ndm)
        {
            _noteDbManager = ndm;
            notes = _noteDbManager.GetNotes();
        }

        public void SaveNote(Guid id, string title, string content)
        {
            Note n = new Note(id, title, content);
            if (_noteDbManager.AddNote(n))
                notes.Add(n);
        }

        public void UpdateNote(Guid id, Note n)
        {
            int idx = notes.FindIndex(x => x.Id == id);
            if (idx == -1)
                return;
            n.UpdatedAt = DateTime.Now;
            if (_noteDbManager.UpdateNote(n))
            {
                notes[idx].Title = n.Title;
                notes[idx].Content = n.Content;
            }
        }

        public void DeleteNote(Guid id)
        {
            Note? n = notes.FirstOrDefault(x => x.Id == id);
            if (n != null)
            {
                if (_noteDbManager.DeleteNote(n))
                    notes.Remove(n);
            }
        }

        // getters
        public List<Note> GetNotes() => notes;
        public Guid GetId() => Guid.NewGuid();
    }
}
