namespace MSS.System.DAL
{
    using Data.Entities;
    using global::System.Data.Entity;

    public partial class MSSContext : DbContext
    {
        public MSSContext()
            : base("name=MSSDB")
        {
        }

        public virtual DbSet<accesscode> accesscodes { get; set; }
        public virtual DbSet<authorizationlevel> authorizationlevels { get; set; }
        public virtual DbSet<caresite> caresites { get; set; }
        public virtual DbSet<caresiteaccess> caresiteaccesses { get; set; }
        public virtual DbSet<managementaccount> managementaccounts { get; set; }
        public virtual DbSet<possibleanswer> possibleanswers { get; set; }
        public virtual DbSet<respondenttype> respondenttypes { get; set; }
        public virtual DbSet<survey> surveys { get; set; }
        public virtual DbSet<surveyanswer> surveyanswers { get; set; }
        public virtual DbSet<surveyquestion> surveyquestions { get; set; }
        public virtual DbSet<unit> units { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accesscode>()
                .HasMany(e => e.caresiteaccesses)
                .WithRequired(e => e.accesscode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<authorizationlevel>()
                .HasMany(e => e.managementaccounts)
                .WithRequired(e => e.authorizationlevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<caresite>()
                .Property(e => e.province)
                .IsFixedLength();

            modelBuilder.Entity<caresite>()
                .HasMany(e => e.caresiteaccesses)
                .WithRequired(e => e.caresite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<caresite>()
                .HasMany(e => e.units)
                .WithRequired(e => e.caresite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<caresite>()
                .HasMany(e => e.managementaccounts)
                .WithMany(e => e.caresites)
                .Map(m => m.ToTable("accountcaresite", "public").MapLeftKey("caresiteid").MapRightKey("managementaccountid"));

            modelBuilder.Entity<respondenttype>()
                .HasMany(e => e.surveys)
                .WithRequired(e => e.respondenttype)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<survey>()
                .Property(e => e.gender)
                .IsFixedLength();

            modelBuilder.Entity<survey>()
                .HasMany(e => e.surveyanswers)
                .WithRequired(e => e.survey)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<surveyquestion>()
                .HasMany(e => e.possibleanswers)
                .WithRequired(e => e.surveyquestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<surveyquestion>()
                .HasMany(e => e.surveyanswers)
                .WithRequired(e => e.surveyquestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<unit>()
                .HasMany(e => e.surveys)
                .WithRequired(e => e.unit)
                .WillCascadeOnDelete(false);
        }
    }
}
