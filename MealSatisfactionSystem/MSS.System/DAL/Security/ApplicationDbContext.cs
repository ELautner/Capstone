using Microsoft.AspNet.Identity.EntityFramework;
using MSS.Data.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.System.DAL.Security
{
    /* 
    CREATED:    P. Chavez       MAR 16 2018

    ApplicationDbContext
    This class allows the ApplicationUser, ApplicationRole and the other default classes in the Microsoft Asp.Net Identity to access the MSS database.
    The ApplicationDbContext class inherits the IdentityDbContext class in Microsoft Asp.Net Identity EntityFramework.

    PROPERTIES: (generic get/set, no validation)
    None

    ODEV METHODS:
    public ApplicationDbContext() : base("name=MSSDB") - connects the ApplicationDbContext to the MSS database (MSSDB)
    */
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        /*
        CREATED:	P. Chavez       MAR 16 2018
        
        ApplicationDbContext()
        This method connects the ApplicationDbContext to the MSS database (MSSDB).

        PARAMETERS: 	
       	None

        RETURNS: 
        None

        ODEV METHOD CALLS:
        None
        */
        public ApplicationDbContext()
            : base("MSSDB")
        {
        }

    }
}
