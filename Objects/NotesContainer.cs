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
        private int _notesCount = 0;

        private JsonParserManager _jsonParserManager;

        public NotesContainer(JsonParserManager jpm)
        {
            _jsonParserManager = jpm;

            if (_notesCount == 0)
            {
                notes = _jsonParserManager.GetNotes();
                _notesCount = notes.Count();
            }
        }

        public void UpdateNote(Note n)
        {
            if (_jsonParserManager.UpdateNote(n))
                return;
            Console.WriteLine($"Error. Note w id:{n.Id} hasn`t updated.");
        }

        // getters
        public List<Note> GetNotes() => notes;
    }
}
