using System;
using MSS.System.BLL.Security;
using Microsoft.AspNet.Identity;

/* 
CREATED:    E. Lautner        MAR 3 2018

Management_my_account
An automatically generated page used to store the my_account's backend C# code.

FIELDS:
None

METHODS:
protected void Page_Load(object sender, EventArgs e) - triggered on page load 
protected void SetPassword_Click(object sender, EventArgs e) - attempts to change the password on UpdatePasswordButton click
*/

public partial class Management_my_account : System.Web.UI.Page
{

    /* 
    CREATED: 	E. Lautner		Mar 22 2018
    MODIFIED:   P. Chavez       Apr 10 2018
        - Updated Page_Load method to check for authentication

    Page_Load()
    Run on page load and is used to display the my_account details on the page.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

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
            string userId = Context.User.Identity.GetUserId();
            UserManager userManager = new UserManager();
            var currentUser = userManager.FindById(userId);

            UsernameLabel.Text = currentUser.UserName;
            FirstNameLabel.Text = currentUser.firstname;
            LastNameLabel.Text = currentUser.lastname;
            EmailLabel.Text = currentUser.Email;
        }
    }

    /* 
    CREATED: 	E. Lautner		Mar 22 2018

    SetPassword_Click()
    Checks that the new password is identical in ConfirmPassTextBox and NewPassTextBox.
    Attempts to change the password by passing the old password and new password.
    Prompts MessageUserControl when action fails or passes.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowSuccessMessage()
    MessageUserControl.ShowErrorMessage()
    UserManager.FindById()
    UserManager.CheckPassword()
    MessageUserControl.ShowInfoMessage()
    */
    protected void SetPassword_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (ConfirmPassTextBox.Text == NewPassTextBox.Text)
            {
                if (NewPassTextBox.Text.Length < 8)
                {
                    MessageUserControl.ShowInfoMessage("New password must be at least 8 characters long.");
                }
                else
                {
                    try
                    {
                        string userId = Context.User.Identity.GetUserId();
                        UserManager userManager = new UserManager();
                        var currentUser = userManager.FindById(userId);
                        bool checkpassword = userManager.CheckPassword(currentUser, OldPassTextBox.Text);
                        if (checkpassword == true)
                        {
                            IdentityResult result = userManager.ChangePassword(Context.User.Identity.GetUserId(), OldPassTextBox.Text, NewPassTextBox.Text);

                            if (result.Succeeded)
                            {
                                MessageUserControl.ShowSuccessMessage("Password successfully updated.");
                            }
                        }
                        else
                        {
                            MessageUserControl.ShowInfoMessage("Old password was incorrect. Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageUserControl.ShowErrorMessage("Password change failed. Please try again. If error persists, please contact your administrator.", ex);
                    }
                }
            }
            else
            {
                MessageUserControl.ShowInfoMessage("New Password and Confirm Password did not match. Please try again.");
            }
        }
    }
}
