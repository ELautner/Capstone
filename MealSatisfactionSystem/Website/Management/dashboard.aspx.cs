using MSS.System.BLL;
using System;
using System.Collections.Generic;

/* 
CREATED:      H.Conant       MAR 3 2018

Management_dashboard
This class manages the management dashboard and access code generation.

FIELDS:
internal CareSiteController careSiteController - allows methods from the controller to be called
internal CareSiteAccessController careSiteAccessController - allows methods from the controller to be called
internal AccessCodeController accessCodeController - allows methods from the controller to be called

METHODS:
protected void Page_Load(object sender, EventArgs e) - this method runs when the page loads and generates access codes for any care sites that need them for the current date or the day after
*/
public partial class Management_dashboard : System.Web.UI.Page
{
    internal CareSiteController careSiteController = new CareSiteController();
    internal CareSiteAccessController careSiteAccessController = new CareSiteAccessController();
    internal AccessCodeController accessCodeController = new AccessCodeController();

    /* 
    CREATED:    H. Conant		MAR 10 2018

    Page_Load()
    This method runs when the page loads and generates access codes 
    for any care sites that need them for the current date or the day after

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    CareSiteAccessController.GetAssignedCareSites()
    CareSiteController.GetCareSiteIds()
    CareSiteAccessController.GetAssignedAccessCodes()
    AccessCodeController.GetAccessCodes()
    CareSiteAccessController.AddCareSiteAccess()
    ErrorMessagesAndValidation.List()
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime currentDate = DateTime.Today;
        DateTime tomorrowsDate = currentDate.AddDays(1);
        List<string> failedToday = new List<string>();
        List<string> failedTomorrow = new List<string>();
        Random rnd = new Random();

        List<int> alreadyAssignedAccessCodes = new List<int>();
        List<int> accessCodeIds = new List<int>();
        List<int> alreadyAssignedCareSites = new List<int>();
        List<int> careSiteIds = new List<int>();

        #region Today's codes
        try
        {
            alreadyAssignedCareSites = careSiteAccessController.GetAssignedCareSites(currentDate);

        }
        catch (Exception ex)
        {
            ErrorMsg.Text = "Retrieving unassigned care sites failed for today's date. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
            ErrorMessage.Style.Add("display", "none");
            ErrorMessage2.Style.Add("display", "none");
        }

        try
        {
            careSiteIds = careSiteController.GetCareSiteIds(alreadyAssignedCareSites);

        }
        catch (Exception ex)
        {
            ErrorMsg.Text = "Retrieving care site IDs failed. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
            ErrorMessage.Style.Add("display", "none");
            ErrorMessage2.Style.Add("display", "none");
        }
        
        if (careSiteIds.Count > 0)
        {

            foreach (int careSiteId in careSiteIds)
            {
                try
                {
                    alreadyAssignedAccessCodes.Clear();
                    alreadyAssignedAccessCodes = careSiteAccessController.GetAssignedAccessCodes(currentDate, careSiteId);

                } catch (Exception ex)
                {
                    ErrorMsg.Text = "Retrieving assigned access codes for today's date failed. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
                    ErrorMessage.Style.Add("display", "none");
                    ErrorMessage2.Style.Add("display", "none");
                }

                try
                {
                    accessCodeIds.Clear();
                    accessCodeIds = accessCodeController.GetAccessCodes(alreadyAssignedAccessCodes, currentDate, careSiteId);
                }
                catch (Exception ex)
                {

                    ErrorMsg.Text = "Retrieving previously assigned access codes failed. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
                    ErrorMessage.Style.Add("display", "none");
                    ErrorMessage2.Style.Add("display", "none");
                }
                

                
                if (accessCodeIds.Count > 0)
                {
                    int index = rnd.Next(0, accessCodeIds.Count);
                    int accessCodeId = accessCodeIds[index];

                    try
                    {
                        careSiteAccessController.AddCareSiteAccess(careSiteId, accessCodeId, currentDate);

                    }
                    catch (Exception ex)
                    {

                        ErrorMsg.Text = "Adding new care site access code failed for today's date. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
                        ErrorMessage.Style.Add("display", "none");
                        ErrorMessage2.Style.Add("display", "none");
                    }

                }
                else
                {
                    string siteName = careSiteController.GetCareSiteName(careSiteId);
                    failedToday.Add(siteName);
                }

            }
        }
        #endregion

        #region Tomorrow's codes
        try
        {
            alreadyAssignedCareSites.Clear();
            alreadyAssignedCareSites = careSiteAccessController.GetAssignedCareSites(tomorrowsDate);

        }
        catch (Exception ex)
        {
            ErrorMsg.Text = "Retrieving unassigned care sites failed for tomorrow's date. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
            ErrorMessage.Style.Add("display", "none");
            ErrorMessage2.Style.Add("display", "none");
        }
        try
        {
            careSiteIds.Clear();
            careSiteIds = careSiteController.GetCareSiteIds(alreadyAssignedCareSites);

        }
        catch (Exception ex)
        {
            ErrorMsg.Text = "Retrieving care site IDs failed. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
            ErrorMessage.Style.Add("display", "none");
            ErrorMessage2.Style.Add("display", "none");
        }
        
        if (careSiteIds.Count > 0)
        {
            foreach (int careSiteId in careSiteIds)
            {
                try
                {
                    alreadyAssignedAccessCodes.Clear();
                    alreadyAssignedAccessCodes = careSiteAccessController.GetAssignedAccessCodes(tomorrowsDate, careSiteId);

                }
                catch (Exception ex)
                {
                    ErrorMsg.Text = "Retrieving assigned access codes for tomorrow's date failed. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
                    ErrorMessage.Style.Add("display", "none");
                    ErrorMessage2.Style.Add("display", "none");
                }

                try
                {
                     accessCodeIds.Clear();
                     accessCodeIds = accessCodeController.GetAccessCodes(alreadyAssignedAccessCodes, tomorrowsDate, careSiteId);

                }
                catch (Exception ex)
                {
                    ErrorMsg.Text = "Retrieving previously assigned access codes failed. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
                    ErrorMessage.Style.Add("display", "none");
                    ErrorMessage2.Style.Add("display", "none");
                }
                

                if (accessCodeIds.Count > 0)
                {
                    int index = rnd.Next(0, accessCodeIds.Count);
                    int accessCodeId = accessCodeIds[index];

                    try
                    {
                        careSiteAccessController.AddCareSiteAccess(careSiteId, accessCodeId, tomorrowsDate);

                    } catch (Exception ex)
                    {

                        ErrorMsg.Text = "Adding new care site access code failed for tomorrow's date. Please try again. If error persists, please contact your administrator. Error Message: " + ex.Message;
                        ErrorMessage.Style.Add("display", "none");
                        ErrorMessage2.Style.Add("display", "none");
                    }

                }
                else
                {
                    string siteName = careSiteController.GetCareSiteName(careSiteId);
                    failedTomorrow.Add(siteName);
                }

            }

        }
        #endregion

        if (ErrorMsg.Text == "")
        {
            ErrorMessagesAndValidation errMessAndVal = new ErrorMessagesAndValidation();

            if(failedToday.Count > 0 && failedTomorrow.Count > 0)
            {
                ErrorMessage.Text = "Could not generate today's access codes for " + errMessAndVal.List(failedToday) + ". More words are needed to generate access codes, please contact your administrator.";
                ErrorMessage2.Text = "Could not generate tomorrow's access codes for " + errMessAndVal.List(failedTomorrow) + ". More words are needed to generate access codes, please contact your administrator.";
                ErrorMsg.Style.Add("display", "none");

            }
            else if(failedToday.Count > 0 && failedTomorrow.Count == 0)
            {
                ErrorMessage.Text = "Could not generate today's access codes for " + errMessAndVal.List(failedToday) + ". More words are needed to generate access codes, please contact your administrator.";
                ErrorMessage2.Style.Add("display", "none");
                ErrorMsg.Style.Add("display", "none");

            }
            else if(failedToday.Count == 0 && failedTomorrow.Count > 0)
            {
                ErrorMessage2.Text = "Could not generate tomorrow's access codes for " + errMessAndVal.List(failedTomorrow) + ". More words are needed to generate access codes, please contact your administrator.";
                ErrorMessage.Style.Add("display", "none");
                ErrorMsg.Style.Add("display", "none");

            }
            else
            {
                errordiv.Style.Add("display", "none");
            }
        }
        else
        {
            ErrorMessage.Style.Add("display", "none");
            ErrorMessage2.Style.Add("display", "none");

        }
    }
}
