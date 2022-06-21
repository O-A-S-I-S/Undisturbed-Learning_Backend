using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Appointment: EntityBase
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
    
    public int PsychopedagogistId { get; set; }
    public Psychopedagogist Psychopedagogist { get; set; }
    
    public int StudentId { get; set; }
    public Student Student { get; set; }
}