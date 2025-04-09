using Library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _repository;

    public BooksController(IBooksRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BooksDto>>> GetAll()
    {
        var books = await _repository.GetAllBooksAsync();
        return Ok(books.Select(b => new BooksDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            ISBN = b.ISBN,
            ReleaseYear = b.ReleaseYear
        }));
    }
}