using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoAppointment
{
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
    [StringLength(30)]
    [Required]
    public string Activity { get; set; }
    [StringLength(40)]
    [Required] 
    public string Cause { get; set; }
    [StringLength(140)]
    [Required] 
    public string CauseDescription { get; set; }
    [DefaultValue(true)]
    [Required] 
    public bool Virtual { get; set; }
    [DefaultValue(false)]
    [Required] 
    public bool Reminder { get; set; }
    [Required]
    public int PsychopedagogistId { get; set; }
    [Required]
    public int StudentId { get; set; }
}