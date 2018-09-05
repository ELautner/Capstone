namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.accesscode")]
    public partial class accesscode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public accesscode()
        {
            caresiteaccesses = new HashSet<caresiteaccess>();
        }

        public int accesscodeid { get; set; }

        [Required]
        [StringLength(8)]
        public string accesscodeword { get; set; }

        public bool activeyn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<caresiteaccess> caresiteaccesses { get; set; }
    }
}
