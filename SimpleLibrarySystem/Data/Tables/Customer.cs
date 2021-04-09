using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLibrarySystem.Data.Tables
{
    public class Customer
    {
        [Key]
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
