// IBorrowRepository.cs
using Library;
using Microsoft.EntityFrameworkCore;


public interface IBorrowsRepository
{
    Task<IEnumerable<BorrowsDto>> GetAllBorrowsAsync();
    Task BorrowBookAsync(int bookId, int memberId);
    Task ReturnBookAsync(int borrowId);
}
// BorrowRepository.cs
public class BorrowRepository : IBorrowsRepository
{
    private readonly LibraryDbContext _context;

    public BorrowRepository(LibraryDbContext context) => _context = context;

    public async Task<IEnumerable<BorrowsDto>> GetAllBorrowsAsync()
        => await _context.Borrows
            .Include(b => b.Book)
            .Include(w => w.Member)
            .ToListAsync();

    public async Task BorrowBookAsync(int bookId, int memberId)
    {
        var borrow = new BorrowsDto
        {
            BookId = bookId,
            MemberId = memberId,
            BorrowDate = DateTime.Now
        };

        await _context.Borrows.AddAsync(borrow);
        await _context.SaveChangesAsync();
    }

    public async Task ReturnBookAsync(int borrowId)
    {
        var borrow = await _context.Borrows.FindAsync(borrowId);
        if (borrow != null)
        {
            borrow.ReturnDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}