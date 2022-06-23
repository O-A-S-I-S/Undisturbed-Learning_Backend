using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Dto.Response;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class WorkshopController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;

    public WorkshopController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }

    private static DtoWorkshopResponse WorkshopToResponse(Workshop workshop) => new DtoWorkshopResponse
    {
        Id = workshop.Id,
        Date = workshop.Date,
        StartTime = workshop.StartTime,
        EndTime = workshop.EndTime,
        Title = workshop.Title,
        Brief = workshop.Brief,
        Text = workshop.Text,
        Comment = workshop.Comment,
        Reminder = workshop.Reminder,
        PsychopedagogistId = workshop.PsychopedagogistId
    };

    [HttpGet]
    public async Task<ActionResult<ICollection<DtoWorkshopResponse>>> Get()
    {
        var workshops = await _context.Workshops.Select(w => WorkshopToResponse(w)).ToListAsync();

        return Ok(workshops);
    }

    [HttpPost]
    public async Task<ActionResult> Post(DtoWorkshop request)
    {
        var psychopedagogist = await _context.Psychopedagogists.FindAsync(request.PsychopedagogistId);

        if (psychopedagogist == null) return BadRequest("The psychopedagogist doesn't exist.");

        var entity = new Workshop
        {
            Start = request.Start,
            End = request.End,
            Title = request.Title,
            Brief = request.Brief,
            Text = request.Text,
            PsychopedagogistId = request.PsychopedagogistId,
        };


        _context.Workshops.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"/api/workshop/{entity.Id}");


        return Ok();
    }

    [HttpGet]
    [Route("psychopedagogist/{id}")]
    public async Task<ActionResult<ICollection<DtoWorkshopResponse>>> GetWorkshopByPsychopedagogistId(int id)
    {
        var entity = await _context.Psychopedagogists.FindAsync(id);
        if (entity == null) return BadRequest("The psychopedagogist doesn't exist.");
        
        var workshops = await _context.Workshops.Where(w => w.PsychopedagogistId == id).Select(w => WorkshopToResponse(w)).ToListAsync();
  
        return Ok(workshops);
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await _context.Workshops.FindAsync(id);
        if (entity == null) return NotFound();
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return Ok(id);
    }
}