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
        Id=appointment.Id,
        Day=appointment.Day,
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
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<DtoAppointmentResponse>>>> GetAppointmentByStudentId(int id)
    {
        
            var entity = await _context.Students.FindAsync(id);
            if (entity == null) return NotFound("No se encontró el registro");
            var response = new BaseResponseGeneric<ICollection<DtoAppointmentResponse>>();
            var appos= await _context.Appointments.Select(A => AppointmentToResponse(A)).ToListAsync();

            var lista = new List<DtoAppointmentResponse>();
       
            for (int i = 0; i < appos.Count; i++)
            {
                var appointment = appos[i];
                if (appointment.StudentId == id) lista.Add(appointment);
            }

        

            return Ok(lista);
        
       

    }
    [HttpPost]
    public async Task<ActionResult> Post(DtoAppointment request)
    {
        
        var sId = await _context.Students.FindAsync(request.Student);
        if(sId==null) return NotFound("No se encontró al estudiante");
        var pId = await _context.Psychopedagogists.FindAsync(request.Psychopedagogist);
        if (pId == null)  return NotFound("No se encontró al profesional");
        var entity = new Appointment
        {
            Day=request.Day,
            Start = request.Start,
            End = request.End,
            CauseDescription = request.CauseDescription,
            Comment = "",
            Reminder =true,
            Rating = 0,
            //PsychopedagogistId = _context.Psychopedagogists.Where(c => c.Code == request.Psychopedagogist).FirstOrDefault().Id,
            //StudentId = _context.Students.Where(c => c.Code == request.Student).FirstOrDefault().Id,
            PsychopedagogistId=request.Psychopedagogist,
            StudentId=request.Student
            

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
