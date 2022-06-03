using Microsoft.EntityFrameworkCore;
using UndisturbedLearning.Entities;

namespace UndisturbedLearning.DataAccess;

public class UndisturbedLearningDbContext : DbContext
{
    public UndisturbedLearningDbContext()
    {
        
    }
    public UndisturbedLearningDbContext(DbContextOptions<UndisturbedLearningDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentWorkshop>().HasKey(sc => new { sc.StudentId, sc.WorkshopId });
    }

    public DbSet<Student> Students { get; set; }
    // public DbSet<Psychopedagogist> Psychopedagogists { get; set; }
    public DbSet<Career> Careers { get; set; }
    public DbSet<Campus> Campuses { get; set; }
    public DbSet<Profession> Professions { get; set; }
    // public DbSet<Report> Reports { get; set; }
    // public DbSet<Appointment> Appointments { get; set; }
    // public DbSet<Workshop> Workshops { get; set; }
    // public DbSet<StudentWorkshop> StudentWorkshops { get; set; }
}