using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/* 
CREATED:	C. Stanhope		Mar 10 2018

UserControls_MessageUserControl
This class is used to display error messages to the user. This code simplified the error message process and allows the same syntax and layout to be used throughout the website.

CLASS-LEVEL VARIABLES:
private const string ERROR_CSS - the CSS class applied to the error message panel
private const string SUCCESS_CSS - the CSS class applied to the success message panel
private const string INFO_CSS - the CSS class applied to the info message panel
private const string CODING_ERR_CSS - the CSS class applied to the coding error message panel CCC: delete

private const string ERROR_TITLE - the title of the error message panel
private const string SUCCESS_TITLE - the title of the success message panel
private const string INFO_TITLE - the title of the info message panel
private const string CODING_ERR_TITLE - the title of the coding error message panel CCC: delete

METHODS:
protected void Page_Load(object sender, EventArgs e) - is run on page load and hides all previous messages
public void ShowErrorMessage(string message) - passed message is displayed as an error message
public void ShowSuccessMessage(string message) - passed message is displayed as a success message
public void ShowInfoMessage(string message) - passed message is displayed as an information message
*/


public partial class UserControls_MessageUserControl : System.Web.UI.UserControl
{
    // CCC: make these not global????
    private const string ERROR_CSS = "muc muc-danger";
    private const string SUCCESS_CSS = "muc muc-success";
    private const string INFO_CSS = "muc muc-info";
    private const string CODING_ERR_CSS = "muc muc-warning";

    private const string ERROR_TITLE = "Error: ";
    private const string SUCCESS_TITLE = "Success: ";
    private const string INFO_TITLE = "User Message: ";
    private const string CODING_ERR_TITLE = "CODING ERROR: ";

    /* 
    CREATED: 	C. Stanhope		MAR 11 2018

    Page_Load()
    This method is run on page load and is used to hides all previous messages.
    
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
        MessagePanel.Visible = false;
    }

    /* 
    CREATED: 	C. Stanhope		MAR 11 2018

    ShowErrorMessage()
    Displays the passed message as an error message with appropriate styles applied to the message user control panel.
    
    PARAMETERS: 	
    string message - the error message to be displayed

    RETURNS: 
    void

    METHOD CALLS: 
    None
    */
    public void ShowErrorMessage(string message)
    {
        MessageTitle.Text = ERROR_TITLE;
        MessageLabel.Text = message;
        MessagePanel.CssClass = ERROR_CSS;
        MessagePanel.Visible = true;
    }

    /* 
    CREATED: 	C. Stanhope		MAR 11 2018

    ShowSuccessMessage()
    Displays the passed message as a success message with appropriate styles applied to the message user control panel.
    
    PARAMETERS: 	
    string message - the success message to be displayed

    RETURNS: 
    void

    METHOD CALLS: 
    None
    */
    public void ShowSuccessMessage(string message)
    {
        MessageTitle.Text = SUCCESS_TITLE;
        MessageLabel.Text = message;
        MessagePanel.CssClass = SUCCESS_CSS;
        MessagePanel.Visible = true;
    }

    /* 
    CREATED: 	C. Stanhope		MAR 11 2018
    MODIFIED: 	

    ShowInfoMessage()
    Displays the passed message as an information message with appropriate styles applied to the message user control panel.
    
    PARAMETERS: 	
    string message - the information message to be displayed

    RETURNS: 
    void

    METHOD CALLS: 
    None
    */
    public void ShowInfoMessage(string message)
    {
        MessageTitle.Text = INFO_TITLE;
        MessageLabel.Text = message;
        MessagePanel.CssClass = INFO_CSS;
        MessagePanel.Visible = true;
    }
}

/*
 * Keep cause eff this stupid code CCC: delete me later
 * 
public partial class UserControls_MessageUserControl : System.Web.UI.UserControl
{
    private const string ERROR_CSS = "card card-danger";
    private const string SUCCESS_CSS = "card card-success";
    private const string INFO_CSS = "card card-info";
    private const string CODING_ERR_CSS = "card card-warning";

    protected void Page_Load(object sender, EventArgs e)
    {
        MessageCard.Visible = false;
    }

    // Show Message types:
    //      e - error
    //      s - success
    //      i - info
    public void ShowMessage(char type, string message)
    {
        switch (type)
        {
            case 'e': // error
                MessageTitle.Text = "Error:";
                MessageLabel.Text = message;
                MessageCard.Attributes["class"] = ERROR_CSS;
                break;
            case 's': // success
                MessageTitle.Text = "Success:";
                MessageLabel.Text = message;
                MessageCard.Attributes["class"] = SUCCESS_CSS;
                break;
            default:
                MessageTitle.Text = "CODING ERROR:";
                MessageLabel.Text = "The message type selected does not exist. Please revise code.";
                MessageCard.Attributes["class"] = CODING_ERR_CSS;
                break;
        }
        MessageCard.Visible = true;
    }

}




 */
