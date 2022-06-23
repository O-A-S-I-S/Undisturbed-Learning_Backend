using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityController: ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;

    public ActivityController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Activity>>> Get()
    {
        ICollection<Activity> response;

        response = await _context.Activities.ToListAsync();


        return Ok(response);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Activity>> Get(int id)
    {
        var entity = await _context.Activities.FindAsync(id);
        if (entity == null)
        {
            return NotFound("Invalid id.");
        }

        return Ok(entity);
    }


    [HttpPost]
    public async Task<ActionResult> Post(DtoActivity request)
    {
        var entity = new Activity
        {
            Name = request.Name
        };

        _context.Activities.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("Name", $"/api/Activity/{entity.Id}");

        return Ok();
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, DtoActivity request)
    {
        var entity = await _context.Activities.FindAsync(id);

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