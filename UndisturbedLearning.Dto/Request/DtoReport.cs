using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoReport
{
    [StringLength(200)]
    [Required]
    public string Resolution { get; set; }
    [StringLength(500)]
    [Required]
    public string Brief { get; set; }
    [StringLength(4000)]
    [Required]
    public string Text { get; set; }
}