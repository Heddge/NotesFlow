using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using NotesFlow.Objects;

namespace NotesFlow.Managers
{
    public class JsonParserManager
    {
        private AppStorageManager _appStorageManager;

        public JsonParserManager(AppStorageManager asm) 
        {
            _appStorageManager = asm;
        }

        public List<Note> GetNotes()
        {
            List<string> jsonNotes = _appStorageManager.GetJsonNotes();
            return jsonNotes.Select(x =>
            {
                Note n = JsonSerializer.Deserialize<Note>(x);
                return n;
            })
                .ToList();
        }

        public void SaveNote(Note note)
            => _appStorageManager.SaveNote(note.Id, note.Title, JsonSerializer.Serialize(note));

        public void DeleteNote(string title)
            => _appStorageManager.DeleteNote(title);

        public void UpdateNote(Note note)
            => _appStorageManager.UpdateNote(
                note.Id, JsonSerializer.Serialize(note));
    }
}
