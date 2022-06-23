using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoLogIn
{
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(25, MinimumLength = 8)]
    public string Password { get; set; }
}