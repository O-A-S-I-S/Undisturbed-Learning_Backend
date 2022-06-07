using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UL_Testing.DataAccess;
using UL_Testing.Dto.Request;
using UL_Testing.Dto.Response;
using UL_Testing.Entities;

namespace UL_Testing.API.Controller
{
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
                //throw;
            }

            
        }

        [HttpPost]
        public async Task<ActionResult> Post(DtoAppointment request)
        {
            //var tempPsychopedagogist = new Psychopedagogist {
            var PsychopedagogistId = _context.Psychopedagogists.Where(p => p.LastName == request.Pyschopedagogist).FirstOrDefault<Psychopedagogist>().Id;
            //};

            var StudentId = _context.Students.Where(p => p.LastName == request.Student).FirstOrDefault<Student>().Id; ;

            var entity = new Appointment
            {
                Start = DateTime.ParseExact(request.Start, "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture),
                End = DateTime.ParseExact(request.End, "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture),                
                CauseDescription = request.CauseDescription,
                Comment = "",
                Reminder = request.Reminder,
                StudentId = StudentId,
                PsychopedagogistId = PsychopedagogistId,
            };

            _context.Appointments.Add(entity);
            await _context.SaveChangesAsync();

            HttpContext.Response.Headers.Add("location",$"api/appointment/{entity.Id}");

            return Ok();
        }

    }
}
