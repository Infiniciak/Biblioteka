using Library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/borrows")]
public class BorrowsController : ControllerBase
{
    private readonly IBorrowsRepository _repository;

    public BorrowsController(IBorrowsRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<BorrowsDto>>> GetAll()
    {
        var borrows = await _repository.GetAllBorrowsAsync();
        return Ok(borrows.Select(b => new BorrowsDto
        {
            Id = b.Id,
            BookId = b.BookId,
            MemberId = b.MemberId,
            BorrowDate = b.BorrowDate,
            ReturnDate = b.ReturnDate
        }));
    }
}