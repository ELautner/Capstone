using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MSS.Data.DTOs
{
    /* 
    CREATED:    H. L'Heureux         MAR 29 2018

    RespondentDTO
    Uses default "get" and "set" methods to access the reposondenttype entity. The RespondentTypeDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public int respondenttypeid - the unique ID of the respondenttype (primary key)
    public string respondenttypename - the name in the database
    public bool activeyn - declares wether or not the respondent type is active 
    */
    public class RespondentDTO
    {
        public int respondenttypeid { get; set; }
        public string respondenttypename { get; set; }
        public bool activeyn { get; set; }
    }
}
