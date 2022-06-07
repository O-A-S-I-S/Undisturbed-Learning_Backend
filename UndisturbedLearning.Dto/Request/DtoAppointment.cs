using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UL_Testing.Dto.Request
{
    public class DtoAppointment
    {
        public string Start { get; set; }
        public string End { get; set; }
        public string CauseDescription { get; set; }
        public bool Reminder { get; set; }
        public string Pyschopedagogist { get; set; }
        public string Student { get; set; }
    }
}
