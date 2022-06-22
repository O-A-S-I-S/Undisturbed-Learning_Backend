using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Activity : EntityBase
{
    [StringLength(30)]
    [Required]
    public string Name { get; set; }
}