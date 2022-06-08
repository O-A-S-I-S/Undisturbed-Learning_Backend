using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto;
using UndisturbedLearning.Dto.Response;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;
[ApiController]
[Route("api/[Controller]")]

public class PsychopedagogistController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;
    
    private static DtoWorkshopResponse WorkshopToResponse(Workshop workshop) => new DtoWorkshopResponse
    {
        Start = workshop.Start,
        End = workshop.End,
        Title = workshop.Title,
        Brief = workshop.Brief,
        Text = workshop.Text,
        Comment = workshop.Comment,
        Reminder = workshop.Reminder,
        PsychopedagogistId = workshop.PsychopedagogistId
       
    };
    public PsychopedagogistController(UndisturbedLearningDbContext context)
    {
        
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<Psychopedagogist>>>> Get()
    {

        var response = new BaseResponseGeneric<ICollection<Psychopedagogist>>();

        try
        {
            response.Result = await _context.Psychopedagogists.ToListAsync();

            response.Success = true;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Errors.Add(ex.Message);
            return response;
        }


    }


    [HttpGet]
    [Route("workshops/{code}")]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<DtoWorkshopResponse>>>> GetWorkshopByPsychopedagogistId(string code)
    {
        var entity = await _context.Workshops.Select(w=>WorkshopToResponse(w)).ToListAsync();
        var lista = new List<DtoWorkshopResponse>();
        var id = _context.Psychopedagogists.Where(p=>p.Code == code).FirstOrDefault().Id;
        for (int i = 0; i < entity.Count; i++)
        {
            var workshop = entity[i];
            if(workshop.PsychopedagogistId == id)lista.Add(workshop);
        }


        if (lista == null)
        {
            return NotFound("No se encontró el registro");
        }
       
        return Ok(lista);


    }

}