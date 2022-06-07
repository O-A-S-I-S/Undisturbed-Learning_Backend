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
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BaseResponseGeneric<Psychopedagogist>>> GetByPsychopedagogistCode(int id)
    {
        var entity = await _context.Psychopedagogists.FindAsync(id);
        var response = new BaseResponseGeneric<PsychopedagogistResponse>();
        response.Result = new PsychopedagogistResponse
        {
            Code = entity.Code,
            Surname = entity.Surname


        };

        if (entity == null)
        {
            return NotFound("No se encontró el registro");
        }

        return Ok(response);

        //var query = (from S in _context.Students where S.Code.Contains("201716094") select new { Codigo = S.Code, Surname = S.Surname });

        //return Ok(query);


    }

}