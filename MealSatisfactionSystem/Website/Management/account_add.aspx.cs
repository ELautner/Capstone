using MSS.Data.Entities.Security;
using MSS.System.BLL.Security;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/* 
CREATED:    P. Chavez         MAR 23 2018

Management_account_add
An automatically generated page used to store the Management_account_add's backend C# code. 

FIELDS:
None

METHODS: 
protected void Page_Load(object sender, EventArgs e) - triggers when the page is loaded
protected void AddAccountButton_Click(object sender, EventArgs e) - creates a new account based on inputted fields
*/
public partial class Management_account_add : System.Web.UI.Page
{
    /* 
    CREATED: 	P. Chavez		APR 12 2018

    Page_Load()
    Run on page load and displays the caresite drop down list based on the authorization level

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
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
    }

    /* 
    CREATED: 	P. Chavez		MAR 22 2018
    MODIFIED:   C. Stanhope     APR 5 2018
        - inputs now trim whitespace
    MODIFIED:   C. Stanhope     APR 14 2018
        - user messages formatted the same as other pages

    AddAccountButton_Click()
    This method creates a new account based on the input fields when the button is clicked.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    ErrorMessagesAndValidation.ErrorList()
    MessageUserControl.ShowInfoMessage()
    UserManager.GenerateNewPassword()
    UserManager.AddAccountUser()
    MessageUserControl.ShowSuccessMessage()
    MessageUserControl.ShowErrorMessage()
    MessageUserControl.ShowInfoList()
    */
    protected void AddAccountButton_Click(object sender, EventArgs e)
    {
        //Retrieve the values from the controls
        string firstNameText = FirstNameTextBox.Text.Trim();
        string lastNameText = LastNameTextBox.Text.Trim();
        string emailText = EmailTextBox.Text.Trim();
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
            bool matchRegex = true;
            List<string> regexErrorList = new List<string>();

            Regex regexName = new Regex(@"^(?m)[A-Za-z][A-Za-z`. -]*$", RegexOptions.IgnoreCase);
            if (!regexName.Match(firstNameText).Success)
            {
                matchRegex = false;
                regexErrorList.Add("First Name can only contain letters, dashes, apostrophes, grave accents, spaces and periods.");
            }
            if (!regexName.Match(lastNameText).Success)
            {
                matchRegex = false;
                regexErrorList.Add("Last Name can only contain letters, dashes, apostrophes, grave accents, spaces and periods.");
            }

            if(!emailText.Contains("@"))
            {
                matchRegex = false;
                regexErrorList.Add("Email must include an '@' symbol.");
            }

            if(careSiteID == 0 && authLevelText == AuthorizationLevelRoles.User)
            {
                matchRegex = false;
                regexErrorList.Add("The Authorization Level User must have a Care Site associated with the account.");
            }

            if (matchRegex)
            {
                try
                {
                    UserManager userManager = new UserManager();
                    string newPassword = userManager.GenerateNewPassword();
                    ApplicationUser newUserAccount = userManager.AddAccountUser(firstNameText, lastNameText, emailText, authLevelText, careSiteID, newPassword);
                    string resultMessage = string.Format("The new account was created! UserName: {0} | Password: {1}", newUserAccount.UserName, newPassword);
                    MessageUserControl.ShowSuccessMessage(resultMessage);

                    //Reset the fields
                    FirstNameTextBox.Text = "";
                    LastNameTextBox.Text = "";
                    EmailTextBox.Text = "";
                    AuthorizationLevelRolesRadioList.SelectedValue = AuthorizationLevelRoles.User;
                    CareSiteDDL.SelectedValue = "0";
                    CareSiteDDL.Visible = true;
                    CareSiteLabel.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Adding account failed. Please try again. If error persists, please contact your administrator.", ex);
                }
            }
            else // A regex didn't match
            {
                MessageUserControl.ShowInfoList("The following errors caused adding a management account to fail: ", regexErrorList);
            }
        }
    }
}
