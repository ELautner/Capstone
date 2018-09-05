using MSS.Data;
using MSS.Data.DTOs;
using MSS.Data.Entities;
using MSS.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MSS.System.BLL;
using System.Text;
using System.Threading.Tasks;

namespace MSS.System.BLL
{
    
    /* 
    CREATED:    A. Valberg		MAR 4 2018

    CareSiteController
    This class provides methods to access the CareSite table in order to read, update, add and deactivate care sites.

    CLASS-LEVEL VARIABLES:
    NONE

    METHODS:
    public List<CareSiteDTO> GetCareSites() - retrieves a list of all currently active care sites.
    public List<int> GetCareSiteIds(List<int> alreadyAssignedCareSites) - returns a list of care site id's that have not been assigned a access code for a particular date
    public CareSiteDTO GetViewableCareSites() - retrieves information for the care site specific to a username that is passed in.
    */
    [DataObject]
    public class CareSiteController
    {
        /* 
            AUTHOR: Ashley Valberg
            DATE CREATED: MARCH 3 2018

            GetCareSites()
            Retrieves a list of all care sites.

            PARAMETERS: 	
            None.

            RETURNS: List<CareSiteDTO>

            METHOD CALLS:
            DbSet.ToList();
            List.Add();
        */
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<CareSiteDTO> GetCareSites()
        {
            using (var context = new MSSContext())
            {
                List<CareSiteDTO> careSiteDTOs = new List<CareSiteDTO>();
                var careSites = context.caresites.ToList();
                foreach (caresite site in careSites)
                {
                    CareSiteDTO temp = new CareSiteDTO();
                    temp.caresiteid = site.caresiteid;
                    temp.caresitename = site.caresitename;
                    careSiteDTOs.Add(temp);
                }
                return careSiteDTOs;
            }
        }

        /* 
        CREATED:	H. Conant		MAR 13 2018

        GetCareSites()
        Retrieves a list of all currently active care sites.

        PARAMETERS: 	
        None.

        RETURNS: 
        List<CareSiteDTO> - A list of care sites

         METHOD CALLS:
         MSSContext()
         CareSiteDTO()
         List<CareSiteDTO>()
        .Add
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CareSiteDTO> GetActiveCareSites()
        {
            using (var context = new MSSContext())
            {
                var activeCareSitess = from aCareSite in context.caresites
                                    where aCareSite.activeyn == true
                                    select aCareSite;
                List<CareSiteDTO> careSiteDTOs = new List<CareSiteDTO>();
                foreach (caresite site in activeCareSitess)
                {
                    CareSiteDTO temp = new CareSiteDTO();
                    temp.caresiteid = site.caresiteid;
                    temp.caresitename = site.caresitename;
                    careSiteDTOs.Add(temp);
                }
                return careSiteDTOs;

            }
        }

        /* 
        CREATED:    H. Conant       MAR 11 2018

        GetCareSiteIds()
        Retrieves a list of care site ids that have not been assigned a access code on a particular date.

        PARAMETERS: 	
        List<int> alreadyAssignedCareSites - list of care site ids that have been assigend a access code

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
         public List<int> GetCareSiteIds(List<int> alreadyAssignedCareSites)
         {
             using (var context = new MSSContext())
             {

                 var careSites = (from aCareSite in context.caresites
                                 where !alreadyAssignedCareSites.Contains(aCareSite.caresiteid)
                                 select aCareSite).ToList();

                List<int> careSiteIds = new List<int>();

                 foreach (caresite site in careSites)
                 {
                    int temp = new int();
                    temp = site.caresiteid;
                    careSiteIds.Add(temp);
                 }

                 return careSiteIds;
             }
         } 

        /* 
            AUTHOR: Ashley Valberg
            DATE CREATED: MARCH 3 2018

            GetViewableCareSites()
            Retrieves information for the care site specific to a username that is passed in.

            PARAMETERS: 	
            //TODO: add username parameter (not sure of datatype)

            RETURNS: CareSiteDTO

            METHOD CALLS:
            None.
        */
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public CareSiteDTO GetViewableCareSites() // Just a thought, shouldn't this return a list? - CCC   yes
        {
            using (var context = new MSSContext())
            {
                //TODO: finish this method (will need a username passed in)
                return null;
            }
        }

        /* 
        CREATED:     H. L'Heureux, H. Conant        MAR 13 2018

        GetCareSiteName()
        This method will accept a care site id and return the name of that care site.

        PARAMETERS:     
        int careSiteID - the ID of the care site in question

        RETURNS: 
        int - the care site ID

        METHOD CALLS: <- Holly: any method calls not just to metods you have made (ex .Find(), CareSiteDTO())
        None
        */
        public string GetCareSiteName(int careSiteID)
        {
            using (var context = new MSSContext())
            {

                var careSite = context.caresites.Find(careSiteID);

                CareSiteDTO tempCareSite = new CareSiteDTO();

                tempCareSite.caresitename = careSite.caresitename;

                return tempCareSite.caresitename;
            }
        }

    }
}
