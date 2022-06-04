using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Entities;
using UndisturbedLearning.Dto;
using UndisturbedLearning.Dto.Request;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController: ControllerBase
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
    public async Task<ActionResult> Post(DtoStudent request)
    {
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
            Undergraduate = request.Undergraduate,
            CareerId = _context.Careers.Where(c => c.Name == request.Career).FirstOrDefault().Id,
            CampusId = _context.Campuses.Where(c => c.Location == request.Campus).FirstOrDefault().Id
        };

        _context.Students.Add(entity);
        await _context.SaveChangesAsync();
        
        HttpContext.Response.Headers.Add("location", $"/api/student/{entity.Id}");

        return Ok();
    }
}