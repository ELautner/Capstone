namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.authorizationlevel")]
    public partial class authorizationlevel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public authorizationlevel()
        {
            managementaccounts = new HashSet<managementaccount>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int authorizationlevelid { get; set; }

        [Required]
        [StringLength(20)]
        public string authorizationlevelname { get; set; }

        [Required]
        [StringLength(150)]
        public string authorizationleveldescription { get; set; }

        public bool activeyn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<managementaccount> managementaccounts { get; set; }
    }
}
