namespace UndisturbedLearning.Dto.Request;

public class DtoReportFilter
{
    public int? StudentId { get; set; }
    public int? PsychopedagogistId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}