using System;
using MSS.Data.Entities.Security;
using MSS.System.BLL.Security;
using Microsoft.AspNet.Identity;
using Website;

/* 
CREATED:    E. Lautner        MAR 3 2018

Management_index
An automatically generated page used to store the index backend C# code.

FIELDS:
None

METHODS:
protected void LogIn(object sender, EventArgs e) - attempts to Log the user into the management side of the website
*/
public partial class Management_index : System.Web.UI.Page
{
    /* 
    CREATED: 	E. Lautner		Mar 22 2018

    LogIn()
    Attempts to log in the user with the given password and username. If successful it will navigate to the dashboard.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowInfoMessage()
    UserManager.find()
    */
    protected void LogIn(object sender, EventArgs e)
    {
        if (IsValid)
        {
            // Validate the user password
            var manager = new UserManager();
            ApplicationUser user = manager.Find(UsernameTextBox.Text, PasswordTextBox.Text);

            //user must be active
            if (user != null && user.activeyn == true)
            {
                IdentityHelper.SignIn(manager, user, false);
                Response.Redirect("~/Management/dashboard");
              
            }
            else
            {
                MessageUserControl.ShowInfoMessage("Invalid username or password. If you have forgotten your account login, please contact your administrator.");
            }
        }
    }
}
