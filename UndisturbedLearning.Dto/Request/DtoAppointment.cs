using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndisturbedLearning.Dto.Request;

public class DtoAppointment
{
    [DataType(DataType.Date)]
    public DateTime Day { get; set; }
    [DataType(DataType.Time)]
    public DateTime Start { get; set; }
    [DataType(DataType.Time)]
    public DateTime End { get; set; }


   
    public string CauseDescription { get; set; }
    //public string Comment { get; set; }
    //public bool Reminder { get; set; }
    //public int Rating{ get; set; }
    public int Psychopedagogist { get; set; }
    public int Student { get; set; }
}
