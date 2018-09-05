using Microsoft.AspNet.Identity;
using MSS.Data.DTOs;
using MSS.Data.Entities;
using MSS.Data.Entities.Security;
using MSS.System.BLL;
using MSS.System.BLL.Security;
using System;
using System.Collections.Generic;

/* 
CREATED:	H. Conant		MAR 17 2018

Management_survey_details
Contains methods necessary to display a surveys details to a user and allow them to process a contact request.
This page refreshes if the back button is pressed. A message is displayed on the screen warning of this.

FIELDS:
internal SurveyController surveyController - allows methods from the controller to be called
internal SurveyAnswerController surveyAnswerController - allows methods from the controller to be called
internal UnitController unitController  - allows methods from the controller to be called

METHODS:
protected void Page_Load(object sender, EventArgs e) - this method runs any code it contains when the page loads
protected void ProcessRequestButton_Click(object sender, EventArgs e) - this method processes a contact request and displays the new status to the user
*/
public partial class Management_survey_details : System.Web.UI.Page
{
    internal SurveyController surveyController = new SurveyController();
    internal SurveyAnswerController surveyAnswerController = new SurveyAnswerController();
    internal SurveyQuestionController surveyQuestionController = new SurveyQuestionController();
    internal UnitController unitController = new UnitController();

    /* 
    CREATED: 	H. Conant		MAR 17 2018

    Page_Load()
    This method runs any code it contains when the page loads. 
    Will obtain survey details, questions and answers to the questions.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    SurveyController.GetSurveyDetails()
    MessageUserControl.ShowErrorMessage()
    UserManager.FindById()
    UnitController.GetUnit()
    SurveyAnswerController.GetSurveyAnswers()
    SurveyQuestionController.GetSurveyQuestions()
    MessageUserControl.ShowInfoMessage()
    */
    protected void Page_Load(object sender, EventArgs e)
    { 
        int surveyId = 0;
        int.TryParse(Request.QueryString["id"], out surveyId);

        if (surveyId == 0)
        {
            Response.Redirect("/Management/contact_requests.aspx");

        } else
        {
            ExtendedSurveyDTO survey = new ExtendedSurveyDTO();

            try
            {
                survey = surveyController.GetSurveyDetails(surveyId);



            } catch (Exception ex)
            {
                  if (survey.surveyid != -1)
                {
                    MessageUserControl.ShowErrorMessage("Could not retrieve survey. Please try again. If error persists, please contact your administrator.", ex);
                }
                
            }

            if (survey.surveyid == 0 || string.IsNullOrEmpty(survey.surveyid.ToString()) || survey.surveyid == -1)
            {
                Response.Redirect("/Management/contact_requests.aspx");

            } else
            {

                if (User.IsInRole(AuthorizationLevelRoles.User))
                {
                    UserManager userManager = new MSS.System.BLL.Security.UserManager();
                    ApplicationUser account = new ApplicationUser();

                    try
                    {
                        account = userManager.FindById(User.Identity.GetUserId());
                    }
                    catch (Exception ex)
                    {
                        MessageUserControl.ShowErrorMessage("Could not retrieve user accounts. Please try again. If error persists, please contact your administrator.", ex);
                        
                    }
                    
                    CareSiteController careSiteController = new CareSiteController();

                    int accountCareSiteId = account.caresiteid == null ? 0 : (int)account.caresiteid;

                    if (accountCareSiteId == 0)
                    {
                        Response.Redirect("/Management/contact_requests.aspx");

                    }
                    else
                    {
                        UnitDTO surveyUnit = new UnitDTO();

                        try
                        {
                            surveyUnit = unitController.GetUnit(survey.unitid);
                        }
                        catch (Exception ex)
                        {
                            MessageUserControl.ShowErrorMessage("Could not retrieve care site. Please try again. If error persists, please contact your administrator.", ex);
                            survey = new ExtendedSurveyDTO();
                        }

                        if (surveyUnit.caresiteid != accountCareSiteId)
                        {
                            Response.Redirect("/Management/contact_requests.aspx");
                        }

                    }
                }
                   
                SurveyIDLabel.Text = survey.surveyid.ToString();

                DateLabel.Text = survey.date.ToString("MMM dd, yyyy");

                UnitLabel.Text = survey.unitname;

                CareSiteLabel.Text = survey.caresitename;

                RespondentTypeLabel.Text = survey.respondenttypename;

                AgeLabel.Text = survey.agegroupname;

                
                GenderLabel.Text = survey.gendername;
                        


                if (string.IsNullOrEmpty(survey.firstname))
                {
                    Name.Text = "Name not provided";

                } else
                {
                    Name.Text = survey.firstname;

                }

                BedNumber.Text = String.IsNullOrEmpty(survey.bednaumber) ? "Not Provided" : survey.bednaumber;

                PhoneNumber.Text = String.IsNullOrEmpty(survey.phonenumber) ? "Not Provided" : survey.phonenumber;

                if (survey.preferredcontact == null || survey.preferredcontact == "I don't want to be contacted")
                {
                    BedNumberLabel.Visible = false;
                    BedNumber.Visible = false;
                    PhoneNumberLabel.Visible = false;
                    PhoneNumber.Visible = false;
                    ProcessedStatusLabel.Visible = false;
                    ProcessedStatus.Visible = false;
                    ProcessRequestButton.Enabled = false;
                    ProcessRequestButton.Visible = false;
                    PreferredContactLabel.Text = String.IsNullOrEmpty(survey.preferredcontact) ? "I don't want to be contacted" : survey.preferredcontact;

                }
                else
                {
                    PreferredContactLabel.Text = survey.preferredcontact;

                    if (BedNumber.Text == "Not Provided")
                    {
                        BedNumberLabel.Visible = false;
                        BedNumber.Visible = false;

                    }
                    else
                    {
                        BedNumberLabel.Visible = true;
                        BedNumber.Visible = true;
                    }

                    if (PhoneNumber.Text == "Not Provided")
                    {
                        PhoneNumberLabel.Visible = false;
                        PhoneNumber.Visible = false;

                    }
                    else
                    {
                        PhoneNumberLabel.Visible = true;
                        PhoneNumber.Visible = true;
                    }

                    if (survey.contactedyn == true)
                    {
                        ProcessedStatus.Text = "Processed";
                        ProcessedStatus.ForeColor = System.Drawing.Color.Green;
                        ProcessRequestButton.Enabled = false;
                        ProcessRequestButton.Visible = false;

                    }
                    else if (survey.contactedyn == false)
                    {
                        ProcessedStatus.Text = "Not Processed";
                        ProcessedStatus.ForeColor = System.Drawing.Color.Red;
                        ProcessRequestButton.Enabled = true;
                        ProcessRequestButton.Visible = true;

                    }
                    else
                    {
                        ProcessedStatus.Text = "Contact not requested";
                        ProcessedStatus.ForeColor = System.Drawing.Color.Black;
                        ProcessRequestButton.Enabled = false;
                        ProcessRequestButton.Visible = false;
                    }
                }

                List <surveyanswer> surveyAnswers = new List<surveyanswer>();
                List<surveyquestion> surveyQuestions = new List<surveyquestion>();

                try
                {
                    surveyAnswers = surveyAnswerController.GetSurveyAnswers(survey.surveyid);
                    surveyQuestions = surveyQuestionController.GetSurveyQuestions();

                } catch(Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Could not retrieve survey answers or questions. Please try again. If error persists, please contact your administrator.", ex);
                }


                if (surveyQuestions.Count > 0)
                {
                    foreach (surveyquestion question in surveyQuestions)
                    {
                        switch (question.surveyquestionid)
                        {
                            case 1:
                                Question1aLabel.Text = question.question;
                                break;
                            case 2:
                                Question1bLabel.Text = question.question;
                                break;
                            case 3:
                                Question1cLabel.Text = question.question;
                                break;
                            case 4:
                                Question1dLabel.Text = question.question;
                                break;
                            case 5:
                                Question1eLabel.Text = question.question;
                                break;
                            case 6:
                                Question2Label.Text = question.question;
                                break;
                            case 7:
                                Question3Label.Text = question.question;
                                break;
                            case 8:
                                Question4Label.Text = question.question;
                                break;
                            case 9:
                                Question5Label.Text = question.question;
                                break;
                            default:
                                MessageUserControl.ShowInfoMessage("New questions have been added to the survey. Please contact your administrator to update this webpage.");
                                break;
                        }
                    }
                
                    if (surveyAnswers.Count > 0)
                    {
                       
                        foreach (surveyanswer answer in surveyAnswers)
                        {

                            switch (answer.possibleanswer.surveyquestionid)
                            {
                                case 1:
                                    Question1aLabel.Text = (answer.historicalquestion == null) ? Question1aLabel.Text : answer.historicalquestion;
                                    Answer1.Text = (answer.answer == null) ? "- No response -" : answer.answer; 
                                    break;
                                case 2:
                                    Question1bLabel.Text = (answer.historicalquestion == null) ? Question1bLabel.Text : answer.historicalquestion;
                                    Answer2.Text = (answer.answer == null) ? "- No response -" : answer.answer;
                                    break;
                                case 3:
                                    Question1cLabel.Text = (answer.historicalquestion == null) ? Question1cLabel.Text : answer.historicalquestion;
                                    Answer3.Text = (answer.answer == null) ? "- No response -" : answer.answer;
                                    break;
                                case 4:
                                    Question1dLabel.Text = (answer.historicalquestion == null) ? Question1dLabel.Text : answer.historicalquestion;
                                    Answer4.Text = (answer.answer == null) ? "- No response -" : answer.answer;
                                    break;
                                case 5:
                                    Question1eLabel.Text = (answer.historicalquestion == null) ? Question1eLabel.Text : answer.historicalquestion;
                                    Answer5.Text = (answer.answer == null) ? "- No response -" : answer.answer;
                                    break;
                                case 6:
                                    Question2Label.Text = (answer.historicalquestion == null) ? Question2Label.Text : answer.historicalquestion;
                                    Answer6.Text = (answer.answer == null) ? "- No response -" : answer.answer;
                                    break;
                                case 7:
                                    Question3Label.Text = (answer.historicalquestion == null) ? Question3Label.Text : answer.historicalquestion;
                                    Answer7.Text = (answer.answer == null) ? "- No response -" : answer.answer;
                                    break;
                                case 8:
                                    Question4Label.Text = (answer.historicalquestion == null) ? Question4Label.Text : answer.historicalquestion;
                                    Answer8.Text = (answer.answer == null) ? "- No response -" : answer.answer;
                                    break;
                                case 9:
                                    Question5Label.Text = (answer.historicalquestion == null) ? Question5Label.Text : answer.historicalquestion;
                                    Answer9.Text = (answer.answer == null) ? "- No response -" : answer.answer;
                                    break;
                                default:
                                    MessageUserControl.ShowInfoMessage("New questions have been added to the survey. Please contact your administrator to update this webpage.");
                                    break;
                            }

                        }

                    } else
                    {
                        MessageUserControl.ShowInfoMessage("Survey has no answers.");
                    }

                } else
                {
                    MessageUserControl.ShowInfoMessage("There are not questions in the database.");
                }

            }


       }

   }

    /* 
    CREATED: 	H. Conant		MAR 17 2018

    ProcessRequestButton_Click()
    This method processes a contact request and displays the new status to the user

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    SurveyController.ProcessContactRequest()
    MessageUserControl.ShowErrorMessage()
    MessageUserControl.ShowSuccessMessage()
    */
    protected void ProcessRequestButton_Click(object sender, EventArgs e)
     {
        try
        {
            surveyController.ProcessContactRequest(int.Parse(SurveyIDLabel.Text));
            ProcessedStatus.Text = "Processed";

        }
        catch (Exception ex)
        {
            MessageUserControl.ShowErrorMessage("Could not process contact request. Please try again. If error persists, please contact your administrator.", ex);
        } 


            if (ProcessedStatus.Text == "Processed")
            {
                ProcessedStatus.ForeColor = System.Drawing.Color.Green;
                ProcessRequestButton.Enabled = false;
                ProcessRequestButton.Visible = false;
                MessageUserControl.ShowSuccessMessage("Contact Request was processed. This change may not be reflected on other pages until page results are refreshed.");
            }
            else
            {
                MessageUserControl.ShowErrorMessage("Could not process contact request. Please try again. If error persists, please contact your administrator.");
            }
           
    }
}
