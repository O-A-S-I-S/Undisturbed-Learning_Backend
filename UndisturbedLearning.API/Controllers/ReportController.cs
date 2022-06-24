using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Dto.Response;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController:ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;

    public ReportController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Report>>> Get()
    {
        ICollection<Report> reports;

        reports = await _context.Reports.ToListAsync();
        
        return Ok(reports);

    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Report>> GetById(int id)
    {
        var report = await _context.Reports.Where(r => r.Id == id).FirstAsync();

        if (report == null) return BadRequest("There's no report with such id.");

        return Ok(report);

    }


    [HttpGet("appointment/{id:int}")]
    public ActionResult<Report> Get(int id)
    {
        //var entity = await _context.Reports.FindAsync(id);
        var appointment = _context.Appointments.Where(a => a.Id == id).FirstOrDefault<Appointment>();
        if (appointment == null) return BadRequest("The appointment doesn't exist.");

        var report = _context.Reports.Where(r => r.AppointmentId == id).FirstOrDefault<Report>();
        if (report == null) return NotFound("A report hasn't been filed yet.");

        return Ok(report);
    }

    [HttpGet("student/{id:int}")]
    public async Task<ActionResult<ICollection<Report>>> GetByStudentId(int id)
    {
        ICollection<DtoReportResponse> reports = await _context.Reports.Join(_context.Appointments,
            report => report.AppointmentId,
            appointment => appointment.Id, (report, appointment) => new
            {
                Id = report.Id,
                StudentId = appointment.StudentId,
                PsychopedagogistId = appointment.PsychopedagogistId,
                ActivityId = appointment.ActivityId,
                CauseId = appointment.CauseId,
                CauseDescription = appointment.CauseDescription,
                Resolution = report.Resolution,
                Brief = report.Brief,
                Text = report.Text,
                Start = appointment.Start,
                Date = appointment.Date,
            })
            .Where(r => r.StudentId == id)
            .OrderByDescending(r => r.Start)
            .Join(_context.Activities, report => report.ActivityId, activity => activity.Id,
            (report, activity) => new 
            {
                Id = report.Id,
                StudentId = report.StudentId,
                PsychopedagogistId = report.PsychopedagogistId,
                Activity = activity.Name,
                CauseId = report.CauseId,
                CauseDescription = report.CauseDescription,
                Resolution = report.Resolution,
                Brief = report.Brief,
                Text = report.Text,
                Date = report.Date,
                
            })
            .Join(_context.Psychopedagogists, report => report.PsychopedagogistId, psychopedagogist => psychopedagogist.Id,
            (report, psychopedagogist) => new 
            {
                Id = report.Id,
                StudentId = report.StudentId,
                PsychopedagogistId = report.PsychopedagogistId,
                Psychopedagogist = psychopedagogist.Surname + " " + psychopedagogist.LastName,
                Activity = report.Activity,
                CauseId = report.CauseId,
                CauseDescription = report.CauseDescription,
                Resolution = report.Resolution,
                Brief = report.Brief,
                Text = report.Text,
                Date = report.Date,
            })
            .Join(_context.Causes, report => report.CauseId, cause => cause.Id,
                (report, cause) => new DtoReportResponse
                {
                    Id = report.Id,
                    StudentId = report.StudentId,
                    PsychopedagogistId = report.PsychopedagogistId,
                    Psychopedagogist = report.Psychopedagogist,
                    Activity = report.Activity,
                    Cause = cause.Name,
                    CauseDescription = report.CauseDescription,
                    Resolution = report.Resolution,
                    Brief = report.Brief,
                    Text = report.Text,
                    Date = report.Date,
                })
            .ToListAsync();

        return Ok(reports);
    }
    
    [HttpGet("psychopedagogist/{id:int}")]
    public async Task<ActionResult<ICollection<Report>>> GetByPsychopedagogistId(int id)
    {
        ICollection<DtoReportResponse> reports = await _context.Reports.Join(_context.Appointments, report => report.AppointmentId,
            appointment => appointment.Id, (report, appointment) => new
            {
                Id = report.Id,
                StudentId = appointment.StudentId,
                PsychopedagogistId = appointment.PsychopedagogistId,
                ActivityId = appointment.ActivityId,
                CauseId = appointment.CauseId,
                CauseDescription = appointment.CauseDescription,
                Resolution = report.Resolution,
                Brief = report.Brief,
                Text = report.Text,
                Start = appointment.Start,
                Date = appointment.Date,
            })
            .Where(r => r.PsychopedagogistId == id)
            .OrderByDescending(r => r.Start)
            .Join(_context.Activities, report => report.ActivityId, activity => activity.Id,
            (report, activity) => new 
            {
                Id = report.Id,
                StudentId = report.StudentId,
                PsychopedagogistId = report.PsychopedagogistId,
                Activity = activity.Name,
                CauseId = report.CauseId,
                CauseDescription = report.CauseDescription,
                Resolution = report.Resolution,
                Brief = report.Brief,
                Text = report.Text,
                Date = report.Date,
                
            })
            .Join(_context.Students, report => report.StudentId, student => student.Id,
            (report, student) => new
            {
                Id = report.Id,
                StudentId = report.StudentId,
                Student = student.Surname + " " + student.LastName,
                PsychopedagogistId = report.PsychopedagogistId,
                Activity = report.Activity,
                CauseId = report.CauseId,
                CauseDescription = report.CauseDescription,
                Resolution = report.Resolution,
                Brief = report.Brief,
                Text = report.Text,
                Date = report.Date,
            })
            .Join(_context.Causes, report => report.CauseId, cause => cause.Id,
                (report, cause) => new DtoReportResponse
                {
                    Id = report.Id,
                    StudentId = report.StudentId,
                    Student = report.Student,
                    PsychopedagogistId = report.PsychopedagogistId,
                    Activity = report.Activity,
                    Cause = cause.Name,
                    CauseDescription = report.CauseDescription,
                    Resolution = report.Resolution,
                    Brief = report.Brief,
                    Text = report.Text,
                    Date = report.Date,
                })
            .ToListAsync();

        return Ok(reports);
    }

    [HttpPost("{id:int}")]
    public async Task<ActionResult> Post(int id, DtoReport request)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null) return BadRequest("The associated appointment doesn't exist.");

        var reports = await _context.Reports.Where(r => r.AppointmentId == appointment.Id).ToListAsync();

        if (reports.Count<Report>() > 0) return BadRequest("A report has already been filed for the appointment.");

        var entity = new Report
        {
            Resolution = request.Resolution,
            Brief = request.Brief,
            Text = request.Text,
            AppointmentId = id,
        };

        _context.Reports.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"/api/report/{id}/{entity.Id}");

        return Ok();
    }

}