using System.Collections.Generic;
using System.ComponentModel;
using MSS.System.DAL;
using System.Linq;
using MSS.Data.Entities;

namespace MSS.System.BLL
{
    /*
    AUTHOR: Patrick Chavez
    DATE CREATED: MARCH 4 2018

    AuthorizationLevelController
    The ManagementAccountController class purpose is to retrieve the AuthorizationLevels from the Authorization Level table in the MSS database.

    METHODS:
    GetAuthorizationLevels()

    */
    [DataObject]
    class AuthorizationLevelController
    {

        /*
        AUTHOR: Patrick Chavez
        DATE CREATEED: MARCH 4 2018

        GetAuthorizationLevels()
        This method retrieves all authorization levels.

        PARAMETERS: N/A

        RETURNS: List<authorizationlevel> - Returns a list of all of the authorization levels.

        METHOD CALLS: N/A 

        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<authorizationlevel> GetAuthorizationLevels()
        {
            using (var context = new MSSContext())
            {
                return context.authorizationlevels.ToList();
            }
        }
    }
}
