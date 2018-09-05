using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MSS.Data;
using MSS.System;
using MSS.System.DAL;
using System.Data.Entity;
using MSS.Data.Entities;

namespace MSS.System.BLL
{
    /* 
    AUTHOR: TJ Blakely
    DATE CREATED: MARCH 5 2018

    SurveyQuestionController
    The SurveyAnswerController class' purpose is to use methods to retrieve data from the SurveyAnswer table in the database.

    METHODS:
    public List<surveyquestion> GetSurveyQuestions()
    
    */
    [DataObject]
    public class SurveyAnswerController
    {
        /* 
        AUTHOR: TJ Blakely
        DATE CREATED: MARCH 5 2018

        GetSurveyAnswers()
        This method retrieves a list of SurveyAnswer.

        PARAMETERS: 	
        int surveyID - The ID of the Survey that the answers belong to.

        RETURNS: 
        List<surveyanswer> - A list of surveyanswers for a specific surveyid that are in the database.
    
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<surveyanswer> GetSurveyAnswers(int surveyId)
        {
            using (var context = new MSSContext())
            {
                var surveyAnswer = from aSurveyAnswer in context.surveyanswers
                                   where aSurveyAnswer.surveyid == surveyId
                                   select aSurveyAnswer;
                List<surveyanswer> surveyAnswersList = new List<surveyanswer>();
                foreach (surveyanswer surveyanswer in surveyAnswer)
                {
                    surveyanswer temp = new surveyanswer();
                    temp.surveyid = surveyanswer.surveyid;
                    temp.surveyquestionid = surveyanswer.surveyquestionid;
                    temp.historicalquestion = surveyanswer.historicalquestion;
                    temp.answer = surveyanswer.answer;
                    surveyAnswersList.Add(temp);
                }
                return surveyAnswersList;
            }
        }

        /* 
        AUTHOR: TJ Blakely
        DATE CREATED: MARCH 5 2018

        GetSurveyAnswer()
        This method retrieves a single SurveyAnswer for a specific survey and question.

        PARAMETERS: 	
        int surveyId - The ID of the Survey that the answer belongs to.
        int surveyQuestionId - The ID of the SurveyQuestion that the answer belongs to.

        RETURNS: 
        List<surveyanswer> - A list of surveyanswers for a specific surveyid and specific surveyquestionid that are in the database.
    
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<surveyanswer> GetSurveyAnswer(int surveyId, int surveyQuestionId)
        {
            using (var context = new MSSContext())
            {
                var surveyAnswer = from aSurveyAnswer in context.surveyanswers
                                   where aSurveyAnswer.surveyid == surveyId && aSurveyAnswer.surveyquestionid == surveyQuestionId
                                   select aSurveyAnswer;
                List<surveyanswer> surveyAnswersList = new List<surveyanswer>();
                foreach (surveyanswer surveyanswer in surveyAnswer)
                {
                    surveyanswer temp = new surveyanswer();
                    temp.surveyid = surveyanswer.surveyid;
                    temp.surveyquestionid = surveyanswer.surveyquestionid;
                    temp.historicalquestion = surveyanswer.historicalquestion;
                    temp.answer = surveyanswer.answer;
                    surveyAnswersList.Add(temp);
                }
                return surveyAnswersList;
            }
        }

        /* 
        AUTHOR: TJ Blakely
        DATE CREATED: MARCH 5 2018

        AddSurveyAnswers()
        This method stores a list of SurveyAnswer that have been retrieved from a Survey Respondent.

        PARAMETERS: 	
        int surveyid - the id of the survey submitted by a Survey Respondent.

        METHOD CALLS:
        context.SaveChanges() - saves changes to the database after the answer is added.

        RETURNS: 
        void - nothing is returned.
    
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public void AddSurveyAnswer(int surveyId, int surveyQuestionId, string historicalQuestion, string answer)
        {
            using (var context = new MSSContext())
            {
                surveyanswer surveyAnswer = new surveyanswer();
                surveyAnswer.surveyid = surveyId;
                surveyAnswer.surveyquestionid = surveyQuestionId;
                surveyAnswer.historicalquestion = historicalQuestion;
                surveyAnswer.answer = answer;

                context.surveyanswers.Add(surveyAnswer);
                context.SaveChanges();
            }
        }
    }
}
