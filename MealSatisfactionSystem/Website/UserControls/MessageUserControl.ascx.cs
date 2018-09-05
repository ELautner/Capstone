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

FIELDS:
private const string ERROR_CSS - the CSS class applied to the error message panel
private const string SUCCESS_CSS - the CSS class applied to the success message panel
private const string INFO_CSS - the CSS class applied to the info message panel

private const string ERROR_TITLE - the title of the error message panel
private const string SUCCESS_TITLE - the title of the success message panel
private const string INFO_TITLE - the title of the info message panel

METHODS:
protected void Page_Load(object sender, EventArgs e) - is run on page load and hides all previous messages
public void ShowErrorMessage(string message) - passed message is displayed as an error message
public void ShowErrorMessage(string message, Exception ex) - passed message is displayed as an error message and the Exception message is extracted and displayed
public void ShowSuccessMessage(string message) - passed message is displayed as a success message
public void ShowInfoMessage(string message) - passed message is displayed as an information message
public void ShowInfoList(string message, List<string> messageList) - passed list of messages is displayed as an unordered list
*/
public partial class UserControls_MessageUserControl : System.Web.UI.UserControl
{
    private const string ERROR_CSS = "muc muc-danger";
    private const string SUCCESS_CSS = "muc muc-success";
    private const string INFO_CSS = "muc muc-info";

    private const string ERROR_TITLE = "System Error: ";
    private const string SUCCESS_TITLE = "Success: ";
    private const string INFO_TITLE = "System Message: ";

    /* 
    CREATED: 	C. Stanhope		MAR 11 2018

    Page_Load()
    This method is run on page load and is used to hides all previous messages.
    
    PARAMETERS: 	
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void
    
    ODEV METHOD CALLS: 
    None
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemError.Visible = false;
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

    ODEV METHOD CALLS: 
    None
    */
    public void ShowErrorMessage(string message)
    {
        MessageTitle.Text = ERROR_TITLE;
        MessageLabel.Text = message;
        MessagePanel.CssClass = ERROR_CSS;
        MessagePanel.Visible = true;

        MessageListRepeater.Visible = false;
    }

    /* 
    CREATED: 	C. Stanhope		APR 11 2018

    ShowErrorMessage()
    Displays the passed message as an error message with appropriate styles applied to the message user control panel. 
    
    PARAMETERS: 	
    string message - the error message to be displayed

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    public void ShowErrorMessage(string message, Exception ex)
    {
        MessageTitle.Text = ERROR_TITLE;
        MessageLabel.Text = message;
        MessagePanel.CssClass = ERROR_CSS;

        string fullMessage = ex.InnerException.ToString();

        string splitMessage = fullMessage.Split(new char[] { '>' }, 2)[0];
        string realMessage = splitMessage.Remove(splitMessage.Length - 41);        
        
        SystemError.Text = realMessage;

        SystemError.Visible = true;
        MessagePanel.Visible = true;

        MessageListRepeater.Visible = false;

        MessageTitle.Text = ERROR_TITLE;
        MessageLabel.Text = message;
        MessagePanel.CssClass = ERROR_CSS;
    }

    /* 
    CREATED: 	C. Stanhope		MAR 11 2018

    ShowSuccessMessage()
    Displays the passed message as a success message with appropriate styles applied to the message user control panel.
    
    PARAMETERS: 	
    string message - the success message to be displayed

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    public void ShowSuccessMessage(string message)
    {
        MessageTitle.Text = SUCCESS_TITLE;
        MessageLabel.Text = message;
        MessagePanel.CssClass = SUCCESS_CSS;
        MessagePanel.Visible = true;

        MessageListRepeater.Visible = false;
    }

    /* 
    CREATED: 	C. Stanhope		MAR 11 2018

    ShowInfoMessage()
    Displays the passed message as an information message with appropriate styles applied to the message user control panel.
    
    PARAMETERS: 	
    string message - the information message to be displayed

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    public void ShowInfoMessage(string message)
    {
        MessageTitle.Text = INFO_TITLE;
        MessageLabel.Text = message;
        MessagePanel.CssClass = INFO_CSS;
        MessagePanel.Visible = true;

        MessageListRepeater.Visible = false;
    }

    /* 
    CREATED: 	C. Stanhope	    APR 2 2018

    ShowInfoList()
    Displays the passed message and message list as an information message with appropriate styles applied to the message user control panel.
    
    PARAMETERS: 	
    string leadingMessage - the leading information message to be displayed
    List<string> messageList - the list of messages to display in a list

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    public void ShowInfoList(string leadingMessage, List<string> messageList)
    {
        MessageTitle.Text = INFO_TITLE;
        MessageLabel.Text = leadingMessage;

        MessagePanel.CssClass = INFO_CSS;
        MessagePanel.Visible = true;

        MessageListRepeater.Visible = true;
        MessageListRepeater.DataSource = messageList;
        MessageListRepeater.DataBind();
    }
}