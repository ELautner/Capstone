using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MSS.Data.DTOs.Security;
using MSS.Data.Entities.Security;
using MSS.System.DAL;
using MSS.System.DAL.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.System.BLL.Security
{
    /* 
    CREATED:    P. Chavez         MAR 16 2018

    RoleManager
    The RoleManager class reads and creates data for the AspNetRoles table.
    This RoleManager class inherits the RoleManager class from Microsoft Asp.Net Identity.

    FIELDS:
    None

    METHODS:
    public void AddDefaultRoles() - adds the default authorization level roles
    public List<ApplicationRolesDTO> ListRoles() - lists all of the active authorization level roles 
    */
    [DataObject]
    public class RoleManager : RoleManager<ApplicationRole, string>
    {

        public RoleManager() : base(new RoleStore<ApplicationRole, string, IdentityUserRole>(new ApplicationDbContext()))
        {

        }
        /* 
        CREATED:    P. Chavez         MAR 22 2018

        AddDefaultRoles()
        This method adds the User, Super User and Administrator authorization level roles if they do not exist in the AspNetRoles table. 
        This method is called when the project is executed after a build/rebuild of the project (Global.asax).

        PARAMETERS:     
        None

        RETURNS: 
        void

        ODEV METHOD CALLS: 
        None
        */
        public void AddDefaultRoles()
        {
            if (Roles.Count() == 0)
            {
                Dictionary<string, string> defaultRoles = AuthorizationLevelRoles.DefaultAuthorizationLevelRoles;

                foreach (KeyValuePair<string, string> role in defaultRoles)
                {
                    var newRole = new ApplicationRole()
                    {
                        Name = role.Key,
                        authorizationleveldescription = role.Value,
                        activeyn = true
                    };
                    this.Create(newRole);
                }
            }
        }

        /* 
        CREATED:    P. Chavez         MAR 23 2018

        ListRoles()
        This method lists all of the active authorization level roles currently in the MSS database.

        PARAMETERS:     
        None

        RETURNS: 
        List<ApplicationRolesDTO> - a list of active authorization level roles

        ODEV METHOD CALLS: 
        None
        */
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ApplicationRoleDTO> ListRoles()
        {
            var currentRoles = from role in Roles.ToList()
                               where role.activeyn == true
                               select new ApplicationRoleDTO
                               {
                                 RoleId = role.Id,
                                 RoleName = role.Name
                               };
            return currentRoles.ToList();
        }
    }

}
