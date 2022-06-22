using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Dto.Response;

public class DtoReportResponse
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string Student { get; set; }
    public int PsychopedagogistId { get; set; }
    public int AppointmentId { get; set; }
    public string Activity { get; set; }
    public string CauseDescription { get; set; }
    public string Resolution { get; set; }
    public string Brief { get; set; }
    public string Text { get; set; }
}