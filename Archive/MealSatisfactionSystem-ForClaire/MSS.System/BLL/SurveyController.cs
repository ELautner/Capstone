using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSS.Data.DTOs;
using MSS.Data;
using System.Data.Entity;
using MSS.Data.Entities;
using MSS.System.DAL;

namespace MSS.System.BLL
{
    /* 
    CREATED:	H. Conant		MAR 4 2018

    SurveyController
    The SurveyController classes purpose it to use methods to insert, update, delete or retrive data from the Survey table in the database.

    CLASS-LEVEL VARIABLES:ys basd of filter data
    None

    METHODS:
    public List<SimpleSurvey> GetSurveys(List<int> unitIds, List<string> genders, List<string> ages, DateTime dateOne, DateTime dateTwo) - returns a list of surveys based on filter data via the parameters 
    public List<SimpleSurvey> ViewContactRequests(List<int> unitIds, List<string> genders, List<string> ages, DateTime dateOne, DateTime dateTwo, List<bool> statuses) - returns contact requests based on filter data via the parameters 
    public SimpleSurvey GetSurveyDetails(int surveyId) - returns a surveys details based off the survey Id passed in via a parameter
    public void StoreSurveyInformation(SimpleSurvey tempSurvey) - stores a survey that has been suubmited in the MSS database
    public void ProcessContactRequest(int surveyId) - processes a contact request based on the surveyId passed in via the parameter
    */

    [DataObject]
    public class SurveyController
    {

        /* 
        CREATED:	H. Conant		MAR 4 2018

        GetSurveys()
        This method retrieves a list of Surveys based on filters pased in as parameters.

        PARAMETERS: 	
        List<int> unitIds - Unit IDs that the Surveys can belong to.
        List<string> genders - Genders that the Surveys can contain.
        List<string> ages - Ages that the Surveys can contain.
        DateTime dateOne - The smallest date in witch the Surveys could have been submitted on.
        DateTime dateTwo - The largest date in witch the Surveys could have been submitted on.

        RETURNS: 
        List<SimpleSurvey> - A list of Surveys that match the filters pased in as parameters.

        METHOD CALLS: 
        .Contains()
        SimpleSurvey()
        MSSContext()
        .ToList()
        DataObjectMethod()
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SurveyDTO> GetSurveys(List<int> unitIds, List<string> genders, List<string> ages, DateTime dateOne, DateTime dateTwo)
        {
            using (var context = new MSSContext())
            {
                var surveyList = (from aSurvey in context.surveys
                                 where unitIds.Contains(aSurvey.unitid) && genders.Contains(aSurvey.gender) && ages.Contains(aSurvey.age) && aSurvey.date >= dateOne && aSurvey.date <= dateTwo
                                 select new SurveyDTO()
                                 {
                                     surveyid = aSurvey.surveyid,
                                     date = aSurvey.date,
                                     age = aSurvey.age,
                                     gender = aSurvey.gender,
                                     firstname = aSurvey.firstname,
                                     lastname = aSurvey.lastname,
                                     bednaumber = aSurvey.bednumber,
                                     phonenumber = aSurvey.phonenumber,
                                     preferredcontact = aSurvey.preferredcontact,
                                     contactedyn = aSurvey.contactedyn,
                                     respondenttypeid = aSurvey.respondenttypeid,
                                     unitid = aSurvey.unitid

                                 }).ToList();

                return surveyList.ToList();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 4 2018

        ViewContactRequests()
        This method retrieves a list of Surveys/ContactRequests based on filters pased in as parameters.

        PARAMETERS: 	
        List<int> unitIds - Unit IDs that the Surveys/ContactRequests can belong to.
        List<string> genders - Genders that the Surveys/ContactRequests can contain.
        List<string> ages - Ages that the Surveys/ContactRequests can contain.
        DateTime dateOne - The smallest date in witch the Surveys/ContactRequests could have been submitted on.
        DateTime dateTwo - The largest date in witch the Surveys/ContactRequests could have been submitted on.
        List<bool> statuses - The statuses the Surveys/ContactRequests can have for contactedyn.

        RETURNS: 
        List<ContactRequestDTO> - A list of Surveys/ContactRequests that match the filters pased in as parameters.

        METHOD CALLS:
        .Contains()
        ContactRequestDTO()
        MSSContext()
        .ToList()
        DataObjectMethod()
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ContactRequestDTO> ViewContactRequests(List<int> unitIds, List<string> genders, List<string> ages, DateTime dateOne, DateTime dateTwo, List<bool> statuses)
        {
            using (var context = new MSSContext())
            {
                var contactRequests = (from aSurvey in context.surveys
                                       where unitIds.Contains(aSurvey.unitid) && genders.Contains(aSurvey.gender)
                                       && ages.Contains(aSurvey.age) && (aSurvey.date >= dateOne) && (aSurvey.date <= dateTwo) && statuses.Contains(aSurvey.contactedyn.Value)
                                       select new ContactRequestDTO()
                                       {
                                           surveyid = aSurvey.surveyid,
                                           date = aSurvey.date,
                                           age = aSurvey.age,
                                           gender = aSurvey.gender,
                                           firstname = aSurvey.firstname,
                                           lastname = aSurvey.lastname,
                                           bednaumber = aSurvey.bednumber,
                                           phonenumber = aSurvey.phonenumber,
                                           preferredcontact = aSurvey.preferredcontact,
                                           contactedyn = aSurvey.contactedyn,
                                           respondenttypeid = aSurvey.respondenttypeid,
                                           unitid = aSurvey.unitid,

                                           unitname = aSurvey.unit.unitname,
                                           caresitename = aSurvey.unit.caresite.caresitename

                                       }).ToList();

                return contactRequests.ToList();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 4 2018

        GetSurveyDetails()
        This method retrieves a single Survey by its Survey ID.

        PARAMETERS: 	
        int surveyId - The ID of the Survey being retrieved.

        RETURNS: 
        SimpleSurvey - The Survey that has been retrieved.

        METHOD CALLS:
        DataObjectMethod()
        MSSContext()
        SimpleSurvey()
        .Find()
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SurveyDTO GetSurveyDetails(int surveyId)
        {
            using (var context = new MSSContext())
            {
                var surveyDetails = context.surveys.Find(surveyId);
                SurveyDTO tempSurvey = new SurveyDTO();
                tempSurvey.surveyid = surveyDetails.surveyid;
                tempSurvey.date = surveyDetails.date;
                tempSurvey.age = surveyDetails.age;
                tempSurvey.gender = surveyDetails.gender;
                tempSurvey.firstname = surveyDetails.firstname;
                tempSurvey.lastname = surveyDetails.lastname;
                tempSurvey.bednaumber = surveyDetails.bednumber;
                tempSurvey.phonenumber = surveyDetails.phonenumber;
                tempSurvey.preferredcontact = surveyDetails.preferredcontact;
                tempSurvey.contactedyn = surveyDetails.contactedyn;
                tempSurvey.respondenttypeid = surveyDetails.respondenttypeid;
                tempSurvey.unitid = surveyDetails.unitid;

                return tempSurvey;
            }
        }

        /* 
        CREATED:	H. Conant		MAR 4 2018

        StoreSurveyInformation()
        This method adds a Survey to the MSS databse.

        PARAMETERS: 	
        SimpleSurvey tempSurvey - The Survey that is to be added to the database.

        RETURNS: 
        void - Nothing is returned.

        METHOD CALLS:
        DataObjectMethod()
        MSSContext()
        survey()
        .Add(newSurvey)
        .SaveChanges()
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void StoreSurveyInformation(SurveyDTO tempSurvey)
        {
            using (var context = new MSSContext())
            {
                survey newSurvey = new survey();
                newSurvey.date = tempSurvey.date;
                newSurvey.age = tempSurvey.age;
                newSurvey.gender = tempSurvey.gender;
                newSurvey.firstname = tempSurvey.firstname;
                newSurvey.lastname = tempSurvey.lastname;
                newSurvey.bednumber = tempSurvey.bednaumber;
                newSurvey.phonenumber = tempSurvey.phonenumber;
                newSurvey.preferredcontact = tempSurvey.preferredcontact;
                newSurvey.contactedyn = false;
                newSurvey.respondenttypeid = tempSurvey.respondenttypeid;
                newSurvey.unitid = tempSurvey.unitid;

                context.surveys.Add(newSurvey);
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 4 2018

        ProcessContactRequest()
        This method updates a Surveys/ContactRequests in the MSS databse as to mark the request as complete.

        PARAMETERS: 	
        int surveyId - The Surveys/ContactRequests ID that is to be updated in the database.

        RETURNS: 
        void - Nothing is returned.

        METHOD CALLS:
        DataObjectMethod()
        MSSContext()
        .Find()
        .Entry()
        .SaveChanges()
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void ProcessContactRequest(int surveyId)
        {
            using (var context = new MSSContext())
            {
                var aSurvey = context.surveys.Find(surveyId);
                aSurvey.contactedyn = true;

                var existingSurvey = context.Entry(aSurvey);
                existingSurvey.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
