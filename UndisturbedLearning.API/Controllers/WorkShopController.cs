using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto;
using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Dto.Response;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class WorkShopController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;

    public WorkShopController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }
    private static DtoWorkshopResponse WorkshopToResponse(Workshop workshop) => new DtoWorkshopResponse
    {
        Id = workshop.Id,
        Start = workshop.Start,
        End = workshop.End,
        Title = workshop.Title,
        Brief = workshop.Brief,
        Text = workshop.Text,
        Comment = workshop.Comment,
        Reminder = workshop.Reminder,
        PsychopedagogistId = workshop.PsychopedagogistId
       
    };
    [HttpGet]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<DtoWorkshopResponse>>>> Get()
    {

        var response = new BaseResponseGeneric<ICollection<DtoWorkshopResponse>>();

        try
        {
            response.Result = await _context.Workshops.Select(w=>WorkshopToResponse(w)).ToListAsync();

            response.Success = true;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Errors.Add(ex.Message);
            return response;
        }



    }

    [HttpPost]
    public async Task<ActionResult> Post(DtoWorkshop request)
    {
        var entity = new Workshop
        {
            Start=request.StartTime,
            End=request.EndTime,
            Title=request.Title,
            Brief=request.Brief,
            Text=request.Text,
            Comment=request.Comment,
            Reminder=true,
            PsychopedagogistId=_context.Psychopedagogists.Where(p=>p.Code==request.Psychopedagogist).FirstOrDefault().Id,
     


        };


        _context.Workshops.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"/api/workshop/{entity.Id}");



        return Ok();
    }
    [HttpGet]
    [Route("psychopedagogist/{id}")]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<DtoWorkshopResponse>>>> GetWorkshopByPsychopedagogistId(int id)
    {
        var entity = await _context.Psychopedagogists.FindAsync(id);
        if (entity == null) return NotFound("No se encontró el registro");
        var response = new BaseResponseGeneric<ICollection<DtoWorkshopResponse>>();
        var works = await _context.Workshops.Select(w => WorkshopToResponse(w)).ToListAsync();
        //var entity = await _context.Workshops.Select(w => WorkshopToResponse(w)).ToListAsync();
        var lista = new List<DtoWorkshopResponse>();
        //var id = _context.Psychopedagogists.Where(p => p.Code == code).FirstOrDefault().Id;
        for (int i = 0; i < works.Count; i++)
        {
            var workshop = works[i];
            if (workshop.PsychopedagogistId == id) lista.Add(workshop);
        }


        //if (lista == null)
        //{
        //    return NotFound("No se encontró el registro");
        //}

        return Ok(lista);


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
