using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
    /* 
    CREATED:   E. Lautner      MAR 4 2018

    CareSiteAccessDTO
    Uses default "get" and "set" methods to access the CareSiteAccess entity. The CareSiteAccessDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public int accesscodeid - the unique ID of the access code (primary key) (foreign key)
    public int caresiteid - the unique ID of the care site (primary key) (foreign key)
    public DateTime dateused -  the date the access code is active
    */
    public class CareSiteAccessDTO
    {
        public int accesscodeid { get; set;  }
        public int caresiteid { get; set; }
        public DateTime dateused { get; set; }
    }
}
