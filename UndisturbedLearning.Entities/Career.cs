using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Career: EntityBase
{
    [StringLength(50)]
    [Required]
    public string Name { get; set; }
    [StringLength(200)]
    public string Description { get; set; }
}