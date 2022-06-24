using System.ComponentModel.DataAnnotations;

namespace UndisturbedLearning.Entities;

public class Report: EntityBase
{
   [StringLength(200)]
   [Required]
   public string Resolution { get; set; }
   [StringLength(500)]
   [Required]
   public string Brief { get; set; }
   [StringLength(4000)]
   [Required]
   public string Text { get; set; }
   
   [Required]
   public int AppointmentId { get; set; }
   public Appointment Appointment { get; set; }
}