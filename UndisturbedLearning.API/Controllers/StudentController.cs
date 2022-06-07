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

    [HttpGet]
    public async Task<ActionResult<BaseResponseGeneric<ICollection<Student>>>> Get()
    {

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
            Telephone = request.Telephone,
            Undergraduate = request.Undergraduate,
            //CareerId = _context.Careers.Where(c => c.Name == request.Career).FirstOrDefault().Id,
            //CampusId = _context.Campuses.Where(c => c.Location == request.Campus).FirstOrDefault().Id
            CareerId = 1,
            CampusId = 1
        };


        _context.Students.Add(entity);
        await _context.SaveChangesAsync();

        HttpContext.Response.Headers.Add("location", $"/api/student/{entity.Code}");



        return Ok();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<BaseResponseGeneric<Student>>>GetByStudentCode(string code)
    {
        var entity = await _context.Students.FindAsync(code);
        var response = new BaseResponseGeneric<StudentResponse>();
        response.Result = new StudentResponse
        {
            Code = entity.Code,
            Surname = entity.Surname
            

        };

        if (entity == null)
        {
            return NotFound("No se encontró el registro");
        }

        return Ok(response);

       

    }


    //[HttpGet]
    //[Route("{code:string)")]
    //public async Task<IEnumerable<DtoStudent>> List([FromQuery]string code)
    //{
    //    //var students =  await _context.Students.Where(s => s.Code.Contains(code)).ToListAsync();
    //    //var response =  students.Select(
    //    //    s => new DtoStudent
    //    //    {
    //    //        Code = s.Code,
    //    //        Surname = s.Surname,
    //    //        LastName = s.LastName
    //    //    }).ToList();

    //    return await response;
    //    //var response=new BaseResponseGeneric<DtoStudent>();
    //    //var student=  _context.Students.Where(p => p.Code == code);
    //    //if(student==null)
    //    //{
    //    //    response.Success = false;
    //    //    return response;
    //    //}
    //    //response.Result = new DtoStudent
    //    //{

    //    //    Code = student.code,
    //    //    Name = customer.Name,
    //    //    BirthDate = customer.BirthDate,
    //    //    NumberId = customer.NumberId
    //    //};


    //    //return  response;
    //}
}