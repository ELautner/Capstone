using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
    /* 
    CREATED:    E. Lautner        MAR 3 2018

    PossibleAnswerDTO
    The PossibleAnswerDTO acts as a DTO for the possibleanswer table in the database.The PossibleAnswerDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public int possibleanswerid - the unique ID of the possible answer (primary key)
    public string possibleanswertext - the wording of the possible answer, shown on the survey
    public bool activeyn - a flag to show whether the possible answer is active in the database or not
    public int surveyquestionid - unique ID for the survey question the possible answer is connected to (foreign key)
    */
    public class PossibleAnswerDTO
    {
        public int possibleanswerid { get; set; }
        public string possibleanswertext { get; set; }
        public bool activeyn { get; set; }
        public int surveyquestionid { get; set; }
    }
}
