using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessionController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;

    public ProfessionController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Profession>>> Get()
    {
        ICollection<Profession> response;

        response = await _context.Professions.ToListAsync();


        return Ok(response);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Profession>> Get(int id)
    {
        var entity = await _context.Professions.FindAsync(id);
        if (entity == null)
        {
            return NotFound("No se encontró el registro");
        }

        return Ok(entity);
    }


    [HttpPost]
    public async Task<ActionResult> Post(DtoProfession request)
    {
        var entity = new Profession
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.Professions.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"/api/profession/{entity.Id}");

        return Ok();
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, DtoProfession request)
    {
        var entity = await _context.Professions.FindAsync(id);

        if (entity == null) return NotFound();

        entity.Description = request.Description;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Id = id
        });
    }
}