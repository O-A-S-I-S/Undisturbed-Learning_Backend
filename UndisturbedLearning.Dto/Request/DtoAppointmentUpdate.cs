using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Request;

public class DtoAppointmentUpdate
{
    [StringLength(1000)]
    public string? Comment { get; set; }
    public bool? ReminderStudent { get; set; }
    public bool? ReminderPsychopedagogist { get; set; }
    [Range(0, 10)]
    public int? Rating { get; set; }
}