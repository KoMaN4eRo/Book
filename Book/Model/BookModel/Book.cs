using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTests.Model.BookModel
{
    public class Book
    {
        [Key]
        public int BookiId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTimeOffset EditionDate { get; set; }
    }
}
