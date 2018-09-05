namespace MSS.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /* 
    CREATED:    Generated by ADO.NET Entity Data Model      MAR 6 2018

    caresiteaccess
    An entity that links to the caresiteacess entity in the MSS database.

    PROPERTIES: (generic get/set, no validation)
    public int accesscodeid - the unique ID of the access code (primary key) (foreign key)
    public int caresiteid - the unique ID of the care site (primary key) (foreign key)
    public DateTime dateused -  the date when the access code is to be used

    ENTITY LINKS:
    public virtual accesscode accesscode - a link to the accesscode entity
    public virtual caresite caresite -  a link to the caresite entity

    CONSTRUCTORS:
    None
    */
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
