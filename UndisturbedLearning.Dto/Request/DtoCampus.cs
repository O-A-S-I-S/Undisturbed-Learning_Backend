using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoCampus
{
    [StringLength(10)]
    [Required]
    public string Location { get; set; }
}