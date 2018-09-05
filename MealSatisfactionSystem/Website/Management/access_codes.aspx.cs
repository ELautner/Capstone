using Microsoft.AspNet.Identity;
using MSS.Data.DTOs;
using MSS.Data.Entities.Security;
using MSS.System.BLL;
using MSS.System.BLL.Security;
using System;
using System.Collections.Generic;

/* 
CREATED:    C. Stanhope        MAR 3 2018

Management_access_codes
An automatically generated page used to store the access_codes's backend C# code.

FIELDS:
internal CareSiteAccessController careSiteAccessController - initialising the CareSiteAccessController to be used by the methods in the class
internal UnitController unitController - initialising the UnitController to be used by the methods in the class

METHODS:
protected void Page_Load(object sender, EventArgs e) - displays today's and tomorrow's codes for Users who only have one care site
protected void CareSiteDDL_SelectedIndexChanged(object sender, EventArgs e) - refreshes the page to reflect the newly selected care site
private void DisplayAccessCodes(int careSiteId) - displays access codes based on the care site ID passed
*/
public partial class Management_access_codes : System.Web.UI.Page
{
    internal CareSiteAccessController careSiteAccessController = new CareSiteAccessController();
    internal UnitController unitController = new UnitController();

    /* 
    CREATED: 	C. Stanhope		Mar 3 2018
    MODIFIED:   P. Chavez       MAR 27 2018
        - Added to method body, used Holly's code

    Page_Load()
    Run on page load and is used to display today's and tomorrow's access codes if a care site is selected from the drop-down list.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS:
    void
    
    ODEV METHOD CALLS:
    UserManager.FindById()
    MessageUserControl.ShowErrorMessage()
    CareSiteController.GetCareSiteByCareSiteID()
    DisplayAccessCodes()
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CareSiteDDL.SelectedIndex == 0)
        {
            CodeCards.Visible = false;
        }

        if (User.IsInRole(AuthorizationLevelRoles.User))
        {
            UserManager userManager = new UserManager();
            ApplicationUser account = userManager.FindById(User.Identity.GetUserId());
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

                    DisplayAccessCodes(accountCareSiteId);
                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Could not retrieve User's care site. Please try again. If error persists, please contact your administrator.", ex);
                }
            }
        }
    }

    /* 
    CREATED: 	C. Stanhope		Mar 11 2018

    CareSiteDDL_SelectedIndexChanged()
    Refreshes the page data to reflect the change made in the drop-down list, showing either an error or the access codes for the newly selected care site.

    PARAMETERS: 	
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS:
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowInfoMessage()
    MessageUserControl.ShowErrorMessage()
    DisplayAccessCodes()
    */
    protected void CareSiteDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(CareSiteDDL.SelectedIndex == 0)
        {
            MessageUserControl.ShowInfoMessage("No care site selected. Please select a care site.");
            CodeCards.Visible = false;
        }
        else // care site was selected
        {
            int selectedCareSiteID;
            if(int.TryParse(CareSiteDDL.SelectedValue, out selectedCareSiteID))
            {
                DisplayAccessCodes(selectedCareSiteID);
            }
            else // the tryparse of the care site ID didn't work
            {
                MessageUserControl.ShowErrorMessage("Selected care site not recognised. Please try again. If error persists, please contact your administrator.");
            }
                      
        }
    }

    /* 
    CREATED: 	P. Chavez		MAR 29 2018
    MODIFIED:   C. Stanhope     APR 6 2018
        - added try-catch for database access
    MODIFIED:   C. Stanhope     APR 14 2018
        - codes will show themselves independent of each other. You no longer need to have today's code AND tomorrow's code, any that have been generated will be displayed.

    DisplayAccessCodes()
    Displays today's and tomorrow's access codes for a care site based on the care site ID passed.

    PARAMETERS: 	
    int careSiteId - the ID of the care site

    RETURNS:
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowErrorMessage()
    CareSiteAccessController.GetCareSiteAccessCodeByDate()
    */
    private void DisplayAccessCodes(int careSiteId)
    {
        try
        {
            // get access codes by today's and tomorrow's dates and the selected caresite
            AccessCodeDTO accessCodeToday = careSiteAccessController.GetCareSiteAccessCodeByDate(careSiteId, DateTime.Now);
            AccessCodeDTO accessCodeTomorrow = careSiteAccessController.GetCareSiteAccessCodeByDate(careSiteId, DateTime.Now.AddDays(1));

            bool anActiveCodeExists = false;

            #region checking and assigning today's code
            if (accessCodeToday.accesscodeid == -3)
            {
                MessageUserControl.ShowErrorMessage("Today's code is not yet generated. Please return to the dashboard (welcome page) and navigate back to this page. If the error persists, please contact your administrator.");
                TodaysCard.Visible = false;
            }
            else // Today's code exists
            {
                CodeCards.Visible = true;
                TodaysCard.Visible = true;

                anActiveCodeExists = true;
                TodayCodeLabel.Text = accessCodeToday.accesscodeword;
                TodayDateLabel.Text = DateTime.Now.ToString("MMM d, yyyy");
            }
            #endregion

            #region checking and assigning tomorrow's code
            if (accessCodeTomorrow.accesscodeid == -3)
            {
                MessageUserControl.ShowErrorMessage("Tomorrow's code is not yet generated. Please return to the dashboard (welcome page) and navigate back to this page. If the error persists, please contact your administrator.");
                TomorrowsCard.Visible = false;
            }
            else // Tomorrow's code exists
            {
                CodeCards.Visible = true;
                TomorrowsCard.Visible = true;

                anActiveCodeExists = true;
                TomorrowCodeLabel.Text = accessCodeTomorrow.accesscodeword;
                TomorrowDateLabel.Text = DateTime.Now.AddDays(1).ToString("MMM d, yyyy");
            }
            #endregion

            if (!anActiveCodeExists) // neither code has been generated
            {
                MessageUserControl.ShowErrorMessage("Today's and tomorrow's codes are not yet generated. Please return to the dashboard (welcome page) and navigate back to this page. If the error persists, please contact your administrator.");
                CodeCards.Visible = false;
            }
        }
        catch (Exception ex)
        {
            MessageUserControl.ShowErrorMessage("Retrieving access codes from the database failed. Please try again. If error persists, please contact your administrator.", ex);
        }
    }
}