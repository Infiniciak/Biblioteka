using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class BorrowsDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public BooksDto? Book { get; set; }
        public int MemberId { get; set; }
        public MembersDto? Member { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
