using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoCareer
{
    [StringLength(50)]
    [Required]
    public string Name { get; set; }
    [StringLength(200)]
    public string Description { get; set; }
}