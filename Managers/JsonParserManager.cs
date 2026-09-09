using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesFlow.Objects;
using static NotesFlow.Objects.Note;
using static NotesFlow.Objects.NotesContainer;
using System.Text.Json;

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

        public void UpdateNote(Note note, string oldTitle)
            => _appStorageManager.UpdateNote(oldTitle, note.Id, note.Title, JsonSerializer.Serialize(note));
    }
}
