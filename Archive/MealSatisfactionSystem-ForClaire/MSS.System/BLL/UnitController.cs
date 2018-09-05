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
    The UnitController classes purpose it to use methods to insert, update, delete or retrive data from the Unit table in the database.

    CLASS-LEVEL VARIABLES:
    NONE

    METHODS:
    public List<unit> GetCareSiteUnits(int careSiteId) - returns a list of units for a particular care site id
    public List<UnitDTO> GetActiveCareSiteUnits(int careSiteId) -  This method retrieves a list of Units that contain a specific Care Site ID and that are active.
    public SimpleUnit GetUnit(int unitId) - returns a unit based on a unit id passed in as a parameter
    public void AddUnit(UnitDTO tempUnit) - inserts a unit into the MSS database
    public void UpdateUnit(UnitDTO updatedUnit) - updates an existing unit in the MSS database
    public void DeactivateUnit(UnitDTO deactivateUnitUnit) - deactivates an existing unit in the MSS database
    */
    [DataObject]
    public class UnitController
    {

        /* 
        CREATED:	H. Conant		MAR 3 2018
        MODIFIED: 	A. Valberg		MAR 4 2018
            - Changed method signature
	        - Updated method body

        GetCareSiteUnits()
        This method retrieves a list of Units that contain a specific Care Site ID.

        PARAMETERS: 	
        int careSiteID - The ID of the Care Site that the units belong to.

        RETURNS: 
        List<unit> - A list of units that contain the Care Site ID

        METHOD CALLS:
        MSSContext()
        List<UnitDTO>()
        UnitDTO()
        .Add
        */

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnitDTO> GetCareSiteUnits(int careSiteId)
        {
            using (var context = new MSSContext())
            {
                var careSiteUnits = from aUnit in context.units
                                    where aUnit.caresiteid == careSiteId
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

        GetActiveCareSiteUnits()
        This method retrieves a list of Units that contain a specific Care Site ID and that are active.

        PARAMETERS: 	
        int careSiteID - The ID of the Care Site that the units belong to.

        RETURNS: 
        List<unit> - A list of units that contain the Care Site ID

        METHOD CALLS:
        MSSContext()
        List<UnitDTO>()
        UnitDTO()
        .Add
       */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnitDTO> GetActiveCareSiteUnits(int careSiteId)
        {
            using (var context = new MSSContext())
            {
                var careSiteUnits = from aUnit in context.units
                                    where aUnit.caresiteid == careSiteId && aUnit.activeyn == true
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnitDTO> GetAllUnits()
        {
            using (var context = new MSSContext())
            {
                var Units = from aUnit in context.units
                            select aUnit;
                List<UnitDTO> unitDTOs = new List<UnitDTO>();
                foreach (unit unit in Units)
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
            - Chnaged method signature 

        GetUnit()
        This method retrieves a single Unit by its Unit ID.

        PARAMETERS: 	
        int unitId - The ID of the Unit being retrieved.

        RETURNS: 
        UnitDTO - The Unit that has been retrieved.

        METHOD CALLS:
        DataObjectMethod()
        MSSContext()
        .Find()
        UnitDTO()
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

        /* 
        CREATED:	H. Conant		MAR 3 2018
        MODIFIED: 	A. Valberg		MAR 4 2018
            - Changed method signature
	        - Updated method body

        AddUnit()
        This method adds a Unit to the MSS databse.

        PARAMETERS: 	
        UnitDTO tempUnit - The Unit that is to be added to the database.

        RETURNS: 
        void - Nothing is returned.

        METHOD CALLS:
        DataObjectMethod()
        MSSContext()
        unit()
        .Add()
        .SaveChanges()
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
        AUTHOR: Holly Conant, Ashley Valberg
        DATE CREATED: MARCH 3 2018

        UpdateUnit()
        This method updates a Unit in the MSS databse.

        PARAMETERS: 	
        SimpleUnit updatedUnit - The Unit that is to be updated in the database.

        RETURNS: 
        void - Nothing is returned.

        METHOD CALLS:
        DataObjectMethod()
        MSSContext()
        .Find()
        .Entry()
        .SaveChanges()
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
        This method deactivates a Unit in the MSS databse.

        PARAMETERS: 	
        UnitDTO deactivateUnitUnit - The Unit that is to be deactivated in the database.

        RETURNS: 
        void - Nothing is returned.

        METHOD CALLS:
        DataObjectMethod()
        MSSContext()
        .Find()
        .Entry()
        .SaveChanges()
        */
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void DeactivateUnit(UnitDTO deactivateUnitUnit)
        {
            using (var context = new MSSContext())
            {
                var unit = context.units.Find(deactivateUnitUnit.unitid);
                unit.activeyn = false;
                var existingUnit = context.Entry(unit);
                existingUnit.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
