namespace UndisturbedLearning.Dto.Response;

public class DtoWorkshopResponse
{
    public int Id { get; set; }
    public string Date { get; set; }
    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public string Title { get; set; }

    public string Brief { get; set; }

    public string Text { get; set; }

    public string Comment { get; set; }

    public bool Reminder { get; set; }

    public int PsychopedagogistId { get; set; }

}