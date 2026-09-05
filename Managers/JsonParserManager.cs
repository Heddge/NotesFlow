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
        private NotesContainer _notesContainer;

        public JsonParserManager(NotesContainer nc, AppStorageManager asm) 
        {
            _appStorageManager = asm;
            _notesContainer = nc;
        }

        public List<Note> GetNotes()
        {
            List<string> jsonNotes = _appStorageManager.GetJsonNotes();
            return jsonNotes.Select(x =>
            {
                Note n = JsonSerializer.Deserialize<Note>(x);
                return n;
            }).ToList();
        }

        public List<string> GetJsons() 
            => _notesContainer.GetNotes().Select(x => JsonSerializer.Serialize(x)).ToList();
    }
}
