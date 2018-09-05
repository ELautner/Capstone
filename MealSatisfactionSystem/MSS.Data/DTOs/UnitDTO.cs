namespace MSS.Data.DTOs
{
    /* 
    CREATED:    A. Valberg        MAR 3 2018

    UnitDTO
    The UnitDTO acts as a DTO for the Unit table in the database. The UnitDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public string unitname - the unit's name
    public int unitid - the unique ID for the unit (primary key)
    public bool activeyn - a flag to show whether the unit is active in the DB or not
    public int caresiteid - unique ID for the care site the unit is connected to (foreign key)
    */
    public class UnitDTO
    {
        public string unitname { get; set; }
        public int unitid { get; set; }
        public bool activeyn { get; set; }
        public int caresiteid { get; set; }
    }
}
