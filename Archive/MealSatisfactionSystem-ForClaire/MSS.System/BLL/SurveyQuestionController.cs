using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MSS.Data;
using MSS.System;
using System.Data.Entity;
using MSS.Data.Entities;
using MSS.System.DAL;

namespace MSS.System.BLL
{
    /* 
    AUTHOR: TJ Blakely
    DATE CREATED: MARCH 5 2018

    SurveyQuestionController
    The SurveyQuestionController class' purpose is to use methods to retrieve data from the SurveyQuestion table in the database.

    METHODS:
    public List<surveyquestion> GetSurveyQuestions()
    
    */
    [DataObject]
    public class SurveyQuestionController
    {
        /* 
        AUTHOR: TJ Blakely
        DATE CREATED: MARCH 5 2018

        GetSurveyQuestions()
        This method retrieves a list of SurveyQuestion.

        PARAMETERS: 	
        None.

        RETURNS: List<surveyquestion> - A list of surveyquestions that are in the database.
    
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<surveyquestion> GetSurveyQuestions()
        {
            using (var context = new MSSContext())
            {
                var surveyQuestions = from aSurveyQuestion in context.surveyquestions
                                      select aSurveyQuestion;
                List<surveyquestion> surveyQuestionsList = new List<surveyquestion>();
                foreach (surveyquestion surveyquestion in surveyQuestions)
                {
                    surveyquestion temp = new surveyquestion();
                    temp.question = surveyquestion.question;
                    temp.questiontype = surveyquestion.questiontype;
                    temp.activeyn = surveyquestion.activeyn;
                    surveyQuestionsList.Add(temp);
                }
                return surveyQuestionsList;
            }
        }

        /* 
        AUTHOR: TJ Blakely
        DATE CREATED: MARCH 5 2018

        GetActiveSurveyQuestions()
        This method retrieves a list of SurveyQuestion that are shown on the survey (active).

        PARAMETERS: 	
        None.

        RETURNS: List<surveyquestion> - A list of active surveyquestions that are in the database.
    
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<surveyquestion> GetActiveSurveyQuestions()
        {
            using (var context = new MSSContext())
            {
                var surveyQuestions = from aSurveyQuestion in context.surveyquestions
                                      where aSurveyQuestion.activeyn == true
                                      select aSurveyQuestion;
                List<surveyquestion> surveyQuestionsList = new List<surveyquestion>();
                foreach (surveyquestion surveyquestion in surveyQuestions)
                {
                    surveyquestion temp = new surveyquestion();
                    temp.question = surveyquestion.question;
                    temp.questiontype = surveyquestion.questiontype;
                    temp.activeyn = surveyquestion.activeyn;
                    surveyQuestionsList.Add(temp);
                }
                return surveyQuestionsList;
            }
        }
    }
}
