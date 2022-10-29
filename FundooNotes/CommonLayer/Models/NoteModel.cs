using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public   class NoteModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public bool IsArchive { get; set; }
        [Required]
        public bool IsPin { get; set; }
        [Required]
        public bool IsTrash { get; set; }
        public DateTime? Createat { get; set; }
        public DateTime? Modifiedat { get; set; }
    }
}
