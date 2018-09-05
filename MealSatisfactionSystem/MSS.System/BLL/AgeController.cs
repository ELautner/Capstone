using MSS.Data.DTOs;
using MSS.Data.Entities;
using MSS.System.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.System.BLL
{

    /* 
    CREATED:	H. L'Heureux		MAR 29 2018

    AgeController
    The AgeController classes purpose it to use methods to insert, update, deactivate or retrieve data from the AgeGroup table in the database.

    FIELDS:
    None

    METHODS:
    public List<AgeGroupDTO> GetAllAges() - returns a list of ages from the database
    public List<AgeGroupDTO> GetActiveAges() - returns ages from the database if they are active
    public void UpdateAgeGroup(AgeGroupDTO updatedAgeGroup) - this method updates a AgeGroup in the MSS database
    public void DeactivateAgeGroup(AgeGroupDTO deactivateAgeGroup) - this method deactivates a AgeGroup in the MSS database
    public void AddAgeGroup(AgeGroupDTO tempAgeGroup) - this method updates a AgeGroup in the MSS database
    */
    [DataObject]
    public class AgeController
    {
        /* 
       CREATED:    H. L'Heureux         MAR 29 2018

       GetAllAges()
       This method gets all ages from the database.

       PARAMETERS:     
       None

       RETURNS: 
       List<AgeGroupDTO> - a list of the ages from the AgeGroupDTO file

       ODEV METHOD CALLS: 
       None
       */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AgeGroupDTO> GetAllAges()
        {
            using (var context = new MSSContext())
            {
                var ageList = context.agegroups.ToList();
                List<AgeGroupDTO> ages = new List<AgeGroupDTO>();
                foreach (var age in ageList)
                {
                    AgeGroupDTO temp = new AgeGroupDTO();
                    temp.agegroupid = age.agegroupid;
                    temp.agegroupname = age.agegroupname;
                    ages.Add(temp);
                }
                return ages;
            }
        }

        /* 
        CREATED:    H. L'Heureux         MAR 29 2018

        GetActiveAges()
        This method gets all active ages from the database.

        PARAMETERS:     
        None

        RETURNS: 
        List<AgeGroupDTO> - a list of the ages from the AgeGroupDTO file

        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AgeGroupDTO> GetActiveAges()
        {
            using (var context = new MSSContext())
            {
                var ageList = from age in context.agegroups where age.activeyn == true
                              select age;
                List<AgeGroupDTO> ages = new List<AgeGroupDTO>();
                foreach (var age in ageList)
                {
                    AgeGroupDTO temp = new AgeGroupDTO();
                    temp.agegroupid = age.agegroupid;
                    temp.agegroupname = age.agegroupname;
                    ages.Add(temp);
                }
                return ages;
            }
        
        }

        /* 
        CREATED:     H. Conant       MAR 29 2018

        UpdateAgeGroup()
        This method updates an AgeGroup in the MSS database.

        PARAMETERS: 	
        AgeGroupDTO updatedAgeGroup - the AgeGroup that is to be updated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateAgeGroup(AgeGroupDTO updatedAgeGroup)
        {
            using (var context = new MSSContext())
            {
                var ageGroup = context.agegroups.Find(updatedAgeGroup.agegroupid);
                ageGroup.agegroupname = updatedAgeGroup.agegroupname;
                var existingAgeGroup = context.Entry(ageGroup);
                existingAgeGroup.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 29 2018

        DeactivateAgeGroup()
        This method deactivates an AgeGroup in the MSS database.

        PARAMETERS: 	
        AgeGroupDTO deactivateAgeGroup - the AgeGroup that is to be deactivated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void DeactivateAgeGroup(AgeGroupDTO deactivatedAgeGroup)
        {
            using (var context = new MSSContext())
            {
                var ageGroup = context.agegroups.Find(deactivatedAgeGroup.agegroupid);
                ageGroup.activeyn = false;
                var existingAgeGroup = context.Entry(ageGroup);
                existingAgeGroup.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		APR 11 2018

        AddAgeGroup()
        This method adds an AgeGroup to the MSS database.

        PARAMETERS: 	
        AgeGroupDTO tempAgeGroup - the AgeGroup that is to be added to the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddAgeGroup(AgeGroupDTO tempAgeGroup)
        {

            using (var context = new MSSContext())
            {
                agegroup newAgeGroup = new agegroup();
                newAgeGroup.agegroupid = tempAgeGroup.agegroupid;
                newAgeGroup.agegroupname = tempAgeGroup.agegroupname;
                newAgeGroup.activeyn = true;
                context.agegroups.Add(newAgeGroup);
                context.SaveChanges();
            }
        }
    }
}
