using Fundsy_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Fundsy_backend.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Relationships
            //User->Projects (1-M)

            modelBuilder.Entity<Project>()
            .HasOne(x => x.User)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.User_id)
            .OnDelete(DeleteBehavior.Cascade);

            //User->Payments (1-M)  
            modelBuilder.Entity<Payment>()
            .HasOne(x => x.User)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.User_id)
            .OnDelete(DeleteBehavior.Cascade);

            //Project->Payments (1-M)
            modelBuilder.Entity<Payment>()
            .HasOne(x => x.Project)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.Project_id)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
