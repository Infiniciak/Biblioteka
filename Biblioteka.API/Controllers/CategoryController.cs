using Library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repository;

    public CategoryController(ICategoryRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<CategoriesDto>>> GetAll()
    {
        var categories = await _repository.GetAllCategoriesAsync();
        return Ok(categories.Select(c => new CategoriesDto
        {
            Id = c.Id,
            Name= c.Name,
        }));
    }
}