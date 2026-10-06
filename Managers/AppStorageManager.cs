using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NotesFlow.Managers
{
    public class AppStorageManager
    {
        private List<string> jsonNotes = new List<string>();

        public string _documentsPath = "";

        public AppStorageManager()
        {
            _documentsPath = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments) + "\\NotesFlow";

            if (!Directory.Exists(_documentsPath))
                Directory.CreateDirectory(_documentsPath);

            ReadDirectory();
        }

        private void ReadDirectory()
        {
            List<string> files = Directory.GetFiles(_documentsPath).ToList();
            int i = 0;
            foreach (var fileName in files)
            {
                jsonNotes.Add(File.ReadAllText(fileName));
            }

        }

        public void SaveNote(Guid id, string title, string jsonContent)
            => File.WriteAllText(_documentsPath + "\\" + id + ".json",
                jsonContent);

        public void DeleteNote(string title)
            => File.Delete(_documentsPath + "\\" + title);

        public void UpdateNote(Guid id, string content)
        {
            File.WriteAllText(_documentsPath + id + ".json", content);

            //File.WriteAllText(Path.Combine(_documentsPath, id + "_" + id + ".json"), content);
            //File.Move(Path.Combine(_documentsPath, id + "_" + id + ".json"), Path.Combine(_documentsPath, id + "_" + title + ".json"));
        }

        // getters
        public List<string> GetJsonNotes()
            => jsonNotes;
    }
}
