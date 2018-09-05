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
CREATED:    A. Valberg      MAR 16 2018

Management_reporting
Contains methods calling the BLL in order to populate reports.

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
protected void RefreshFields() - this method refreshes all the controls on the page
protected void RefreshCheckboxes(Repeater repeater) - this method refreshes (re-checks) a checkbox repeater passed in as a parameter
*/
public partial class Management_reporting : System.Web.UI.Page
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
    CREATED: 	A. Valberg		MAR 16 2018

    Page_Load()
    This method runs when the page loads. It also checks user authorization levels before displaying data.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS:
    MessageUserControl.ShowErrorMessage()
    CareSiteController.GetCareSiteByCareSiteID()
    UnitController.GetCareSiteUnits()
    GetActiveCareSites() 
    RespondentTypeController.GetAllRespondentTypes()
    GenderController.GetAllGenders()
    AgeController.GetAllAges()
    CareSiteController.GetCareSites()
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
                    UnitRepeater.DataSource = unitController.GetCareSiteUnits(careSiteID);
                    UnitRepeater.DataBind();
                    RespondentTypeRepeater.DataSource = respondentTypeController.GetAllRespondentTypes();
                    RespondentTypeRepeater.DataBind();
                    GenderRepeater.DataSource = genderController.GetAllGenders();
                    GenderRepeater.DataBind();
                    AgeGroupRepeater.DataSource = ageController.GetAllAges();
                    AgeGroupRepeater.DataBind();
                }
            }
            else
            {
                CareSiteDDL.DataSource = careSiteController.GetCareSites();
                CareSiteDDL.DataBind();
                unitsdiv.Attributes.Add("style", "display:none");
                RespondentTypeRepeater.DataSource = respondentTypeController.GetAllRespondentTypes();
                RespondentTypeRepeater.DataBind();
                GenderRepeater.DataSource = genderController.GetAllGenders();
                GenderRepeater.DataBind();
                AgeGroupRepeater.DataSource = ageController.GetAllAges();
                AgeGroupRepeater.DataBind();
            }
        }
    }
    #endregion

    #region Button Methods
    /* 
    CREATED: 	A. Valberg		MAR 19 2018

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
        if (CareSiteDDL.SelectedValue != "" && !User.IsInRole(AuthorizationLevelRoles.User))
        {
            int tempCareSiteId;
            int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
            List<UnitDTO> tempUnitList = unitController.GetCareSiteUnits(tempCareSiteId);
            unitsdiv.Attributes.Remove("style");
            UnitRepeater.DataSource = tempUnitList;
            UnitRepeater.DataBind();
            if (ReportViewer1.Visible)
            {
                ReportViewer1.Visible = false;
            }
        }
        else
        {
            unitsdiv.Attributes.Add("style", "display:none");
            UnitRepeater.DataSource = null;
            UnitRepeater.DataBind();
            if (ReportViewer1.Visible)
            {
                ReportViewer1.Visible = false;
            }
        }
    }

    /* 
    CREATED: 	A. Valberg		MAR 16 2018

    GenerateReportButton_Click()
    This method runs when the Generate Report Button is clicked.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS:
    MessageUserController.ShowInfoMessage() 
    MessageUserController.ShowErrorMessage()
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
    CREATED: 	A. Valberg		APR 3 2018

    ResetReport_Click()
    This method runs when the Reset Filters button is clicked, and calls a method which refreshes all fields on the page.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    RefreshFields()
    */
    protected void ResetReport_Click(object sender, EventArgs e)
    {
        RefreshFields();
    }
    #endregion

    #region Report Processing Methods
    /* 
    CREATED: 	A. Valberg		MAR 19 2018

    ProcessReport()
    This method validates all user input & filters, and then processes the report.

    PARAMETERS:
    None 	

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    ControlValidation()
    MessageUserControl.ShowInfoMessage()
    MessageUserControl.ShowInfoList()
    GetData()
    */
    protected void ProcessReport()
    {
        // list to hold error msgs
        List<string> errors = new List<string>();

        // local variables for page controls
        int questionID;
        questionID = QuestionDDL.SelectedValue != "" ? int.Parse(QuestionDDL.SelectedValue) : 0;
        List<string> unitIDs = new List<string>();
        List<string> respondentTypes = new List<string>();
        List<string> genders = new List<string>();
        List<string> ages = new List<string>();
        DateTime startDate = new DateTime(), endDate = new DateTime();

        // regex to check date fields
        string pattern = @"^$|^(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/][0-9]{4}$";
        Regex reg = new Regex(pattern);

        // finding and retrieving page controls
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
            errors.Add("Please enter valid date(s) using the pattern mm/dd/yyyy or leave the date fields blank.");
        }
        else
        {
            // checking if the data can be parsed
            if (dateStart.Text != "" && !DateTime.TryParse(dateStart.Text, out startDate))
            {
                errors.Add("Please enter a valid start date value or leave it blank.");
            }
            if (dateEnd.Text != "" && !DateTime.TryParse(dateEnd.Text, out endDate))
            {
                errors.Add("Please enter a valid start date value or leave it blank.");
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

                // creating a dataset and trying to fill it with the GetData method
                DataSet dt = new DataSet();
                try
                {
                    dt = GetData(questionID, unitIDs, respondentTypes, genders, ages, startDate, endDate);
                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("An error occurred while processing the report. Please reload the page and try again. If the error persists, please contact your administrator.", ex);
                }

                // checking if 0 surveys are found - display "no data found"
                int count = 0;
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    count += int.Parse(row.ItemArray[3].ToString());
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
                    ReportDataSource rds = new ReportDataSource("ReportSource", dt.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.ReportPath = "Reports/Report.rdlc";
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.DisplayName = "Reports_" + QuestionDDL.SelectedItem.Text.Split(':')[0] + "_" + DateTime.Now.ToString("MMMddyyyy_HHmm");
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
        }
    }

    /* 
    CREATED: 	A. Valberg		MAR 19 2018

    GetData()
    This method runs if all the page validation is successful in ProcessReport - it then processes the SQL query and retrieves the results.

    PARAMETERS: 	
    int QID - the question ID pulled from the page
    List<string> unitIDs - the unit IDs to include when filtering the data
    List<string respondentTypes - the respondent types to include when filtering the data
    List<string> genders - the genders to include when filtering the data
    List<string> ages - the age groups to include when filtering the data
    DateTime startDate - the start date used when filtering the data
    DateTime endDate - the end date used when filtering the data

    RETURNS: 
    DataSet - a dataset that includes data pulled using the SQL query

    ODEV METHOD CALLS: 
    PopulateFilters()
    */
    protected DataSet GetData(int QID, List<string> unitIDs, List<string> respondentTypes, List<string> genders, List<string> ages, DateTime startDate, DateTime endDate)
    {
        // using a string to hold the sql query as it's created
        // each validation section checks if the passed-in list or value is empty; if they are, the query doesn't need to be changed
        string filters = "";
        string sql = "SELECT surveyquestion.question, possibleanswer.possibleanswertext, possibleanswer.possibleanswerid, "
                + "COUNT(";

        if (unitIDs.Count > 0)
        {
            filters = "CASE WHEN (survey.unitid IN(" + PopulateFilters(unitIDs) + ")";
        }

        if ((startDate == DateTime.MinValue && endDate != DateTime.MinValue) || (startDate != DateTime.MinValue && endDate != DateTime.MinValue))
        {
            if (filters == "")
                filters = "CASE WHEN (";
            else
                filters += " AND ";
            filters += "survey.date BETWEEN '" + startDate.ToString() + "' AND '" + endDate.ToString() + "'";
        }

        if (respondentTypes.Count > 0)
        {
            if (filters == "")
                filters = "CASE WHEN (survey.respondenttypeid IN(";
            else
                filters += " AND survey.respondenttypeid IN(";
            filters += PopulateFilters(respondentTypes) + ")";
        }

        if (genders.Count > 0)
        {
            if (filters == "")
                filters = "CASE WHEN (survey.genderid IN(";
            else
                filters += " AND survey.genderid IN(";
            filters += PopulateFilters(genders) + ")";
        }

        if (ages.Count > 0)
        {
            if (filters == "")
                filters = "CASE WHEN (survey.agegroupid IN(";
            else
                filters += " AND survey.agegroupid IN(";
            filters += PopulateFilters(ages) + ")";
        }

        // if nothing is in the filters string, there are no conditions on the sql query
        if (filters.Length > 0)
        {
            sql += filters + ") THEN surveyanswer.possibleanswerid END";
        }
        else
        {
            sql += "surveyanswer.possibleanswerid";
        }
        sql += ") AS countanswers "
                + "FROM survey LEFT OUTER JOIN surveyanswer ON surveyanswer.surveyid = survey.surveyid "
                + "RIGHT OUTER JOIN possibleanswer ON surveyanswer.possibleanswerid = possibleanswer.possibleanswerid "
                + "RIGHT OUTER JOIN surveyquestion ON possibleanswer.surveyquestionid = surveyquestion.surveyquestionid "
                + "WHERE surveyquestion.surveyquestionid = :qid "
                + "GROUP BY surveyquestion.question, possibleanswer.possibleanswerid, possibleanswer.possibleanswertext;";

        // connecting to the db and passing in the query string
        var conString = ConfigurationManager.ConnectionStrings["MSSDB"];
        string connection = conString.ConnectionString;
        using (NpgsqlConnection conn = new NpgsqlConnection(connection))
        {
            conn.Open();

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
            {
                // adding a parameter for question ID
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
    CREATED: 	A. Valberg		MAR 22 2018

    PopulateFilters()
    This method takes in filter information from the GetData method and processes it.

    PARAMETERS:
    List<string> filter - a list of strings with filter data (units/respondent types/genders etc.)

    RETURNS: 
    string - returns a string that is appended to the SQL query

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
    CREATED: 	A. Valberg		MAR 22 2018

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
                check = (CheckBox)item.FindControl("CheckBox1");
            }
            if (check.Checked)
            {
                HiddenField field = (HiddenField)item.FindControl(hiddenFieldID);
                controlsList.Add(field.Value);
            }
        }
    }

    /* 
    CREATED: 	A. Valberg		APR 12 2018

    RefreshFields()
    This method 'refreshes' all of the fields on the page.

    PARAMETERS:
    None

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    RefreshCheckboxes()
    */
    protected void RefreshFields()
    {
        if (!User.IsInRole(AuthorizationLevelRoles.User))
        {
            CareSiteDDL.SelectedValue = "";
            unitsdiv.Attributes.Add("style", "display:none");
        }
        RefreshCheckboxes(UnitRepeater);
        dateStart.Text = "";
        dateEnd.Text = "";
        RefreshCheckboxes(RespondentTypeRepeater);
        RefreshCheckboxes(GenderRepeater);
        RefreshCheckboxes(AgeGroupRepeater);
        QuestionDDL.SelectedValue = "";
        ReportViewer1.Visible = false;
    }

    /* 
    CREATED: 	A. Valberg		APR 12 2018

    RefreshCheckboxes()
    This method 'refreshes' a checkbox repeater on the page.

    PARAMETERS:
    Repeater repeater - a repeater from the page that will have its checkboxes checked if currently unchecked

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void RefreshCheckboxes(Repeater repeater)
    {
        foreach (RepeaterItem item in repeater.Items)
        {
            CheckBox check = (CheckBox)item.FindControl("Checkbox");
            if (check.Text.Length > 10)
            {
                check = (CheckBox)item.FindControl("CheckBox1");
            }
            if (!check.Checked)
            {
                check.Checked = true;
            }
        }
    }
    #endregion
}
