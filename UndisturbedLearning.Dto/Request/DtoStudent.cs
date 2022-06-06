using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoStudent
{
   public string Code { get; set; }
   [DataType(DataType.Password)]
   public string Password { get; set; }
   public string Dni { get; set; }
   public string Surname { get; set; }
   public string LastName { get; set; }
   [DataType(DataType.Date)]
   public DateTime BirthDate { get; set; }
   [DataType(DataType.EmailAddress)]
   public string Email { get; set; }
   public string Cellphone { get; set; }
   public string Telephone { get; set; }
   public bool Undergraduate { get; set; }
   //public int Career { get; set; }
   //public int Campus { get; set; }
}