using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MSS.System.BLL.Security;

/* 
CREATED:    P. Chavez         MAR 22 2018

Management_ManagementMasterPage
An automatically generated page used to store the Management_ManagementMasterPage's backend C# code.

FIELDS:
None

METHODS: 
protected void Page_Load(object sender, EventArgs e) - triggers when the page is loaded
protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e) - logouts out the current user that is authenticated
*/
public partial class Management_ManagementMasterPage : System.Web.UI.MasterPage
{

    /* 
    CREATED: 	P. Chavez		Mar 22 2018

    Page_Load()
    This method runs when the page is loaded where it checks if a user is logged in. 
    If the user is logged in, the user's first name is displayed.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    LoginCancelEventArgs e - optional class that provides data for a cancelable event

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    UserManager.FindById()
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Management/index");
        }
        else 
        {
            try
            {
                string userId = Context.User.Identity.GetUserId();
                UserManager userManager = new UserManager();
                var currentUser = userManager.FindById(userId);
                if (!currentUser.activeyn)
                {
                    Context.GetOwinContext().Authentication.SignOut();
                    Response.Redirect("~/Management/index");
                }
                else
                {
                    WelcomeNameLabel.Text = currentUser.firstname;
                }
            }
            catch
            {
                Context.GetOwinContext().Authentication.SignOut();
                Response.Redirect("~/database_connection_failed.html");
            }  
        }
    }


    /* 
    CREATED: 	E. Lautner		Mar 22 2018

    Unnamed_LoggingOut()
    Logs the user out when the logout button is clicked.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    LoginCancelEventArgs e - optional class that provides data for a cancelable event

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }
}
