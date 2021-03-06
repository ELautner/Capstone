namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /*
    CREATED:    Generated by ADO.NET Entity Data Model      MAR 6 2018

    possibleanswer
    An entity that links to the possibleanswer table in the database.

    PROPERTIES: (generic get/set, no validation)
    public int possibleanswerid - the unique ID of the possible answer (primary key)
    public string possibleanswertext - the wording of the possible answer, shown on the survey
    public bool activeyn - a flag indicating whether the possible answer is active
    public int surveyquestionid - unique ID for the survey question the possible answer is connected to (foreign key)

    ENTITY LINKS:
    public virtual surveyquestion surveyquestion - a link to the survey question associated with a possible answer
    public virtual ICollection<surveyanswer> surveyanswers - a link to the list of surveyanswers connected to the possibleanswer

    CONSTRUCTORS:
    public possibleanswer() - the possibleanswer constructor
    */
    [Table("public.possibleanswer")]
    public partial class possibleanswer
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public possibleanswer()
        {
            surveyanswers = new HashSet<surveyanswer>();
        }


        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int possibleanswerid { get; set; }

        [StringLength(250)]
        public string possibleanswertext { get; set; }
        
        public bool activeyn { get; set; }

        public int surveyquestionid { get; set; }

        public virtual surveyquestion surveyquestion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<surveyanswer> surveyanswers { get; set; }
    }
}
