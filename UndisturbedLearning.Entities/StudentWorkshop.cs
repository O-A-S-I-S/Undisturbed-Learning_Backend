namespace UndisturbedLearning.Entities;

public class StudentWorkshop:EntityBase
{
    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int WorkshopId { get; set; }
    public Workshop Workshop { get; set; }
}