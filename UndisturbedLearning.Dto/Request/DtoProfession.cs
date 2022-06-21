using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoProfession
{
    [StringLength(30)]
    public string Name { get; set; }
    [StringLength(100)]
    public string Description { get; set; }
}