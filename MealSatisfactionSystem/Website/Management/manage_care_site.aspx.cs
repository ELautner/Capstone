using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

/* 
CREATED:	C. Stanhope		MAR 23 2018

Management_manage_care_site
An automatically generated page used to store the manage_care_site's backend C# code.

FIELDS:
internal CareSiteController careSiteController - allows methods from the controller to be called
internal static CareSiteDTO selectedCareSite - the currently selected care site from the drop down list

METHODS:
protected void Page_Load(object sender, EventArgs e) - triggered when page is loaded, used to show and hide sections of page
protected void CareSiteDDL_SelectedIndexChanged(object sender, EventArgs e) - triggered when care site DDL changed, used to populate care site form and show appropriate sections of page
protected void UpdateCareSiteButton_Click(object sender, EventArgs e) - updates the selected care site in the database
protected void DeactivateCareSiteButton_Click(object sender, EventArgs e) - deactivates the selected care site
*/
public partial class Management_manage_care_site : System.Web.UI.Page
{
    internal CareSiteController careSiteController = new CareSiteController();
    internal static CareSiteDTO selectedCareSite;

    /* 
    CREATED: 	C. Stanhope		MAR 23 2018

    Page_Load()
    Triggered on page load and is used to show or hide the form depending on whether the user has selected a care site from the drop-down list.

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
        if (CareSiteDDL.SelectedIndex == 0)
        {
            AccountForm.Visible = false;
            selectedCareSite = null;
        }
        else
        {
            AccountForm.Visible = true;
        }
    }

    /* 
    CREATED: 	C. Stanhope		MAR 23 2018

    CareSiteDDL_SelectedIndexChanged()
    Triggered when the care site DDL's selected field is changed. If the DDL is changed to "Select...", a message is displayed and the manage care site form is hidden. If the user selects a care site, the manage care site form is displayed and populated with the proper information. 

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void
    
    ODEV METHOD CALLS: 
    MessageUserControl.ShowInfoMessage()
    CareSiteController.GetCareSiteByCareSiteID()
    MessageUserControl.ShowErrorMessage()
    */
    protected void CareSiteDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(CareSiteDDL.SelectedIndex == 0)
        {
            MessageUserControl.ShowInfoMessage("Please select a care site to manage.");
            AccountForm.Visible = false;
            selectedCareSite = null;
        }
        else
        {
            int selectedID;

            if (int.TryParse(CareSiteDDL.SelectedValue, out selectedID))
            {
                selectedCareSite = careSiteController.GetCareSiteByCareSiteID(selectedID);

                // populate form with info from database
                CareSiteNameTextBox.Text = selectedCareSite.caresitename;
                AddressTextBox.Text = selectedCareSite.address;
                CityTextBox.Text = selectedCareSite.city;
                ProvinceDDL.SelectedValue = selectedCareSite.province;
            }
            else
            {
                MessageUserControl.ShowErrorMessage("An error occurred converting the care site drop-down list value. Please try again. If error persists, please contact your administrator.");
            }
        }
    }

    /* 
    CREATED: 	C. Stanhope		MAR 23 2018
    MODIFIED:   C. Stanhope     MAR 24 2018
        - Added validation, finished method
    MODIFIED:   C. Stanhope     APR 5 2018
        - trim whitespace on inputs
        - validation now matches add care site
    MODIFIED:   C. Stanhope     APR 6 2018
        - added try-catch for database access
    MODIFIED:   C. Stanhope     APR 17 2018
        - fixed city regex to limit at 30 characters
        - fixed care site regex to limit at 80 characters

    UpdateCareSiteButton_Click()
    Used to update a care site in the database. Validates all input fields and updates only if valid.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void
    
    ODEV METHOD CALLS: 
    MessageUserControl.ShowInfoMessage()
    CareSiteController.GetCareSiteByCareSiteID()
    MessageUserControl.ShowErrorMessage()
    ErrorMessagesAndValidation.ErrorList()
    */
    protected void UpdateCareSiteButton_Click(object sender, EventArgs e)
    {
        if (selectedCareSite != null)
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
                errorList.Add("Address must be a minimum of 1 character and a maximum of 40 characters long. It can contain letters, numbers, and the following symbols: # . -");
                isValid = false;
            }

            string city = CityTextBox.Text.Trim();
            if (!cityRegex.IsMatch(city))
            {
                errorList.Add("City must be a minimum of 1 letter and a maximum of 30 characters long. It must start with a letter and can contain letters and the following symbols: . -");
                isValid = false;
            }

            string province = ProvinceDDL.SelectedValue;

            #endregion

            if (isValid)
            {
                #region see if data changed
                bool dataChanged = false;
                if(selectedCareSite.caresitename != careSiteName)
                {
                    dataChanged = true;
                }
                else if(selectedCareSite.address != address)
                {
                    dataChanged = true;
                }
                else if(selectedCareSite.city != city)
                {
                    dataChanged = true;
                }
                else if(selectedCareSite.province != province)
                {
                    dataChanged = true;
                }
                #endregion

                if (dataChanged)
                {
                    #region put valid data into selectedCareSite, add to database, show success message, change DDL
                    selectedCareSite.caresitename = careSiteName;
                    selectedCareSite.address = address;
                    selectedCareSite.city = city;
                    selectedCareSite.province = province;

                    try
                    {
                        careSiteController.UpdateCareSite(selectedCareSite);

                        MessageUserControl.ShowSuccessMessage("The " + selectedCareSite.caresitename + " care site was successfully updated.");

                        CareSiteDDL.SelectedItem.Text = selectedCareSite.caresitename;
                    }
                    catch (Exception ex)
                    {
                        MessageUserControl.ShowErrorMessage("Updating care site failed. Please try again. If error persists, please contact your administrator.", ex);
                    }
                    #endregion
                }
                else
                {
                    MessageUserControl.ShowInfoMessage("No changes to save.");
                }

            }
            else
            {
                #region show user message with "errors"

                ErrorMessagesAndValidation errMessAndVal = new ErrorMessagesAndValidation();

                string errorMessage = errMessAndVal.ErrorList(errorList);

                //MessageUserControl.ShowInfoMessage(errorMessage);
                MessageUserControl.ShowInfoList("The following errors caused adding a care site to fail: ", errorList);
                #endregion
            }
        }
        else // no care site selected
        {
            MessageUserControl.ShowErrorMessage("No care site selected. The \"Update Care Site\" button should not be available if no care site is selected. Please try again. If error persists, please contact your administrator.");
        }
    }

    /* 
    CREATED: 	C. Stanhope		MAR 23 2018
    MODIFIED:   C. Stanhope     APR 6 2018
        - added try-catch for database access

    DeactivateCareSiteButton_Click()
    Used to deactivate a care site in the database.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void
    
    ODEV METHOD CALLS: 
    MessageUserControl.ShowInfoMessage()
    CareSiteController.GetCareSiteByCareSiteID()
    MessageUserControl.ShowErrorMessage()
    */
    protected void DeactivateCareSiteButton_Click(object sender, EventArgs e)
    {
        if(selectedCareSite != null)
        {
            try
            {
                careSiteController.DeactivateCareSite(selectedCareSite);

                MessageUserControl.ShowSuccessMessage("The " + selectedCareSite.caresitename + " care site was successfully deactivated.");

                CareSiteDDL.SelectedIndex = 0;
                CareSiteNameTextBox.Text = "";
                AddressTextBox.Text = "";
                CityTextBox.Text = "";
                AccountForm.Visible = false;

                #region resetting care site ddl
                CareSiteDDL.AppendDataBoundItems = false; // clears old values
                CareSiteDDL.DataSourceID = null;

                // get new list of care sites
                List<CareSiteDTO> ddlCareSites = careSiteController.GetActiveCareSites();

                // create a fake care site that acts as a "select" in the ddl
                CareSiteDTO fakeSelectCareSite = new CareSiteDTO();
                fakeSelectCareSite.caresiteid = 0;
                fakeSelectCareSite.caresitename = "Select...";

                ddlCareSites.Add(fakeSelectCareSite);

                // put the new "select" care site at the top of the list
                ddlCareSites = ddlCareSites.OrderBy(site => site.caresiteid).ToList();

                // bind data source
                CareSiteDDL.DataSource = ddlCareSites;
                CareSiteDDL.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                MessageUserControl.ShowErrorMessage("Deactivating care site failed. Please try again. If error persists, please contact your administrator.", ex);
            }
        }
        else
        {
            MessageUserControl.ShowErrorMessage("No care site selected. The \"Deactivate Care Site\" button should not be available if no care site is selected. Please try again. If error persists, please contact your administrator.");
        }
    }
}
