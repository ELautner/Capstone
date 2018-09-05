using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string error = Request.QueryString["errorMSG"];

        ClearMsgs();

        if (!IsPostBack)
        {
            ErrorMSG.Text = error;
        }
    }

    /* 
    CREATED:      H. L'Heureux         MAR 13 2018
    
    Submit_Click()
    This method is called when the user clicks or taps on the enter survey button to access the survey.

    PARAMETERS:     
    object sender - the element making the call for the method
    EventArgs e - the additional details of the elements that is needed

    RETURNS: 
    None

    METHOD CALLS: 
    ClearMsgs()
    */
    protected void Submit_Click(object sender, EventArgs e)
    {
        //TODO: Make sure the access code is the ACTIVE code in the database
        //HOW DO I DO THIS AGAIN?
        ClearMsgs();
    }

    /* 
    CREATED:      H. L'Heureux         MAR 13 2018
    
    ClearMsgs()
    This method is called by other methods in order to clear any error messages.

    PARAMETERS:     
    None

    RETURNS: 
    none

    METHOD CALLS: 
    None
    */
    private void ClearMsgs()
    {
        ErrorMSG.Text = "";
    }
}