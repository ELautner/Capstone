using MSS.Data.DTOs;
using MSS.Data.Entities;
using MSS.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace MSS.System.BLL
{
    /* 
    CREATED:    A. Valberg      MAR 3 2018

    AccessCodeController
    This class provides methods to access the AccessCode table of the DB in order to read, update, insert, or deactivate access codes.

    FIELDS:
    None

    METHODS:
    public void AddAccessCode(AccessCodeDTO tempCode) - adds the passed access code to the database
    public void UpdateAccessCode(AccessCodeDTO tempCode) - updates the passed access code in the database
    public List<AccessCodeDTO> GetActiveAccessCodes() - returns list of active access codes
    public List<int> GetAccessCodes(List<int> alreadyAssignedCodes, DateTime assignDate, int careSiteId) - returns a list of access code IDs that have not been assigned on a particular date or care site
    public List<AccessCodeDTO> GetAccessCodesByStatus(bool? activeStatus) - returns a list of access codes depending on their active status
    public List<AccessCodeDTO> GetAccessCodesByKeyword(string keyword, bool? activeStatus) - returns a list of access codes depending on their active status and if the code word contains the passed keyword
    public List<AccessCodeDTO> GetAccessCodesByStartingLetter(string startsWith, bool? activeStatus) - returns a list of access codes depending on their active status and if the code word starts with the passed keyword
    public List<AccessCodeDTO> GetAccessCodesByExactMatch(string matchString, bool? activeStatus) - returns a list of access codes depending on their active status and if the code word exactly matches the passed keyword
    public List<char> GetAccessCodeStartingLetters() - returns a list of every letter of the alphabet that has an access code starting with that letter
    */
    [DataObject]
    public class AccessCodeController
    {

        #region Add, Update Methods
        /* 
        CREATED: 	A. Valberg		MAR 3 2018
        MODIFIED: 	C. Stanhope     MAR 13 2018
            - access codes are now automatically made lowercase

        AddAccessCode()
        Takes a new access code from a user and puts it in the database.

        PARAMETERS: 	
        AccessCodeDTO tempCode - the access code to be added

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddAccessCode(AccessCodeDTO tempCode)
        {
            using (var context = new MSSContext())
            {
                accesscode newAccessCode = new accesscode();
                newAccessCode.accesscodeword = tempCode.accesscodeword.ToLower();
                newAccessCode.activeyn = true;
                context.accesscodes.Add(newAccessCode);
                context.SaveChanges();
            }
        }

        /* 
        CREATED:    A. Valberg      MARCH 3 2018
        MODIFIED:   C. Stanhope     MARCH 19 2018
            - now updates affect the word and the active status

        UpdateAccessCode()
        Updates an existing access code.

        PARAMETERS: 	
        AccessCodeDTO tempCode - the access code to be updated

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
       */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateAccessCode(AccessCodeDTO tempCode)
        {
            using (var context = new MSSContext())
            {
                var accessCode = context.accesscodes.Find(tempCode.accesscodeid);
                accessCode.accesscodeword = tempCode.accesscodeword;
                accessCode.activeyn = tempCode.activeyn;
                var existingCode = context.Entry(accessCode);
                existingCode.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        #endregion

        #region "Get" Methods
        /* 
        CREATED: 	A. Valberg		MAR 3 2018
        MODIFIED:   C. Stanhope     MAR 17 2018
            - renamed to show it only gets ACTIVE access codes
            - access codes now return in ascending order alphabetically

        GetActiveAccessCodes()
        Retrieves a list of all currently active access codes.

        PARAMETERS: 	
        None

        RETURNS: 
        List<AccessCodeDTO> - the list of active access codes

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<AccessCodeDTO> GetActiveAccessCodes()
        {
            using (var context = new MSSContext())
            {
                var accessCodes = from item in context.accesscodes
                                  where item.activeyn == true
                                  orderby item.accesscodeword ascending
                                  select item;
                List<AccessCodeDTO> accessCodeDTOs = new List<AccessCodeDTO>();
                foreach (accesscode code in accessCodes)
                {
                    AccessCodeDTO temp = new AccessCodeDTO();
                    temp.accesscodeid = code.accesscodeid;
                    temp.accesscodeword = code.accesscodeword;
                    temp.activeyn = true;
                    accessCodeDTOs.Add(temp);
                }
                return accessCodeDTOs;
            }
        }

        /* 
        CREATED:    H. Conant       MAR 11 2018

        GetAccessCodes()
        Retrieves a list of access code IDs that have not been assigned on a particular date or to a particular caresite.

        PARAMETERS: 	
        List<int> alreadyAssignedCodes - list of access code IDs that have been on a particular date or to a particular caresite
        DateTime assignDate - date that the access codes are being assigned on
        int careSiteId - the caresite that is getting assigned an access code

        RETURNS: 
        List<int> - list of access code IDs

        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<int> GetAccessCodes(List<int> alreadyAssignedCodes, DateTime assignDate, int careSiteId)
        {
            using (var context = new MSSContext())
            {
                List<string> dateWords = new List<string>();
                dateWords = (from aCareSiteCode in context.caresiteaccesses
                         where aCareSiteCode.dateused == assignDate
                         select aCareSiteCode.accesscode.accesscodeword).ToList();

                DateTime yesterDay = assignDate.AddDays(-1);

                List<string> yesterWords = new List<string>();
                yesterWords = (from aCareSiteCode in context.caresiteaccesses
                             where aCareSiteCode.dateused == yesterDay && aCareSiteCode.caresiteid == careSiteId
                             select aCareSiteCode.accesscode.accesscodeword).ToList();

                var accessCodes = (from aAccessCode in context.accesscodes
                                   where !alreadyAssignedCodes.Contains(aAccessCode.accesscodeid) && (!dateWords.Contains(aAccessCode.accesscodeword)) && (!yesterWords.Contains(aAccessCode.accesscodeword)) && aAccessCode.activeyn == true
                                   select aAccessCode).ToList();

                List<int> accessCodeIds = new List<int>();

                foreach (accesscode code in accessCodes)
                {
                    int temp = new int();
                    temp = code.accesscodeid;
                    accessCodeIds.Add(temp);
                }

                return accessCodeIds;
            }
        }

        /* 
        CREATED:   C. Stanhope     MAR 17 2018

        GetAccessCodesByStatus()
        Retrieves a list of access codes based on status. If activeStatus has no value, all access codes are returned regardless of active status. If activeStatus is "true", only active access codes are returned. If activeStatus is "false", only inactive access codes are returned.

        PARAMETERS: 	
        bool? activeStatus - the desired status of the returned access codes

        RETURNS: 
        List<AccessCodeDTO> - the list of access codes matching the parameters given

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AccessCodeDTO> GetAccessCodesByStatus(bool? activeStatus)
        {
            using (var context = new MSSContext())
            {
                IQueryable<accesscode> accessCodes = null;

                if (!activeStatus.HasValue) // return all regardless of status
                {
                    accessCodes = from item in context.accesscodes
                                  orderby item.accesscodeword ascending
                                  select item;
                }
                else // filter by status
                {
                    accessCodes = from item in context.accesscodes
                                  where item.activeyn == activeStatus
                                  orderby item.accesscodeword ascending
                                  select item;
                }

                List<AccessCodeDTO> accessCodeDTOs = new List<AccessCodeDTO>();
                foreach (accesscode code in accessCodes)
                {
                    AccessCodeDTO temp = new AccessCodeDTO();
                    temp.accesscodeid = code.accesscodeid;
                    temp.accesscodeword = code.accesscodeword;
                    temp.activeyn = code.activeyn;
                    accessCodeDTOs.Add(temp);
                }
                return accessCodeDTOs;
            }
        }

        /* 
        CREATED: 	C. Stanhope		MAR 10 2018
        MODIFIED: 	C. Stanhope     MAR 13 2018
            - now returns access code words in ascending order alphabetically
            - starting letter search added
        MODIFIED:   C. Stanhope     MAR 17 2018
            - starting letter search removed; put into its own method
            - added status filtering

        GetAccessCodesByKeyword()
        Retrieves a list of access codes that contain the keyword passed as a parameter. Access codes are filtered by active status using the activeStatus parameter. If activeStatus has no value, all access codes are returned regardless of active status. If activeStatus is "true", only active access codes are returned. If activeStatus is "false", only inactive access codes are returned.

        PARAMETERS: 	
        string keyword - the keyword used for searching
        bool? activeStatus - the desired status of the returned access codes
        
        RETURNS: 
        List<AccessCodeDTO> - a list of matching AccessCodeDTOs
        
        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AccessCodeDTO> GetAccessCodesByKeyword(string keyword, bool? activeStatus)
        {
            using (var context = new MSSContext())
            {
                IQueryable<accesscode> accessCodes = null;

                if (!activeStatus.HasValue) // return all regardless of status
                {
                    accessCodes = from item in context.accesscodes
                                  where item.accesscodeword.Contains(keyword.ToLower())
                                  orderby item.accesscodeword ascending
                                  select item;
                }
                else // filter by status
                {
                    accessCodes = from item in context.accesscodes
                                  where item.activeyn == activeStatus && item.accesscodeword.Contains(keyword.ToLower())
                                  orderby item.accesscodeword ascending
                                  select item;
                }

                List<AccessCodeDTO> accessCodeDTOs = new List<AccessCodeDTO>();
                foreach (accesscode code in accessCodes)
                {
                    AccessCodeDTO temp = new AccessCodeDTO();
                    temp.accesscodeid = code.accesscodeid;
                    temp.accesscodeword = code.accesscodeword;
                    temp.activeyn = code.activeyn;
                    accessCodeDTOs.Add(temp);
                }
                return accessCodeDTOs;
            }
        }

        /* 
        CREATED:    C. Stanhope     MAR 17 2018

        GetAccessCodesByStartingLetter()
        Retrieves a list of access codes that start with the keyword passed. Access codes are filtered by active status using the activeStatus parameter. If activeStatus has no value, all access codes are returned regardless of active status. If activeStatus is "true", only active access codes are returned. If activeStatus is "false", only inactive access codes are returned.

        PARAMETERS: 	
        string startsWith - the keyword used for searching
        bool? activeStatus - the desired status of the returned access codes
        
        RETURNS: 
        List<AccessCodeDTO> - a list of matching AccessCodeDTOs
        
        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AccessCodeDTO> GetAccessCodesByStartingLetter(string startsWith, bool? activeStatus)
        {
            using (var context = new MSSContext())
            {
                IQueryable<accesscode> accessCodes = null;
                if (!activeStatus.HasValue) // return all regardless of status
                {
                    accessCodes = from item in context.accesscodes
                                  where item.accesscodeword.StartsWith(startsWith)
                                  orderby item.accesscodeword ascending
                                  select item;
                }
                else // filter by status
                {
                    accessCodes = from item in context.accesscodes
                                  where item.activeyn == activeStatus && item.accesscodeword.StartsWith(startsWith)
                                  orderby item.accesscodeword ascending
                                  select item;
                }

                List<AccessCodeDTO> accessCodeDTOs = new List<AccessCodeDTO>();
                foreach (accesscode code in accessCodes)
                {
                    AccessCodeDTO temp = new AccessCodeDTO();
                    temp.accesscodeid = code.accesscodeid;
                    temp.accesscodeword = code.accesscodeword;
                    temp.activeyn = code.activeyn;
                    accessCodeDTOs.Add(temp);
                }
                return accessCodeDTOs;
            }
        }

        /* 
        CREATED:    C. Stanhope     MAR 19 2018

        GetAccessCodesByExactMatch()
        Retrieves a list of access codes that exactly match the keyword passed. Access codes are filtered by active status using the activeStatus parameter. If activeStatus has no value, all access codes are returned regardless of active status. If activeStatus is "true", only active access codes are returned. If activeStatus is "false", only inactive access codes are returned.

        PARAMETERS: 	
        string matchString - the keyword used for searching
        bool? activeStatus - the desired status of the returned access codes

        RETURNS: 
        List<AccessCodeDTO> - a list of matching AccessCodeDTOs

        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AccessCodeDTO> GetAccessCodesByExactMatch(string matchString, bool? activeStatus)
        {
            using (var context = new MSSContext())
            {
                IQueryable<accesscode> accessCodes = null;
                if (!activeStatus.HasValue) // return all regardless of status
                {
                    accessCodes = from item in context.accesscodes
                                  where item.accesscodeword.Equals(matchString)
                                  orderby item.accesscodeword ascending
                                  select item;
                }
                else // filter by status
                {
                    accessCodes = from item in context.accesscodes
                                  where item.activeyn == activeStatus && item.accesscodeword.Equals(matchString)
                                  orderby item.accesscodeword ascending
                                  select item;
                }

                List<AccessCodeDTO> accessCodeDTOs = new List<AccessCodeDTO>();
                foreach (accesscode code in accessCodes)
                {
                    AccessCodeDTO temp = new AccessCodeDTO();
                    temp.accesscodeid = code.accesscodeid;
                    temp.accesscodeword = code.accesscodeword;
                    temp.activeyn = code.activeyn;
                    accessCodeDTOs.Add(temp);
                }
                return accessCodeDTOs;
            }
        }


        /* 
        CREATED: 	C. Stanhope		MAR 13 2018
        MODIFIED:   C. Stanhope     MAR 17 2018
            - Changed to reflect GetAccessCodes being renamed to GetActiveAccessCodes

        GetAccessCodeStartingLetters()
        Retrieves a list of every letter of the alphabet that has an access code starting with that letter. Used to populate the starting letter link buttons on the manage access codes page. 

        PARAMETERS: 	
        None
        
        RETURNS: 
        List<char> - a list of the starting letters of all access codes
        
        ODEV METHOD CALLS: 
        AccessCodeController.GetAccessCodes()
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<char> GetAccessCodeStartingLetters()
        {
            using (var context = new MSSContext())
            {
                List<char> startingLetters = new List<char>();

                List<AccessCodeDTO> codeList = GetActiveAccessCodes();

                startingLetters = codeList.GroupBy(cl => cl.accesscodeword[0]).Select(cl => cl.First().accesscodeword[0]).ToList();

                startingLetters.Sort(); // sort alphabetically

                return startingLetters;
            } 
        }

        #endregion
    }
}
