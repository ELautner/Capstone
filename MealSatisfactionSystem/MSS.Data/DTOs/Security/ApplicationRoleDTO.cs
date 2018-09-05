using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.Data.DTOs.Security
{
    /* 
    CREATED:   P. Chavez      MAR 23 2018

    ApplicationRoleDTO
    Uses default "get" and "set" methods to access the ApplicationRole entity. The ApplicationRoleDTO is used to act as a layer of protection and avoids direct modification of database entities.

    PROPERTIES: (generic get/set, no validation)
    public string RoleId - the unique ID of a AspNetRole (primary key)
    public string RoleName - the name of the authorization level role
    */
    public class ApplicationRoleDTO
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
