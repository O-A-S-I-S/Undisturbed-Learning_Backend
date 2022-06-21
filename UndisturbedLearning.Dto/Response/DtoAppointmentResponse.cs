namespace UndisturbedLearning.Dto.Response;

public class DtoAppointmentResponse
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string Student { get; set; }
    public int PsychopedagogistId { get; set; }
    public string Psychopedagogist { get; set; }
    public string Activity { get; set; }
    public string CauseDescription { get; set; }
    public bool Virtual { get; set; }
    public DateTime Day { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Comment { get; set; }
    public bool Reminder { get; set; }
    public int Rating { get; set; }
}