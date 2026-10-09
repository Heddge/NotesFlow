using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private List<Note> notes;
        //private NoteDbManager _noteDbManager;
        //private NotesApiManager _notesApiManager;
        private SynchronizeManager _synchronizeManager;

        public NotesContainer(SynchronizeManager sm)
        {
            //_noteDbManager = ndm;
            //_notesApiManager = nam;
            _synchronizeManager = sm;
            notes = new List<Note>();
            //notes = _noteDbManager.GetNotes();
            //GetNotesAsync();
        }

        public async Task GetNotesAsync()
        {
            List<Note>? ns = await _notesApiManager.GetNotes();
            if (ns != null)
                foreach (var note in ns)
                    this.notes.Add(note);
        }

        public async Task SaveNote(Guid id, string title, string content)
        {
            Note n = new Note(id, title, content);
            //if (_noteDbManager.AddNote(n))
            //notes.Add(n);
            if (_notesApiManager.SaveNote(n) != null)
                notes.Add(n);
        }

        public void UpdateNote(Guid id, Note n)
        {
            int idx = notes.FindIndex(x => x.Id == id);
            if (idx == -1)
                return;
            n.UpdatedAt = DateTime.UtcNow;
            if (_notesApiManager.UpdateNote(n) != null)
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
                if (_notesApiManager.DeleteNote(id) != null)
                    notes.Remove(n);
                //if (_noteDbManager.DeleteNote(n))
                    //notes.Remove(n);
            }
        }

        // getters
        public List<Note> GetNotes() => notes;
        public Guid GetId() => Guid.NewGuid();
    }
}
