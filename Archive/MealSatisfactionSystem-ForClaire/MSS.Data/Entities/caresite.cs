namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.caresite")]
    public partial class caresite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public caresite()
        {
            caresiteaccesses = new HashSet<caresiteaccess>();
            units = new HashSet<unit>();
            managementaccounts = new HashSet<managementaccount>();
        }

        public int caresiteid { get; set; }

        [Required]
        [StringLength(80)]
        public string caresitename { get; set; }

        [Required]
        [StringLength(40)]
        public string address { get; set; }

        [Required]
        [StringLength(30)]
        public string city { get; set; }

        [Required]
        [StringLength(2)]
        public string province { get; set; }

        public bool activeyn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<caresiteaccess> caresiteaccesses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<unit> units { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<managementaccount> managementaccounts { get; set; }
    }
}
