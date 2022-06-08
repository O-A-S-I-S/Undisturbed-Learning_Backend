using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndisturbedLearning.Dto.Response;

public class AppointmentResponse
{
    
    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
 
    public string CauseDescription { get; set; }
 
    public string Comment { get; set; }


    public int Rating { get; set; }


    public PsychopedagogistResponse Psychopedagogist { get; set; }


    public DtoStudentResponse Student { get; set; }
}
