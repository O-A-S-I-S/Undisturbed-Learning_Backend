using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampusController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;

    public CampusController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Campus>>> Get()
    {
        ICollection<Campus> response;

        response = await _context.Campuses.ToListAsync();


        return Ok(response);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Campus>> Get(int id)
    {
        var entity = await _context.Campuses.FindAsync(id);
        if (entity == null)
        {
            return NotFound("Invalid id.");
        }

        return Ok(entity);
    }


    [HttpPost]
    public async Task<ActionResult> Post(DtoCampus request)
    {
        var entity = new Campus
        {
            Location = request.Location
        };

        _context.Campuses.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"/api/campus/{entity.Id}");

        return Ok();
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, DtoCampus request)
    {
        var entity = await _context.Campuses.FindAsync(id);

        if (entity == null) return NotFound();

        entity.Location = request.Location;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Id = id
        });
    }
}