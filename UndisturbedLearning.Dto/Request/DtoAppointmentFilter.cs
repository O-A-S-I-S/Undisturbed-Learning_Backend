using System.Diagnostics.CodeAnalysis;

namespace UndisturbedLearning.Dto.Request;

public class DtoAppointmentFilter
{
    public int? StudentId { get; set; }
    public int? PsychopedagogistId { get; set; }
    public string? Activity { get; set; }
    public string? Cause { get; set; }
    public bool? Virtual { get; set; }
    public bool? Report { get; set; }
    public bool? Comment { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}