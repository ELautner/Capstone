using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs
{
    /* 
    CREATED:    H. L'Heureux        MAR 20 2018

    SurveyAnswerDTO
    Uses default "get" and "set" methods to access the surveyanswer entity. The SurveyAnswerDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public int surveyid - the unique ID of the survey (primary key) (foreign key)
    public int possibleanswerid - the unique ID of the answer (primary key) (foreign key)
    public string historicalquestion - the question that was posed to the user at the time of response
    public string answer - the response from the user
    */
    public class SurveyAnswerDTO
    {
        public int surveyid {get;set;}
        public int possibleanswerid { get; set; }
        public string historicalquestion { get; set; }
        public string answer { get; set; }
    }
}
