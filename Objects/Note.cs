using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesFlow.Objects
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Note() { }
        public Note(string title, string content, int id, DateTime crAt, DateTime updAt)
        {
            Id = id;
            Title = title;
            Content = content;
            CreatedAt = crAt;
            UpdatedAt = updAt;
        }
    }
}
