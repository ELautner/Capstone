using MSS.Data.DTOs;
using MSS.System.DAL;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using MSS.Data.Entities;

namespace MSS.System.BLL
{

    /* 
    CREATED:	H. Conant		MAR 3 2018

    UnitController
    The UnitController classes' purpose it to use methods to insert, update, deactivate or retrieve data from the Unit table in the database.

    FIELDS:
    None

    METHODS:
    public List<UnitDTO> GetCareSiteUnits(int careSiteId) - returns a list of units for a particular care site ID
    public List<UnitDTO> GetActiveCareSiteUnits(int careSiteId) -  this method retrieves a list of tnits that contain a specific care site ID and that are active
    public List<UnitDTO> GetAllUnits() - returns a list of all units in the MSS database
    public UnitDTO GetUnit(int unitId) - returns a unit based on a unit ID passed in as a parameter
    public void AddUnit(UnitDTO tempUnit) - inserts a unit into the MSS database
    public void UpdateUnit(UnitDTO updatedUnit) - updates an existing unit in the MSS database
    public void DeactivateUnit(UnitDTO deactivateUnitUnit) - deactivates an existing unit in the MSS database
    */
    [DataObject]
    public class UnitController
    {
        #region "Get" Methods
        /* 
        CREATED:	H. Conant		MAR 3 2018
        MODIFIED: 	A. Valberg		MAR 4 2018
            - Changed method signature
	        - Updated method body
        MODIFIED:   C. Stanhope     MAR 24 2018
            - units now return in ascending order alphabetically

        GetCareSiteUnits()
        This method retrieves a list of units that contain a specific care site ID.

        PARAMETERS: 	
        int careSiteID - the ID of the care site that the units belong to

        RETURNS: 
        List<UnitDTO> - a list of units that contain the care site ID

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnitDTO> GetCareSiteUnits(int careSiteId)
        {
            using (var context = new MSSContext())
            {
                var careSiteUnits = from aUnit in context.units
                                    where aUnit.caresiteid == careSiteId
                                    orderby aUnit.unitname ascending
                                    select aUnit;
                List<UnitDTO> unitDTOs = new List<UnitDTO>();
                foreach (unit unit in careSiteUnits)
                {
                    UnitDTO temp = new UnitDTO();
                    temp.unitid = unit.unitid;
                    temp.unitname = unit.unitname;
                    unitDTOs.Add(temp);
                }
                return unitDTOs;

            }
        }

        /* 
        CREATED:	H. Conant		MAR 13 2018
        MODIFIED:   C. Stanhope     MAR 24 2018
            - units now return in ascending order alphabetically

        GetActiveCareSiteUnits()
        This method retrieves a list of units that contain a specific care site ID and that are active.

        PARAMETERS: 	
        int careSiteID - the ID of the care site that the units belong to

        RETURNS: 
        List<UnitDTO> - a list of units that contain the care site ID

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnitDTO> GetActiveCareSiteUnits(int careSiteId)
        {
            using (var context = new MSSContext())
            {
                var careSiteUnits = from aUnit in context.units
                                    where aUnit.caresiteid == careSiteId && aUnit.activeyn == true
                                    orderby aUnit.unitname ascending
                                    select aUnit;
                List<UnitDTO> unitDTOs = new List<UnitDTO>();
                foreach (unit unit in careSiteUnits)
                {
                    UnitDTO temp = new UnitDTO();
                    temp.unitid = unit.unitid;
                    temp.unitname = unit.unitname;
                    unitDTOs.Add(temp);
                }
                return unitDTOs;

            }
        }

        /* 
        CREATED:	H. Conant		MAR 11 2018
        MODIFIED:   C. Stanhope     MAR 24 2018
            - units now return in ascending order alphabetically
       
        GetAllUnits()
        This method retrieves all units from the database.

        PARAMETERS: 	
        None

        RETURNS: 
        List<UnitDTO> - the list of units that has been retrieved

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnitDTO> GetAllUnits()
        {
            using (var context = new MSSContext())
            {
                var units = from aUnit in context.units
                            orderby aUnit.unitname ascending
                            select aUnit;
                List<UnitDTO> unitDTOs = new List<UnitDTO>();
                foreach (unit unit in units)
                {
                    UnitDTO temp = new UnitDTO();
                    temp.unitid = unit.unitid;
                    temp.unitname = unit.unitname;
                    unitDTOs.Add(temp);
                }
                return unitDTOs;

            }
        }

        /* 
        CREATED:	H. Conant		MAR 3 2018
        MODIFIED: 	A. Valberg		MAR 4 2018
            - Changed method signature
	        - Updated method body
        MODIFIED:   H. Conant		MAR 11 2018
            - Changed method signature 

        GetUnit()
        This method retrieves a single unit by its unit ID.

        PARAMETERS: 	
        int unitId - the ID of the unit being retrieved

        RETURNS: 
        UnitDTO - the unit that has been retrieved

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public UnitDTO GetUnit(int unitId)
        {
            using (var context = new MSSContext())
            {
                var careSiteUnit = context.units.Find(unitId);
                UnitDTO tempUnit = new UnitDTO();
                tempUnit.unitid = careSiteUnit.unitid;
                tempUnit.unitname = careSiteUnit.unitname;
                tempUnit.caresiteid = careSiteUnit.caresiteid;
                return tempUnit;
            }
        }
        #endregion

        #region Add, Update, Deactivate Units
        /* 
        CREATED:	H. Conant		MAR 3 2018
        MODIFIED: 	A. Valberg		MAR 4 2018
            - Changed method signature
	        - Updated method body

        AddUnit()
        This method adds a unit to the MSS database.

        PARAMETERS: 	
        UnitDTO tempUnit - the unit that is to be added to the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddUnit(UnitDTO tempUnit)
        {

            using (var context = new MSSContext())
            {
                unit newUnit = new unit();
                newUnit.unitname = tempUnit.unitname;
                newUnit.caresiteid = tempUnit.caresiteid;
                newUnit.activeyn = true;
                context.units.Add(newUnit);
                context.SaveChanges();
            }
        }

        /* 
        CREATED:     H. Conant, A. Valberg       MAR 3 2018

        UpdateUnit()
        This method updates a unit in the MSS database.

        PARAMETERS: 
        UnitDTO updatedUnit - the unit that is to be updated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateUnit(UnitDTO updatedUnit)
        {
            using (var context = new MSSContext())
            {
                var unit = context.units.Find(updatedUnit.unitid);
                unit.unitname = updatedUnit.unitname;
                var existingUnit = context.Entry(unit);
                existingUnit.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /* 
        CREATED:	H. Conant		MAR 3 2018
        MODIFIED: 	A. Valberg		MAR 4 2018
            - Changed method signature
	        - Updated method body
        MODIFIED: 	H. Conant		MAR 11 2018
            - Changed method signature

        DeactivateUnit()
        This method deactivates a unit in the MSS database.

        PARAMETERS: 	
        UnitDTO deactivateUnitUnit - the unit that is to be deactivated in the database

        RETURNS: 
        void

        ODEV METHOD CALLS:
        None
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void DeactivateUnit(UnitDTO deactivateUnit)
        {
            using (var context = new MSSContext())
            {
                var unit = context.units.Find(deactivateUnit.unitid);
                unit.activeyn = false;
                var existingUnit = context.Entry(unit);
                existingUnit.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        #endregion
    }
}
