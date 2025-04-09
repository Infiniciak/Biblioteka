using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class CategoriesDto
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public List<BooksCategoriesDto> BookCategory { get; set; } = new List<BooksCategoriesDto>();
    }
}