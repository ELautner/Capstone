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
    CREATED:    T.J. Blakely      MAR 5 2018

    SurveyQuestionController
    The SurveyAnswerController class' purpose is to use methods to retrieve and add data in the surveyanswer table in the MSS database.

    FIELDS:
    None

    METHODS:
    public List<surveyanswer> GetSurveyAnswers(int surveyId) - retrieves a list of all survey answers for a specified surveyid
    public List<surveyanswer> GetSurveyAnswer(int surveyId, int surveyQuestionId) - retrieves a single survey answer for a specified surveyid
    public void AddSurveyAnswer(int surveyId, int surveyQuestionId, string historicalQuestion, string answer) - adds a single survey answer to the database for a specified surveyid and surquestionid
    */
    [DataObject]
    public class SurveyAnswerController
    {
        /* 
        CREATED:    T.J. Blakely      MAR 5 2018
        MODIFIED:   H. Conant         MAR 27 2018
            - Updated method body

        GetSurveyAnswers()
        This method retrieves a list of survey answers.

        PARAMETERS: 	
        int surveyID - the survey that the answers belong to

        RETURNS: 
        List<surveyanswer> - a list of survey answers for a specific survey ID that are in the database
    
        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<surveyanswer> GetSurveyAnswers(int surveyId)
        {
            using (var context = new MSSContext())
            {
                var surveyAnswer = (from aSurveyAnswer in context.surveyanswers
                                   where aSurveyAnswer.surveyid == surveyId
                                   select aSurveyAnswer).ToList();
                List<surveyanswer> surveyAnswersList = new List<surveyanswer>();
                foreach (surveyanswer answer in surveyAnswer)
                {
                    surveyanswer temp = new surveyanswer();
                    temp.surveyid = answer.surveyid;
                    temp.possibleanswerid = answer.possibleanswerid;
                    temp.historicalquestion = answer.historicalquestion;
                    temp.answer = answer.answer;
                    temp.possibleanswer = answer.possibleanswer;
                    surveyAnswersList.Add(temp);
                }
                return surveyAnswersList;
            }
        }

        /* 
        CREATED:    T.J. Blakely      MAR 5 2018
        MODIFIED:   H. Conant         MAR 27 2018
            - Updated method body

        GetSurveyAnswer()
        This method retrieves a single survey answer for a specific survey and question.

        PARAMETERS: 	
        int surveyId - the survey that the answer belongs to
        int surveyQuestionId - the survey question that the answer belongs to

        RETURNS: 
        List<surveyanswer> - a list of survey answers for a specific survey ID and specific survey question ID that are in the database
    
        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<surveyanswer> GetSurveyAnswer(int surveyId, int surveyQuestionId)
        {
            using (var context = new MSSContext())
            {
                var surveyAnswer = from aSurveyAnswer in context.surveyanswers
                                   where aSurveyAnswer.surveyid == surveyId && aSurveyAnswer.possibleanswer.surveyquestionid == surveyQuestionId
                                   select aSurveyAnswer;
                List<surveyanswer> surveyAnswersList = new List<surveyanswer>();
                foreach (surveyanswer surveyanswer in surveyAnswer)
                {
                    surveyanswer temp = new surveyanswer();
                    temp.surveyid = surveyanswer.surveyid;
                    temp.possibleanswerid = surveyanswer.possibleanswerid;
                    temp.historicalquestion = surveyanswer.historicalquestion;
                    temp.answer = surveyanswer.answer;
                    temp.possibleanswer = surveyanswer.possibleanswer;
                    surveyAnswersList.Add(temp);
                }
                return surveyAnswersList;
            }
        }

        /* 
        CREATED:    T.J. Blakely      MAR 5 2018
        MODIFIED:   H. Conant         MAR 27 2018
            - Changed method signature
            - Updated method body

        AddSurveyAnswer()
        This method stores a list of survey answers that have been retrieved from a Survey Respondent.

        PARAMETERS: 	
        int surveyid - the ID of the survey submitted by a Survey Respondent
        int surveyQuestionId - the ID of the survey question that was answered
        string historicalQuestion - the question that was being asked at the time of survey submission
        string answer - the answer given by the Survey Respondent

        RETURNS: 
        void
    
        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public void AddSurveyAnswer(int surveyId, int possibleAnswerId, string historicalQuestion, string answer)
        {
            using (var context = new MSSContext())
            {
                surveyanswer surveyAnswer = new surveyanswer();
                surveyAnswer.surveyid = surveyId;
                surveyAnswer.possibleanswerid = possibleAnswerId;
                surveyAnswer.historicalquestion = historicalQuestion;
                surveyAnswer.answer = answer;

                context.surveyanswers.Add(surveyAnswer);
                context.SaveChanges();
            }
        }
    }
}
