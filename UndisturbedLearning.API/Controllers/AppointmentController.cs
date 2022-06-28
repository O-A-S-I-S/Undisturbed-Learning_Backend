using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        ICollection<DtoAppointmentResponse> appointments = await _context.Appointments
            .Join(_context.Psychopedagogists, 
            appointment => appointment.PsychopedagogistId, 
            psychopedagogist => psychopedagogist.Id,
            (appointment, psychopedagogist) => new
            {
                Id = appointment.Id,
                StudentId = appointment.StudentId,
                PsychopedagogistId = appointment.PsychopedagogistId,
                Psychopedagogist = psychopedagogist.Surname + " " + psychopedagogist.LastName,
                ActivityId = appointment.ActivityId,
                CauseId = appointment.CauseId,
                CauseDescription = appointment.CauseDescription,
                Virtual = appointment.Virtual,
                Start = appointment.Start,
                Date = appointment.Date,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Comment = appointment.Comment,
                Reminder = appointment.ReminderStudent,
                Rating = appointment.Rating,
            })
            .Where(a => a.StudentId == id)
            .OrderByDescending(a => a.Start)
            .Join(_context.Activities, appointment => appointment.ActivityId, activity => activity.Id,
            (appointment, activity) => new 
            {
                Id = appointment.Id,
                StudentId = appointment.StudentId,
                PsychopedagogistId = appointment.PsychopedagogistId,
                Psychopedagogist = appointment.Psychopedagogist,
                Activity = activity.Name,
                CauseId = appointment.CauseId,
                CauseDescription = appointment.CauseDescription,
                Virtual = appointment.Virtual,
                Date = appointment.Date,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Comment = appointment.Comment,
                Reminder = appointment.Reminder,
                Rating = appointment.Rating,
            }
            )
            .Join(_context.Causes, appointment => appointment.CauseId, cause => cause.Id,
                (appointment, cause) => new DtoAppointmentResponse
                {
                    Id = appointment.Id,
                    StudentId = appointment.StudentId,
                    PsychopedagogistId = appointment.PsychopedagogistId,
                    Psychopedagogist = appointment.Psychopedagogist,
                    Activity = appointment.Activity,
                    Cause = cause.Name,
                    CauseDescription = appointment.CauseDescription,
                    Virtual = appointment.Virtual,
                    Date = appointment.Date,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    Comment = appointment.Comment,
                    Reminder = appointment.Reminder,
                    Rating = appointment.Rating,
                })
            .ToListAsync();
        
        foreach (DtoAppointmentResponse appointment in appointments)
        {
            var report = await _context.Reports.Where(r => r.AppointmentId == appointment.Id).FirstAsync();
        
            if (report == null) appointment.ReportId = 0;
            else appointment.ReportId = report.Id;
        }
        
        return Ok(appointments);
    }
    
    [HttpGet("psychopedagogist/{id:int}")]
    public async Task<ActionResult<ICollection<Appointment>>> GetByPsychopedagogistId(int id)
    {
        ICollection<DtoAppointmentResponse> appointments = await _context.Appointments
            .Join(_context.Students, 
            appointment => appointment.PsychopedagogistId, 
            student => student.Id,
            (appointment, student) => new 
            {
                Id = appointment.Id,
                StudentId = appointment.StudentId,
                Student = student.Surname + " " +  student.LastName,
                PsychopedagogistId = appointment.PsychopedagogistId,
                ActivityId = appointment.ActivityId,
                CauseId = appointment.CauseId,
                CauseDescription = appointment.CauseDescription,
                Virtual = appointment.Virtual,
                Start = appointment.Start,
                Date = appointment.Date,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Comment = appointment.Comment,
                Reminder = appointment.ReminderPsychopedagogist,
                Rating = appointment.Rating,
            })
            .Where(a => a.PsychopedagogistId == id)
            .OrderByDescending(a => a.Start)
            .Join(_context.Activities, appointment => appointment.ActivityId, 
            activity => activity.Id,
            (appointment, activity) => new
            {
                Id = appointment.Id,
                StudentId = appointment.StudentId,
                Student = appointment.Student,
                PsychopedagogistId = appointment.PsychopedagogistId,
                Activity = activity.Name,
                CauseId = appointment.CauseId,
                CauseDescription = appointment.CauseDescription,
                Virtual = appointment.Virtual,
                Date = appointment.Date,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Comment = appointment.Comment,
                Reminder = appointment.Reminder,
                Rating = appointment.Rating,
            })
            .Join(_context.Causes, appointment => appointment.CauseId, cause => cause.Id,
                (appointment, cause) => new DtoAppointmentResponse
                {
                    Id = appointment.Id,
                    StudentId = appointment.StudentId,
                    Student = appointment.Student,
                    PsychopedagogistId = appointment.PsychopedagogistId,
                    Activity = appointment.Activity,
                    Cause = cause.Name,
                    CauseDescription = appointment.CauseDescription,
                    Virtual = appointment.Virtual,
                    Date = appointment.Date,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    Comment = appointment.Comment,
                    Reminder = appointment.Reminder,
                    Rating = appointment.Rating,
                })
            .ToListAsync();

        foreach (DtoAppointmentResponse appointment in appointments)
        {
            var report = await _context.Reports.Where(r => r.AppointmentId == appointment.Id).FirstAsync();
        
            if (report == null) appointment.ReportId = 0;
            else appointment.ReportId = report.Id;
        }
        
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

        var activity = await _context.Activities.Where(a => a.Name == request.Activity).FirstAsync();

        if (activity == null) return BadRequest("The activity name provided is not valid.");

        var cause = await _context.Causes.Where(c => c.Name == request.Cause).FirstAsync();

        if (cause == null) return BadRequest("The cause name provided is not valid.");

        var appointment = new Appointment
        {
            Start = request.Start,
            End = request.End,
            CauseId = cause.Id,
            CauseDescription = request.CauseDescription,
            Comment = "",
            Virtual = request.Virtual,
            ReminderStudent = request.ReminderStudent,
            ReminderPsychopedagogist = false,
            ActivityId = activity.Id,
            PsychopedagogistId = psychopedagogist.Id,
            StudentId = student.Id,
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"api/appointment/{appointment.Id}");

        return Ok();
    }
    
    [HttpPut("comment/{id:int}")]
    public async Task<ActionResult> Put(int id, string comment)
    {
        var entity = await _context.Appointments.FindAsync(id);

        if (entity == null) return NotFound();

        entity.Comment = comment;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Id = entity.Id,
            Comment = entity.Comment,
        });
    }
    
    [HttpPut("rating/{id:int}")]
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