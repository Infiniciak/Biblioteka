// ICategoryRepository.cs
using Library;
using Microsoft.EntityFrameworkCore;
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoriesDto>> GetAllCategoriesAsync();
        Task<CategoriesDto?> GetByIdAsync(int id);
    }

    // CategoryRepository.cs
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDbContext _context;

        public CategoryRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoriesDto>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Include(c => c.BookCategory)  // Poprawiona nazwa właściwości
            .ThenInclude(bc => bc.Book)    // Dodane include dla książek
            .ToListAsync();
    }

    public async Task<CategoriesDto?> GetByIdAsync(int id)
    { 
                  return await _context.Categories
            .Include(c => c.BookCategory)
            .ThenInclude(bc => bc.Book) // Ładujemy powiązane książki
            .FirstOrDefaultAsync(c => c.Id == id);
}
    }
