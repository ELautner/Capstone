using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.Entities.Security
{
    /* 
    CREATED:    P. Chavez       MAR 16 2018

    AuthorizationLevelRoles
    An entity that contains the three authorization level roles for the MSS.

    PROPERTIES: (generic get/set, no validation)
    public const string User - the User role in a string value
    public const string Super_User - the Super User role in a string value
    public const string Administrator - the Administrator role in a string value
    public static Dictionary<string, string> DefaultAuthorizationLevelRoles - the dictionary contains the authorization level roles as the key and their respective descriptions as the values

    CONSTRUCTORS:
    None
    */
    public static class AuthorizationLevelRoles
    {
        public const string User = "User";
        public const string Super_User = "Super User";
        public const string Administrator = "Administrator";

        public static Dictionary<string, string> DefaultAuthorizationLevelRoles
        {
            get
            {
                Dictionary<string, string> value = new Dictionary<string, string>();
                value.Add(User, "Can view surveys, survey reports, and update contact requests.Can update name, email, and pasword on the account.");
                value.Add(Super_User, "Can view surveys, survey reports, and update contact requests. Can update name, email, and pasword on the account. Can modify units for care sites.");
                value.Add(Administrator, "Can view surveys, survey reports, and update contact requests. Can create and update an account. Can modify units for care sites.");
                return value;
            }
        }
    }
}
