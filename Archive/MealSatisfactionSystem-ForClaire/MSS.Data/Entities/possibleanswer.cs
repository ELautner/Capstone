namespace MSS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.possibleanswer")]
    public partial class possibleanswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int possibleanswerid { get; set; }

        [StringLength(250)]
        public string possibleanswertext { get; set; }

        public bool activeyn { get; set; }

        public int surveyquestionid { get; set; }

        public virtual surveyquestion surveyquestion { get; set; }
    }
}
