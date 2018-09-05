using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/* 
CREATED:    C. Stanhope         MAR 23 2018

Management_care_site_add
An automatically generated page used to store the care_site_add's backend C# code.

FIELDS:
None

METHODS: 
protected void AddCareSiteButton_Click(object sender, EventArgs e) - adds a new care site to the database
*/
public partial class Management_care_site_add : System.Web.UI.Page
{
    /* 
    CREATED: 	C. Stanhope		MAR 23 2018
    MODIFIED:   C. Stanhope     MAR 24 2018
        - moved error list method to its own class and call it
        - changed validation to account for care site name length
    MODIFIED:   C. Stanhope     APR 5 2018
        - trim whitespace on inputs
    MODIFIED:   C. Stanhope     APR 6 2018
        - added try-catch for database access
    MODIFIED:   C. Stanhope     APR 17 2018
        - fixed city regex to limit at 30 characters
        - fixed care site regex to limit at 80 characters

    AddCareSiteButton_Click()
    Used to add a care site to the database. Validates all input fields.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void
    
    ODEV METHOD CALLS: 
    CareSiteController.AddCareSite()
    MessageUserControl.ShowSuccessMessage()
    MessageUserControl.ShowInfoMessage()
    ErrorMessagesAndValidation.ErrorList()
    */
    protected void AddCareSiteButton_Click(object sender, EventArgs e)
    {
        bool isValid = true;
        List<string> errorList = new List<string>();

        #region regexes for validation
        Regex careSiteRegex = new Regex(@"^[A-z]{1}[A-z 0-9 .-]{4,79}$");
        Regex addressRegex = new Regex(@"^[A-z 0-9 .#-]{1,40}$");
        Regex cityRegex = new Regex(@"^[A-z]{1}[A-z .-]{0,29}$");
        #endregion

        #region get values from page and validate
        string careSiteName = CareSiteNameTextBox.Text.Trim();
        if (!careSiteRegex.IsMatch(careSiteName))
        {
            errorList.Add("Care site must be a minimum of 5 characters and a maximum of 80 characters long. It must start with a letter and can contain letters, numbers, and the following symbols: . -");
            isValid = false;
        }

        string address = AddressTextBox.Text.Trim();
        if (!addressRegex.IsMatch(address))
        {
            errorList.Add("Address must be a minimum of 1 letter and a maximum of 40 characters long. It can contain letters, numbers, and the following symbols: # . -");
            isValid = false;
        }

        string city = CityTextBox.Text.Trim();
        if (!cityRegex.IsMatch(city))
        {
            errorList.Add("City must be a minimum of 1 letter and a maximum of 30 characters long. It must start with a letter and can contain letters and the following symbols: . -");
            isValid = false;
        }
        
        string province = ProvinceDDL.SelectedValue;
        if (ProvinceDDL.SelectedIndex == 0)
        {
            errorList.Add("A province must be selected from the drop-down list");
            isValid = false;
        }

        #endregion

        if (isValid)
        {
            #region put valid data into DTO, add to database, show success message, clear page
            CareSiteDTO newCareSite = new CareSiteDTO();

            newCareSite.caresitename = careSiteName;
            newCareSite.address = address;
            newCareSite.city = city;
            newCareSite.province = province;

            CareSiteController careSiteController = new CareSiteController();
            try
            {
                careSiteController.AddCareSite(newCareSite);

                // show message, clear page
                MessageUserControl.ShowSuccessMessage("New care site " + careSiteName + " was successfully added. Please navigate to the Manage Units page to add units to the new care site.");

                CareSiteNameTextBox.Text = "";
                AddressTextBox.Text = "";
                CityTextBox.Text = "";
                ProvinceDDL.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageUserControl.ShowErrorMessage("Adding care site failed. Please try again. If error persists, please contact your administrator.", ex);
            }
            #endregion
        }
        else
        {
            MessageUserControl.ShowInfoList("The following errors caused adding a care site to fail: ", errorList);
        }
    }
}
