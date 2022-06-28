using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Cause : EntityBase
{
    [StringLength(40)]
    [Required]
    public string Name { get; set; }
}