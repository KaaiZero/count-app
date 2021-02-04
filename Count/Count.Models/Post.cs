
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Count.Models
{
    public class Post
    {
        public Post()
        {
            List<File> Files = new List<File>(); 
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostedOn { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }

        public File CoverPhoto { get; set; }
        public List<File> Files { get; set; }

        [ForeignKey("User")]
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public bool IsDelete { get; set; }
    }
}
