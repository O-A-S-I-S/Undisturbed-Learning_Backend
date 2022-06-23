using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoCareer
{
    [StringLength(30)]
    [Required]
    public string Name { get; set; }
    [StringLength(80)]
    public string Description { get; set; }
}