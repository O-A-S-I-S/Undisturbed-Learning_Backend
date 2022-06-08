using Microsoft.AspNetCore.Http;
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

public class AppointmentController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;
    private static DtoAppointmentResponse AppointmentToResponse(Appointment appointment) => new DtoAppointmentResponse
    {
        Start=appointment.Start,
        End =appointment.End,
        CauseDescription=appointment.CauseDescription,
        Comment =appointment.Comment,
        Rating=appointment.Rating,
        PsychopedagogistId=appointment.PsychopedagogistId,
        StudentId  =appointment.StudentId,

};
    public AppointmentController(UndisturbedLearningDbContext context)
    {
        _context = context;
        
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<DtoAppointmentResponse>>>> Get()
    {

        var response = new BaseResponseGeneric<ICollection<DtoAppointmentResponse>>();
         
        try
        {
            response.Result = await _context.Appointments.Select(a=>AppointmentToResponse(a)).ToListAsync();
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
    public async Task<ActionResult> Post(DtoAppointment request)
    {
        
       
        var entity = new Appointment
        {
            Start = request.StartTime,
            End = request.EndTime,
            CauseDescription = request.CauseDescription,
            //Comment = request.Comment,
            Reminder = request.Reminder,
            //Rating = request.Rating,
            PsychopedagogistId = _context.Psychopedagogists.Where(c => c.Code == request.Psychopedagogist).FirstOrDefault().Id,
            StudentId = _context.Students.Where(c => c.Code == request.Student).FirstOrDefault().Id,
            

        };


        _context.Appointments.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"/api/appointment/{entity.Id}");



        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, int rating)
    {
        var entity = await _context.Appointments.FindAsync(id);

        if (entity == null) return NotFound();

        entity.Rating = rating;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Id = id
        });
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        //_context.Entry(new Appointment
        //{
        //    Id = id
        //}).State = EntityState.Deleted;
        //await  _context.SaveChangesAsync();
        //return null;
        var entity = await _context.Appointments.FindAsync(id);
        if (entity == null) return NotFound();
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return Ok(id);
    }
}
