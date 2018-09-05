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
using System.Data.Entity;

namespace MSS.System.BLL
{

    /* 
    CREATED:    E. Lautner      MAR 10 2018

    PossibleAnswerController
    The PossibleAnswerController classes purpose it to use methods to update, deactivate or retrieve data from the PossibleAnswer table in the database.

    FIELDS:
    None
    
    METHODS:
    public List<PossibleAnswerDTO> GetPossibleAnswers(int surveyQuestionID) - this method retrieves a list of PossibleAnswers with a given surveyQuestionID 
    public void UpdatePossibleAnswer(PossibleAnswerDTO updatedPossibleAnswer) - this method updates a PossibleAnswer in the MSS database
    public void DeactivatePossibleAnswer(PossibleAnswerDTO deactivatedPossibleAnswer) - this method deactivates a PossibleAnswer in the MSS database
    public void AddPossibleAnswer(PossibleAnswerDTO tempPossibleAnswer) - this method adds a PossibleAnswer to the MSS database
    */
    [DataObject]
    public class PossibleAnswerController
    {

        /* 
        CREATED:    E. Lautner      MAR 10 2018

        GetPossibleAnswers()
        This method retrieves a list of PossibleAnswers with a given surveyQuestionID 

        PARAMETERS: 	
        int surveyQuestionID - The ID of the survey question

        RETURNS: 
        List<PossibleAnswerDTO> - the possibleanswer ID and text is returned

        ODEV METHOD CALLS:
        None
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

        /* 
        CREATED:     H. Conant       MAR 20 2018

        UpdatePossibleAnswer()
        This method updates a PossibleAnswer in the MSS database.

        PARAMETERS: 	
        PossibleAnswerDTO updatedPossibleAnswer - the PossibleAnswer that is to be updated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdatePossibleAnswer(PossibleAnswerDTO updatedPossibleAnswer)
        {
            using (var context = new MSSContext())
            {
                var possibleAnswer = context.possibleanswers.Find(updatedPossibleAnswer.possibleanswerid);
                possibleAnswer.possibleanswertext = updatedPossibleAnswer.possibleanswertext;
                possibleAnswer.surveyquestionid = updatedPossibleAnswer.surveyquestionid;
                var existingPossibleAnswer = context.Entry(possibleAnswer);
                existingPossibleAnswer.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 20 2018

        DeactivatePossibleAnswer()
        This method deactivates a PossibleAnswer in the MSS databse.

        PARAMETERS: 	
        PossibleAnswerDTO deactivatedPossibleAnswer - the PossibleAnswer that is to be deactivated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        public void DeactivatePossibleAnswer(PossibleAnswerDTO deactivatedPossibleAnswer)
        {
            using (var context = new MSSContext())
            {
                var possibleAnswer = context.units.Find(deactivatedPossibleAnswer.possibleanswerid);
                possibleAnswer.activeyn = false;
                var existingPossibleAnswer = context.Entry(possibleAnswer);
                existingPossibleAnswer.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		APR 11 2018

        AddPossibleAnswer()
        This method adds a PossibleAnswer to the MSS database.

        PARAMETERS: 	
        PossibleAnswerDTO tempPossibleAnswer - the PossibleAnswer that is to be added to the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddPossibleAnswer(PossibleAnswerDTO tempPossibleAnswer)
        {

            using (var context = new MSSContext())
            {
                possibleanswer newPossibleAnswer = new possibleanswer();
                newPossibleAnswer.possibleanswerid = tempPossibleAnswer.possibleanswerid;
                newPossibleAnswer.possibleanswertext = tempPossibleAnswer.possibleanswertext;
                newPossibleAnswer.activeyn = true;
                newPossibleAnswer.surveyquestionid = tempPossibleAnswer.surveyquestionid;
                context.possibleanswers.Add(newPossibleAnswer);
                context.SaveChanges();
            }
        }
    }
}
