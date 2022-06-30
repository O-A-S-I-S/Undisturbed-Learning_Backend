using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic;
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
    private static DtoAppoPsychoTimeResponse AppointmentToTimeFreeResponse(Appointment appointment) => new DtoAppoPsychoTimeResponse
    {
       Date=appointment.Date,
       StartTime=appointment.StartTime
    };
    private static DtoAppoTimeResponse AppoTimeOnly(DtoAppoPsychoTimeResponse appointment) => new DtoAppoTimeResponse
    {
        
        StartTime = appointment.StartTime
    };
    private bool compareDateString(string a, string b, string sep, int[] format)
    {
        var date1 = a.Split(sep);
        var date2 = b.Split(sep);
        foreach (int index in format)
        {
            if (Int32.Parse(date1[format[index]]) > Int32.Parse(date2[format[index]])) return false;
        }

        return true;
    }
    private bool compareDateStringV2(string a, string b, string sep, int[] format)
    {
        var date1 = a.Split(sep);
        var date2 = b.Split('-');
        foreach (int index in format)
        {
            if (Int32.Parse(date1[format[index]]) != Int32.Parse(date2[format[index]])) return false;
        }

        return true;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Appointment>>> Get()
    {
        ICollection<Appointment> response = await _context.Appointments.ToListAsync();

        return Ok(response);
    }

    [HttpPost("filter")]
    public async Task<ActionResult<ICollection<Appointment>>> GetCustom(DtoAppointmentFilter filter)
    {
        if (filter.StudentId == null && filter.PsychopedagogistId == null) return BadRequest("No student nor psychopedagogist specified in request.");

        ICollection<DtoAppointmentResponse> appointments = new Collection<DtoAppointmentResponse>();
        
        if (filter.StudentId != null)
        { 
            appointments = await _context.Appointments
            .Where(a => a.StudentId == filter.StudentId)
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
                    ReportId = 0,
                })
            .ToListAsync();

            if (filter.PsychopedagogistId != null) appointments = appointments.Where(a => a.PsychopedagogistId == filter.PsychopedagogistId).ToList();
        }
        else if (filter.PsychopedagogistId != null)
        {
            appointments = await _context.Appointments
                .Where(a => a.PsychopedagogistId == filter.PsychopedagogistId)
                .Join(_context.Students,
                    appointment => appointment.StudentId,
                    student => student.Id,
                    (appointment, student) => new
                    {
                        Id = appointment.Id,
                        StudentId = appointment.StudentId,
                        Student = student.Surname + " " + student.LastName,
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
                        ReportId = 0,
                    })
                .ToListAsync();
        }

        if (_context.Reports.ToList().Count() > 0)
        {
            foreach (DtoAppointmentResponse appointment in appointments)
            {
                var report = await _context.Reports.Where(r => r.AppointmentId == appointment.Id).FirstOrDefaultAsync();
        
                if (report != null) appointment.ReportId = report.Id;
            }
        }

        if (filter.Activity != null) appointments = appointments.Where(a => a.Activity == filter.Activity).ToList();
        
        if (filter.Cause != null) appointments = appointments.Where(a => a.Cause == filter.Cause).ToList();
        
        if (filter.Virtual != null) appointments = appointments.Where(a => a.Virtual == filter.Virtual).ToList();

        if (filter.Report != null)
        {
            if (filter.Report ?? false) appointments = appointments.Where(a => a.ReportId > 0).ToList();
            
            else appointments = appointments.Where(a => a.ReportId == 0).ToList();
        }

        if (filter.Comment != null)
        {
            if (filter.Comment ?? false) appointments = appointments.Where(a => a.Comment.Length > 0).ToList();
            
            else appointments = appointments.Where(a => a.Comment.Length == 0).ToList();
        }

        if (filter.StartDate != null) appointments = appointments.Where(a => compareDateString(filter.StartDate, a.Date, "/", new int[] {2, 0, 1})).ToList();

        if (filter.EndDate != null) appointments = appointments.Where(a => compareDateString(a.Date, filter.EndDate, "/", new int[] {2, 0, 1})).ToList();

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
    
    [HttpPut("update/{id:int}")]
    public async Task<ActionResult> Put(int id, DtoAppointmentUpdate request)
    {
        var entity = await _context.Appointments.FindAsync(id);

        if (entity == null) return BadRequest("Non-existent appointment id.");

        if (request.Comment != null) entity.Comment = request.Comment;
        if (request.ReminderStudent != null) entity.ReminderStudent = request.ReminderStudent ?? false;
        if (request.ReminderPsychopedagogist != null)
            entity.ReminderPsychopedagogist = request.ReminderPsychopedagogist ?? false;
        if (request.Rating != null) entity.Rating = request.Rating ?? 0;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Id = entity.Id
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
    [HttpGet]
    [Route("psychopedagogist/{id}/{date}")]
    public async Task<ActionResult<ICollection<DtoAppointmentResponse>>> GetAppointmentsByPsychopedagogistIdAndDate(int id,string date)
    {
        var entity = await _context.Psychopedagogists.FindAsync(id);
        if (entity == null) return BadRequest("The psychopedagogist doesn't exist.");
        var appointments = await _context.Appointments.Where(a => a.PsychopedagogistId == id).Select(a => AppointmentToTimeFreeResponse(a)).ToListAsync();
        if (appointments == null) return BadRequest("The psychopedagogist doesn't have dates.");
        var times = appointments.Where(a => compareDateStringV2(a.Date, date, "/", new int[] { 2, 0, 1 })).Select(a => AppoTimeOnly(a)).ToList();
        //var appointments =  _context.Appointments.Where(a=> compareDateStringV2(a.Date, date, new int[] { 2, 0, 1 })).Select(a => AppointmentToTimeFreeResponse(a)).ToList();

        return Ok(times);
    }
    [HttpGet]
    [Route("psychopedagogist/{id}")]
    public async Task<ActionResult<ICollection<DtoAppointmentResponse>>> GetAppointmentsByPsychopedagogistId(int id)
    {
        var entity = await _context.Psychopedagogists.FindAsync(id);
        if (entity == null) return BadRequest("The psychopedagogist doesn't exist.");
        var appointments = await _context.Appointments.Where(a => a.PsychopedagogistId == id).Select(a=>AppointmentToTimeFreeResponse(a)).ToListAsync();

        return Ok(appointments);
    }
}