using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UL_Testing.DataAccess;
using UL_Testing.Dto.Request;
using UL_Testing.Entities;

namespace UL_Testing.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController:ControllerBase
    {
        private readonly UL_TestingDBContext _context;

        public ReportController(UL_TestingDBContext context)
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


        [HttpPost("{id:int}")]
        public async Task<ActionResult> Post(int id, DtoReport request)
        {
            var tempAppointment = await _context.Appointments.FindAsync(id);

            if (tempAppointment == null) return NotFound();

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
}
