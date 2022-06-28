using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoCause
{
    [StringLength(40)]
    [Required]
    public string Name { get; set; }
}