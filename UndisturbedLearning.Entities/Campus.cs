using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Campus: EntityBase
{
    [StringLength(20)]
    [Required]
    public string Location { get; set; }
}