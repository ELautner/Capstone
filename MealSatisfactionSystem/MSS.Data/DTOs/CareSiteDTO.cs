using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
    /* 
    CREATED:    C. Stanhope           MAR 6 2018

    CareSiteDTO
    Uses default "get" and "set" methods to access the caresite entity. The CareSiteDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public int caresiteid - the unique ID of the care site (primary key)
    public string caresitename  - the name of the care site
    public string address - the address of care site
    public string city - the city where the care site resides in
    public string province - the province where the care site resides in
    public bool activeyn - indicator of whether or not the care site is active in the Meal Satisfaction System (true = active)
    */
    public class CareSiteDTO
    {
        public int caresiteid { get; set; }
        public string caresitename { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public bool activeyn { get; set; }
    }
}
