namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.unit")]
    public partial class unit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public unit()
        {
            surveys = new HashSet<survey>();
        }

        public int unitid { get; set; }

        [Required]
        [StringLength(60)]
        public string unitname { get; set; }

        public bool activeyn { get; set; }

        public int caresiteid { get; set; }

        public virtual caresite caresite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<survey> surveys { get; set; }
    }
}
