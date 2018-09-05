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
    The SurveyController class' purpose is to use methods to insert, update, deactivate or retrieve data from the Survey table in the database.

    FIELDS:
    None

    METHODS:
    public List<ExtendedSurveyDTO> GetSurveys(List<int> unitIds, List<int> genders, List<int> ages, DateTime dateOne, DateTime dateTwo) - returns a list of surveys based on filter data via the parameters 
    public List<ExtendedSurveyDTO> GetSurveys(List<int> unitIds, List<int> genders, List<int> ages) - returns a list of surveys based on filter data via the parameters, an overloaded method with no date parameters
    public List<ContactRequestDTO> ViewContactRequests(List<int> unitIds, List<int> genders, List<int> ages, DateTime dateOne, DateTime dateTwo, List<bool> statuses) - returns contact requests based on filter data via the parameters
    public List<ContactRequestDTO> ViewContactRequests(List<int> unitIds, List<int> genders, List<int> ages, List<bool> statuses) - returns contact requests based on filter data via the parameters, an overloaded method with no date parameters
    public ExtendedSurveyDTO GetSurveyDetails(int surveyId) - returns a survey's details based off the survey ID passed in via a parameter
    public void StoreSurveyInformation(SurveyDTO tempSurvey) - stores a survey that has been submitted in the MSS database
    public void ProcessContactRequest(int surveyId) - processes a contact request based on the survey ID passed in via the parameter
    public int AddSurvey(SurveyDTO tempSurvey) - this method adds a survey to the MSS database
    */
    [DataObject]
    public class SurveyController
    {
        /* 
        CREATED:	H. Conant		MAR 4 2018
        MODIFIED:   H. Conant       MAR 17 2018
            - Updated return type
        MODIFIED:   A. Valberg      MAR 28 2018
            - Updated fields to reflect the 2 new tables (gender & agegroup)

        GetSurveys()
        This method retrieves a list of surveys based on filters passed in as parameters.

        PARAMETERS: 	
        List<int> unitIds - unit IDs that the surveys can belong to
        List<int> genders - genders that the surveys can contain
        List<int> ages - ages that the surveys can contain
        DateTime dateOne - the smallest date on which the surveys could have been submitted
        DateTime dateTwo - the largest date on which the surveys could have been submitted
        List<int> respodentIds - respondent IDs that the surveys can contain

        RETURNS: 
        List<ExtendedSurveyDTO> - a list of surveys that match the filters passed in as parameters

        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ExtendedSurveyDTO> GetSurveys(List<int> unitIds, List<int> genders, List<int> ages, DateTime dateOne, DateTime dateTwo, List<int> respodentIds)
        {
            using (var context = new MSSContext())
            {
                var surveyList = (from aSurvey in context.surveys
                                  where unitIds.Contains(aSurvey.unitid) && genders.Contains(aSurvey.genderid) && respodentIds.Contains(aSurvey.respondenttypeid) && ages.Contains(aSurvey.agegroupid) && aSurvey.date >= dateOne && aSurvey.date <= dateTwo
                                  select new ExtendedSurveyDTO()
                                  {
                                      surveyid = aSurvey.surveyid,
                                      date = aSurvey.date,
                                      agegroupid = aSurvey.agegroupid,
                                      genderid = aSurvey.genderid,
                                      firstname = aSurvey.firstname,
                                      bednaumber = aSurvey.bednumber,
                                      phonenumber = aSurvey.phonenumber,
                                      preferredcontact = aSurvey.preferredcontact,
                                      contactedyn = aSurvey.contactedyn,
                                      respondenttypeid = aSurvey.respondenttypeid,
                                      unitid = aSurvey.unitid,
                                      unitname = aSurvey.unit.unitname,
                                      caresitename = aSurvey.unit.caresite.caresitename,
                                      gendername = aSurvey.gender.gendername,
                                      agegroupname = aSurvey.agegroup.agegroupname

                                 }).ToList();

                return surveyList.ToList();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 20 2018
        MODIFIED:   A. Valberg      MAR 28 2018
            - Updated fields to reflect the 2 new tables (gender & agegroup)

        GetSurveys()
        This method retrieves a list of surveys based on filters passed in as parameters, an overloaded method with no date parameters.

        PARAMETERS: 	
        List<int> unitIds - unit IDs that the surveys can belong to
        List<int> genders - genders that the surveys can contain
        List<int> ages - ages that the surveys can contain
        List<int> respodentIds - respondent IDs that the surveys can contain

        RETURNS: 
        List<ExtendedSurveyDTO> - a list of surveys that match the filters passed in as parameters

        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ExtendedSurveyDTO> GetSurveys(List<int> unitIds, List<int> genders, List<int> ages, List<int> respodentIds)
        {
            using (var context = new MSSContext())
            {
                var surveyList = (from aSurvey in context.surveys
                                  where unitIds.Contains(aSurvey.unitid) && genders.Contains(aSurvey.genderid) && ages.Contains(aSurvey.agegroupid) && respodentIds.Contains(aSurvey.respondenttypeid)
                                  select new ExtendedSurveyDTO()
                                  {
                                      surveyid = aSurvey.surveyid,
                                      date = aSurvey.date,
                                      agegroupid = aSurvey.agegroupid,
                                      genderid = aSurvey.genderid,
                                      firstname = aSurvey.firstname,
                                      bednaumber = aSurvey.bednumber,
                                      phonenumber = aSurvey.phonenumber,
                                      preferredcontact = aSurvey.preferredcontact,
                                      contactedyn = aSurvey.contactedyn,
                                      respondenttypeid = aSurvey.respondenttypeid,
                                      unitid = aSurvey.unitid,
                                      unitname = aSurvey.unit.unitname,
                                      caresitename = aSurvey.unit.caresite.caresitename,
                                      gendername = aSurvey.gender.gendername,
                                      agegroupname = aSurvey.agegroup.agegroupname
                                  }).ToList();

                return surveyList.ToList();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 4 2018
        MODIFIED:   A. Valberg      MAR 28 2018
            - Updated fields to reflect the 2 new tables (gender & agegroup)

        ViewContactRequests()
        This method retrieves a list of surveys/contact requests based on filters passed in as parameters.

        PARAMETERS: 	
        List<int> unitIds - unit IDs that the surveys/contact requests can belong to
        List<int> genders - genders that the surveys/contact requests can contain
        List<int> ages - ages that the surveys/contact requests can contain
        DateTime dateOne - the smallest date on which the surveys/contact requests could have been submitted
        DateTime dateTwo - the largest date on which the surveys/contact requests could have been submitted
        List<bool> statuses - the statuses the surveys/contact requests can have for contactedyn
        List<int> respodentIds - respondent IDs that the surveys/contact requests can contain

        RETURNS: 
        List<ContactRequestDTO> - a list of surveys/contact requests that match the filters passed in as parameters

        ODEV METHOD CALLS:
        Name
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ContactRequestDTO> ViewContactRequests(List<int> unitIds, List<int> genders, List<int> ages, DateTime dateOne, DateTime dateTwo, List<bool> statuses, List<int> respodentIds)
        {
            using (var context = new MSSContext())
            {
                var contactRequests = (from aSurvey in context.surveys
                                       where unitIds.Contains(aSurvey.unitid) && genders.Contains(aSurvey.genderid)
                                       && ages.Contains(aSurvey.agegroupid) && (aSurvey.date >= dateOne) && (aSurvey.date <= dateTwo) && statuses.Contains(aSurvey.contactedyn.Value) && respodentIds.Contains(aSurvey.respondenttypeid)
                                       select new ContactRequestDTO()
                                       {
                                           surveyid = aSurvey.surveyid,
                                           date = aSurvey.date,
                                           agegroupid = aSurvey.agegroupid,
                                           genderid = aSurvey.genderid,
                                           firstname = aSurvey.firstname,
                                           bednaumber = aSurvey.bednumber,
                                           phonenumber = aSurvey.phonenumber,
                                           preferredcontact = aSurvey.preferredcontact,
                                           contactedyn = aSurvey.contactedyn,
                                           respondenttypeid = aSurvey.respondenttypeid,
                                           unitid = aSurvey.unitid,
                                           gendername = aSurvey.gender.gendername,
                                           agegroupname = aSurvey.agegroup.agegroupname,
                                           unitname = aSurvey.unit.unitname,
                                           caresitename = aSurvey.unit.caresite.caresitename
                                       }).ToList();
                return contactRequests.ToList();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 20 2018
        MODIFIED:   A. Valberg      MAR 28 2018
            - Updated fields to reflect the 2 new tables (gender & agegroup)

        ViewContactRequests()
        This method retrieves a list of surveys/contact requests based on filters passed in as parameters, an overloaded method with no date parameters.

        PARAMETERS: 	
        List<int> unitIds - unit IDs that the Ssurveys/contact requests can belong to
        List<int> genders - genders that the surveys/contact requests can contain
        List<int> ages - ages that the surveys/contact requests can contain
        List<bool> statuses - the statuses the surveys/contact requests can have for contacted status
        List<int> respodentIds - respondent IDs that the surveys/contact requests can contain

        RETURNS: 
        List<ContactRequestDTO> - A list of surveys/contact requests that match the filters passed in as parameters

        ODEV METHOD CALLS:
        Name
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ContactRequestDTO> ViewContactRequests(List<int> unitIds, List<int> genders, List<int> ages, List<bool> statuses, List<int> respodentIds)
        {
            using (var context = new MSSContext())
            {
                var contactRequests = (from aSurvey in context.surveys
                                       where unitIds.Contains(aSurvey.unitid) && genders.Contains(aSurvey.genderid)
                                       && ages.Contains(aSurvey.agegroupid) && statuses.Contains(aSurvey.contactedyn.Value) && respodentIds.Contains(aSurvey.respondenttypeid)
                                       select new ContactRequestDTO()
                                       {
                                           surveyid = aSurvey.surveyid,
                                           date = aSurvey.date,
                                           agegroupid = aSurvey.agegroupid,
                                           genderid = aSurvey.genderid,
                                           firstname = aSurvey.firstname,
                                           bednaumber = aSurvey.bednumber,
                                           phonenumber = aSurvey.phonenumber,
                                           preferredcontact = aSurvey.preferredcontact,
                                           contactedyn = aSurvey.contactedyn,
                                           respondenttypeid = aSurvey.respondenttypeid,
                                           unitid = aSurvey.unitid,
                                           gendername = aSurvey.gender.gendername,
                                           agegroupname = aSurvey.agegroup.agegroupname,
                                           unitname = aSurvey.unit.unitname,
                                           caresitename = aSurvey.unit.caresite.caresitename
                                       }).ToList();
                return contactRequests.ToList();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 4 2018
        MODIFIED:   H. Conant       MAR 17 2018
            - Added unitname, caresitename, respondenttypename
        MODIFIED:   A. Valberg      MAR 28 2018
            - Updated fields to reflect the 2 new tables (gender & agegroup)

        GetSurveyDetails()
        This method retrieves a single survey by its survey ID.

        PARAMETERS: 	
        int surveyId - the ID of the survey being retrieved

        RETURNS: 
        ExtendedSurveyDTO - the survey that has been retrieved

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ExtendedSurveyDTO GetSurveyDetails(int surveyId)
        {
            using (var context = new MSSContext())
            {
                var surveyDetails = context.surveys.Find(surveyId);
                
                ExtendedSurveyDTO tempSurvey = new ExtendedSurveyDTO();
                if (surveyDetails == null)
                {
                    tempSurvey.surveyid = -1;
                } else
                {
                    tempSurvey.surveyid = surveyDetails.surveyid;
                    tempSurvey.date = surveyDetails.date;
                    tempSurvey.agegroupid = surveyDetails.agegroupid;
                    tempSurvey.genderid = surveyDetails.genderid;
                    tempSurvey.firstname = surveyDetails.firstname;
                    tempSurvey.bednaumber = surveyDetails.bednumber;
                    tempSurvey.phonenumber = surveyDetails.phonenumber;
                    tempSurvey.preferredcontact = surveyDetails.preferredcontact;
                    tempSurvey.contactedyn = surveyDetails.contactedyn;
                    tempSurvey.respondenttypeid = surveyDetails.respondenttypeid;
                    tempSurvey.unitid = surveyDetails.unitid;
                    tempSurvey.gendername = surveyDetails.gender.gendername;
                    tempSurvey.agegroupname = surveyDetails.agegroup.agegroupname;
                    tempSurvey.unitname = surveyDetails.unit.unitname;
                    tempSurvey.caresitename = surveyDetails.unit.caresite.caresitename;
                    tempSurvey.respondenttypename = surveyDetails.respondenttype.respondenttypename;

                }   
                return tempSurvey;
            }
        }

        /* 
        CREATED:	H. Conant		MAR 4 2018
        MODIFIED:   A. Valberg      MAR 28 2018
            - Updated fields to reflect the 2 new tables (gender & agegroup)

        StoreSurveyInformation()
        This method adds a survey to the MSS database.

        PARAMETERS: 	
        SurveyDTO tempSurvey - the survey that is to be added to the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void StoreSurveyInformation(SurveyDTO tempSurvey)
        {
            using (var context = new MSSContext())
            {
                survey newSurvey = new survey();
                newSurvey.date = tempSurvey.date;
                newSurvey.agegroupid = tempSurvey.agegroupid;
                newSurvey.genderid = tempSurvey.genderid;
                newSurvey.firstname = tempSurvey.firstname;
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
        This method updates a survey/contact request in the MSS database as to mark the request as complete.

        PARAMETERS: 	
        int surveyId - the survey/contact request ID that is to be updated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
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

        /* 
        CREATED:	H. L'Heureux		MAR 20 2018
        MODIFIED:   A. Valberg          MAR 28 2018
            - Updated fields to reflect the 2 new tables (gender & agegroup)
        MODIFIED:   H. Conant           MAR 29 2018
            - Updated contactedyn field

        AddSurvey()
        This method adds a survey to the MSS database.

        PARAMETERS: 	
        SurveyDTO tempSurvey - the survey that is to be added to the database

        RETURNS: 
        int - survey ID that is returned

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int AddSurvey(SurveyDTO tempSurvey)
        {

            using (var context = new MSSContext())
            {
                survey newSurveyWithId;
                survey newSurvey = new survey();

                newSurvey.date = tempSurvey.date;
                newSurvey.agegroupid = tempSurvey.agegroupid;
                newSurvey.genderid = tempSurvey.genderid;
                newSurvey.firstname = tempSurvey.firstname;
                newSurvey.bednumber = tempSurvey.bednaumber;
                newSurvey.phonenumber = tempSurvey.phonenumber;
                newSurvey.preferredcontact = tempSurvey.preferredcontact;
                newSurvey.contactedyn = tempSurvey.contactedyn;
                newSurvey.respondenttypeid = tempSurvey.respondenttypeid;
                newSurvey.unitid = tempSurvey.unitid;
                newSurveyWithId = context.surveys.Add(newSurvey);
                
                context.SaveChanges();
                return newSurveyWithId.surveyid;
            }
        }

    }
}
