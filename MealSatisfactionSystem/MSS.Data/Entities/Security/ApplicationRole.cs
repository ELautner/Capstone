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

    ApplicationRole
    Uses default "get" and "set" methods to access the AspNetRoles entity.
    The ApplicationRole class inherits the IdentityRole class in Microsoft Asp.Net Identity EntityFramework.

    PROPERTIES: (generic get/set, no validation)
    public string authorizationleveldescription - the description of the authorization level role
    public bool activeyn - indicates whether the authorization level role is active in the MSS

    CONSTRUCTORS:
    public ApplicationRole() - the ApplicationRole constructor that generates a new GUID value
    */
    public class ApplicationRole : IdentityRole<string, IdentityUserRole>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string authorizationleveldescription { get; set; }
        public bool activeyn { get; set; }
    }
}
