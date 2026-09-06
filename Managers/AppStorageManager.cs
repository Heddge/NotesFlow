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

        private string _documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public AppStorageManager()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NotesFlow"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NotesFlow");
            CheckStorage();
        }

        public bool CheckStorage()
        {
            if (Directory.EnumerateFiles(_documentsPath + "\\NotesFlow").Count() == 0)
                return false;

            ReadDirectory();
            return true;
        }

        private void ReadDirectory()
        {
            List<string> files = Directory.GetFiles(_documentsPath + "\\NotesFlow").ToList();
            foreach (var fileName in files)
                jsonNotes.Add(File.ReadAllText(fileName));
        }


        public bool UpdateNote(string title, string note)
        {
            File.WriteAllText(_documentsPath + "\\NotesFlow" + "\\" + title, note);
            if (File.Exists(_documentsPath + "\\NotesFlow" + "\\" + title))
                return true;

            return false;
        }

        //public bool DeleteNote()

        // getters
        public List<string> GetJsonNotes()
            => jsonNotes;
    }
}
