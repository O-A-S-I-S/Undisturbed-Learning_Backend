using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Workshop: EntityBase
{
    [Required]
    public DateTime Start { get; set; }
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
    [StringLength(200)]
    public string Comment { get; set; }
    [DefaultValue(false)]
    [Required]
    public bool Reminder { get; set; }
    
    public int PsychopedagogistId { get; set; }
    public Psychopedagogist Psychopedagogist { get; set; }

    public ICollection<Student> Students { get; set; }
}