using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UL_Testing.DataAccess;
using UL_Testing.Dto.Request;
using UL_Testing.Dto.Response;
using UL_Testing.Entities;

namespace UL_Testing.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly UndisturbedLearningDbContext _context;

        public StudentController(UndisturbedLearningDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponseGeneric<ICollection<Student>>>> Get()
        {
            // if (_context.Database.CanConnect())
            // {
            //     
            // }
            var response = new BaseResponseGeneric<ICollection<Student>>();

            try
            {
                response.Result = await _context.Students.ToListAsync();
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
        public async Task <ActionResult> Post(DtoStudent request)
        {

            var tempCarreer = new Career
            {
                Id = _context.Careers.Where(c => c.Name == request.Career).FirstOrDefault<Career>().Id,
                //Name = _context.Careers.Where(c => c.Name == request.Career).FirstOrDefault<Career>().Name,
                //Description = _context.Careers.Where(c => c.Name == request.Career).FirstOrDefault<Career>().Description
                //Id = 1,
                Name = "Mecatronica",
                Description = "Carrera de Mecatronica"
            };
            var tempCampus = new Campus
            {
                Id = _context.Campuses.Where(c => c.Location == request.Campus).FirstOrDefault<Campus>().Id,
                //Location = _context.Campuses.Where(c => c.Location == request.Campus).FirstOrDefault().Location
                //Id = 1,
                Location = "SanMiguel"
            };

            var entity = new Student
            {
                Code = request.Code,
                Password = request.Password,
                Dni = request.Dni,
                Surname = request.Surname,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email,
                Cellphone = request.Cellphone,
                Telephone = request.Telephone,
                Undergraduate = request.Undergraduate,
                CareerId = tempCarreer.Id,               
                //Career = tempCarreer,
                CampusId = tempCampus.Id,
                //Campus = tempCampus
            };

            _context.Students.Add(entity);
            await _context.SaveChangesAsync();

            HttpContext.Response.Headers.Add("location", $"/api/student/{entity.Id}");

            return Ok();
        }
    }
}
