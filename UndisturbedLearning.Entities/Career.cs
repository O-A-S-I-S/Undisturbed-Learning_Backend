using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Career: EntityBase
{
    [StringLength(30)]
    public string Name { get; set; }
    [StringLength(80)]
    public string Description { get; set; }
}