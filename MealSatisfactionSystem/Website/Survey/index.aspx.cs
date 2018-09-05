using System;

/* 
CREATED:    H. L'Heureux        MAR 16 2018

Survey_index
This class contains methods to show the page to the user. These methods are for initial set-up of the page (Page_Load) as well as a method that the user interacts with the page when they want to enter into the survey (Submit_Click) 

FIELDS:
None

METHODS:
protected void Page_Load(object sender, EventArgs e) - this method gets the information sent in and then sets up the page
protected void Submit_Click(object sender, EventArgs e) - this method gets the information sent in and then sends the user to the survey page
private void ClearMsgs() - this method takes in no information and clears any error messages
*/
public partial class Survey_index : System.Web.UI.Page
{

    #region Page_Load
    /* 
    CREATED:      H. L'Heureux         MAR 16 2018

    Page_Load()
    This method sets up the page when the user first opens it up. 

    PARAMETERS:     
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    ClearMsgs()
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        //retrieve the error message (if applicable)
        string error = Request.QueryString["errorMSG"];

        //clear out old error messages
        ClearMsgs();

        if (!IsPostBack)
        {
            //display the error
            ErrorMSG.Text = error;
        }
        else
        {
            ErrorMSG.Text = "";
        }
    }
    #endregion

    #region Submit_Click
    /* 
    CREATED:      H. L'Heureux         MAR 13 2018

    Submit_Click()
    This method is called when the user clicks or taps on the enter survey button to access the survey.

    PARAMETERS:     
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    ClearMsgs()
    */
    protected void Submit_Click(object sender, EventArgs e)
    {
        //clear any error messages displayed currently
        ClearMsgs();
        //get the access code the user entered in the textbox
        string userCodeWord = AccessText.Text;
        //send the user to the survey page
        Response.Redirect("survey.aspx?accesscode=" + userCodeWord);
    }
    #endregion

    #region ClearMsgs
    /* 
    CREATED:      H. L'Heureux         MAR 13 2018

    ClearMsgs()
    This method is called by other methods in order to clear any error messages.

    PARAMETERS:     
    None

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    private void ClearMsgs()
    {
        //clears any error message text
        ErrorMSG.Text = "";
    } 
    #endregion
}
