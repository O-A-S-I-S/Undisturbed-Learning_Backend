using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Dto.Response;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;

[ApiController] //Para tener acceso a todos los verbos para CRUD
[Route("api/[Controller]")]
public class AppointmentController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;


    public AppointmentController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Appointment>>> Get()
    {
        ICollection<Appointment> response = await _context.Appointments.ToListAsync();

        return Ok(response);
    }

    [HttpGet("student/{id:int}")]
    public async Task<ActionResult<ICollection<Appointment>>> GetByStudentId(int id)
    {
        ICollection<DtoAppointmentResponse> appointments = await _context.Appointments.Join(_context.Psychopedagogists, 
            appointment => appointment.PsychopedagogistId, 
            psychopedagogist => psychopedagogist.Id,
            (appointment, psychopedagogist) => new DtoAppointmentResponse
            {
                Id = appointment.Id,
                StudentId = appointment.StudentId,
                PsychopedagogistId = appointment.PsychopedagogistId,
                Psychopedagogist = psychopedagogist.Surname + " " + psychopedagogist.LastName,
                Activity = appointment.Activity,
                CauseDescription = appointment.CauseDescription,
                Virtual = appointment.Virtual,
                Date = appointment.Date,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Comment = appointment.Comment,
                Reminder = appointment.Reminder,
                Rating = appointment.Rating,
            }).Where(a => a.PsychopedagogistId == id).ToListAsync();
        
        return Ok(appointments);
    }
    
    [HttpGet("psychopedagogist/{id:int}")]
    public async Task<ActionResult<ICollection<Appointment>>> GetByPsychopedagogistId(int id)
    {
        ICollection<DtoAppointmentResponse> appointments = await _context.Appointments.Join(_context.Students, 
            appointment => appointment.PsychopedagogistId, 
            student => student.Id,
            (appointment, student) => new DtoAppointmentResponse
            {
                Id = appointment.Id,
                StudentId = appointment.StudentId,
                Student = student.Surname + " " +  student.LastName,
                PsychopedagogistId = appointment.PsychopedagogistId,
                Activity = appointment.Activity,
                CauseDescription = appointment.CauseDescription,
                Virtual = appointment.Virtual,
                Date = appointment.Date,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Comment = appointment.Comment,
                Reminder = appointment.Reminder,
                Rating = appointment.Rating,
            }).Where(a => a.PsychopedagogistId == id).ToListAsync();
        
        return Ok(appointments);
    }

    [HttpPost]
    public async Task<ActionResult> Post(DtoAppointment request)
    {
        var student = await _context.Students.Where(p => p.Id == request.StudentId).FirstAsync();

        if (student == null) return BadRequest("The student doesn't exist.");
        
        //var tempPsychopedagogist = new Psychopedagogist {
        var psychopedagogist = await _context.Psychopedagogists.Where(p => p.Id == request.PsychopedagogistId)
            .FirstAsync();
        //};

        if (psychopedagogist == null) return BadRequest("The psychopedagogist doesn't exist.");

        var appointment = new Appointment
        {
            Start = request.Start,
            End = request.End,
            Activity = request.Activity,
            CauseDescription = request.CauseDescription,
            Comment = "",
            Reminder = request.Reminder,
            PsychopedagogistId = psychopedagogist.Id,
            StudentId = student.Id,
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"api/appointment/{appointment.Id}");

        return Ok();
    }
}