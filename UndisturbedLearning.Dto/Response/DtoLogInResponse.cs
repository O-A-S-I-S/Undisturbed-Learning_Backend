namespace UndisturbedLearning.Dto.Response;

public class DtoLogInResponse
{
   public int Id { get; set; }
   public string Code { get; set; }
   public string Surname { get; set; }
   public string LastName { get; set; }
   public string Email { get; set; }
   public bool Undergraduate { get; set; }
}