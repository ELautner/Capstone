using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSS.Data.Entities
{
    /* 
    CREATED:    A. Valberg      MAR 28 2018

    gender
    An entity that links to the gender entity in the MSS database.

    PROPERTIES: (generic get/set, no validation)
    public int genderid - the unique ID of the gender (primary key)
    public string gendername - the name of the gender
    public bool activeyn - a flag indicating whether the gender is active

    ENTITY LINKS:
    public virtual ICollection<survey> surveys - a link to the list of surveys connected to the gender

    CONSTRUCTORS:
    public gender() - the gender constructor
    */
    [Table("public.gender")]
    public class gender
    {
        public gender()
        {
            surveys = new HashSet<survey>();
        }

        public int genderid { get; set; }

        [Required, StringLength(20)]
        public string gendername { get; set; }

        public bool activeyn { get; set; }

        public virtual ICollection<survey> surveys { get; set; }
    }
}
