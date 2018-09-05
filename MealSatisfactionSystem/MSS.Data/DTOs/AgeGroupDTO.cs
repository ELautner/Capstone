namespace MSS.Data.DTOs
{
    /* 
    CREATED:    A. Valberg      MAR 29 2018

    AgeGroupDTO
    Uses default "get" and "set" methods to access the agegroup entity. The AgeGroupDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public int agegroupid - the unique ID of the age group (primary key)
    public string agegroupname - the name of the age group
    public bool activeyn - indicator of whether or not the age group is active in the Meal Satisfaction System (true = active)
    */
    public class AgeGroupDTO
    {
        public int agegroupid { get; set; }
        public string agegroupname { get; set; }
        public bool activeyn { get; set; }
    }
}
