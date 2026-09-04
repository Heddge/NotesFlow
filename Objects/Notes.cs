using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesFlow.Objects
{
    public class NotesContainer
    {
        public List<Note> notes = new List<Note>();
        private int notesCount = 0;
        public NotesContainer() { }
        public NotesContainer(int num)
        {
            for (int i = 0; i < num; i++)
            {
                notesCount++;
                notes.Add(new Note($"{i}-ая заметка", $"привет {i} раз", notesCount));
            }
        }
        public void AddNote(string title, string content)
        {
            notesCount++;
            notes.Add(new Note(title, content, notesCount));
        }
    }
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Note(string title, string content, int id)
        {
            Title = title;
            Content = content;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Id = id;
        }
    }
}
