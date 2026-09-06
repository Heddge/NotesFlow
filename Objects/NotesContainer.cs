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
        public int _nextId;

        private JsonParserManager _jsonParserManager;

        public NotesContainer(JsonParserManager jpm)
        {
            _jsonParserManager = jpm;

            if (_nextId == 0)
            {
                notes = _jsonParserManager.GetNotes();
                _nextId = notes.Count() + 1;
            }
        }

        //public void UpdateNote(Note n)
        //{
        //    if (_jsonParserManager.UpdateNote(n))
        //        return;
        //    Console.WriteLine($"Error. Note w id:{n.Id} hasn`t updated.");
        //}

        public void SaveNote(string title, string content)
        {
            Note n = new Note(_nextId++, title, content);
            notes.Add(n);
            _jsonParserManager.SaveNote(n);
        }

        // getters
        public List<Note> GetNotes() => notes;
        public int GetIdForNew() => _nextId;
    }
}
