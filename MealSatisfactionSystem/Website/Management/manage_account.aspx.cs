using System;
using System.Linq;
using MSS.Data.Entities.Security;
using MSS.System.BLL.Security;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;
using System.Collections.Generic;

/* 
CREATED:    E. Lautner        APR 1 2018

Management_manage_account
An automatically generated page used to store the manage_account's backend C# code.

FIELDS:
string sentUserName - stores the username sent from accounts page

METHODS:
protected void Page_Load(object sender, EventArgs e) - triggered on page load 
protected void ModifyUser_Click(object sender, EventArgs e) - fires when update account button is clicked, gathers all provided information on the page to update the account
protected void DeactivateUser_Click (object sender, EventArgs e) - fires when deactivate account button is clicked, sets the accounts activeyn status to false
protected void ResetPassword_Click (object sender, EventArgs e) - fires when reset password button is clicked, deletes the old password and generates a new one for the selected account 
*/
public partial class Management_manage_account : System.Web.UI.Page
{
    string sentUserName = "";

    /* 
    CREATED: 	E. Lautner		APR 1 2018

    Page_Load()
    Run on page load and is used to display the selected accounts details

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowErrorMessage()
    UserManager.FindByName()
    UserManager.GetRoles()
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (AuthorizationLevelRolesRadioList.SelectedValue == AuthorizationLevelRoles.User)
            {
                CareSiteDDL.Visible = true;
                CareSiteLabel.Visible = true;
            }
            else
            {
                CareSiteDDL.Visible = false;
                CareSiteLabel.Visible = false;
            }
        }
        else
        {
            try
            {
                sentUserName = Request.QueryString["id"];
                if (sentUserName == "administratoraccount") //can't modify webmaster
                {
                    Response.Redirect("~/Management/accounts");
                }
                else
                {
                    if (sentUserName == null)
                    {
                        Response.Redirect("~/Management/accounts");
                    }
                    else
                    {
                        UsernameLabel.Text = sentUserName;

                        UserManager userManager = new UserManager();
                        var selectedUser = userManager.FindByName(sentUserName);

                        if (selectedUser == null)
                        {
                            Response.Redirect("~/Management/accounts");
                        }
                        if (selectedUser.activeyn == true)
                        {
                            PasswordBtn.Visible = true;
                            DeactivateAccountButton.Visible = true;
                            UpdateAccountButton.Visible = true;
                            FirstNameTB.Enabled = true;
                            LastNameTB.Enabled = true;
                            EmailTB.Enabled = true;
                            AuthorizationLevelRolesRadioList.Enabled = true;
                            CareSiteDDL.Enabled = true;

                            if (selectedUser.Id == Context.User.Identity.GetUserId())
                            {
                                DeactivateAccountButton.Visible = false;
                                AuthorizationLevelRolesRadioList.Enabled = false;
                            }
                        }
                        else
                        {
                            PasswordBtn.Visible = false;
                            DeactivateAccountButton.Visible = false;
                            UpdateAccountButton.Visible = false;
                            FirstNameTB.Enabled = false;
                            LastNameTB.Enabled = false;
                            EmailTB.Enabled = false;
                            AuthorizationLevelRolesRadioList.Enabled = false;
                            CareSiteDDL.Enabled = false;
                        }

                        var userRoles = userManager.GetRoles(selectedUser.Id);

                        string userRole = string.Join("", userRoles.ToArray());

                        FirstNameTB.Text = selectedUser.firstname;
                        LastNameTB.Text = selectedUser.lastname;
                        EmailTB.Text = selectedUser.Email;

                        CareSiteDDL.SelectedValue = selectedUser.caresiteid.ToString();
                        if (selectedUser.caresiteid == null)
                        {
                            CareSiteDDL.SelectedValue = "0";
                        }

                        AuthorizationLevelRolesRadioList.SelectedValue = userRole;

                        if (userRole == AuthorizationLevelRoles.Administrator || userRole == AuthorizationLevelRoles.Super_User)
                        {
                            CareSiteDDL.Visible = false;
                            CareSiteLabel.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageUserControl.ShowErrorMessage("Retrieving account information from the database failed. Please try again. If error persists, please contact your administrator.", ex);
            }
        }
    }

    /* 
    CREATED: 	E. Lautner		APR 1 2018
    MODIFIED:   C. Stanhope     APR 14 2018
        - changed validation to match the account_add validation

    ModifyUser_Click()
    Gathers all given information on the page about the selected account. Sends this information to the userManager so that the account can be updated.
     
    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowSuccessMessage()
    MessageUserControl.ShowErrorMessage()
    UserManager.ModifyAccount()
    UserManager.GetRoles()
    */
    protected void ModifyUser_Click(object sender, EventArgs e)
    {
        {
            sentUserName = Request.QueryString["id"];
            if (sentUserName == "" || sentUserName == null)
            {
                MessageUserControl.ShowErrorMessage("An account has not been selected. Please navigate back to the Account Search page and select an account. If error persists, please contact your administrator.");
            }

            else
            {
                //Retrieve the values from the controls
                string firstNameText = FirstNameTB.Text.Trim();
                string lastNameText = LastNameTB.Text.Trim();
                string emailText = EmailTB.Text.Trim();
                string authLevelText = AuthorizationLevelRolesRadioList.SelectedValue;
                int careSiteID = int.Parse(CareSiteDDL.Visible == false ? "0" : CareSiteDDL.SelectedValue);

                List<string> errorList = new List<string>();
                bool isValid = true;

                #region check if any inputs are blank
                if (string.IsNullOrWhiteSpace(firstNameText))
                {
                    errorList.Add("First Name");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(lastNameText))
                {
                    errorList.Add("Last Name");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(emailText))
                {
                    errorList.Add("Email");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(authLevelText))
                {
                    errorList.Add("Authorization Level");
                    isValid = false;
                }
                #endregion

                if (!isValid)
                {
                    ErrorMessagesAndValidation errMessAndVal = new ErrorMessagesAndValidation();
                    string errorMessage = errMessAndVal.ErrorList(errorList);
                    MessageUserControl.ShowInfoMessage(errorMessage);
                }
                else
                {
  
                    if (!emailText.Contains("@"))
                    {
                        MessageUserControl.ShowInfoMessage("Email must include an '@' symbol.");
                    }
                    else
                    {

                        if (System.Text.RegularExpressions.Regex.IsMatch(FirstNameTB.Text, @"^(?m)[A-Za-z][A-Za-z`. -]*$") && System.Text.RegularExpressions.Regex.IsMatch(LastNameTB.Text, @"^(?m)[A-Za-z][A-Za-z`. -]*$"))
                        {

                            if (int.Parse(CareSiteDDL.SelectedValue) == 0 && AuthorizationLevelRolesRadioList.SelectedValue == AuthorizationLevelRoles.User)
                            {
                                MessageUserControl.ShowInfoMessage("Authorization Level: User, must be associated with a care site");
                            }
                            else
                            {

                                try
                                {
                                    UserManager userManager = new UserManager();
                                    var selectedUser = userManager.FindByName(UsernameLabel.Text);
                                    var userRoles = userManager.GetRoles(selectedUser.Id);

                                    string userRole = string.Join("", userRoles.ToArray());

                                    string newUserName = userManager.ModifyAccount(UsernameLabel.Text, FirstNameTB.Text.Trim(), LastNameTB.Text.Trim(), EmailTB.Text.Trim(), int.Parse(CareSiteDDL.SelectedValue), userRole, AuthorizationLevelRolesRadioList.SelectedValue);
                                    if (newUserName != UsernameLabel.Text)
                                    {
                                        string resultMessage = string.Format("Update successful, new UserName is {0} ", newUserName);
                                        MessageUserControl.ShowSuccessMessage(resultMessage);
                                        UsernameLabel.Text = newUserName;
                                    }

                                    else
                                    {
                                        string resultMessage = string.Format("Update successful for user: {0}", UsernameLabel.Text);
                                        MessageUserControl.ShowSuccessMessage(resultMessage);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageUserControl.ShowErrorMessage("Update Failed. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message);
                                }

                            }
                        }
                        else
                        {
                            MessageUserControl.ShowInfoMessage("First Name and Last Name can only contain letters, dashes, apostrophes, grave accents, spaces and periods.");
                        }
                    }
                }

            }
        }
    }

    /* 
    CREATED: 	E. Lautner		Apr 1 2018

    DeactivateUser_Click()
    Deactivates the selected user by setting the users activeyn to false.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowSuccessMessage()
    MessageUserControl.ShowErrorMessage()
    UserManager.FindByName()
    UserManager.Update()
    */
    protected void DeactivateUser_Click(object sender, EventArgs e)
    {
        sentUserName = Request.QueryString["id"];

        if (sentUserName == "" || sentUserName == null)
        {
            MessageUserControl.ShowErrorMessage("An account has not been selected. Please navigate back to the Account Search page and select an account. If error persists, please contact your administrator.");
        }
        else
        {
            try
            {
                UserManager userManager = new UserManager();
                var selectedUser = userManager.FindByName(UsernameLabel.Text);

                if (selectedUser.activeyn == true)
                {
                    selectedUser.activeyn = false;

                    userManager.Update(selectedUser);
                    MessageUserControl.ShowSuccessMessage("Account has been deactivated.");

                    PasswordBtn.Visible = false;
                    DeactivateAccountButton.Visible = false;
                    UpdateAccountButton.Visible = false;
                    FirstNameTB.Enabled = false;
                    LastNameTB.Enabled = false;
                    EmailTB.Enabled = false;
                    AuthorizationLevelRolesRadioList.Enabled = false;
                    CareSiteDDL.Enabled = false;

                }
                else
                {
                    MessageUserControl.ShowErrorMessage("Account is already inactive. Inactive accounts should not be available to edit.");
                }
            }
            catch (Exception ex)
            {
                MessageUserControl.ShowErrorMessage("Account deactivation failed. Please try again. If error persists, please contact your administrator.", ex);
            }

        }
    }

    /* 
    CREATED: 	E. Lautner		Apr 1 2018

    ResetPassword_Click()
    Resets the password for the given account by deleting the old one and then generating a new one.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowSuccessMessage()
    UserManager.GenerateNewPassword()
    UserManager.FindByName()
    UserManager.RemovePassword()
    UserManager.AddPassword()
    MessageUserControl.ShowErrorMessage()
    */
    protected void ResetPassword_Click(object sender, EventArgs e)
    {
        sentUserName = Request.QueryString["id"];

        try
        {
            UserManager userManager = new UserManager();
            string newPassword = userManager.GenerateNewPassword();

            var selectedUser = userManager.FindByName(UsernameLabel.Text);
            userManager.RemovePassword(selectedUser.Id);
            userManager.AddPassword(selectedUser.Id, newPassword);

            string resultMessage = string.Format("Account password has been reset! UserName: {0} | Password: {1}", UsernameLabel.Text, newPassword);
            MessageUserControl.ShowSuccessMessage(resultMessage);
        }
        catch (Exception ex)
        {
            MessageUserControl.ShowErrorMessage("Reset password failed. Please try again. If error persists, please contact your administrator.", ex);
        }
    }
}
