using Count.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Models.Post
{
    public class EditPostBindingModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public Count.Models.User Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
        public IFormFile CoverPhoto { get; set; }
    }
}
