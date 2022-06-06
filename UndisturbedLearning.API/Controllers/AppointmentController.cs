using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;
[ApiController]
[Route("api/[Controller]")]

public class AppointmentController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;

    public AppointmentController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<Appointment>>>> Get()
    {

        var response = new BaseResponseGeneric<ICollection<Appointment>>();

        try
        {
            response.Result = await _context.Appointments.ToListAsync();
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
