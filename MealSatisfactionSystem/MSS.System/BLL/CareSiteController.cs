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
using System.Data.Entity;

namespace MSS.System.BLL
{

    /* 
    CREATED:    A. Valberg		MAR 4 2018

    CareSiteController
    This class provides methods to access the CareSite table in order to read, update, add and deactivate care sites.

    FIELDS:
    None

    METHODS:
    public List<CareSiteDTO> GetCareSites() - retrieves a list of all care sites
    public List<CareSiteDTO> GetActiveCareSites() - retrieves a list of all active care sites
    public List<int> GetCareSiteIds(List<int> alreadyAssignedCareSites) - returns a list of care site IDs that have not been assigned a access code for a particular date
    public string GetCareSiteName(int careSiteID) - retrieves a care site name associated with a passed-in care site ID
    public CareSiteDTO GetCareSiteByCareSiteID(int careSiteID) - retrieves a care site that matches the passed care site ID
    public void AddCareSite(CareSiteDTO careSiteToAdd) - adds the passed care site to the database
    public void UpdateCareSite(CareSiteDTO updatedCareSite) - updates a CareSite in the MSS database
    public void DeactivateCareSite(CareSiteDTO deactivateCareSite) - deactivates a CareSite in the MSS database
    */
    [DataObject]
    public class CareSiteController
    {
        #region "Get" Methods
        /* 
        CREATED:    A. Valberg      MAR 3 2018
        MODIFIED:   C. Stanhope     MAR 24 2018
            - care sites now return in ascending order alphabetically

        GetCareSites()
        Retrieves a list of all care sites.

        PARAMETERS: 	
        None

        RETURNS: 
        List<CareSiteDTO> - a list of care sites returned as DTOs

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<CareSiteDTO> GetCareSites()
        {
            using (var context = new MSSContext())
            {
                List<CareSiteDTO> careSiteDTOs = new List<CareSiteDTO>();
                var careSites = context.caresites.ToList().OrderBy(site => site.caresitename);
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
        MODIFIED:   C. Stanhope     MAR 24 2018
            - care sites now return in ascending order alphabetically

        GetActiveCareSites()
        Retrieves a list of all currently active care sites.

        PARAMETERS: 	
        None

        RETURNS: 
        List<CareSiteDTO> - A list of care sites

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CareSiteDTO> GetActiveCareSites()
        {
            using (var context = new MSSContext())
            {
                var activeCareSitess = from aCareSite in context.caresites
                                    where aCareSite.activeyn == true
                                    orderby aCareSite.caresitename ascending
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
        Retrieves a list of care site IDs that have not been assigned an access code on a particular date.

        PARAMETERS: 	
        List<int> alreadyAssignedCareSites - list of care site IDs that have been assigned an access code

        RETURNS: 
        List<int> - list of care site IDs

        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
         public List<int> GetCareSiteIds(List<int> alreadyAssignedCareSites)
         {
             using (var context = new MSSContext())
             {

                 var careSites = (from aCareSite in context.caresites
                                 where !alreadyAssignedCareSites.Contains(aCareSite.caresiteid) && aCareSite.activeyn == true
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
        CREATED:     H. L'Heureux, H. Conant        MAR 13 2018

        GetCareSiteName()
        This method will accept a care site id and return the name of that care site.

        PARAMETERS:     
        int careSiteID - the ID of the care site in question

        RETURNS: 
        int - the care site ID

        METHOD CALLS:
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

        /* 
        CREATED:    C. Stanhope         MAR 23 2018
        MODIFIED:   C. Stanhope         MAR 24 2018
            - forgot to assign care site ID. Fixed now.

        GetCareSiteByCareSiteID()
        Retrieves a care site object that matches the passed care site ID.

        PARAMETERS: 	
        int careSiteID - the ID of the desired care site

        RETURNS: 
        CareSiteDTO - the desired care site

        ODEV METHOD CALLS:
        None
        */
        public CareSiteDTO GetCareSiteByCareSiteID(int careSiteID)
        {
            using (var context = new MSSContext())
            {
                CareSiteDTO desiredCareSite = new CareSiteDTO();

                var tempCareSite = context.caresites.Find(careSiteID);

                desiredCareSite.caresiteid = tempCareSite.caresiteid;
                desiredCareSite.caresitename = tempCareSite.caresitename;
                desiredCareSite.address = tempCareSite.address;
                desiredCareSite.city = tempCareSite.city;
                desiredCareSite.province = tempCareSite.province;
                desiredCareSite.activeyn = tempCareSite.activeyn;

                return desiredCareSite;
            }
        }
        #endregion

        #region Add, Update, Deactivate Care Sites
        /* 
        CREATED:     C. Stanhope         MAR 23 2018

        AddCareSite()
        Adds a CareSite into the MSS database.

        PARAMETERS: 	
        CareSiteDTO careSiteToAdd - the care site that is to be added to the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddCareSite(CareSiteDTO careSiteToAdd)
        {
            using (var context = new MSSContext())
            {
                caresite newCareSite = new caresite();
                newCareSite.caresitename = careSiteToAdd.caresitename;
                newCareSite.address = careSiteToAdd.address;
                newCareSite.city = careSiteToAdd.city;
                newCareSite.province = careSiteToAdd.province;
                newCareSite.activeyn = true;

                context.caresites.Add(newCareSite);
                context.SaveChanges();
            }
        }

        /* 
        CREATED:     H. Conant       MAR 20 2018

        UpdateCareSite()
        Updates a CareSite in the MSS database.

        PARAMETERS: 	
        CareSiteDTO updatedCareSite - the CareSite that is to be updated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateCareSite(CareSiteDTO updatedCareSite)
        {
            using (var context = new MSSContext())
            {
                var caresite = context.caresites.Find(updatedCareSite.caresiteid);
                caresite.caresitename = updatedCareSite.caresitename;
                caresite.address = updatedCareSite.address;
                caresite.city = updatedCareSite.city;
                caresite.province = updatedCareSite.province;
                var existingCareSite = context.Entry(caresite);
                existingCareSite.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 20 2018

        DeactivateCareSite()
        This method deactivates a CareSite in the MSS databse.

        PARAMETERS: 	
        CareSiteDTO deactivateCareSite - the CareSite that is to be deactivated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void DeactivateCareSite(CareSiteDTO deactivateCareSite)
        {
            using (var context = new MSSContext())
            {
                var caresite = context.caresites.Find(deactivateCareSite.caresiteid);
                caresite.activeyn = false;
                
                var existingCareSite = context.Entry(caresite);
                existingCareSite.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        #endregion
    }
}
