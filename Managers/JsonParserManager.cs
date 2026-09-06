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
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NotesFlow"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NotesFlow");
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

        public bool UpdateNote(Note note)
        {
            if (_appStorageManager.UpdateNote(note.Title, JsonSerializer.Serialize(note)))
                return true;
            return false;
        }

    }
}
