using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/* 
CREATED:    C. Stanhope        MAR 3 2018

Management_access_codes
An automatically generated page used to store the access_codes's backend C# code.

CLASS-LEVEL VARIABLES:
internal CareSiteAccessController careSiteAccessController - initialising the CareSiteAccessController to be used by the methods in the class

METHODS:
protected void Page_Load(object sender, EventArgs e) - triggered on page load
protected void CareSiteDDL_SelectedIndexChanged(object sender, EventArgs e) - refreshes the page to reflect the newly selected care site
*/

public partial class Management_access_codes : System.Web.UI.Page
{
    internal CareSiteAccessController careSiteAccessController = new CareSiteAccessController();

    /* 
    CREATED: 	C. Stanhope		Mar 3 2018

    Page_Load()
    Run on page load and is used to display today's and tomorrow's access codes if a care site is selected from the drop-down list.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void
    
    METHOD CALLS: 
    None
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CareSiteDDL.SelectedIndex == 0)
        {
            CodeCards.Visible = false;
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

    METHOD CALLS: 
    MessageUserControl.ShowErrorMessage()
    int.TryParse()
    DateTime.Now()
    DateTime.AddDays()
    ToString()
    */
    protected void CareSiteDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(CareSiteDDL.SelectedIndex == 0)
        {
            MessageUserControl.ShowErrorMessage("No care site selected. Please select a care site.");
            CodeCards.Visible = false;
        }
        else // care site was selected
        {
            int selectedCareSiteID;
            if(int.TryParse(CareSiteDDL.SelectedValue, out selectedCareSiteID))
            {
                // get access codes by today's and tomorrow's dates and the selected caresite
                AccessCodeDTO accessCodeToday = careSiteAccessController.GetCareSiteAccessCodeByDate(selectedCareSiteID, DateTime.Now);
                AccessCodeDTO accessCodeTomorrow = careSiteAccessController.GetCareSiteAccessCodeByDate(selectedCareSiteID, DateTime.Now.AddDays(1));

                if (accessCodeToday.accesscodeid == -3)
                {
                    MessageUserControl.ShowErrorMessage("Today's code not yet generated. Please contact the administrator.");
                    CodeCards.Visible = false;
                }
                else if (accessCodeTomorrow.accesscodeid == -3)
                {
                    MessageUserControl.ShowErrorMessage("Tomorrows's code not generated. Please contact the administrator.");
                    CodeCards.Visible = false;
                }
                else // today's and tomorrow's codes are generated and displayed
                {
                    TodayCodeLabel.Text = accessCodeToday.accesscodeword;
                    TomorrowCodeLabel.Text = accessCodeTomorrow.accesscodeword;

                    TodayDateLabel.Text = DateTime.Now.ToString("MMM d, yyyy");
                    TomorrowDateLabel.Text = DateTime.Now.AddDays(1).ToString("MMM d, yyyy");

                    CodeCards.Visible = true;
                }
            }
            else // the tryparse of the care site ID didn't work
            {
                MessageUserControl.ShowErrorMessage("Something failed. Please contact the administrator.");
            }
                      
        }
    }
}