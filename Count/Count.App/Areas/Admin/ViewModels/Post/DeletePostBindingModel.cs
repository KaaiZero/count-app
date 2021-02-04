using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Count.App.Areas.Admin.ViewModels.Post
{
    public class DeletePostBindingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostedOn { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }

        public List<Count.Models.File> Files { get; set; }
        public Count.Models.File CoverPhoto { get; set; }
        public string AuthorId { get; set; }
        public Count.Models.User Author { get; set; }
    }
}
