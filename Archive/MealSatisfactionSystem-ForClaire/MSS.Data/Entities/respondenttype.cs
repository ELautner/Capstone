namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.respondenttype")]
    public partial class respondenttype
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public respondenttype()
        {
            surveys = new HashSet<survey>();
        }

        public int respondenttypeid { get; set; }

        [Required]
        [StringLength(15)]
        public string respondenttypename { get; set; }

        public bool activeyn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<survey> surveys { get; set; }
    }
}
