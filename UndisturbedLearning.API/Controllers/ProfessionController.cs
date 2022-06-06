using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;
[ApiController]
[Route("api/[Controller]")]

public class ProfessionController : Controller
{
    // GET: ProfessionController
    private readonly UndisturbedLearningDbContext _context;

    public ProfessionController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<Profession>>>> Get()
    {

        var response = new BaseResponseGeneric<ICollection<Profession>>();

        try
        {
            response.Result = await _context.Professions.ToListAsync();
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
