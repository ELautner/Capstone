using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
    /* 
    CREATED:    H. Conant       MAR 4 2018

    ContactRequestDTO
    Uses default "get" and "set" methods to access the Survey entity. The ContactRequestDTO is used to act as a layer of protection and avoids direct modification of database entities.
    Also includes the unitname, caresitename that are related to the Survey.

    PROPERTIES: (generic get/set, no validation)
    public int surveyid - the unique ID of the survey (primary key)
    public DateTime date - the date the survey was taken
    public string firstname - the first name submitted on the survey 
    public string bednumber - the bed number submitted on the survey
    public string phonenumber - the phone number submitted on the survey
    public string preferredcontact - the preferred method of contact submitted on the survey
    public bool? contactedyn - a bool that indicates if a survey respondent has been contacted
    public int genderid - the unique ID of the gender (foreign key)
    public int agegroupid - the unique ID of the age group (foreign key)
    public int respondenttypeid - the unique ID of the respondent type (foreign key)
    public int unitid - the unique ID of the unit (foreign key)
    public string gendername - the name of the gender
    public string agegroupname - the name of the age group
    public string unitname - the name of the unit
    public string caresitename - the name of the care site
    */
    public class ContactRequestDTO
    {
        public int surveyid { get; set; }
        public DateTime date { get; set; }
        public string firstname { get; set; }
        public string bednaumber { get; set; }
        public string phonenumber { get; set; }
        public string preferredcontact { get; set; }
        public bool? contactedyn { get; set; }
        public int genderid { get; set; }
        public int agegroupid { get; set; }
        public int respondenttypeid { get; set; }
        public int unitid { get; set; }
        public string gendername { get; set; }
        public string agegroupname { get; set; }
        public string unitname { get; set; }
        public string caresitename { get; set; }
    }
}
