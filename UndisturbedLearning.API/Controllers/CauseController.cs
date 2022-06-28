using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CauseController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;

    public CauseController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Cause>>> Get()
    {
        ICollection<Cause> response;

        response = await _context.Causes.ToListAsync();


        return Ok(response);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Cause>> Get(int id)
    {
        var entity = await _context.Causes.FindAsync(id);
        if (entity == null)
        {
            return NotFound("Invalid id.");
        }

        return Ok(entity);
    }


    [HttpPost]
    public async Task<ActionResult> Post(DtoCause request)
    {
        var entity = new Cause
        {
            Name = request.Name
        };

        _context.Causes.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("Name", $"/api/Cause/{entity.Id}");

        return Ok();
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, DtoCause request)
    {
        var entity = await _context.Causes.FindAsync(id);

        if (entity == null) return NotFound();

        entity.Name = request.Name;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Id = id
        });
    }
}