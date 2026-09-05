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
        private int notesCount = 0;

        private AppStorageManager _appStorageManager;
        private JsonParserManager _jsonParserManager;

        public NotesContainer(AppStorageManager asm, JsonParserManager jpm) 
        {
            _appStorageManager = asm;
            _jsonParserManager = jpm;

            if (notesCount == 0)
            {
                if (!_appStorageManager.CheckStorage())
                    return;

                notes = _jsonParserManager.GetNotes();
            }
        }
        //public EventCallback SaveNote(string title, string content)
        //{
        //    notesCount++;
        //    notes.Add(new Note(title, content, notesCount));

        //    return EventCallback.Empty;
        //}

        //public List<string> GetTitles() =>
        //    notes.Select(x => x.Title).ToList();

        public List<Note> GetNotes() => notes;
    }
}
