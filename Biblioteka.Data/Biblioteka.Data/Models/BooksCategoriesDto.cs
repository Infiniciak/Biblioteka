using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class BooksCategoriesDto
    {
        public int BookId { get; set; }
        public BooksDto? Book { get; set; }
        public int CategoryId { get; set; }
        public CategoriesDto? Category { get; set; }
    }
}
