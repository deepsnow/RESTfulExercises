namespace TimeLogging.Models
{
    using System.Data.Entity;

    public interface ITimeLoggingContext
    {
        DbSet<Log> Logs { get; set; }
    }

    public partial class TimeLoggingContext : DbContext, ITimeLoggingContext
    {
        public TimeLoggingContext()
            : base("name=TimeLogging")
        {
        }

        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .Property(e => e.UserId)
                .IsFixedLength();

            modelBuilder.Entity<Log>()
                .Property(e => e.Comment)
                .IsFixedLength();
        }
    }
}
