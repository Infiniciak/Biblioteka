using Library;
using Microsoft.EntityFrameworkCore;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService()
    {
        _connectionString = "Server=localhost;Database=biblioteka;User=root;Password=;";
    }

    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
            optionsBuilder.UseMySql(_connectionString,
                new MySqlServerVersion(new Version(8, 0, 23)));

            await using var context = new LibraryDbContext(optionsBuilder.Options);
            return await context.Database.CanConnectAsync();
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<CategoriesDto>> GetCategoriesAsync()
    {
        var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
        optionsBuilder.UseMySql(_connectionString,
            new MySqlServerVersion(new Version(8, 0, 23)));

        await using var context = new LibraryDbContext(optionsBuilder.Options);
        return await context.Categories.ToListAsync();
    }
}