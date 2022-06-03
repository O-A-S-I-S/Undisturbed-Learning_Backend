namespace UndisturbedLearning.Dto;

public class BaseResponse
{
    public bool Success { get; set; }
    public ICollection<string> Errors { get; set; }

    protected BaseResponse()
    {
        Errors = new List<string>();
    }
}

public class BaseResponseGeneric<TClass> : BaseResponse
{
    public TClass Result { get; set; }
    
}