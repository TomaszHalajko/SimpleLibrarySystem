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
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
