using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto;
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

}