namespace UndisturbedLearning.Dto.Response;

public class DtoAppointmentResponse
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string Student { get; set; }
    public int PsychopedagogistId { get; set; }
    public string Psychopedagogist { get; set; }
    public string Activity { get; set; }
    public string Cause { get; set; }
    public string CauseDescription { get; set; }
    public bool Virtual { get; set; }
    public string Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Comment { get; set; }
    public bool Reminder { get; set; }
    public int Rating { get; set; }
    public int ReportId { get; set; }
}