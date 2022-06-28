using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Profession: EntityBase
{
    [StringLength(40)]
    public string Name { get; set; }
    [StringLength(200)]
    public string Description { get; set; }
}