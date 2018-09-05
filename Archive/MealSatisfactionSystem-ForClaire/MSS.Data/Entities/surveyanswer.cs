namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.surveyanswer")]
    public partial class surveyanswer
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int surveyid { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int surveyquestionid { get; set; }

        [Required]
        [StringLength(250)]
        public string historicalquestion { get; set; }

        [StringLength(250)]
        public string answer { get; set; }

        public virtual survey survey { get; set; }

        public virtual surveyquestion surveyquestion { get; set; }
    }
}
