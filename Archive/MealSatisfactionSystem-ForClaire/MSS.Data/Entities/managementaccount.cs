namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.managementaccount")]
    public partial class managementaccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public managementaccount()
        {
            caresites = new HashSet<caresite>();
        }

        public int managementaccountid { get; set; }

        [Required]
        [StringLength(40)]
        public string firstname { get; set; }

        [Required]
        [StringLength(40)]
        public string lastname { get; set; }

        [Required]
        [StringLength(254)]
        public string email { get; set; }

        [Required]
        [StringLength(83)]
        public string username { get; set; }

        [Required]
        [StringLength(16)]
        public string userpassword { get; set; }

        public bool activeyn { get; set; }

        public int authorizationlevelid { get; set; }

        public virtual authorizationlevel authorizationlevel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<caresite> caresites { get; set; }
    }
}
