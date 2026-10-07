using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NotesFlow.Managers;
using Windows.System;

namespace NotesFlow.Objects
{
    public class NotesContainer
    {
        private List<Note> notes = new List<Note>();
        private JsonParserManager _jsonParserManager;

        public NotesContainer(JsonParserManager jpm)
        {
            _jsonParserManager = jpm;

            if (notes.Count() == 0)
            {
                notes = _jsonParserManager.GetNotes();
            }
        }

        public void SaveNote(Guid id, string title, string content)
        {
            Note n = new Note(id, title, content);
            notes.Add(n);
            _jsonParserManager.SaveNote(n);
        }

        public void UpdateNote(Guid id, Note n)
        {
            int idx = notes.IndexOf(n);
            if (idx != -1)
            {
                notes[idx].Title = n.Title;
                notes[idx].Content = n.Content;
                notes[idx].UpdatedAt = DateTime.Now;

                _jsonParserManager.UpdateNote(notes[idx]);
            }
        }

        public void DeleteNote(Guid id)
        {
            Note? n = notes.FirstOrDefault(x => x.Id == id);
            if (n != null)
            {
                File.WriteAllText("C://Users//Mi//Desktop//hi.txt", "norm");
                notes.Remove(n);
                _jsonParserManager.DeleteNote(id + ".json");
            }
        }

        // getters
        public List<Note> GetNotes() => notes;
        public Guid GetId() => Guid.NewGuid();
    }
}
