using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Appointment
{
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
    [StringLength(140)]
    [Required] 
    public string CauseDescription { get; set; }
    [StringLength(200)]
    public string Comment { get; set; }
    [DefaultValue(false)]
    [Required] 
    public bool Reminder { get; set; }
    [Range(0, 10)]
    public int Rating { get; set; }
    
    [Required] 
    public int PsychopedagogistId { get; set; }
    public Psychopedagogist Psychopedagogist { get; set; }
    
    [Required] 
    public int StudentId { get; set; }
    public Psychopedagogist Student { get; set; }
}