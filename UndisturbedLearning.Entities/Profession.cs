using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Profession: EntityBase
{
    [StringLength(30)]
    public string Name { get; set; }
    [StringLength(100)]
    public string Description { get; set; }
}