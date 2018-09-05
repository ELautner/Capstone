using MSS.Data.DTOs;
using MSS.Data.Entities.Security;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using MSS.System.BLL.Security;
using System.Text.RegularExpressions;
using System.Globalization;


/* 
CREATED:	H. Conant		MAR 4 2018

Management_surveys
Contains methods necessary to display surveys to users. This page refreshes
if the back button is pressed. A message is displayed on the screen warning of this.

FIELDS:
internal UnitController unitController - allows methods from the controller to be called
internal SurveyController surveyController - allows methods from the controller to be called

METHODS:
protected void Page_Load(object sender, EventArgs e) - this method runs any code it contains when the page loads
protected void RetrieveUnitList_Select(object sender, EventArgs e) - this method retrieves the units of a specified care site 
protected void RetrieveSurveys_Click(object sender, EventArgs e) - this method retrieves surveys from the database based off the filters entered
protected void ResetPage_Click(object sender, EventArgs e) - this method resets the page 
*/
public partial class Management_surveys : System.Web.UI.Page
{
    internal UnitController unitController = new UnitController();
    internal SurveyController surveyController = new SurveyController();

    /* 
    CREATED: 	H. Conant		MAR 4 2018
    MODIFIED:   H. Conant       MAR 20 2018
        - Added method body

    Page_Load()
    This method runs any code it contains when the page loads.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    UserManager.FindById()
    MessageUserControl.ShowErrorMessage()
    CareSiteController.GetCareSiteByCareSiteID()
    UnitController.GetCareSiteUnits()
    UnitController.GetAllUnits()
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.IsInRole(AuthorizationLevelRoles.User))
            {
                UserManager userManager = new UserManager();

                ApplicationUser account = new ApplicationUser();

                try
                {
                    account = userManager.FindById(User.Identity.GetUserId());
                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Your account has no assigned care site. Please contact your administrator to be assigned a care site.", ex);

                }

                CareSiteController careSiteController = new CareSiteController();

                int accountCareSiteId = account.caresiteid == null ? 0 : (int)account.caresiteid;

                if (accountCareSiteId == 0)
                {
                    MessageUserControl.ShowErrorMessage("Your account has no assigned care site. Please contact your administrator to be assigned a care site.");

                }
                else
                {
                    try
                    {
                        CareSiteDDL.DataSourceID = null;
                        CareSiteDDL.AppendDataBoundItems = false;
                        List<CareSiteDTO> tempCareSiteList = new List<CareSiteDTO>();
                        tempCareSiteList.Add(careSiteController.GetCareSiteByCareSiteID(accountCareSiteId));
                        CareSiteDDL.DataSource = tempCareSiteList;
                        CareSiteDDL.DataBind();

                        List<UnitDTO> tempUnitList = unitController.GetCareSiteUnits(accountCareSiteId);
                        UnitRepeater.DataSource = tempUnitList;
                        UnitRepeater.DataBind();
                        unitsdiv.Attributes.Remove("style");

                    }
                    catch (Exception ex)
                    {
                        MessageUserControl.ShowErrorMessage("Retrieving user care site and/or units from the database failed. Please try again. If error persists, please contact your administrator.", ex);
                    }
                }

            }
            else
            {

                if (CareSiteDDL.SelectedValue == "0")
                {
                    try
                    {
                        int tempCareSiteId;
                        int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
                        List<UnitDTO> tempUnitList = unitController.GetAllUnits();
                        UnitRepeater.DataSource = tempUnitList;
                        UnitRepeater.DataBind();
                        unitsdiv.Attributes.Add("style", "display:none");

                    }
                    catch (Exception ex)
                    {
                        MessageUserControl.ShowErrorMessage("Retrieving units from the database failed. Please try again. If error persists, please contact your administrator.", ex);
                    }
                }
                else
                {
                    try
                    {
                        int tempCareSiteId;
                        int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
                        List<UnitDTO> tempUnitList = unitController.GetCareSiteUnits(tempCareSiteId);
                        UnitRepeater.DataSource = tempUnitList;
                        UnitRepeater.DataBind();
                        unitsdiv.Attributes.Remove("style");

                    }
                    catch (Exception ex)
                    {
                        MessageUserControl.ShowErrorMessage("Retrieving units from the database failed. Please try again. If error persists, please contact your administrator.", ex);
                    }

                }
            }
        }
    }

    /* 
    CREATED: 	H. Conant		MAR 13 2018
    MODIFIED:   H. Conant       MAR 20 2018
         - Updated method signature

    RetrieveUnitList_Select()
    This method runs any code it contains when the index changes for the care site drop down.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    UnitController.GetCareSiteUnits()
    MessageUserControl.ShowErrorMessage()
    UnitController.GetAllUnits()
    */
    protected void RetrieveUnitList_Select(object sender, EventArgs e)
    {
        SurveyRepeater.DataSource = null;
        SurveyRepeater.DataBind();

        ResultCountLabel.Text = "-";

        if (User.IsInRole(AuthorizationLevelRoles.User))
        {
            try
            {
                int tempCareSiteId;
                int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
                List<UnitDTO> tempUnitList = unitController.GetCareSiteUnits(tempCareSiteId);
                UnitRepeater.DataSource = tempUnitList;
                UnitRepeater.DataBind();
                unitsdiv.Attributes.Remove("style");

            }
            catch (Exception ex)
            {
                MessageUserControl.ShowErrorMessage("Retrieving units from the database failed. Please try again. If error persists, please contact your administrator.", ex);
            }
            
        }
        else
        {
            if (CareSiteDDL.SelectedValue == "0")
            {
                try
                {
                    int tempCareSiteId;
                    int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
                    List<UnitDTO> tempUnitList = unitController.GetAllUnits();
                    UnitRepeater.DataSource = tempUnitList;
                    UnitRepeater.DataBind();
                    unitsdiv.Attributes.Add("style", "display:none");


                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Retrieving units from the database failed. Please try again. If error persists, please contact your administrator.", ex);
                }
                
            }
            else
            {
                try
                {
                    int tempCareSiteId;
                    int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
                    List<UnitDTO> tempUnitList = unitController.GetCareSiteUnits(tempCareSiteId);
                    UnitRepeater.DataSource = tempUnitList;
                    UnitRepeater.DataBind();
                    unitsdiv.Attributes.Remove("style");

                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Retrieving units from the database failed. Please try again. If error persists, please contact your administrator.", ex);
                }
               
            }
        }
    }

    /* 
    CREATED: 	H. Conant		MAR 13 2018
    MODIFIED:   A. Valberg      MAR 28 2018
        -Updated fields after adding 2 tables (agegroup & gender)
    MODIFIED:   H. Conant		MAR 31 2018
        -Updated gender and age to repeaters and ODSes

    RetrieveSurveys_Click()
    This method runs any code it contains when the "Retrieve Surveys" button is clicked.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowInfoMessage()
    MessageUserControl.ShowInfoList()
    SurveyController.GetSurveys()
    MessageUserControl.ShowErrorMessage()
    */
    protected void RetrieveSurveys_Click(object sender, EventArgs e)
    {
        ResultCountLabel.Text = "0";
        List<string> message = new List<string>();

        SurveyRepeater.DataSource = null;
        SurveyRepeater.DataBind();
        List<int> unitIds = new List<int>();
        List<int> genders = new List<int>();
        List<int> respodentIds = new List<int>();
        List<int> ages = new List<int>();
        DateTime dateOne = new DateTime();
        DateTime dateTwo = new DateTime();

        
        string pattern = @"^$|^(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/][0-9]{4}$";

        Regex reg = new Regex(pattern);

        Match properStartDateFormat = reg.Match(dateStart.Value);
        Match properEndDateFormat = reg.Match(dateEnd.Value);

        if (!properStartDateFormat.Success || !properEndDateFormat.Success)
        {
            message.Add("Please enter valid date(s) using the pattern mm/dd/yyyy or leave the date fields blank.");
        }
        else
        {
            if (dateStart.Value != "")
            {
                if (DateTime.TryParse(dateStart.Value, out dateOne) == false)
                {
                    message.Add("Please enter a valid start date value or leave it blank.");
                }
            }

            if (dateEnd.Value != "")
            {
                if (DateTime.TryParse(dateEnd.Value, out dateTwo) == false)
                {
                    message.Add("Please enter a valid end date value or leave it blank.");
                }
            }
        }
        

        if (dateStart.Value != "" && dateEnd.Value != "" && dateOne > dateTwo)
        {
            message.Add("Please ensure that the provided end date occurs after or on the same day as the provided start date.");
        }

        if (dateTwo > DateTime.Today || dateOne > DateTime.Today)
        {
            message.Add("Please select dates that are on or before today's date.");
        }

        foreach (RepeaterItem item in UnitRepeater.Items)
        {
            CheckBox tempCheckBox = (CheckBox)item.FindControl("CheckBox");
            HiddenField tempHiddenField = (HiddenField)item.FindControl("HiddenUnitID");

            if (tempCheckBox.Text.Length > 10)
            {
                tempCheckBox = (CheckBox)item.FindControl("CheckBox1");
            }
            if (tempCheckBox.Checked == true)
            {
                int tempUntId;
                int.TryParse(tempHiddenField.Value, out tempUntId);
                unitIds.Add(tempUntId);
            }

        }

        if (unitIds.Count < 1)
        {
            message.Add("Please select at least one unit to filter by.");
        }

        foreach (RepeaterItem item in GenderRepeater.Items)
        {
            CheckBox tempCheckBox = (CheckBox)item.FindControl("CheckBox");
            HiddenField tempHiddenField = (HiddenField)item.FindControl("HiddenGenderID");

            if (tempCheckBox.Text.Length > 10)
            {
                tempCheckBox = (CheckBox)item.FindControl("CheckBox1");
            }
            if (tempCheckBox.Checked == true)
            {
                int tempGenderId;
                int.TryParse(tempHiddenField.Value, out tempGenderId);
                genders.Add(tempGenderId);
            }

        }

        if (genders.Count < 1)
        {
            message.Add("Please select at least one gender to filter by.");
        }

        foreach (RepeaterItem item in RespondentTypeRepeater.Items)
        {
            CheckBox tempCheckBox = (CheckBox)item.FindControl("CheckBox");
            HiddenField tempHiddenField = (HiddenField)item.FindControl("HiddenRespondentID");

            if (tempCheckBox.Text.Length > 10)
            {
                tempCheckBox = (CheckBox)item.FindControl("CheckBox1");
            }
            if (tempCheckBox.Checked == true)
            {
                int tempTypeId;
                int.TryParse(tempHiddenField.Value, out tempTypeId);
                respodentIds.Add(tempTypeId);
            }

        }

        if (respodentIds.Count < 1)
        {
            message.Add("Please select at least one respondent type to filter by.");

        }

        foreach (RepeaterItem item in AgeRepeater.Items)
        {
            CheckBox tempCheckBox = (CheckBox)item.FindControl("CheckBox");
            HiddenField tempHiddenField = (HiddenField)item.FindControl("HiddenAgeGroupID");

            if (tempCheckBox.Text.Length > 10)
            {
                tempCheckBox = (CheckBox)item.FindControl("CheckBox1");
            }
            if (tempCheckBox.Checked == true)
            {
                int tempAgeId;
                int.TryParse(tempHiddenField.Value, out tempAgeId);
                ages.Add(tempAgeId);
            }

        }

        if (ages.Count < 1)
        {
            message.Add("Please select at least one age range to filter by.");

        }

        if (message.Count == 1)
        {

            MessageUserControl.ShowInfoMessage(message[0]);

        }
        else if (message.Count > 1)
        {
            MessageUserControl.ShowInfoList("Please address the following errors and try again: ", message);
        }
        else
        {

            try
            {
                List<ExtendedSurveyDTO> surveys = new List<ExtendedSurveyDTO>();

                if (dateStart.Value == "" && dateEnd.Value == "")
                {
                    surveys = surveyController.GetSurveys(unitIds, genders, ages, respodentIds);

                }
                else if (dateStart.Value == "" && dateEnd.Value != "")
                {
                    dateOne = DateTime.MinValue;

                    surveys = surveyController.GetSurveys(unitIds, genders, ages, dateOne, dateTwo, respodentIds);
                }
                else if (dateStart.Value != "" && dateEnd.Value == "")
                {
                    dateTwo = DateTime.Today;
                    dateEnd.Value = DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    surveys = surveyController.GetSurveys(unitIds, genders, ages, dateOne, dateTwo, respodentIds);

                }
                else
                {

                    surveys = surveyController.GetSurveys(unitIds, genders, ages, dateOne, dateTwo, respodentIds);
                }


                if (surveys.Count < 1)
                {
                    MessageUserControl.ShowInfoMessage("No surveys found matching the entered search criteria.");


                }
                else
                {
                    ResultCountLabel.Text = (surveys.Count).ToString();

                    SurveyRepeater.DataSource = surveys;
                    SurveyRepeater.DataBind();
                }

            }
            catch (Exception ex)
            {
                MessageUserControl.ShowErrorMessage("Retrieving surveys from the database failed. Please try again. If error persists, please contact your administrator.", ex);

            }
        }

    }

    /* 
    CREATED: 	H. Conant		APR 04 2018

    ResetPage_Click()
    This method runs any code it contains when the "ResetPage" button is clicked.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void ResetPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("surveys");
    }
}
