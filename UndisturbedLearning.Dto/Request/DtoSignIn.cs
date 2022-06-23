using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoSignIn
{
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(25, MinimumLength = 8)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [StringLength(25, MinimumLength = 8)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}