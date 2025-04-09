using Library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly IMemberRepository _repository;

    public MembersController(IMemberRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembersDto>>> GetAll()
    {
        var members = await _repository.GetAllMembersAsync();
        return Ok(members.Select(m => new MembersDto
        {
            Id = m.Id,
            Name = m.Name,
            Surname = m.Surname,
            CardNumber = m.CardNumber,
            Email = m.Email
        }));
    }
}