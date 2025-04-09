using Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
    public DbSet<BooksDto> Books { get; set; }
    public DbSet<CategoriesDto> Categories { get; set; }
    public DbSet<MembersDto> Members { get; set; }
    public DbSet<BorrowsDto> Borrows { get; set; }
    public DbSet<BooksCategoriesDto> BooksCategories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=localhost;Database=biblioteka;User=root;Password=",
            new MySqlServerVersion(new Version(8, 0, 23)));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfiguracja relacji wiele-do-wielu
        modelBuilder.Entity<BooksCategoriesDto>()
            .HasKey(bc => new { bc.BookId, bc.CategoryId });

        // Seedowanie danych
        modelBuilder.Entity<CategoriesDto>().HasData(
            new CategoriesDto { Id = 1, Name = "Fantasy" },
            new CategoriesDto { Id = 2, Name = "Science Fiction" },
            new CategoriesDto { Id = 3, Name = "Novel" },
            new CategoriesDto { Id = 4, Name = "Criminal" }
        );

        modelBuilder.Entity<BooksDto>().HasData(
            new BooksDto
            {
                Id = 1,
                Title = "Diuna",
                Author = "Frank Herbert",
                ReleaseYear = 1965
            },
            new BooksDto
            {
                Id =2,
                Title = "451 Stopni Fahrenheita",
                Author = "Ray Bradbury",
                ReleaseYear = 1953
            },
            new BooksDto
            {
                Id = 3,
                Title = "Nowy wspaniały świat",
                Author = "Aldous Huxley",
                ReleaseYear = 1932
            },
            new BooksDto
            {
                Id = 4,
                Title = "Anioły i demony",
                Author = "Dan Brown",
                ReleaseYear = 2000
            }
        );

        modelBuilder.Entity<BooksCategoriesDto>().HasData(
            new BooksCategoriesDto { BookId = 1, CategoryId = 1 },
            new BooksCategoriesDto { BookId = 2, CategoryId = 2 },
            new BooksCategoriesDto { BookId = 3, CategoryId = 3 },
            new BooksCategoriesDto { BookId = 4, CategoryId = 4 }
        );

        modelBuilder.Entity<MembersDto>().HasData(
            new MembersDto
            {
                Id = 1,
                Name = "Jan",
                Surname = "Kowalski",
                CardNumber = "BIBL001",
                Email = "jan.kowalski@gmail.com"
            },
            new MembersDto
            {
                Id = 2,
                Name = "Anna Nowak",
                Surname = "Kowalski",
                CardNumber = "BIBL002",
                Email = "anna.nowak@gmail.com"
            }
        );
    }
}