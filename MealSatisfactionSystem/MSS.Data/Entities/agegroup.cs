using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.Entities
{
    /* 
    CREATED:    A. Valberg      MAR 28 2018

    agegroup
    An entity that links to the agegroup entity in the MSS database.

    PROPERTIES: (generic get/set, no validation)
    public int agegroupid - the unique ID of the agegroup (primary key)
    public string agegroupname - the name of the agegroup
    public bool activeyn - a flag indicating whether the agegroup is active

    ENTITY LINKS:
    public virtual ICollection<survey> surveys - a link to the list of surveys connected to the agegroup

    CONSTRUCTORS:
    public agegroup() - the agegroup constructor
    */
    [Table("public.agegroup")]
    public class agegroup
    {
        public agegroup()
        {
            surveys = new HashSet<survey>();
        }

        public int agegroupid { get; set; }

        [Required, StringLength(20)]
        public string agegroupname { get; set; }

        public bool activeyn { get; set; }

        public virtual ICollection<survey> surveys { get; set; }
    }
}
