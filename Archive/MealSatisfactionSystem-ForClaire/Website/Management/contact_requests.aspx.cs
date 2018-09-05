using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/* 
CREATED:	C. Stanhope		MAR 4 2018

Management_contact_requests
Contains methods nessasary to display contact requests to users.

CLASS-LEVEL VARIABLES:
internal UnitController unitController = new UnitController(); - allows methods from the controller to be called
internal SurveyController surveyController = new SurveyController(); - allows methods from the controller to be called

METHODS:
protected void Page_Load(object sender, EventArgs e) - this method runs any code it contains when the page loads
protected void RetrieveUnitListButton_Click(object sender, EventArgs e) - this method retrieves the units of a specified care site 
protected void RetrieveContactRequests_Click(object sender, EventArgs e) - this method retrieves contact requests from the database based off the filters entered
*/
public partial class Management_contact_requests : System.Web.UI.Page
{
    internal UnitController unitController = new UnitController();
    internal SurveyController surveyController = new SurveyController();

    /* 
    CREATED: 	C. Stanhope		MAR 4 2018

    Page_Load()
    This method runs any code it contains when the page loads.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    None
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }

    /* 
    CREATED: 	H. Conant		MAR 13 2018

    RetrieveUnitListButton_Click()
    This method runs any code it contains when the "Retrieve Units" button is clicked.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    .TryParse()
    .GetAllUnits()
    .DataBind()
    */
    protected void RetrieveUnitListButton_Click(object sender, EventArgs e)
    {
        if (CareSiteDDL.SelectedValue == "0")
        {
            int tempCareSiteId;
            int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
            List<UnitDTO> tempUnitList = unitController.GetAllUnits();
            UnitRepeater.DataSource = tempUnitList;
            UnitRepeater.DataBind();
         
        }
        else
        {
            int tempCareSiteId;
            int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
            List<UnitDTO> tempUnitList = unitController.GetCareSiteUnits(tempCareSiteId);
            UnitRepeater.DataSource = tempUnitList;
            UnitRepeater.DataBind();
           
        }
    }


    /* 
    CREATED: 	H. Conant		MAR 13 2018

    RetrieveContactRequests_Click()
    This method runs any code it contains when the "Retrieve Contact Requests" button is clicked.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    List<int>()
    List<string>()
    DateTime()
    List<bool>()
    .Parse()
    .FindControl()
    .Add()
    List<ContactRequestDTO>()
    .ViewContactRequests()
    .DataBind()
    */
    protected void RetrieveContactRequests_Click(object sender, EventArgs e)
    {
        UserMessage.Text = "";
        List<int> unitIds = new List<int>();
        List<string> genders = new List<string>();
        List<string> ages = new List<string>();
        DateTime dateOne = new DateTime();
        DateTime dateTwo = new DateTime();
        List<bool> statuses = new List<bool>();

        
        dateOne = DateTime.Parse(dateStart.Value);
        dateTwo = DateTime.Parse(dateEnd.Value);

        if (dateOne == null || dateTwo == null || dateOne > dateTwo)
        {
            UserMessage.Text = "Please enter a vaild start and end date";

        } else
        {
            foreach (RepeaterItem item in UnitRepeater.Items)
            {
                CheckBox tempCheckBox = (CheckBox)item.FindControl("CheckBox");
                HiddenField tempHiddenField = (HiddenField)item.FindControl("HiddenUnitID");

                if (tempCheckBox.Checked == true)
                {
                    int tempUntId;
                    int.TryParse(tempHiddenField.Value, out tempUntId);
                    unitIds.Add(tempUntId);
                }

            }

            if (unitIds.Count < 1)
            {
                UserMessage.Text = "Please select at least one unit";

            } else
            {
                if (GenderFemale.Checked == true)
                {
                    genders.Add("F");
                }

                if (GenderMale.Checked == true)
                {
                    genders.Add("M");
                }

                if (GenderOther.Checked == true)
                {
                    genders.Add("O");
                }

                if (GenderPrefNotAns.Checked == true)
                {
                    genders.Add("N");
                }

                if(genders.Count < 1)
                {
                    UserMessage.Text = "Please select at least one gender";

                } else
                {

                    if (Age017.Checked == true)
                    {
                        ages.Add("0-17");
                    }

                    if (Age1834.Checked == true)
                    {
                        ages.Add("18-34");
                    }

                    if (Age3554.Checked == true)
                    {
                        ages.Add("35-54");
                    }

                    if (Age5574.Checked == true)
                    {
                        ages.Add("55-74");
                    }

                    if (Age75.Checked == true)
                    {
                        ages.Add("75+");
                    }

                    if (Age0.Checked == true)
                    {
                        ages.Add("0");
                    }

                    if (ages.Count < 1)
                    {
                        UserMessage.Text = "Please select at least one age range";

                    } else
                    {
                        if (ProcessedY.Checked == true)
                        {
                            statuses.Add(true);
                        }

                        if (ProcessedN.Checked == true)
                        {
                            statuses.Add(false);
                        }

                        if (statuses.Count < 1)
                        {
                            UserMessage.Text = "Please select at least 'Processed' or 'Not processed'";
                        } else
                        {
                            try
                            {
                                List<ContactRequestDTO> contactRequests = new List<ContactRequestDTO>();


                                contactRequests = surveyController.ViewContactRequests(unitIds, genders, ages, dateOne, dateTwo, statuses);

                                if (contactRequests.Count < 1)
                                {
                                    UserMessage.Text = "There are no contact requests that match the filers.";

                                } else
                                {
                                    ResultCountLabel.Text = (contactRequests.Count).ToString();

                                    ContactRequestsRepeater.DataSource = contactRequests;
                                    ContactRequestsRepeater.DataBind();
                                }

                            } catch (Exception ex)
                            {
                                ErrorMessage.Text = "Retrieve contact requests failed. Please contact the system administrator.";
                            }
                        }

                    }
                }

            }
        }
    }
}