using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoWorkshop
{
    [DataType(DataType.Date)]
    [Required] 
    public DateTime Day { get; set; }
    [DataType(DataType.Time)]
    [Required]
    public DateTime Start { get; set; }
    [DataType(DataType.Time)]
    [Required]
    public DateTime End { get; set; }
    [StringLength(50)]
    [Required] 
    public string Title { get; set; }
    [StringLength(200)]
    [Required]
    public string Brief { get; set; }
    [StringLength(500)]
    [Required]
    public string Text { get; set; }
    [DefaultValue(false)]
    [Required]
    public bool Reminder { get; set; }
    [Required]
    public int PsychopedagogistId { get; set; }
}