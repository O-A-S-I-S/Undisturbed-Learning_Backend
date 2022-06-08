using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.DataAccess;
using UndisturbedLearning.Entities;

using UndisturbedLearning.Dto.Request;
using UndisturbedLearning.Dto;
using System.Text.Json;
using UndisturbedLearning.Dto.Response;

namespace UndisturbedLearning.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class StudentController : ControllerBase
{


    private readonly UndisturbedLearningDbContext _context;

    public StudentController(UndisturbedLearningDbContext context)
    {
        _context = context;
    }
    private static DtoStudentResponse StudentToResponse(Student student) => new DtoStudentResponse
    {
        Id = student.Id,
        Code = student.Code,
        Dni = student.Dni,
        Surname = student.Surname,
        LastName = student.LastName,
        BirthDate = student.BirthDate,
        Email = student.Email,
        Cellphone = student.Cellphone,
        Telephone = student.Telephone,
        Undergraduate = student.Undergraduate,
    };

    [HttpGet]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<DtoStudentResponse>>>> Get()
    {

        var response = new BaseResponseGeneric<ICollection<DtoStudentResponse>>();

        try
        {
            response.Result = await _context.Students.Select(s=>StudentToResponse(s)).ToListAsync();

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
            Telephone = request.Telephone,
            Undergraduate = request.Undergraduate,
            CareerId = _context.Careers.Where(c => c.Name == request.Career).FirstOrDefault().Id,
            CampusId = _context.Campuses.Where(c => c.Location == request.Campus).FirstOrDefault().Id,

            
        };


        _context.Students.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"/api/student/{entity.Id}");



        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BaseResponseGeneric<Student>>>GetStudentById(int id)
    {
        var entity = await _context.Students.FindAsync(id);
        var response = new BaseResponseGeneric<DtoStudentResponse>();
        response.Result = new DtoStudentResponse
        {

            Id = entity.Id,
            Code = entity.Code,
            Dni = entity.Dni,
            Surname = entity.Surname,
            LastName = entity.LastName,
            BirthDate = entity.BirthDate,
            Email = entity.Email,
            Cellphone = entity.Cellphone,
            Telephone = entity.Telephone,
            Undergraduate = entity.Undergraduate,


        };

        if (entity == null)
        {
            return NotFound("No se encontró el registro");
        }

        return Ok(response);


    }
    
    [HttpGet]
    [Route("email/{id}")]
    public async Task<ActionResult<BaseResponseGeneric<Student>>> GetEmailFromStudent(int id)
    {
        var entity = await _context.Students.FindAsync(id);


        if (entity == null)
        {
            return NotFound("No se encontró el registro");
        }
        var email = entity.Email;
        return Ok(email);


    }


}