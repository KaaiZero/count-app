
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Count.Models
{
    public class File
    {
        public int Id { get; set; }
        public byte[] FileUpload { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }

        public bool IsCoverPhoto { get; set; }
    }
}
