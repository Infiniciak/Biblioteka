using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class BooksDto
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Author { get; set; }
        public string? ISBN { get; set; }
        public int ReleaseYear { get; set; }
        public List<BorrowsDto> Borrow { get; set; } = new List<BorrowsDto>();
        public List<BooksCategoriesDto> BooksCategory { get; set; } = new List<BooksCategoriesDto>();
    }

}
