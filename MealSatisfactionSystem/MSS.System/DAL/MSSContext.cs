namespace MSS.System.DAL
{
    using Data.Entities;
    using global::System.Data.Entity;

    /* 
    CREATED:    A. Valberg       MAR 1 2018

    MSSContext
    Uses default "get" and "set" methods to access the entities in the MSS.Data.Entities folder to be used for business logic methods.

    PROPERTIES: (generic get/set, no validation)
    public virtual DbSet<accesscode> accesscodes - allows connection  to the accesscode entity
    public virtual DbSet<caresite> caresites - allows connection  to the caresite entity
    public virtual DbSet<caresiteaccess> caresiteaccesses - allows connection  to the caresiteaccesse entity
    public virtual DbSet<possibleanswer> possibleanswers - allows connection  to the possibleanswer entity
    public virtual DbSet<respondenttype> respondenttypes - allows connection  to the respondenttype entity
    public virtual DbSet<survey> surveys - allows connection  to the survey entity
    public virtual DbSet<surveyanswer> surveyanswers - allows connection  to the surveyanswer entity
    public virtual DbSet<surveyquestion> surveyquestions - allows connection  to the surveyquestion entity
    public virtual DbSet<unit> units - allows connection  to the unit entity

    ODEV METHODS:
    public MSSContext(): base("name=MSSDB") - connects the MSSContext to the MSS database (MSSDB)
    protected override void OnModelCreating(DbModelBuilder modelBuilder) - implements the relationships between the entities from the database
    */
    internal partial class MSSContext : DbContext
    {
        /* 
        CREATED:	A. Valberg       MAR 1 2018
        
        MSSContext()
        This method connects the MSSContext to the MSS database (MSSDB).

        PARAMETERS: 	
        None

        RETURNS: 
        None

        ODEV METHOD CALLS:
        None
        */
        public MSSContext()
            : base("name=MSSDB")
        {
        }

        public virtual DbSet<accesscode> accesscodes { get; set; }
        public virtual DbSet<caresite> caresites { get; set; }
        public virtual DbSet<caresiteaccess> caresiteaccesses { get; set; }
        public virtual DbSet<possibleanswer> possibleanswers { get; set; }
        public virtual DbSet<respondenttype> respondenttypes { get; set; }
        public virtual DbSet<survey> surveys { get; set; }
        public virtual DbSet<surveyanswer> surveyanswers { get; set; }
        public virtual DbSet<surveyquestion> surveyquestions { get; set; }
        public virtual DbSet<unit> units { get; set; }
        public virtual DbSet<gender> genders { get; set; }
        public virtual DbSet<agegroup> agegroups { get; set; }


        /* 
        CREATED:	A. Valberg       MAR 1 2018
        
        OnModelCreating()
        This method connects the MSSContext to the MSS database (MSSDB).

        PARAMETERS: 	
        DbModelBuilder modelBuilder - maps the classes to the database schema/relationships

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accesscode>()
                .HasMany(e => e.caresiteaccesses)
                .WithRequired(e => e.accesscode)
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

            modelBuilder.Entity<respondenttype>()
                .HasMany(e => e.surveys)
                .WithRequired(e => e.respondenttype)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<gender>()
                .HasMany(e => e.surveys)
                .WithRequired(e => e.gender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<agegroup>()
                .HasMany(e => e.surveys)
                .WithRequired(e => e.agegroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<survey>()
                .HasMany(e => e.surveyanswers)
                .WithRequired(e => e.survey)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<surveyquestion>()
                .HasMany(e => e.possibleanswers)
                .WithRequired(e => e.surveyquestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<possibleanswer>()
                .HasMany(e => e.surveyanswers)
                .WithRequired(e => e.possibleanswer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<unit>()
                .HasMany(e => e.surveys)
                .WithRequired(e => e.unit)
                .WillCascadeOnDelete(false);
        }
    }
}
