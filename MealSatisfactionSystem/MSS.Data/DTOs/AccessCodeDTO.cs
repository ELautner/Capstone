using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
    /* 
    CREATED:    P. Chavez           MAR 6 2018

    AccessCodeDTO
    Uses default "get" and "set" methods to access the accesscode entity. The AccessCodeDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public int accesscodeid - the unique ID of the access code (primary key)
    public string accesscodeword - the text of the access code
    public bool activeyn - indicator of whether or not the access code is in rotation (true = active)
    */
    public class AccessCodeDTO
    {
        public int accesscodeid { get; set; }
        public string accesscodeword { get; set; }
        public bool activeyn { get; set; }
    }
}
