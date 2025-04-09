using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class MembersDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;
        [Required]
        public string CardNumber { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public List<BorrowsDto> Borrow { get; set; } = new List<BorrowsDto>();
    }

}
