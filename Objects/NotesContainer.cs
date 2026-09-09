using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NotesFlow.Managers;
using static NotesFlow.Managers.AppStorageManager;
using static NotesFlow.Managers.JsonParserManager;

namespace NotesFlow.Objects
{
    public class NotesContainer
    {
        private List<Note> notes = new List<Note>();
        private JsonParserManager _jsonParserManager;
        public int NextId { get; set; }

        public NotesContainer(JsonParserManager jpm)
        {
            _jsonParserManager = jpm;

            if (notes.Count() == 0)
            {
                notes = _jsonParserManager.GetNotes();
                NextId = 100 + notes.Count();
            }
        }

        public void SaveNote(string title, string content)
        {
            Note n = new Note(NextId++, title, content);
            notes.Add(n);
            _jsonParserManager.SaveNote(n);
        }

        public void UpdateNote(int id, Note n, string oldTitle)
        {
            int idx = notes.IndexOf(n);
            if (idx != -1)
            {
                notes[idx].Title = n.Title;
                notes[idx].Content = n.Content;
                notes[idx].UpdatedAt = DateTime.Now;

                _jsonParserManager.UpdateNote(notes[idx], oldTitle);
            }
        }

        public void DeleteNote(int id)
        {
            Note? title = notes.FirstOrDefault(x => x.Id == id);
            if (title != null)
            {
                notes.Remove(title);
                _jsonParserManager.DeleteNote(id + "_" + title.Title + ".json");
            }
        }

        // getters
        public List<Note> GetNotes() => notes;
        public int GetIdForNew() => NextId++;
    }
}
