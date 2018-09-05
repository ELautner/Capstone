using System.Collections.Generic;
using System.Linq;
using MSS.System.DAL;
using System.ComponentModel;
using System.Data.Entity;
using MSS.Data.Entities;
using MSS.Data.DTOs;

namespace MSS.System.BLL
{
    /* 
    CREATED:	    H. Conant		MAR 20 2018

    RespondentTypeController
    The RespondentTypeController classes purpose it to use methods to update, deactivate, add or retrieve data from the RespondentType table in the database.

    FIELDS:
    None

    METHODS:
    public List<RespondentDTO> GetRespondentTypes() - retrieves all active respondent types from the MSS database
    public List<RespondentDTO> GetAllRespondentTypes() - retrieves all respondent types from the MSS database
    public void UpdateRespondentType(RespondentDTO updatedRespondentType) - this method updates a RespondentType in the MSS database
    public void DeactivateRespondentType(RespondentDTO deactivatedRespondentType) - this method deactivates a RespondentType in the MSS database
    public void AddRespondentType(RespondentDTO tempRespondentType) - this method adds a RespondentType to the MSS database
    */
    [DataObject]
    public class RespondentTypeController
    {
        /* 
        CREATED:	H. Conant		MAR 20 2018
        MODIFIED:   A. Valberg      MAR 29 2018
            -Updated return type to RespondentDTO
       
        GetRespondentTypes()
        This method retrieves all active respondent types from the MSS database.

        PARAMETERS: 	
        None

        RETURNS: 
        List<RespondentDTO> - the list of Respondent Types that has been retrieved

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RespondentDTO> GetRespondentTypes()
        {
            using (var context = new MSSContext())
            {
                List<RespondentDTO> types = new List<RespondentDTO>();
                var respondentTypes = (from aRespondentType in context.respondenttypes
                                       where aRespondentType.activeyn == true
                                       select aRespondentType).ToList();
                foreach (var item in respondentTypes)
                {
                    RespondentDTO temp = new RespondentDTO();
                    temp.respondenttypeid = item.respondenttypeid;
                    temp.respondenttypename = item.respondenttypename;
                    temp.activeyn = item.activeyn;
                    types.Add(temp);
                }
                return types;
            }
        }

        /* 
        CREATED:	H. Conant		APR 8 2018
       
        GetAllRespondentTypes()
        This method retrieves all respondent types from the MSS database.

        PARAMETERS: 	
        None

        RETURNS: 
        List<RespondentDTO> - the list of Respondent Types that has been retrieved

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RespondentDTO> GetAllRespondentTypes()
        {
            using (var context = new MSSContext())
            {
                List<RespondentDTO> types = new List<RespondentDTO>();
                var respondentTypes = (from aRespondentType in context.respondenttypes
                                       select aRespondentType).ToList();
                foreach (var item in respondentTypes)
                {
                    RespondentDTO temp = new RespondentDTO();
                    temp.respondenttypeid = item.respondenttypeid;
                    temp.respondenttypename = item.respondenttypename;
                    temp.activeyn = item.activeyn;
                    types.Add(temp);
                }
                return types;
            }
        }

        /* 
        CREATED:     H. Conant       MAR 20 2018
        MODIFIED:    H. Conant       MAR 29 2018
            -Updated return type to RespondentDTO

        UpdateRespondentType()
        This method updates a RespondentType in the MSS database.

        PARAMETERS: 	
        RespondentDTO updatedRespondentType - the RespondentType that is to be updated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateRespondentType(RespondentDTO updatedRespondentType)
        {
            using (var context = new MSSContext())
            {
                var respondentType = context.respondenttypes.Find(updatedRespondentType.respondenttypeid);
                respondentType.respondenttypename = updatedRespondentType.respondenttypename;
                var existingRespondenType = context.Entry(respondentType);
                existingRespondenType.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 20 2018
        MODIFIED:    H. Conant       MAR 29 2018
            -Updated return type to RespondentDTO

        DeactivateRespondentType()
        This method deactivates a RespondentType in the MSS database.

        PARAMETERS: 	
        RespondentDTO deactivatedRespondentType - the RespondentType that is to be deactivated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void DeactivateRespondentType(RespondentDTO deactivatedRespondentType)
        {
            using (var context = new MSSContext())
            {
                var respondentType = context.respondenttypes.Find(deactivatedRespondentType.respondenttypeid);
                respondentType.activeyn = false;
                var existingRespondentType = context.Entry(respondentType);
                existingRespondentType.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		APR 11 2018

        AddRespondentType()
        This method adds a RespondentType to the MSS database.

        PARAMETERS: 	
        RespondentDTO tempRespondentType - the RespondentType that is to be added to the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddRespondentType(RespondentDTO tempRespondentType)
        {

            using (var context = new MSSContext())
            {
                respondenttype newRespondentType = new respondenttype();
                newRespondentType.respondenttypeid = tempRespondentType.respondenttypeid;
                newRespondentType.respondenttypename = tempRespondentType.respondenttypename;
                newRespondentType.activeyn = true;
                context.respondenttypes.Add(newRespondentType);
                context.SaveChanges();
            }
        }
    }
}
