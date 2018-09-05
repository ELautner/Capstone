using MSS.Data;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Management_dashboard : System.Web.UI.Page
{
    internal CareSiteController careSiteController = new CareSiteController();
    internal CareSiteAccessController careSiteAccessController = new CareSiteAccessController();
    internal AccessCodeController accessCodeController = new AccessCodeController();

    protected void Page_Load(object sender, EventArgs e)
    {

        DateTime currentDate = DateTime.Today;
        DateTime tomorrowsDate = currentDate.AddDays(1);
        String todaysMessage = "";
        String tomorrowsMessage = "";
        Random rnd = new Random();

        List<int> alreadyAssignedAccessCodes = new List<int>();
        List<int> accessCodeIds = new List<int>();


        List<int> alreadyAssignedCareSites = careSiteAccessController.GetAssignedCareSites(currentDate);

        List<int> careSiteIds = careSiteController.GetCareSiteIds(alreadyAssignedCareSites);

        if (careSiteIds.Count > 0)
        {

            foreach (int careSiteId in careSiteIds)
            {
                alreadyAssignedAccessCodes.Clear();
                alreadyAssignedAccessCodes = careSiteAccessController.GetAssignedAccessCodes(currentDate, careSiteId);

                accessCodeIds.Clear();
                accessCodeIds = accessCodeController.GetAccessCodes(alreadyAssignedAccessCodes);

                if (accessCodeIds.Count > 0)
                {
                    int index = rnd.Next(0, accessCodeIds.Count);
                    int accessCodeId = accessCodeIds[index];

                    try
                    {
                        careSiteAccessController.AddCareSiteAccess(careSiteId, accessCodeId, currentDate);

                    } catch (Exception ex)
                    {
                        ErrorMessage.Text = "Care site access code generation failed. Please contact the system administrator.";
                    }

                }
                else
                {
                    todaysMessage = todaysMessage + careSiteId.ToString() + ", ";
                }

            }
        }

        alreadyAssignedCareSites.Clear();
        alreadyAssignedCareSites = careSiteAccessController.GetAssignedCareSites(tomorrowsDate);

        careSiteIds.Clear();
        careSiteIds = careSiteController.GetCareSiteIds(alreadyAssignedCareSites);

        if (careSiteIds.Count > 0)
        {


            foreach (int careSiteId in careSiteIds)
            {
                alreadyAssignedAccessCodes.Clear();
                alreadyAssignedAccessCodes = careSiteAccessController.GetAssignedAccessCodes(tomorrowsDate, careSiteId);

                accessCodeIds.Clear();
                accessCodeIds = accessCodeController.GetAccessCodes(alreadyAssignedAccessCodes);

                if (accessCodeIds.Count > 0)
                {
                    int index = rnd.Next(0, accessCodeIds.Count);
                    int accessCodeId = accessCodeIds[index];

                    try
                    {
                        careSiteAccessController.AddCareSiteAccess(careSiteId, accessCodeId, tomorrowsDate);

                    } catch (Exception ex)
                    {
                        ErrorMessage.Text = "Care site access code generation failed. Please contact the system administrator.";
                    }

                }
                else
                {
                    tomorrowsMessage = tomorrowsMessage + careSiteId.ToString() + ", ";
                }

            }

        }

        string compoundMessage = "";

        if (todaysMessage != "" && tomorrowsMessage != "")
        {
            compoundMessage = "Could not generate today's access codes for care sites: " + todaysMessage + "more words are needed to generate access codes, please contact the system  administrator."
                + "Could not generate tommorrows's access codes for care sites: " + tomorrowsMessage + "more words are needed to generate access codes, please contact the system  administrator.";
        } else if (todaysMessage != "" && tomorrowsMessage == "")
        {
            compoundMessage = "Could not generate today's access codes for care sites: " + todaysMessage + "more words are needed to generate access codes, please contact the system  administrator.";
        } else if (todaysMessage == "" && tomorrowsMessage != "")
        {
            compoundMessage = "Could not generate tommorrows's access codes for care sites: " + tomorrowsMessage + "more words are needed to generate access codes, please contact the system  administrator.";
        } else if (todaysMessage == "" && tomorrowsMessage == "")
        {
            compoundMessage = "";
        }

        ErrorMessage.Text = compoundMessage;

    }
}