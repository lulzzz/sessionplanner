using Microsoft.EntityFrameworkCore;
using SessionPlanner.Domain;

namespace SessionPlanner.Repositories
{
    public class SessionPlannerDbContext: DbContext
    {
        public SessionPlannerDbContext(DbContextOptions<SessionPlannerDbContext> options):base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }    

        public DbSet<Session> Sessions {get;set;}
        public DbSet<Speaker> Speakers {get;set;}
        public DbSet<Submission> Submissions {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().Property<byte[]>("Version").IsRowVersion().IsRequired();
            modelBuilder.Entity<Submission>().Property<byte[]>("Version").IsRowVersion().IsRequired();
            modelBuilder.Entity<Speaker>().Property<byte[]>("Version").IsRowVersion().IsRequired();

            modelBuilder.Entity<Session>().HasKey(x => x.Id);
            modelBuilder.Entity<Event>().HasKey(x => x.Id);
            modelBuilder.Entity<Speaker>().HasKey(x => x.Id);
            modelBuilder.Entity<Submission>().HasKey(x => x.Id);
        }
    }
}