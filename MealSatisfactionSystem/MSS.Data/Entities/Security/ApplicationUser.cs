using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.Entities.Security
{
    /* 
    CREATED:    P. Chavez       MAR 16 2018

    ApplicationUser
    Uses default "get" and "set" methods to access the AspNetUsers entity.
    The ApplicationUser class inherits the IdentityUser class in Microsoft Asp.Net Identity EntityFramework.

    PROPERTIES: (generic get/set, no validation)
    public int caresiteid  - the unique ID of the care site that the account is associated with
    public string firstname - the first name of the account
    public string lastname - the last name of the account
    public bool activeyn - indicates whether the account is active in the MSS

    CONSTRUCTORS:
    public ApplicationUser() - the ApplicationUser constructor that generates a new GUID value
    */
    public class ApplicationUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public int? caresiteid  { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool activeyn { get; set; }
    }
}
