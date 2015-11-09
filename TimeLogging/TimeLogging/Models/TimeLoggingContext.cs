namespace TimeLogging.Models
{
    using System.Data.Entity;

    public partial class TimeLoggingContext : DbContext
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
