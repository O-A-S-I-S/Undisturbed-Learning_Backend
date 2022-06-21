using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndisturbedLearning.Dto.Response;

public class DtoWorkshopResponse
{
    public int Id { get; set; }
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string Title { get; set; }

    public string Brief { get; set; }

    public string Text { get; set; }

    public string Comment { get; set; }

    public bool Reminder { get; set; }

    public int PsychopedagogistId { get; set; }

}
