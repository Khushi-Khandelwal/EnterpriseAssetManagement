using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetManagementMVC.Models
{
    public class BookModel
    {
        public int BookId { get; set; }

       
        public string BookName { get; set; }

       
        public string AuthorName { get; set; }

        public DateTime DateOfPublishing { get; set; }

      
        public string Genre { get; set; }

        public int Quantity { get; set; }
    }
}