using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/* 
CREATED:    C. Stanhope        MAR 10 2018

Management_manage_access_codes
An automatically generated page used to store the manage_access_codes's backend C# code.

CLASS-LEVEL VARIABLES:
internal AccessCodeController accessCodeController - initialising the AccessCodeController to be used by the methods in the class

METHODS:
protected void Page_Load(object sender, EventArgs e) - triggered on page load
protected void AddAccessCodeButton_Click(object sender, EventArgs e) -  adds a new access code to the database
protected void LetterLinkButton_Click(object sender, EventArgs e) - changes search keyword to search by the selected letter
protected void ClearPage() - clears all text boxes on the page
protected void ClearSearchButton_Click(object sender, EventArgs e) - clears search textbox
*/

public partial class Management_manage_access_codes : System.Web.UI.Page
{
    internal AccessCodeController accessCodeController = new AccessCodeController();

    /* 
    CREATED: 	C. Stanhope		MAR 13 2018

    Page_Load()
    Run on page load and is used to .... CCC

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
        // CCC: delete me?
    }


    #region Button Methods
    /* 
    CREATED:      C. Stanhope         MAR 13 2018

    AddAccessCodeButton_Click()
    Triggered when "AddAccessCodeButton" is clicked and is used to add an access code to the database.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    METHOD CALLS: 
    AccessCodeController.AddAccessCode()
    MessageUserControl.ShowSuccessMessage()
    */
    protected void AddAccessCodeButton_Click(object sender, EventArgs e)
    {
        AccessCodeDTO newAccessCode = new AccessCodeDTO();
        newAccessCode.accesscodeword = AddAccessCodeTB.Text;
        newAccessCode.activeyn = true; // defaults to active

        accessCodeController.AddAccessCode(newAccessCode);

        MessageUserControl.ShowSuccessMessage("New access code added!");
        ClearPage();
    }

    /* 
    CREATED:      C. Stanhope         MAR 13 2018

    LetterLinkButton_Click()
    Triggered when a "LetterLinkButton" is clicked and is used to determine which letter link was clicked on the page and refresh the search and access code list to match.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    METHOD CALLS: 
    None
    */
    protected void LetterLinkButton_Click(object sender, EventArgs e)
    {
        LinkButton letterButton = (LinkButton)sender;
        string selectedLetter = letterButton.Text;

        // an underscore ("_") is used to indicate that only words starting with the given letter are desired
        SearchKeywordTB.Text = selectedLetter + "_";
    }

    /* 
    CREATED:      C. Stanhope         MAR 13 2018

    ClearSearchButton_Click()
    Triggered when "ClearSearchButton" is clicked and is used to clear the search keyword checkbox and therefore refresh the search keyword to empty.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    METHOD CALLS: 
    None
    */
    protected void ClearSearchButton_Click(object sender, EventArgs e)
    {
        SearchKeywordTB.Text = "";
    }
    #endregion

    #region Helper Methods

    /* 
    CREATED:    C. Stanhope         MAR 13 2018  

    ClearPage()
    Clears all text boxes on the page.

    PARAMETERS:     
    None

    RETURNS: 
    void

    METHOD CALLS: 
    None
    */
    protected void ClearPage()
    {
        AddAccessCodeTB.Text = "";
        SearchKeywordTB.Text = "";
        // CCC TODO: refresh access code list
    }

    #endregion

    
}