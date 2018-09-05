namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.caresiteaccess")]
    public partial class caresiteaccess
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int accesscodeid { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int caresiteid { get; set; }

        public DateTime dateused { get; set; }

        public virtual accesscode accesscode { get; set; }

        public virtual caresite caresite { get; set; }
    }
}
