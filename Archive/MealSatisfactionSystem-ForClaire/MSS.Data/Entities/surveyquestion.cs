namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.surveyquestion")]
    public partial class surveyquestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public surveyquestion()
        {
            possibleanswers = new HashSet<possibleanswer>();
            surveyanswers = new HashSet<surveyanswer>();
        }

        public int surveyquestionid { get; set; }

        [Required]
        [StringLength(250)]
        public string question { get; set; }

        [Required]
        [StringLength(20)]
        public string questiontype { get; set; }

        public bool activeyn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<possibleanswer> possibleanswers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<surveyanswer> surveyanswers { get; set; }
    }
}
