using MSS.Data;
using MSS.Data.DTOs;
using MSS.System.DAL;
using MSS.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.System.BLL
{

    /* 
AUTHOR: Eric Lautner
DATE CREATED: MARCH 10 2018

SurveyController
The PossibleAnswerController classes purpose it to use methods to insert, update, delete or retrive data from the PossibleAnswer table in the database.

METHODS:
  public List<PossibleAnswerDTO> GetPossibleAnswers(int surveyQuestionID)
*/

    [DataObject]
    public class PossibleAnswerController
    {

                /* 
        AUTHOR: Eric Lautner
        DATE CREATED: MARCH 10 2018

        GetSurveyDetails()
        This method retrieves a list of PossibleAnswers with a given surveyQuestionID 

        PARAMETERS: 	
        int surveyQuestionID - The ID of the survey question.

        RETURNS: List<PossibleAnswerDTO> - the possible answer id and text is returned

        METHOD CALLS:
        DataObjectMethod()
        MSSContext()
        

        */


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PossibleAnswerDTO> GetPossibleAnswers(int surveyQuestionID)
        {
            using (var context = new MSSContext())
            {
                var possibleAnswers = from aAnswer in context.possibleanswers
                                      where aAnswer.surveyquestionid == surveyQuestionID && aAnswer.activeyn == true
                                      select aAnswer;
                List<PossibleAnswerDTO> possibleAnswerDTOs = new List<PossibleAnswerDTO>();
                foreach (possibleanswer possibleanswer in possibleAnswers)
                {
                    PossibleAnswerDTO temp = new PossibleAnswerDTO();
                    temp.possibleanswerid = possibleanswer.possibleanswerid;
                    temp.possibleanswertext = possibleanswer.possibleanswertext;
                    possibleAnswerDTOs.Add(temp);
                }
                return possibleAnswerDTOs;

            }
        }
    }
}