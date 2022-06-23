using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Campus: EntityBase
{
    [StringLength(10)]
    [Required]
    public string Location { get; set; }
}