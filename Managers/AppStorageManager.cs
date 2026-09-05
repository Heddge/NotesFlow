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

        private string _documentsPath = Environment.SpecialFolder.MyDocuments.ToString();

        private JsonParserManager _jsonParserManager;

        public AppStorageManager(JsonParserManager jsm) 
        {
            _jsonParserManager = jsm;
        }

        public bool CheckStorage()
        {
            if (Directory.EnumerateFiles(_documentsPath).Count() == 0)
                return false;

            ReadDirectory();
            return true;
        }

        private void ReadDirectory()
        {
            List<string> files = Directory.GetFiles(_documentsPath).ToList();
            foreach (var fileName in files)
                jsonNotes.Add(File.ReadAllText(fileName));
        }

        public List<string> GetJsonNotes() 
            => jsonNotes;

        public void SaveToDirectory()
        {
            _jsonParserManager.GetJsons();
        }
    }
}
