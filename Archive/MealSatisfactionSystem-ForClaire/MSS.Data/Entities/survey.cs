namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.survey")]
    public partial class survey
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public survey()
        {
            surveyanswers = new HashSet<surveyanswer>();
        }

        public int surveyid { get; set; }

        public DateTime date { get; set; }

        [StringLength(8)]
        public string age { get; set; }

        [StringLength(1)]
        public string gender { get; set; }

        [StringLength(40)]
        public string firstname { get; set; }

        [StringLength(40)]
        public string lastname { get; set; }

        [StringLength(10)]
        public string bednumber { get; set; }

        [StringLength(14)]
        public string phonenumber { get; set; }

        [StringLength(10)]
        public string preferredcontact { get; set; }

        public bool? contactedyn { get; set; }

        public int respondenttypeid { get; set; }

        public int unitid { get; set; }

        public virtual respondenttype respondenttype { get; set; }

        public virtual unit unit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<surveyanswer> surveyanswers { get; set; }
    }
}
