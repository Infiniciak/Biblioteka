using Microsoft.EntityFrameworkCore;
using Library;
using System.Collections.Generic;
using System.Threading.Tasks;


    public interface IBooksRepository
    {
        Task<IEnumerable<BooksDto>> GetAllBooksAsync();
        Task<BooksDto?> GetByIdAsync(int id);
        Task AddAsync(BooksDto book);
        Task UpdateAsync(BooksDto book);
        Task DeleteAsync(int id);
    Task AddCategoryToBookAsync(int bookId, int categoryId);
    Task RemoveCategoryFromBookAsync(int bookId, int categoryId);
}


    public class BooksRepository : IBooksRepository
        {
            private readonly LibraryDbContext _context;

            public BooksRepository(LibraryDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<BooksDto>> GetAllBooksAsync()
            {
                return await _context.Books
                    .Include(b => b.BooksCategory)
                    .ThenInclude(bc => bc.Category)
                    .Include(b => b.Borrow)
                    .ToListAsync();
            }

            public async Task<BooksDto?> GetByIdAsync(int id)
            {
                return await _context.Books
                    .Include(b => b.BooksCategory)
                    .ThenInclude(bc => bc.Category)
                    .Include(b => b.Borrow)
                    .FirstOrDefaultAsync(b => b.Id == id);
            }

            public async Task<BooksDto?> GetByTitleAsync(string title)
            {
                return await _context.Books
                    .FirstOrDefaultAsync(b => b.Title.ToLower() == title.ToLower());
            }

            public async Task AddAsync(BooksDto book)
            {
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(BooksDto book)
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var book = await GetByIdAsync(id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<bool> ExistsAsync(int id)
            {
                return await _context.Books.AnyAsync(b => b.Id == id);
            }

            public async Task AddCategoryToBookAsync(int bookId, int categoryId)
            {
                var bookCategory = new BooksCategoriesDto
                {
                    BookId = bookId,
                    CategoryId = categoryId
                };
                await _context.BooksCategories.AddAsync(bookCategory);
                await _context.SaveChangesAsync();
            }
        
            public async Task RemoveCategoryFromBookAsync(int bookId, int categoryId)
            {
                var bookCategory = await _context.BooksCategories
                    .FirstOrDefaultAsync(bc => bc.BookId == bookId && bc.CategoryId == categoryId);

                if (bookCategory != null)
                {
                    _context.BooksCategories.Remove(bookCategory);
                    await _context.SaveChangesAsync();
                }
            }
        }
    
