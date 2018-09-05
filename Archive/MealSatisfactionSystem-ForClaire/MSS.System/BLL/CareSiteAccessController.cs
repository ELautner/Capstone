using MSS.Data;
using MSS.Data.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSS.System.DAL;
using MSS.Data.Entities;

namespace MSS.System.BLL
{
    /* 
    CREATED:	C. Stanhope		MAR 10 2018

    CareSiteAccessController
    This class gives methods access the CareSiteAccess table in order to read, update, add and deactivate care site access.

    CLASS-LEVEL VARIABLES:
    None

    METHODS:
    public void AccessSurvey() - TODO
    public void AddCareSiteAccess(int careSiteId, int accessCodeId, DateTime currentDate) - inserts a new instance of 'caresiteaccess' into the MSS database
    public List<AccessCodeDTO> GetActiveAccessCodes() - TODO
    public AccessCodeDTO GetCareSiteAccessCodeByDate(int careSiteID, DateTime date) - retrieves the care site access code that fits the passed date and care site ID parameters
    public List<int> GetAssignedCareSites(DateTime date) - retrieves a list of care site ids that have not been assigned a access code on a particular date
    public List<int> GetAssignedAccessCodes(DateTime date, int careSiteId) -  retrieves a list of access code ids that have been assigned to a particular care site or on a particular day
    */
    [DataObject]
    public class CareSiteAccessController
    {
        //TODO: HYDN -> Copy code from my computer and add it back in
        public void AccessSurvey()
        {
            using (var context = new MSSContext())
            {
                
            }
        }

        /* 
        CREATED: 	C. Stanhope		MAR 10 2018
        MODIFIED: 	H. Conant		MAR 11 2018
            - Added functionnality/content to method

        AddCareSiteAccess()
        This method inserts a new instance of 'caresiteaccess' into the MSS datbase.

        PARAMETERS: 	
        int careSiteId - the care site id of the 'caresiteaccess' to be added to the database
        int accessCodeId - the access code id of the 'caresiteaccess' to be added to the database
        DateTime date - the date used of the 'caresiteaccess' to be added to the database

        RETURNS: 
        void - Nothing is returned.

        METHOD CALLS:
        DataObjectMethod()
        MSSContext() 
        caresiteaccess()
        .Add()
        .SaveChanges()
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddCareSiteAccess(int careSiteId, int accessCodeId, DateTime date)
        {
            using (var context = new MSSContext())
            {
                caresiteaccess newCareSiteAccess = new caresiteaccess();
                newCareSiteAccess.caresiteid = careSiteId;
                newCareSiteAccess.caresite = context.caresites.Find(careSiteId);
                newCareSiteAccess.accesscodeid = accessCodeId;
                newCareSiteAccess.accesscode = context.accesscodes.Find(accessCodeId);
                newCareSiteAccess.dateused = date;
                context.caresiteaccesses.Add(newCareSiteAccess);
                context.SaveChanges();
            }
        }

        #region "Get" Methods
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AccessCodeDTO> GetActiveAccessCodes()
        {
            using (var context = new MSSContext())
            {
                List<AccessCodeDTO> activeCodeList = new List<AccessCodeDTO>();

                // TODO: code this

                return activeCodeList;
            }
        }

        /* 
        CREATED: 	C. Stanhope		Mar 10 2018
        MODIFIED: 	C. Stanhope     Mar 11 2018
            - fixed date comparison
            - added error handling
        MODIFIED: 	C. Stanhope     Mar 13 2018
            - removed the "dateused" from returned AccessCodeDTO because no longer needed

        GetCareSiteAccessCodeByDate()
        Retrieves the care site access code that matches the passed care site ID and date. Returns an AccessCodeDTO with an accesscodeid of -3 if no access code matching the passed parameters was found.

        PARAMETERS: 	
        int careSiteID - the ID of the desired care site
	    DateTime date - the desired date used of the access code
        
        RETURNS: 
        AccessCodeDTO - the access code that matches the care site and date (accesscodeid = -3 if no matching access code is found)
        
        METHOD CALLS: 
        Count()
        First()
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AccessCodeDTO GetCareSiteAccessCodeByDate(int careSiteID, DateTime date)
        {
            using (var context = new MSSContext())
            {
                // Note: unfortunately, because of Linq, the only way to compare dates without times is to compare each piece of the date (year, month, day) individually. Linq does not allow the .Date function in its queries and therefore cannot be used. 
                var accessCode = from aCareSiteAccess in context.caresiteaccesses
                                 where aCareSiteAccess.caresiteid == careSiteID && aCareSiteAccess.dateused.Year == date.Year && aCareSiteAccess.dateused.Month == date.Month && aCareSiteAccess.dateused.Day == date.Day
                                 select aCareSiteAccess;

                // using .First() here because this search should only return ONE CareSiteAccess item
                AccessCodeDTO selectedCode = new AccessCodeDTO();
                if(accessCode.Count() == 0)
                {
                    selectedCode.accesscodeid = -3;
                }
                else
                {
                    selectedCode.accesscodeid = accessCode.First().accesscodeid;
                    selectedCode.accesscodeword = accessCode.First().accesscode.accesscodeword;
                    selectedCode.activeyn = accessCode.First().accesscode.activeyn;
                }
                
                return selectedCode;
            }
        }

        /* 
        CREATED:    H. Conant       MAR 11 2018

        GetCareSites()
        Retrieves a list of care site ids that have not been assigned a access code on a particular date.
        PARAMETERS: 	
            DateTime date - date when an access code is to be used
        RETURNS: 
            List<int> - list of care site id's
        METHOD CALLS: 
            DataObjectMethod()
            MSSContext()
            .ToList()
            .Contains()
            .Add()
            .int()
            List<int>()
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<int> GetAssignedCareSites(DateTime date)
        {
            using (var context = new MSSContext())
            {
                var careSiteAccesses = (from aCareSiteAccess in context.caresiteaccesses
                                        where aCareSiteAccess.dateused == date
                                        select aCareSiteAccess).ToList();
                List<int> careSiteIds = new List<int>();
                foreach (caresiteaccess careSiteAccess in careSiteAccesses)
                {
                    int temp = new int();
                    temp = careSiteAccess.caresiteid;
                    careSiteIds.Add(temp);
                }
                return careSiteIds;
            }
        }

        /* 
        CREATED:    H. Conant       MAR 11 2018

        GetAccessCodes()
        Retrieves a list of access code ids that have been assigned to a particular care site or on a particular day.
        PARAMETERS: 	
            DateTime date - date when an access code is to used
            int careSiteId - an id of a particular caresite
        RETURNS: 
            List<int> - list of access code id's
        METHOD CALLS: 
            DataObjectMethod()
            MSSContext()
            .ToList()
            .Contains()
            .Add()
            .int()
            List<int>()
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<int> GetAssignedAccessCodes(DateTime date, int careSiteId)
        {
            using (var context = new MSSContext())
            {
                var careSiteAccesses = (from aCareSiteAccess in context.caresiteaccesses
                                        where aCareSiteAccess.dateused == date || aCareSiteAccess.caresiteid == careSiteId
                                        select aCareSiteAccess).ToList();
                List<int> accessCodeIds= new List<int>();
                foreach (caresiteaccess careSiteAccess in careSiteAccesses)
                {
                    int temp = new int();
                    temp = careSiteAccess.accesscodeid;
                    accessCodeIds.Add(temp);
                }
                return accessCodeIds;
            }
        }

        /* 
        CREATED:     H.L'Heureux, H. Conant        MAR 13 2018
        
        GetCareSiteID()
        This method will accept a word entered in and make sure that it is the active code for the day at that care site, then returns the care site id that it is for.

        PARAMETERS:     
        string accessCode - the days access code word

        RETURNS: 
        int - the care site ID

        METHOD CALLS: 
        None
        */
        public int GetCareSiteID(string accessCode)
        {

            using (var context = new MSSContext())
            {
                int careSiteID;

                var tempCareSite = (from caresite in context.caresiteaccesses
                                    where caresite.accesscode.accesscodeword == accessCode && caresite.dateused == DateTime.Today
                                    select caresite).Single();

                careSiteID = tempCareSite.caresiteid;

                return careSiteID;
            }

        }
        #endregion
    }
}
