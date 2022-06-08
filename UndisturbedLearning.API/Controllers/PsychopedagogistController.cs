namespace UndisturbedLearning.API.Controllers;

public class Psychopedagogist
{
    
}
{
    [ApiController]
    [Route("api/[controller]")]
    public class PsychopedagogistController:ControllerBase
    {
        private readonly UndisturbedLearningDbContext _context;

        public PsychopedagogistController(UndisturbedLearningDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponseGeneric<ICollection<Psychopedagogist>>>> Get()
        {
            // if (_context.Database.CanConnect())
            // {
            //     
            // }
            var response = new BaseResponseGeneric<ICollection<Psychopedagogist>>();

            try
            {
                response.Result = await _context.Psychopedagogists.ToListAsync();
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
        public async Task<ActionResult> Post(DtoPsychopedagogist request)
        {

            var tempProfession = new Profession
            {
                Id = _context.Professions.Where(c => c.Name == request.Profession).FirstOrDefault<Profession>().Id,
                Name = _context.Professions.Where(c => c.Name == request.Profession).FirstOrDefault<Profession>().Name,
                Description = _context.Professions.Where(c => c.Name == request.Profession).FirstOrDefault<Profession>().Description
                //Id = 1,
                //Name = "Mecatronica",
                //Description = "Carrera de Mecatronica"
            };
            var tempCampus = new Campus
            {
                Id = _context.Campuses.Where(c => c.Location == request.Campus).FirstOrDefault<Campus>().Id,
                Location = _context.Campuses.Where(c => c.Location == request.Campus).FirstOrDefault().Location
                //Id = 1,
                //Location = "SanMiguel"
            };

            var entity = new Psychopedagogist
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
                IndividualAssistance = true,
                CampusId = tempCampus.Id,
                //Campus = tempCampus,
                ProfessionId = tempProfession.Id,
                //Profession = tempProfession
            };

            _context.Psychopedagogists.Add(entity);
            await _context.SaveChangesAsync();

            HttpContext.Response.Headers.Add("location", $"/api/psychopedagogist/{entity.Id}");

            return Ok();
        }
    }
}
