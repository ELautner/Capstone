using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs.Security
{
    /* 
    CREATED:   E. Lautner      MAR 4 2018

    ApplicationUserDTO
    Uses default "get" and "set" methods to access the AspNetUser entity. The ApplicationUserDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public string firstname - the first name of the selected user
    public string lastname - the last name of the selected user
    public bool activeyn -  the status of the selected user
    public string username - the username of the selected user
    */
    public class ApplicationUserDTO
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool activeyn { get; set; }
        public string username { get; set; }
    }
}
