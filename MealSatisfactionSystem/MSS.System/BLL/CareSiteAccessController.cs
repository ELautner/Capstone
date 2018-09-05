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
    This class gives methods access the CareSiteAccess table in order to read or add caresiteaccess entries.

    FIELDS:
    None

    METHODS:
    public void AddCareSiteAccess(int careSiteId, int accessCodeId, DateTime date) - inserts a new instance of 'caresiteaccess' into the MSS database
    public AccessCodeDTO GetCareSiteAccessCodeByDate(int careSiteID, DateTime date) - retrieves the care site access code that fits the passed date and care site ID parameters
    public List<int> GetAssignedCareSites(DateTime date) - retrieves a list of care site IDs that have not been assigned an access code on a particular date
    public List<int> GetAssignedAccessCodes(DateTime date, int careSiteId) - retrieves a list of access code IDs that have been assigned to a particular care site or on a particular day
    public int GetCareSiteID(string accessCode) - retrieves the care site ID that matches the passed access code and today's date
    */
    [DataObject]
    public class CareSiteAccessController
    {
        /* 
        CREATED: 	C. Stanhope		MAR 10 2018
        MODIFIED: 	H. Conant		MAR 11 2018
            - Added functionality/ content to method

        AddCareSiteAccess()
        This method inserts a new instance of 'caresiteaccess' into the MSS database.

        PARAMETERS: 	
        int careSiteId - the care site ID of the 'caresiteaccess' to be added to the database
        int accessCodeId - the access code ID of the 'caresiteaccess' to be added to the database
        DateTime date - the date used of the 'caresiteaccess' to be added to the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
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
        
        ODEV METHOD CALLS: 
        None
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

        GetAssignedCareSites()
        Retrieves a list of care site IDs that have not been assigned an access code on a particular date.

        PARAMETERS: 	
        DateTime date - date when an access code is to be used

        RETURNS: 
        List<int> - list of care site IDs

        ODEV METHOD CALLS: 
        None
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

        GetAssignedAccessCodes()
        Retrieves a list of access code IDs that have been assigned to a particular care site or on a particular day.

        PARAMETERS: 	
        DateTime date - date when an access code is to be used
        int careSiteId - an ID of a particular caresite

        RETURNS: 
        List<int> - list of access code IDs

        ODEV METHOD CALLS: 
        None
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
        CREATED:     H. L'Heureux, H. Conant        MAR 13 2018
        
        GetCareSiteID()
        Will accept a word entered in and make sure that it is the active code for the day at that care site, then returns the care site ID that it is for.

        PARAMETERS:     
        string accessCode - the days access code word

        RETURNS: 
        int - the care site ID

        ODEV METHOD CALLS: 
        None
        */
        public int GetCareSiteID(string accessCode)
        {

            using (var context = new MSSContext())
            {
                int careSiteID;

                var tempCareSite = (from caresite in context.caresiteaccesses                                                        //If a caresite becomes inactive, people will not be able to take the survey
                                    where caresite.accesscode.accesscodeword == accessCode && caresite.dateused == DateTime.Today && caresite.caresite.activeyn == true
                                    select caresite).Single();

                careSiteID = tempCareSite.caresiteid;

                return careSiteID;
            }

        }
        #endregion
    }
}
