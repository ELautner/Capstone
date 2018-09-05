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
        AUTHOR: Ashley Valberg
        DATE CREATED: MARCH 3 2018

        AccessCodeController
        This class provides methods to access the AccessCode table of the DB in order to read, update, insert, and deactivate access codes.

        METHODS:
        public List<AccessCodeDTO> GetAccessCodes()
        public AccessCodeDTO CheckSurveyAccessCode()
        public void AddAccessCode(AccessCodeDTO tempCode)
        public void UpdateAccessCode(AccessCodeDTO tempCode)
        public void DeactivateAccessCode(AccessCodeDTO tempCode)
        public List<int> GetAccessCodes(List<int> alreadyAssignedCodes) - returns a list of access code id's that have not been used on a particular day or for a particular care site
    */
    [DataObject]
    public class AccessCodeController
    {

        #region Add, Update, Delete Methods
        /* 
        CREATED: 	A. Valberg		MAR 3 2018
        MODIFIED: 	C. Stanhope     MAR 13 2018
            - access codes are now automatically made lowercase

        AddAccessCode()
        Takes a new access code from a user and puts it in the database.

        PARAMETERS: 	
        AccessCodeDTO

        RETURNS: 
        AccessCodeDTO - 

        METHOD CALLS:
        DbSet.Add();
        DbSet.SaveChanges();
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
           AUTHOR: Ashley Valberg
           DATE CREATED: MARCH 3 2018

           UpdateAccessCode()
           Updates an existing access code.

           PARAMETERS: 	
           AccessCodeDTO

           RETURNS: void

           METHOD CALLS:
           DbSet.Find();
           DbSet.Entry();
           DbSet.SaveChanges();
       */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateAccessCode(AccessCodeDTO tempCode)
        {
            using (var context = new MSSContext())
            {
                var accessCode = context.accesscodes.Find(tempCode.accesscodeid);
                accessCode.accesscodeword = tempCode.accesscodeword;
                var existingCode = context.Entry(accessCode);
                existingCode.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
            AUTHOR: Ashley Valberg
            DATE CREATED: MARCH 3 2018

            DeactivateAccessCode()
            Sets the ActiveYN flag on a specific access code to false.

            PARAMETERS: 	
            AccessCodeDTO

            RETURNS: void

            METHOD CALLS:
            DbSet.Find();
            DbSet.Entry();
            DbSet.SaveChanges();
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void DeactivateAccessCode(AccessCodeDTO tempCode)
        {
            using (var context = new MSSContext())
            {
                var accessCode = context.accesscodes.Find(tempCode.accesscodeid);
                accessCode.activeyn = false;
                var existingCode = context.Entry(accessCode);
                existingCode.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        #endregion

        #region "Get" Methods
        /* 
        CREATED: 	A. Valberg		MAR 3 2018

        GetAccessCodes()
        Retrieves a list of all currently active access codes.

        PARAMETERS: 	
        None.

        RETURNS: 
        List<AccessCodeDTO> - 

        METHOD CALLS:
        List.Add();
        */
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<AccessCodeDTO> GetAccessCodes()
        {
            using (var context = new MSSContext())
            {
                var accessCodes = from item in context.accesscodes
                                  where item.activeyn == true
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

        GGetAccessCodes
        Retrieves a list of access code ids that have not been assigned on a particular date or to a particular caresite.

        PARAMETERS: 	
            List<int> alreadyAssignedCodes - list of access code ids that have been on a particular date or to a particular caresite

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
        public List<int> GetAccessCodes(List<int> alreadyAssignedCodes)
        {
            using (var context = new MSSContext())
            {

                var accessCodes = (from aAccessCode in context.accesscodes
                                   where !alreadyAssignedCodes.Contains(aAccessCode.accesscodeid)
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
        CREATED: 	C. Stanhope		MAR 10 2018
        MODIFIED: 	C. Stanhope     MAR 13 2018
            - now returns access code words in ascending order alphabetically
            - starting letter search added

        GetAccessCodesByKeyword()
        Retrieves a list of all currently active access codes that match the keyword passed as a parameter. If the passed keyword contains an underscore, the first letter of the keyword is used to search for active access codes that start with the first letter.

        PARAMETERS: 	
        string keyword - the keyword used for searching
        
        RETURNS: 
        List<AccessCodeDTO> - a list of matching AccessCodeDTOs
        
        METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AccessCodeDTO> GetAccessCodesByKeyword(string keyword)
        {
            using (var context = new MSSContext())
            {
                IQueryable<accesscode> accessCodes = null;
                if (string.IsNullOrEmpty(keyword)) // empty keyword, return all active 
                {
                    accessCodes = from item in context.accesscodes
                                  where item.activeyn == true
                                  orderby item.accesscodeword ascending
                                  select item;
                }
                else if (keyword.Contains("_")) // search for only the first letter
                {
                    string startingLetter = keyword[0].ToString().ToLower();

                    accessCodes = from item in context.accesscodes
                                  where item.activeyn == true && item.accesscodeword.StartsWith(startingLetter)
                                  orderby item.accesscodeword ascending
                                  select item;
                }
                else // search to match keyword if the keyword variable is not empty
                {
                    accessCodes = from item in context.accesscodes
                                  where item.activeyn == true && item.accesscodeword.Contains(keyword.ToLower())
                                  orderby item.accesscodeword ascending
                                  select item;
                }
                
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
        CREATED: 	C. Stanhope		MAR 13 2018

        GetAccessCodeStartingLetters()
        Retrieves a list of every letter of the alphabet that has an access code starting with that letter. Used to populate the starting letter link buttons on the manage access codes page. 

        PARAMETERS: 	
        None
        
        RETURNS: 
        List<char> - a list of the starting letters of all access codes
        
        METHOD CALLS: 
        AccessCodeController.GetAccessCodes()
        GroupBy() CCC: do we need these???
        Select()
        ToList()
        Sort()
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<char> GetAccessCodeStartingLetters()
        {
            using (var context = new MSSContext())
            {
                List<char> startingLetters = new List<char>();

                List<AccessCodeDTO> codeList = GetAccessCodes();

                startingLetters = codeList.GroupBy(cl => cl.accesscodeword[0]).Select(cl => cl.First().accesscodeword[0]).ToList();

                startingLetters.Sort(); // sort alphabetically

                return startingLetters;
            } 
        }

        #endregion

        /* 
            AUTHOR: Ashley Valberg
            DATE CREATED: MARCH 3 2018

            CheckSurveyAccessCode()
            TODO: add description here

            PARAMETERS: 	
            None.

            RETURNS: AccessCodeDTO

            METHOD CALLS:
            None.
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public AccessCodeDTO CheckSurveyAccessCode() // CCC: delete this if not being used
        {
            using (var context = new MSSContext())
            {
                //TODO: add text - sorry, not sure what this method does?
                return null;
            }
        }
    }
}
