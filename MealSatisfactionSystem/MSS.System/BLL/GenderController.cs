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
     CREATED:	    H. L'Heureux		MAR 29 2018

     GenderController
     The GenderController classes purpose it to use methods to insert, update, deactivate or retrieve data from the Gender table in the database.

     FIELDS:
     None

     METHODS:
     public List<GenderDTO> GetAllGenders() - returns a list of ages from the database
     public List<GenderDTO> GetActiveGenders() - returns ages from the database if they are active
     public void UpdateGender(GenderDTO updatedgender) - this method updates a Gender in the MSS database
     public void DeactivateGender(GenderDTO deactivatedGender) - this method deactivates a Gender in the MSS database
     public void AddGender(GenderDTO tempGender) - this method adds a Gender to the MSS database
     */

    [DataObject]
    public class GenderController
    {
        /* 
        CREATED:    H. L'Heureux         MAR 29 2018

        GetAllGenders()
        This method gets all genders from the database.

        PARAMETERS:     
        None

        RETURNS: 
        List<GenderDTO> - a list of the genders from the GenderDTO file

        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GenderDTO> GetAllGenders()
        {
            using (var context = new MSSContext())
            {
                var genderList = context.genders.ToList();
                List<GenderDTO> genders = new List<GenderDTO>();
                foreach (var gender in genderList)
                {
                    GenderDTO temp = new GenderDTO();
                    temp.genderid = gender.genderid;
                    temp.gendername = gender.gendername;
                    genders.Add(temp);
                }
                return genders;
            }
        }

        /* 
        CREATED:    H. L'Heureux         MAR 29 2018

        GetActiveGenders()
        This method gets all active genders from the database.

        PARAMETERS:     
        None

        RETURNS: 
        List<GenderDTO> - a list of the genders from the GenderDTO file

        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GenderDTO> GetActiveGenders()
        {
            using (var context = new MSSContext())
            {
                var genderList = from gender in context.genders where gender.activeyn == true select gender;
                List<GenderDTO> genders = new List<GenderDTO>();
                foreach (var gender in genderList)
                {
                    GenderDTO temp = new GenderDTO();
                    temp.genderid = gender.genderid;
                    temp.gendername = gender.gendername;
                    genders.Add(temp);
                }
                return genders;
            }
        }

        /* 
        CREATED:     H. Conant       MAR 29 2018

        UpdateGender()
        This method updates a Gender in the MSS database.

        PARAMETERS: 	
        GenderDTO updatedgender - the Gender that is to be updated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateGender(GenderDTO updatedgender)
        {
            using (var context = new MSSContext())
            {
                var gender = context.genders.Find(updatedgender.genderid);
                gender.gendername = updatedgender.gendername;
                var existingGender = context.Entry(gender);
                existingGender.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 29 2018

        DeactivateGender()
        This method deactivates a Gender in the MSS database.

        PARAMETERS: 	
        GenderDTO deactivatedGender - the Gender that is to be deactivated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void DeactivateGender(GenderDTO deactivatedGender)
        {
            using (var context = new MSSContext())
            {
                var gender = context.agegroups.Find(deactivatedGender.genderid);
                gender.activeyn = false;
                var existingGender = context.Entry(gender);
                existingGender.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		APR 11 2018

        AddGender()
        This method adds a Gender to the MSS database.

        PARAMETERS: 	
        GenderDTO tempGender - the Gender that is to be added to the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddGender(GenderDTO tempGender)
        {

            using (var context = new MSSContext())
            {
                gender newGender = new gender();
                newGender.genderid = tempGender.genderid;
                newGender.gendername = tempGender.gendername;
                newGender.activeyn = true;
                context.genders.Add(newGender);
                context.SaveChanges();
            }
        }
    }
}
