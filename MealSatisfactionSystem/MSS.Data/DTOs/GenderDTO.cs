namespace MSS.Data.DTOs
{
    /* 
    CREATED:    A. Valberg      MAR 29 2018

    GenderDTO
    Uses default "get" and "set" methods to access the gender entity. The GenderDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public int genderid - the unique ID of the gender (primary key)
    public string gendername - the name of the gender
    public bool activeyn - indicator of whether or not the gender is active in the Meal Satisfaction System (true = active)
    */
    public class GenderDTO
    {
        public int genderid { get; set; }
        public string gendername { get; set; }
        public bool activeyn { get; set; }
    }
}
