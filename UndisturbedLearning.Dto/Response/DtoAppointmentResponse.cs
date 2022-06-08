using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndisturbedLearning.Dto.Response;

public class DtoAppointmentResponse
{
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
 
    public string CauseDescription { get; set; }
 
    public string Comment { get; set; }


    public int Rating { get; set; }


    public int PsychopedagogistId { get; set; }


    public int StudentId { get; set; }
}
