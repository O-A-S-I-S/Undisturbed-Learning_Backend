using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UndisturbedLearning.Dto.Request;

public class DtoStudent
{
   [Required(ErrorMessage = "{0} is required")]
   [StringLength(9, MinimumLength = 9)]
   public string Code { get; set; }
   [StringLength(25, MinimumLength = 8)]
   [DataType(DataType.Password)]
   public string Password { get; set; }
   [Required(ErrorMessage = "{0} is required")]
   [StringLength(8, MinimumLength = 8)]
   public string Dni { get; set; }
   [Required(ErrorMessage = "{0} is required")]
   [StringLength(30)]
   public string Surname { get; set; }
   [Required(ErrorMessage = "{0} name is required")]
   [StringLength(30)]
   public string LastName { get; set; }
   [DataType(DataType.Date)]
   public DateTime BirthDate { get; set; }
   [Required(ErrorMessage = "{0} is required")]
   [DataType(DataType.EmailAddress)]
   public string Email { get; set; }
   [AllowNull]
   [StringLength(9)]
   public string Cellphone { get; set; }
   [AllowNull]
   [StringLength(7)]
   public string Telephone { get; set; }
   [Required(ErrorMessage = "Type of student is required")]
   public bool Undergraduate { get; set; }

   [Required(ErrorMessage = "{0} is required")]
   public string Career { get; set; }
   
   [Required(ErrorMessage = "{0} is required")]
   public string Campus { get; set; }
}