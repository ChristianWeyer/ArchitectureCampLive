namespace ConferenceDudeServices
{
    using System.Data.Entity;

    public partial class ConferenceDudeContext : DbContext
    {
        public ConferenceDudeContext()
            : base("name=ConferenceDudeContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public virtual DbSet<Speaker> Speakers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
