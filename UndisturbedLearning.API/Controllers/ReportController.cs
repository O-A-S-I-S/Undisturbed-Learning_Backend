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
        ICollection<Report> response;

        response = await _context.Reports.ToListAsync();
        
        return Ok(response);

    }


    [HttpGet("{id:int}")]
    public ActionResult<Report> Get(int id)
    {
        //var entity = await _context.Reports.FindAsync(id);
        var entity_appointment = _context.Appointments.Where(a => a.Id == id).FirstOrDefault<Appointment>();
        if (entity_appointment == null)
        {
            return NotFound("No existe la cita en los registros");
        }

        var entity = _context.Reports.Where(r => r.AppointmentId == id).FirstOrDefault<Report>();
        if (entity == null)
        {
            return NotFound("No se ha generado reporte alguno de la cita");
        }

        return Ok(entity);
    }

    [HttpGet("student/{id:int}")]
    public async Task<ActionResult<ICollection<Report>>> GetByStudentId(int id)
    {
        ICollection<DtoReportResponse> reports = await _context.Reports.Join(_context.Appointments, report => report.AppointmentId,
            appointment => appointment.Id, (report, appointment) => new DtoReportResponse
            {
                Id = report.Id,
                StudentId = appointment.StudentId,
                Activity = appointment.Activity,
                CauseDescription = appointment.CauseDescription,
                Resolution = report.Resolution,
                Brief = report.Brief,
                Text = report.Text,
            }).Where(r => r.StudentId == id).ToListAsync();

        return Ok(reports);
    }
    
    [HttpGet("psychopedagogist/{id:int}")]
    public async Task<ActionResult<ICollection<Report>>> GetByPsychopedagogistId(int id)
    {
        ICollection<DtoReportResponse> reports = await _context.Reports.Join(_context.Appointments, report => report.AppointmentId,
            appointment => appointment.Id, (report, appointment) => new DtoReportResponse
            {
                Id = report.Id,
                StudentId = appointment.StudentId,
                Activity = appointment.Activity,
                CauseDescription = appointment.CauseDescription,
                Resolution = report.Resolution,
                Brief = report.Brief,
                Text = report.Text,
            }).Where(r => r.PsychopedagogistId == id).ToListAsync();

        return Ok(reports);
    }

    [HttpPost("{id:int}")]
    public async Task<ActionResult> Post(int id, DtoReport request)
    {
        var tempAppointment = await _context.Appointments.FindAsync(id);

        if (tempAppointment == null) return BadRequest("The associated appointment doesn't exist.");

        var entity = new Report
        {
            Resolution = request.Resolution,
            Brief = request.Brief,
            Text = request.Text,
            AppointmentId = tempAppointment.Id,
        };

        _context.Reports.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"/api/report/{id}/{entity.Id}");

        return Ok();
    }

}