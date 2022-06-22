using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Dto.Response;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PsychopedagogistController : ControllerBase
{
    private readonly UndisturbedLearningDbContext _context;

    public PsychopedagogistController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }
    
    private static DtoPsychopedagogistResponse PsychopedagogistToResponse(Psychopedagogist psychopedagogist) => new DtoPsychopedagogistResponse
    {
        Id = psychopedagogist.Id,
        Code = psychopedagogist.Code,
        Dni = psychopedagogist.Dni,
        Surname = psychopedagogist.Surname,
        LastName = psychopedagogist.LastName,
        BirthDate = psychopedagogist.BirthDate,
        Email = psychopedagogist.Email,
        Cellphone = psychopedagogist.Cellphone,
        Telephone = psychopedagogist.Telephone,
        IndividualAssistance = psychopedagogist.IndividualAssistance,
    };

    private static DtoLogInResponse PsychopedagogistToLogInResponse(Psychopedagogist psychopedagogist) => new DtoLogInResponse
    {
        Id = psychopedagogist.Id,
        Code = psychopedagogist.Code,
        Surname = psychopedagogist.Surname,
        LastName = psychopedagogist.LastName,
        Email = psychopedagogist.Email,
    };

    [HttpGet]
    public async Task<ActionResult<ICollection<Psychopedagogist>>> Get()
    {
        ICollection<DtoPsychopedagogistResponse> response = await _context.Psychopedagogists.Select(p => PsychopedagogistToResponse(p)).ToListAsync();

        return Ok(response);
    }
    
    [HttpGet("access/{code}")]
    public async Task<ActionResult<string>> AccessUsername(string code)
    {
        var psychopedagogist = await _context.Psychopedagogists.Where(s => s.Code == code).FirstAsync();

        if (psychopedagogist == null) return NotFound("Invalid student code");

        return Ok(
            new
            {
                Code = code
            });
    }
    
    [HttpPost("access")]
    public async Task<ActionResult<Student>> LogIn(DtoLogIn credentials)
    {
        var psychopedagogist = await _context.Psychopedagogists.Where(p => p.Code == credentials.Username)
            .Where(p => p.Password == credentials.Password).FirstAsync();

        if (psychopedagogist == null) return BadRequest("Incorrect password");

        return Ok(PsychopedagogistToLogInResponse(psychopedagogist));
    }

    [HttpPost]
    public async Task<ActionResult> Post(DtoPsychopedagogist request)
    {
        var tempProfession = new Profession
        {
            Id = _context.Professions.Where(c => c.Id == request.ProfessionId).First().Id,
            Name = _context.Professions.Where(c => c.Id == request.ProfessionId).First().Name,
            Description = _context.Professions.Where(c => c.Id == request.ProfessionId).First().Description
        };
        var tempCampus = new Campus
        {
            Id = _context.Campuses.Where(c => c.Id == request.CampusId).First().Id,
            Location = _context.Campuses.Where(c => c.Id == request.CampusId).First().Location
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