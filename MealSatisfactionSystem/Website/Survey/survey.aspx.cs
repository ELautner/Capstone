using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

/* 
CREATED:	H. L'Heureux		MAR 2 2018

Survey_survey
Contains methods necessary to show the survey and survey questions to the user.

FIELDS:
internal UnitController unitCont - is used to call methods from the UnitController class
internal SurveyController surveyCont - is used to call methods from the SurveyController class
internal SurveyAnswerController surveyAnswerCont - is used to call methods from the SurveyAnswerController class
internal RespondentTypeController respCont - is used to call methods from the RespondentTypeController class

METHODS:
protected void Page_Load(object sender, EventArgs e) - this method runs any code it contains when the page loads
protected void Submit_Click(object sender, EventArgs e) - this method allows the user to submit a survey to the database
protected void ContactDDL_SelectedIndexChanged(object sender, EventArgs e) - this method will update the visible textboxes dependent on the drop-down list value
 */
public partial class Survey_survey : System.Web.UI.Page
{
    internal UnitController unitCont = new UnitController();
    internal SurveyController surveyCont = new SurveyController();
    internal SurveyAnswerController surveyAnswerCont = new SurveyAnswerController();
    internal RespondentTypeController respCont = new RespondentTypeController();

    #region Page_Load
    /* 
    CREATED:      H. L'Heureux         MAR 16 2018
    MODIFIED:     H. Conant            MAR 29 2018
        - Added !IsPostBack check
        - Removed get respondent types
    MODIFIED:     H. L'Heureux            APR 12 2018
        - Added error handling

    Page_Load()
    This method contains information to show the initial set up of the take survey page to the user. It will show the Care Site Name with a valid access code word for the day, or it will redirect the user back to the landing page (index.aspx) with an error message to inform the user to check their meal/chit ticket.

    PARAMETERS:     
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    CareSiteController.GetCareSiteID()
    CareSiteController.GetCareSitename()
    UnitController.GetActiveCareSiteUnits()
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();
        Response.Cache.AppendCacheExtension("no-cache");
        Response.Expires = 0;

        if (!IsPostBack) // added this to stop information from disapearing if there was an error
        {
            FirstNameLbl.Visible = false;
            FirstName.Visible = false;
            RoomNumber.Visible = false;
            RoomNumberLbl.Visible = false;
            BedNumber.Visible = false;
            BedNumberLbl.Visible = false;
            PhoneNumberLbl.Visible = false;
            PhoneNumber.Visible = false;
        }
        CareSiteAccessController csaController = new CareSiteAccessController();
        CareSiteController csController = new CareSiteController();

        string codeword = Request.QueryString["accesscode"];
        string caresitename = "";

        try
        {
            //get the care site ID from the database for the codeword given by the user
            var caresiteid = csaController.GetCareSiteID(codeword);
            //get the care site name for the care site ID
            caresitename = csController.GetCareSiteName(caresiteid);

            // check if new caresite
            if (SiteNameLabel.Text != caresitename)
            {
                //display the caresite name in the label for the user to see
                SiteNameLabel.Text = caresitename;

                UnitListDDL.Items.Clear();
                UnitListDDL.Items.Insert(0, new ListItem("Please select your unit", "-1"));

                //populate the unit list for the user to choose their unit
                List<UnitDTO> tempUnitList = unitCont.GetActiveCareSiteUnits(caresiteid);
                UnitListDDL.DataSource = tempUnitList;
                UnitListDDL.DataBind();

            }

        }
        catch (Exception ex)
        {
            //redirect to the index page with an appropriate error message
            if (codeword == "")
            {
                Response.Redirect("index.aspx?errormsg=You must enter the access code on your meal or tray ticket");
            }
            else if (caresitename == "" || string.IsNullOrWhiteSpace(caresitename))
            {
                Response.Redirect("index.aspx?errormsg=That code is not active, please check your meal or tray ticket. If problem persists this care site may be no longer supplying survey service.");
            }
            else
            {
                ErrorMSG.Text = "Login not available. If error persists, please contact covenant health. Error Message: " + ex.Message;
            }


        }
    }
    #endregion

    #region Submit Click
    /* 
    CREATED:    H. L'Heureux         MAR 15 2018
    MODIFIED:   H. Conant            MAR 27 2018
            - Updated method body
    MODIFIED:   H. Conant            MAR 29 2018
            - Updated method body to show error messages
    
    Submit_Click()
    This method checks to make sure the survey has answers in the questions. As long as one question has an answer, it will store the survey in the database and then the question answers.

    PARAMETERS:     
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    SurveyController.AddSurvey()
    SurveyAnswerController.AddSurveyAnswer()
    */
    protected void Submit_Click(object sender, EventArgs e)
    {
        //clear any errors that may have happened
        ErrorMSG.Text = "";
        ContactErr.Text = "";

        //make a regex for phone number validation
        Regex regex = new Regex(@"^(1-[1-9][0-9][0-9]|[1-9][0-9][0-9])-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]$");

        // check that at least one question or customer profile question has been answered
        if (Q1ADDL.SelectedValue != "Unselected" ||
            Q1BDDL.SelectedValue != "Unselected" ||
            Q1CDDL.SelectedValue != "Unselected" ||
            Q1DDDL.SelectedValue != "Unselected" ||
            Q1EDDL.SelectedValue != "Unselected" ||
            Q2DDL.SelectedValue != "Unselected" ||
            Q3DDL.SelectedValue != "Unselected" ||
            Q4DDL.SelectedValue != "Unselected" ||
            (Q5Text.Text.Trim().Length > 0 && Q5Text.Text.Trim().Length < 250) ||
            (AgeDDL.SelectedIndex != 0 && AgeDDL.SelectedIndex != 6) ||
            (GenderDDL.SelectedIndex != 0 && GenderDDL.SelectedIndex != 4 )||
            (ContactDDL.SelectedIndex != 0 && ContactDDL.SelectedIndex != 3)
            )
        {
            //if it has, create a new survey ID for inserting into the database
            int newSurveyID;
            SurveyDTO newSurvey = new SurveyDTO();

            //insert the survey for todays date
            newSurvey.date = DateTime.Today;
            #region Age
            //get the chosen age range, or set it to prefer not to answer
            if (AgeDDL.SelectedIndex > 0 && AgeDDL.SelectedIndex < 6)
            {
                newSurvey.agegroupid = int.Parse(AgeDDL.SelectedValue);
            }
            else
            {
                newSurvey.agegroupid = 6;
            }
            #endregion

            #region Gender
            //get the chosen gender value, or set it to preferred to not answer
            if (GenderDDL.SelectedIndex > 0 && GenderDDL.SelectedIndex < 4)
            {
                newSurvey.genderid = int.Parse(GenderDDL.SelectedValue);
            }
            else
            {
                newSurvey.genderid = 4;
            }
            #endregion

            #region First Name
            //check that the value is within the database contraint of 40 characters
            if (FirstName.Value.Length > 0 && FirstName.Value.Length <= 40)
            {
                newSurvey.firstname = FirstName.Value;
            }
            else
            {
                //check that there is a value, or show an error message to the user
                if (FirstName.Value.Length == 0)
                {
                    newSurvey.firstname = "";
                }
                else
                {
                    ErrorMSG.Text += "\nYour first name can not be longer than 40 characters. Please try again.";
                }
            }
            #endregion

            #region Bed Number
            /*check that the contact is for in person,
             * and that bed number has a value which is less than 10 characters.
             */
            if (ContactDDL.SelectedIndex == 1 && (BedNumber.Value.ToString().Length > 0 && BedNumber.Value.ToString().Length < 6))
            {
                newSurvey.bednaumber = RoomNumber.Value + "-" + BedNumber.Value;
            }
            else
            {
                //set the value to null, or show the error message
                if (ContactDDL.SelectedIndex == 1 && BedNumber.Value.ToString().Length > 10)
                {
                    ErrorMSG.Text += "Bed number cannot be longer than 4 characters. Please try again.";
                }
                else
                {
                    newSurvey.bednaumber = null;
                }
            }
            #endregion

            #region Phone Number
            if (ContactDDL.SelectedIndex == 2)
            {
                if (regex.Match(PhoneNumber.Value).Success)
                {
                    newSurvey.phonenumber = PhoneNumber.Value;
                }
                else if (regex.Match(PhoneNumber.Value).Success == false)
                {
                    
                    ContactErr.Text += "Your Phone number must be in the following format for us to contact you: 555-555-5555 or 1-555-555-5555. (# = a number)";
                }
            }
            #endregion

            #region Preferred Contact
            // get the contact preferences, or set it to default of prefer to not be contacted
            if (ContactDDL.SelectedIndex > 0)
            {
                if (ContactDDL.SelectedIndex == 1)
                {
                    //if they chose to be contacted, make sure they fill out the necessary fields
                    if (FirstName.Value.Trim().Length == 0 || BedNumber.Value.Trim().Length == 0 || BedNumber.Value.Trim().Length == 0)
                    {
                        ContactErr.Text = "<br>Please make sure you are filling out all fields if you want to be contacted.<br> Otherwise, please choose not to be contacted.<br>";
                    }

                    if(FirstName.Value.Trim().Length == 0)
                    {
                        ContactErr.Text += "<br>Enter your first name.";
                    }
                    if (RoomNumber.Value.Trim().Length == 0)
                    {
                        ContactErr.Text += "<br>Enter your room number (##-##) found at the bottom of your meal/order chit";
                    }
                     if (BedNumber.Value.Trim().Length == 0)
                    {
                        ContactErr.Text += "<br>Enter your bed number";
                    }
                     if(FirstName.Value.Trim().Length > 0 && RoomNumber.Value.Trim().Length > 0 && BedNumber.Value.Trim().Length > 0)
                    {
                        newSurvey.preferredcontact = ContactDDL.SelectedValue;
                    }
                }
                else if (ContactDDL.SelectedIndex == 2)
                {
                    if (FirstName.Value.Trim().Length == 0)
                    {
                        ContactErr.Text += "<br>Enter Your preferred first name";
                    }
                    else if (PhoneNumber.Value.Trim().Length == 0)
                    {
                        ContactErr.Text += "<br>Enter The best phone number to reach you";
                    }
                    else if(FirstName.Value.Trim().Length > 0 && PhoneNumber.Value.Trim().Length > 0)
                    {
                        newSurvey.preferredcontact = ContactDDL.SelectedValue;
                    }
                }
            }
            else
            {
                newSurvey.preferredcontact = "Prefer Not to Be Contacted";
            }
            #endregion

            #region Contacted Yes/No
            if (ContactDDL.SelectedIndex > 0  && ContactDDL.SelectedIndex < 3) 
            {
                newSurvey.contactedyn = false;
            } else
            {
                newSurvey.contactedyn = null;
            }
            #endregion

            #region Respondent Type ID
                //get the chosen respondent type, an error will display from the required field on the page
                if (ConsumerTypeDDL.SelectedIndex > 0)
            {
                newSurvey.respondenttypeid = int.Parse(ConsumerTypeDDL.SelectedValue);
            }
            #endregion

            #region Unit ID
            //get the chosen unit, an error will display from the required field on the page
            if (UnitListDDL.SelectedIndex > 0)
            {
                newSurvey.unitid = int.Parse(UnitListDDL.SelectedValue);
            }
            #endregion

            //check that there is an error message
            if (ErrorMSG.Text == "" && ContactErr.Text == "")
            {
                //save the new survey ID for the next step - survey answers
                newSurveyID = surveyCont.AddSurvey(newSurvey);

                //Insert -9 questions- into SurveyAnswer, as long as they have an answer
                if (Q1ADDL.SelectedValue != "Unselected")
                {
                    surveyAnswerCont.AddSurveyAnswer(newSurveyID, int.Parse(Q1ADDL.SelectedValue), Q1ALabel.Text, Q1ADDL.SelectedItem.Text);
                }
                if (Q1BDDL.SelectedValue != "Unselected")
                {
                    surveyAnswerCont.AddSurveyAnswer(newSurveyID, int.Parse(Q1BDDL.SelectedValue), Q1BLabel.Text, Q1BDDL.SelectedItem.Text);
                }
                if (Q1CDDL.SelectedValue != "Unselected")
                {
                    surveyAnswerCont.AddSurveyAnswer(newSurveyID, int.Parse(Q1CDDL.SelectedValue), Q1CLabel.Text, Q1CDDL.SelectedItem.Text);
                }
                if (Q1DDDL.SelectedValue != "Unselected")
                {
                    surveyAnswerCont.AddSurveyAnswer(newSurveyID, int.Parse(Q1DDDL.SelectedValue), Q1DLabel.Text, Q1DDDL.SelectedItem.Text);
                }
                if (Q1EDDL.SelectedValue != "Unselected")
                {
                    surveyAnswerCont.AddSurveyAnswer(newSurveyID, int.Parse(Q1EDDL.SelectedValue), Q1ELabel.Text, Q1EDDL.SelectedItem.Text);
                }
                if (Q2DDL.SelectedValue != "Unselected")
                {
                    surveyAnswerCont.AddSurveyAnswer(newSurveyID, int.Parse(Q2DDL.SelectedValue), Q2Label.Text.Substring(3), Q2DDL.SelectedItem.Text);
                }
                if (Q3DDL.SelectedValue != "Unselected")
                {
                    surveyAnswerCont.AddSurveyAnswer(newSurveyID, int.Parse(Q3DDL.SelectedValue), Q3Label.Text.Substring(3), Q3DDL.SelectedItem.Text);
                }
                if (Q4DDL.SelectedValue != "Unselected")
                {
                    surveyAnswerCont.AddSurveyAnswer(newSurveyID, int.Parse(Q4DDL.SelectedValue), Q4Label.Text.Substring(3), Q4DDL.SelectedItem.Text);
                }
                if (Q5Text.Text.Length > 0 && !string.IsNullOrWhiteSpace(Q5Text.Text.Trim()))
                {
                    surveyAnswerCont.AddSurveyAnswer(newSurveyID, 39, Q5Label.Text.Substring(3), Q5Text.Text.Trim());
                }

                //redirect to thank you page - > survey submited
                Response.Redirect("thankYou.aspx?accesscode=codeword1");
            }
            
        }
        else if (Q5Text.Text.Trim().Length > 250)
        {
            ErrorMSG.Text += "<br>Additional information about your meal can not be longer than 250 characters.";
        }
        else
        {
            //redirect to thank you page -> no questions answered
            Response.Redirect("thankYou.aspx?accesscode=codeword2");
        }
    }
    #endregion

    #region ContactDDL_SelectedIndexChanged
    /* 
    CREATED:    H. L'Heureux         MAR 23 2018
    MODIFIED:   H. L'Heureux         MAR 29 2018
        - added in error message clearing for phone number validation

    ContactDDL_SelectedIndexChanged()
    This method will display the labels and inputs that are related to the user's choice of the drop down list that is selected.

    PARAMETERS:     
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void ContactDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (ContactDDL.SelectedIndex)
        {

            case 1:
                //clear error message
                ErrorMSG.Text = "";
                ContactErr.Text = "";

                //show only the In Person details
                FirstNameLbl.Visible = true;
                FirstName.Visible = true;
                RoomNumber.Visible = true;
                RoomNumberLbl.Visible = true;
                BedNumber.Visible = true;
                BedNumberLbl.Visible = true;

                //hide the phone details
                PhoneNumberLbl.Visible = false;
                PhoneNumber.Visible = false;
                break;


            case 2:
                //clear error message
                ErrorMSG.Text = "";

                //show only the Over The Phone details
                FirstNameLbl.Visible = true;
                FirstName.Visible = true;
                PhoneNumberLbl.Visible = true;
                PhoneNumber.Visible = true;

                //hide the in person details
                RoomNumber.Visible = false;
                RoomNumberLbl.Visible = false;
                BedNumber.Visible = false;
                BedNumberLbl.Visible = false;
                break;
            case 0:
            case 3:
            default:
                //clear error message
                ErrorMSG.Text = "";

                //hide everything
                FirstNameLbl.Visible = false;
                FirstName.Visible = false;
                RoomNumber.Visible = false;
                RoomNumberLbl.Visible = false;
                BedNumber.Visible = false;
                BedNumberLbl.Visible = false;
                PhoneNumberLbl.Visible = false;
                PhoneNumber.Visible = false;
                break;
        }

    }
    #endregion
}
