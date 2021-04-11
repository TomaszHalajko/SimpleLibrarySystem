using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLibrarySystem.Data.Tables
{
    public class Borrowings
    {
        public int BorrowingsID { get; set; }
        [ForeignKey("Customer")]
        public string? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public string? BookID { get; set; }
        public virtual Book Book { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }
    }
}
