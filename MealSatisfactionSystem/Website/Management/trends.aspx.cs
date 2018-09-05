using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using MSS.Data.DTOs;
using MSS.Data.Entities.Security;
using MSS.System.BLL;
using MSS.System.BLL.Security;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

/*
CREATED:    T.J. Blakely      MAR 16 2018

Management_trends
Contains methods calling the BLL in order to populate trends.

FIELDS:
internal UnitController unitController - an instance of the unit controller
internal GenderController genController - an instance of the gender controller
internal SurveyController surveyController - an instance of the survey controller
internal AgeController ageController - an instance of the age controller
internal RespondentTypeController - an instance of the respondent type controller
internal UserManager userManager - an instance of the user manager class
internal CareSiteController careSiteController - an instance of the care site controller

METHODS:
protected void Page_Load(object sender, EventArgs e) - this method runs when the page loads
protected void CareSiteDDL_OnSelectedIndexChanged(object sender, EventArgs e) - this method runs when the index of the care site DDL is changed, and populates the unit checkboxes based on the care site ID
protected void GenerateReportButton_Click(object sender, EventArgs e) - this method runs when the Generate Report button is clicked and calls the ProcessReport method
protected void ResetReport_Click(object sender, EventArgs e) - this method runs when the Reset Filters button is clicked and resets all filters
protected void ProcessReport() - this method runs after the Generate Report button has been clicked and the question ID has been validated. This method validates all the filters from the page and then calls the GetData method
protected DataSet GetData(int QID, List<string> unitIDs, List<string> respondentTypes, List<string> genders, List<string> ages, DateTime startDate, DateTime endDate) - this method runs when all the page filters have been validated. This method assesses the filters and queries the database
protected string PopulateFilters(List<string> filter) - this method takes filter data from the GetData method and processes it
protected void ControlValidation(Repeater repeater, List<string> controlsList, string hiddenFieldID) - this method checks all the filters on the page and assesses whether they are selected/checked or not
*/
public partial class Management_trends : System.Web.UI.Page
{
    internal UnitController unitController = new UnitController();
    internal SurveyController surveyController = new SurveyController();
    internal GenderController genderController = new GenderController();
    internal AgeController ageController = new AgeController();
    internal RespondentTypeController respondentTypeController = new RespondentTypeController();
    internal UserManager userManager = new UserManager();
    internal CareSiteController careSiteController = new CareSiteController();

    #region Page Load
    /* 
    CREATED: 	T.J. Blakely		MAR 16 2018

    Page_Load()
    This method runs when the page loads. It also checks user authorization levels before displaying data.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS:
    MessageUserControl.ShowErrorMessage()
    UnitControllerGetActiveCareSiteUnits()
    CareSiteControllerGetActiveCareSites() 
    RespondentTypeController.GetRespondentTypes()
    GenderController.GetActiveGenders()
    AgeController.GetActiveAges()
    UserManager.FindById()
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.IsInRole(AuthorizationLevelRoles.User))
            {
                ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
                int careSiteID = user.caresiteid == null ? 0 : (int)user.caresiteid;
                if (careSiteID == 0)
                {
                    MessageUserControl.ShowErrorMessage("Your account has no assigned care site. Please contact your administrator to be assigned a care site.");
                }
                else
                {
                    CareSiteDDL.Items.Clear();
                    CareSiteDDL.Items.Add(new ListItem(careSiteController.GetCareSiteByCareSiteID(careSiteID).caresitename, careSiteID.ToString()));
                    UnitRepeater.DataSource = unitController.GetActiveCareSiteUnits(careSiteID);
                    UnitRepeater.DataBind();
                    RespondentTypeRepeater.DataSource = respondentTypeController.GetRespondentTypes();
                    RespondentTypeRepeater.DataBind();
                    GenderRepeater.DataSource = genderController.GetActiveGenders();
                    GenderRepeater.DataBind();
                    AgeGroupRepeater.DataSource = ageController.GetActiveAges();
                    AgeGroupRepeater.DataBind();
                }
            }
            else
            {
                CareSiteDDL.DataSource = careSiteController.GetCareSites();
                CareSiteDDL.DataBind();
                unitsdiv.Attributes.Add("style", "display:none");
                RespondentTypeRepeater.DataSource = respondentTypeController.GetRespondentTypes();
                RespondentTypeRepeater.DataBind();
                GenderRepeater.DataSource = genderController.GetActiveGenders();
                GenderRepeater.DataBind();
                AgeGroupRepeater.DataSource = ageController.GetActiveAges();
                AgeGroupRepeater.DataBind();
            }
        }
    }
    #endregion

    #region Button Methods
    /* 
    CREATED: 	T.J. Blakely		MAR 19 2018

    CareSiteDDL_OnSelectedIndexChanged()
    This method runs if the index of the care site DDL changes.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    UnitController.GetCareSiteUnits()
    */
    protected void CareSiteDDL_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (CareSiteDDL.SelectedValue != "" && !User.IsInRole("User"))
        {
            int tempCareSiteId;
            int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
            List<UnitDTO> tempUnitList = unitController.GetCareSiteUnits(tempCareSiteId);
            unitsdiv.Attributes.Remove("style");
            UnitRepeater.DataSource = tempUnitList;
            UnitRepeater.DataBind();
        }
        else
        {
            unitsdiv.Attributes.Add("style", "display:none");
            UnitRepeater.DataSource = null;
            UnitRepeater.DataBind();
        }
    }

    /* 
    CREATED: 	T.J. Blakely		MAR 16 2018

    GenerateReportButton_Click()
    This method runs when the Generate Report Button is clicked.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS:
    MessageUserControl.ShowInfoMessage() 
    MessageUserControl.ShowErrorMessage()
    ProcessReport()
    */
    protected void GenerateReportButton_Click(object sender, EventArgs e)
    {
        try
        {
            ProcessReport();
        }
        catch (Exception ex)
        {
            MessageUserControl.ShowErrorMessage("An error occurred when processing the report. Please reload the page and try again. If the error persists, please contact your administrator.", ex);
        }
    }

    /* 
    CREATED: 	T.J. Blakely		APR 3 2018

    ResetReport_Click()
    This method runs when the Reset Filters button is clicked.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void ResetReport_Click(object sender, EventArgs e)
    {
        ReportViewer1.Visible = false;
        Response.Redirect("trends");
    }
    #endregion

    #region Report Processing Methods
    /* 
    CREATED: 	T.J. Blakely		MAR 19 2018

    ProcessReport()
    This method validates all user input & filters, and then processes the report.

    PARAMETERS:
    None 	

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    ControlValidation()
    GetData()
    MessageUserControl.ShowInfoMessage()
    */
    protected void ProcessReport()
    {
        // list to hold error msgs
        List<string> errors = new List<string>();

        // local variable declaration
        int questionID;
        if (QuestionDDL.SelectedValue != "")
        {
            questionID = int.Parse(QuestionDDL.SelectedValue);
        }
        else
        {
            questionID = 0;
        }
        List<string> unitIDs = new List<string>();
        List<string> respondentTypes = new List<string>();
        List<string> genders = new List<string>();
        List<string> ages = new List<string>();
        DateTime startDate = new DateTime(), endDate = new DateTime();

        // regex to check date fields
        string pattern = @"^$|^(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/][0-9]{4}$";
        Regex reg = new Regex(pattern);

        // finding and validating page controls
        if (CareSiteDDL.SelectedValue != "")
        {
            ControlValidation(UnitRepeater, unitIDs, "HiddenUnitID");
        }
        ControlValidation(RespondentTypeRepeater, respondentTypes, "HiddenRespondentTypeID");
        ControlValidation(GenderRepeater, genders, "HiddenGenderID");
        ControlValidation(AgeGroupRepeater, ages, "HiddenAgeGroupID");

        // care site & unit validation
        if (CareSiteDDL.SelectedValue != "" && unitIDs.Count == 0)
        {
            errors.Add("Please select at least one unit.");
        }

        // date validation
        // checking if the data matches the regex
        if (!reg.IsMatch(dateStart.Text) || !reg.IsMatch(dateEnd.Text))
        {
            errors.Add("Dates must be in mm/dd/yyyy format.");
        }
        else
        {
            // checking if the data can be parsed
            if (dateStart.Text != "" && !DateTime.TryParse(dateStart.Text, out startDate))
            {
                errors.Add("Start date must be in mm/dd/yyyy format.");
            }
            if (dateEnd.Text != "" && !DateTime.TryParse(dateEnd.Text, out endDate))
            {
                errors.Add("End date must be in mm/dd/yyyy format.");
            }
            // checking if the dates are valid
            if (startDate > DateTime.Today || endDate > DateTime.Today)
            {
                errors.Add("Please select dates that are on or before today's date.");
            }
            if (dateStart.Text != "" && dateEnd.Text != "" && startDate > endDate)
            {
                errors.Add("Please ensure that the provided end date occurs after or on the same day as the provided start date.");
            }
            else
            {
                if (dateStart.Text != "" && dateEnd.Text == "")
                {
                    dateEnd.Text = DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    endDate = DateTime.Today;
                }
            }
        }

        // respondent type validation
        if (respondentTypes.Count == 0)
        {
            errors.Add("Please select at least one respondent type to filter by.");
        }

        // gender validation
        if (genders.Count == 0)
        {
            errors.Add("Please select at least one gender to filter by.");
        }

        // age group validation
        if (ages.Count == 0)
        {
            errors.Add("Please select at least one age group to filter by.");
        }

        // question validation
        if (questionID == 0)
        {
            errors.Add("Please select a question from the drop-down list.");
        }

        // check if there were errors found; if not, process the report
        if (errors.Count == 1)
        {
            MessageUserControl.ShowInfoMessage(errors[0]);
            ReportViewer1.Visible = false;
        }
        else
        {
            if (errors.Count > 1)
            {
                MessageUserControl.ShowInfoList("Please address the following errors and try again:", errors);
                ReportViewer1.Visible = false;
            }
            else // all data is assumed valid, create dataset
            {
                // these statements are to clear filters lists if all are selected; makes the query neater
                if (dateEnd.Text == "")
                {
                    dateEnd.Text = DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                if (respondentTypes.Count == RespondentTypeRepeater.Items.Count)
                {
                    respondentTypes.Clear();
                }

                if (ages.Count == AgeGroupRepeater.Items.Count)
                {
                    ages.Clear();
                }

                if (genders.Count == GenderRepeater.Items.Count)
                {
                    genders.Clear();
                }

                DataSet dt = GetData(questionID, unitIDs, respondentTypes, genders, ages, startDate, endDate);

                // checking if 0 surveys are found - display "no data found"
                int count = 0;
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    count += int.Parse(row.ItemArray[2].ToString());
                }

                if (count == 0)
                {
                    MessageUserControl.ShowInfoMessage("No data was found matching the entered search criteria.");
                    ReportViewer1.Visible = false;
                }
                else // if you get here, the filters are valid! run the report.
                {
                    ReportViewer1.Visible = true;
                    ReportParameter param = new ReportParameter("QuestionID", QuestionDDL.SelectedItem.Text.Split(':')[0]);
                    ReportDataSource rds = new ReportDataSource("TrendSource", dt.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.ReportPath = "Reports/Trend.rdlc";
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.DisplayName = "Trends_" + QuestionDDL.SelectedItem.Text.Split(':')[0] + "_" + DateTime.Now.ToString("MMMddyyyy_HHmm");
                    ReportViewer1.LocalReport.Refresh();
                }
            }
        }
    }

    /* 
    CREATED: 	T.J. Blakely		MAR 19 2018

    GetData()
    This method runs if all the page validation is successful in ProcessReport - it then processes the SQL query and retrieves the results.

    PARAMETERS: 	
    int QID - The question ID pulled from the page
    List<string> unitIDs - The unit IDs to include when filtering the data
    List<string respondentTypes - The respondent types to include when filtering the data
    List<string> genders - The genders to include when filtering the data
    List<string> ages - The age groups to include when filtering the data
    DateTime startDate - The start date used when filtering the data
    DateTime endDate - The end date used when filtering the data

    RETURNS: 
    DataSet - A dataset that includes data pulled using the SQL query

    ODEV METHOD CALLS: 
    PopulateFilters()
    */
    protected DataSet GetData(int QID, List<string> unitIDs, List<string> respondentTypes, List<string> genders, List<string> ages, DateTime startDate, DateTime endDate)
    {
        // local variable declaration
        string filters = "";
        string sql = "SELECT surveyquestion.question, possibleanswer.possibleanswertext, possibleanswer.possibleanswerid, survey.date "
                + "FROM survey LEFT OUTER JOIN surveyanswer ON surveyanswer.surveyid = survey.surveyid LEFT OUTER JOIN possibleanswer ON surveyanswer.possibleanswerid = possibleanswer.possibleanswerid RIGHT OUTER JOIN surveyquestion ON possibleanswer.surveyquestionid = surveyquestion.surveyquestionid "
                + "WHERE ";

        // if the user only selected some units add them to the filter string
        if (unitIDs.Count > 0)
        {
            filters += "survey.unitid IN(" + PopulateFilters(unitIDs) + ") AND ";
        }

        // if the user did not select a start date and selected an end date, OR if the user selected a start date and an end date
        if ((startDate == DateTime.MinValue && endDate != DateTime.MinValue) || (startDate != DateTime.MinValue && endDate != DateTime.MinValue))
        {
            filters += "survey.date BETWEEN '" + startDate.ToString() + "' AND '" + endDate.ToString() + "' AND ";
        }

        // if the user only selected some respondent types add them to the filter string
        if (respondentTypes.Count > 0)
        {
            filters += "survey.respondenttypeid IN(" + PopulateFilters(respondentTypes) + ") AND ";
        }

        // if the user only selected some genders add them to the filter string
        if (genders.Count > 0)
        {
            filters += "survey.genderid IN(" + PopulateFilters(genders) + ") AND ";
        }

        // if the user only selected some age groups add them to the filter string
        if (ages.Count > 0)
        {
            filters += "survey.agegroupid IN(" + PopulateFilters(ages) + ") AND ";
        }

        // finally, add all selected filters to the sql query
        sql += filters + "surveyquestion.surveyquestionid = :qid "
            + "GROUP BY surveyquestion.question, surveyanswer.possibleanswerid, possibleanswer.possibleanswerid, surveyanswer.surveyid, survey.date ORDER BY possibleanswer.possibleanswerid;";

        // create a connection to the database and pass in the query string
        var conString = ConfigurationManager.ConnectionStrings["MSSDB"];
        string connection = conString.ConnectionString;
        using (NpgsqlConnection conn = new NpgsqlConnection(connection))
        {
            conn.Open();

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.Add("qid", NpgsqlDbType.Integer);
                cmd.Parameters[0].Value = QID;

                DataSet ds = new DataSet();
                var rdr = cmd.ExecuteReader();
                ds.Tables.Add("Report");
                ds.Tables[0].Load(rdr);

                return ds;
            }
        }
    }
    #endregion

    #region Helper Methods

    /* 
    CREATED: 	T.J. Blakely		MAR 22 2018

    PopulateFilters()
    This method takes in filter information from the GetData method and processes it.

    PARAMETERS:
    List<string> filter - A list of strings with filter data (units/respondent types/genders etc)

    RETURNS: 
    string - Returns one string that is appended to the SQL query

    ODEV METHOD CALLS: 
    None
    */
    protected string PopulateFilters(List<string> filter)
    {
        string filters = "";
        foreach (string item in filter)
        {
            if (item == filter.First())
                filters += "'" + item + "'";
            else
                filters += ", '" + item + "'";
        }
        return filters;
    }

    /* 
    CREATED: 	T.J. Blakely		MAR 22 2018

    ControlValidation()
    This method takes in filter information from the page and processes it.

    PARAMETERS:
    Repeater repeater - a repeater from the page whose content will be processed
    List<string> controlsList - a list of the checked controls from the current repeater
    string hiddenFieldID - the hidden field ID for the current repeater

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void ControlValidation(Repeater repeater, List<string> controlsList, string hiddenFieldID)
    {
        foreach (RepeaterItem item in repeater.Items)
        {
            CheckBox check = (CheckBox)item.FindControl("CheckBox");
            if (check.Text.Length > 10)
            {
                check = (CheckBox)item.FindControl("Checkbox1");
            }
            if (check.Checked)
            {
                HiddenField field = (HiddenField)item.FindControl(hiddenFieldID);
                controlsList.Add(field.Value);
            }
        }
    }
    #endregion
}
