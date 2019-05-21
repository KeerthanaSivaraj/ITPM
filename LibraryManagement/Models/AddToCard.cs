using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class AddToCard
    {
        [Key]
        public int Id { get; set; }
        public string Book_Name { get; set; }


        public string des { get; set; }

       
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int BookId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datetime { get; set; }


    }
}
