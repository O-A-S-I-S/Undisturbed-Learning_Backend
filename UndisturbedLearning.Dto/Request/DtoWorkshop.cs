using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndisturbedLearning.Dto.Request;

public class DtoWorkshop
{
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime EndTime { get; set; }
   
    public string Title { get; set; }
    public string Brief { get; set; }
    public string Text { get; set; }
    public string Comment { get; set; }
    public bool Reminder { get; set; }
    public string Psychopedagogist { get; set; }
}
