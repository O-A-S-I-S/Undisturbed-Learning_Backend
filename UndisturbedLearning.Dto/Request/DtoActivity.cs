using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoActivity
{
    [StringLength(60)]
    [Required]
    public string Name { get; set; }
}