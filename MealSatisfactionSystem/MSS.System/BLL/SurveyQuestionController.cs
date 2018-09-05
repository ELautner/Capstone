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
    CREATED:    T.J. Blakely      MAR 5 2018

    SurveyQuestionController
    The SurveyQuestionController class's purpose is to use methods to retrieve, update and deactivate data from the SurveyQuestion table in the database.

    FIELDS:
    None

    METHODS:
    public List<surveyquestion> GetSurveyQuestions() - this method retrieves a list of all survey questions
    public List<surveyquestion> GetActiveSurveyQuestions() - this method retrieves a list of all survey questions that are shown on the survey (active)
    public List<surveyquestion> GetQuestionsforReports() - this method retrives a list of active survey questions for the reporting page
    public void UpdateSurveyQuestion(surveyquestion updatedSurveyQuestion) - this method updates a survey question in the MSS database
    public void DeactivateSurveyQuestion(surveyquestion deactivatedSurveyQuestion) - this method deactivates a survey question in the MSS database
    public void AddSurveyQuestion(surveyquestion tempSurveyQuestion) - this method adds a survey question to the MSS database
    */
    [DataObject]
    public class SurveyQuestionController
    {
        /* 
        CREATED:    T.J. Blakely      MAR 5 2018

        GetSurveyQuestions()
        This method retrieves a list of all survey questions in the database.

        PARAMETERS: 	
        None

        RETURNS: 
        List<surveyquestion> - a list of all survey questions that are in the database

        ODEV METHOD CALLS:
        None
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
                    temp.surveyquestionid = surveyquestion.surveyquestionid;
                    temp.question = surveyquestion.question;
                    temp.questiontype = surveyquestion.questiontype;
                    temp.activeyn = surveyquestion.activeyn;
                    surveyQuestionsList.Add(temp);
                }
                return surveyQuestionsList;
            }
        }

        /* 
        CREATED:    T.J. Blakely      MAR 5 2018

        GetActiveSurveyQuestions()
        This method retrieves a list of survey questions that are shown on the survey (active).

        PARAMETERS: 	
        None

        RETURNS: 
        List<surveyquestion> - a list of all active survey questions that are in the database
        
        ODEV METHOD CALLS:
        None
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
        /* 
        CREATED:     A. Valberg       MAR 26 2018

        GetQuestionsforReports()
        This method pulls questions from the DB, but only those needed for the reports DDL.

        PARAMETERS: 	
        None

        RETURNS: 
        List<surveyquestion> - a list of all active survey questions to be displayed on the reporting page

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<surveyquestion> GetQuestionsforReports()
        {
            using (var context = new MSSContext())
            {
                var surveyQuestions = from aSurveyQuestion in context.surveyquestions
                                      where aSurveyQuestion.activeyn == true && aSurveyQuestion.surveyquestionid != 9
                                      select aSurveyQuestion;
                List<surveyquestion> surveyQuestionsList = new List<surveyquestion>();
                foreach (surveyquestion surveyquestion in surveyQuestions)
                {
                    surveyquestion temp = new surveyquestion();
                    switch (surveyquestion.surveyquestionid)
                    {
                        case 1:
                            temp.question = "Q1A: " + surveyquestion.question;
                            break;
                        case 2:
                            temp.question = "Q1B: " + surveyquestion.question;
                            break;
                        case 3:
                            temp.question = "Q1C: " + surveyquestion.question;
                            break;
                        case 4:
                            temp.question = "Q1D: " + surveyquestion.question;
                            break;
                        case 5:
                            temp.question = "Q1E: " + surveyquestion.question;
                            break;
                        case 6:
                            temp.question = "Q2: " + surveyquestion.question;
                            break;
                        case 7:
                            temp.question = "Q3: " + surveyquestion.question.Split('(')[0];
                            break;
                        case 8:
                            temp.question = "Q4: " + surveyquestion.question;
                            break;
                        default:
                            break;
                    }
                    temp.surveyquestionid = surveyquestion.surveyquestionid;
                    temp.questiontype = surveyquestion.questiontype;
                    temp.activeyn = surveyquestion.activeyn;
                    surveyQuestionsList.Add(temp);
                }
                return surveyQuestionsList;
            }
        }
        /* 
        CREATED:     H. Conant       MAR 20 2018

        UpdateSurveyQuestion()
        This method updates a survey question in the MSS database.

        PARAMETERS: 	
        surveyquestion updatedSurveyQuestion - the survey question to be updated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateSurveyQuestion(surveyquestion updatedSurveyQuestion)
        {
            using (var context = new MSSContext())
            {
                var surveyQuestion = context.surveyquestions.Find(updatedSurveyQuestion.surveyquestionid);
                surveyQuestion.question = updatedSurveyQuestion.question;
                surveyQuestion.questiontype = updatedSurveyQuestion.questiontype;
                var existingSurveyQuestion = context.Entry(surveyQuestion);
                existingSurveyQuestion.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 20 2018

        DeactivateSurveyQuestion()
        This method deactivates a survey question in the MSS database.

        PARAMETERS: 	
        surveyquestion deactivatedSurveyQuestion - the survey question to be deactivated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void DeactivateSurveyQuestion(surveyquestion deactivatedSurveyQuestion)
        {
            using (var context = new MSSContext())
            {
                var surveyQuestion = context.respondenttypes.Find(deactivatedSurveyQuestion.surveyquestionid);
                surveyQuestion.activeyn = false;
                var existingRespondentType = context.Entry(surveyQuestion);
                existingRespondentType.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	    H. Conant		APR 11 2018

        AddSurveyQuestion()
        This method adds a survey question to the MSS database.

        PARAMETERS: 	
        surveyquestion tempSurveyQuestion - the survey question that is to be added to the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddSurveyQuestion(surveyquestion tempSurveyQuestion)
        {

            using (var context = new MSSContext())
            {
                surveyquestion newSurveyQuestion = new surveyquestion();
                newSurveyQuestion.question = tempSurveyQuestion.question;
                newSurveyQuestion.questiontype = tempSurveyQuestion.questiontype;
                newSurveyQuestion.activeyn = true; 
                context.surveyquestions.Add(newSurveyQuestion);
                context.SaveChanges();
            }
        }
    }
}
