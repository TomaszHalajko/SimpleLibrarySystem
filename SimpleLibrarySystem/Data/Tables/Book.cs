using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLibrarySystem.Data.Tables
{
    public class Book
    {
        [Key]
        public string BookID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        public string Publisher { get; set; }
        [Required]
        [Range(1000,2099, ErrorMessage = "1000-2099")]
        public int ReleaseDate { get; set; }
        [Required]
        public string ISBN { get; set; }
        public virtual ICollection<Borrowings> Borrowings { get; set; }
    }
}
