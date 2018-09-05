using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

/* 
CREATED:    C. Stanhope         MAR 10 2018

Management_manage_access_codes
An automatically generated page used to store the manage_access_codes's backend C# code.

FIELDS:
internal AccessCodeController accessCodeController - initialising the AccessCodeController to be used by the methods in the class
internal readonly string containsRadioButtonValue - stores the value of the radio button "contains"
internal readonly string startsWithRadioButtonValue - stores the value of the radio button "starts with"
internal readonly string matchExactlyRadioButtonValue - stores the value of the radio button "match exactly"
internal static List<AccessCodeDTO> accessCodeList - list of the access codes currently displayed in the AccessCodeTableRepeater
internal static AccessCodeDTO selectedAccessCode - the access code selected from the access code table to be updated

METHODS: 
protected void Page_Load(object sender, EventArgs e) - initialises list on first page load
protected void AddAccessCodeButton_Click(object sender, EventArgs e) - adds a new access code to the database
protected void SearchButton_Click(object sender, EventArgs e) - searches access codes by the user's parameters on the page
protected void BindAccessCodeRepeater() - binds the accessCodeList variable to the access code table repeater
protected void LetterLinkButton_Click(object sender, EventArgs e) - changes search keyword to search by the selected letter
protected void ClearSearchButton_Click(object sender, EventArgs e) - calls the ResetSearchFilters() method
protected void AccessCodeWordButton_Click(object sender, EventArgs e) - populates the update box with the selected word's information
protected void UpdateAccessCodeButton_Click(object sender, EventArgs e) - updates the selected word to the new parameters entered on the page
protected bool ValidateAccessCodeWord(string word) - checks if the access code word is between 6 and 8 characters
protected void ClearPage() - clears all text boxes on the page
protected void ResetSearchFilters() - resets search to default values, clears access code table and update box
*/

public partial class Management_manage_access_codes : System.Web.UI.Page
{
    internal AccessCodeController accessCodeController = new AccessCodeController();
    internal readonly string containsRadioButtonValue = "contains";
    internal readonly string startsWithRadioButtonValue = "starts";
    internal readonly string matchExactlyRadioButtonValue = "match";

    internal static List<AccessCodeDTO> accessCodeList;
    internal static AccessCodeDTO selectedAccessCode;

    /* 
    CREATED: 	C. Stanhope		MAR 13 2018
    MODIFIED:   C. Stanhope     MAR 17 2018
        - added reset of search filters
    MODIFIED:   C. Stanhope     MAR 19 2018
        - added instantiation of accessCodeList

    Page_Load()
    Used to set the search filters to their defaults the first time the page is loaded.

    PARAMETERS:
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void
    
    ODEV METHOD CALLS: 
    ResetSearchFilters()
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack){ // only run when page is first loaded
            accessCodeList = new List<AccessCodeDTO>();
            ResetSearchFilters();
        }
    }

    #region for Adding Access Codes
    /* 
    CREATED:    C. Stanhope     MAR 13 2018
    MODIFIED:   C. Stanhope     MAR 21 2018
        - added ResetSearchFilters() method call
        - added validation
    MODIFIED:   C. Stanhope     APR 5 2018
        - new word trims whitespace
    MODIFIED:   C. Stanhope     APR 6 2018
        - added try-catch for database access

    AddAccessCodeButton_Click()
    Triggered when "AddAccessCodeButton" is clicked and is used to add an access code to the database.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    ValidateAccessCodeWord()
    AccessCodeController.AddAccessCode()
    MessageUserControl.ShowSuccessMessage()
    ClearPage()
    ResetSearchFilters()
    MessageUserControl.ShowInfoMessage()
    MessageUserControl.ShowErrorMessage()
    */
    protected void AddAccessCodeButton_Click(object sender, EventArgs e)
    {
        string newWord = AddAccessCodeTB.Text.ToLower().Trim();
        if (newWord.Length > 0)
        {
            if (ValidateAccessCodeWord(newWord))
            {
                AccessCodeDTO newAccessCode = new AccessCodeDTO();
                newAccessCode.accesscodeword = newWord;
                newAccessCode.activeyn = true; // defaults to active

                try
                {
                    accessCodeController.AddAccessCode(newAccessCode);

                    MessageUserControl.ShowSuccessMessage("New access code '" + newWord + "' added");
                    ClearPage();
                    ResetSearchFilters();
                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Adding access code failed. Please try again. If error persists, please contact your administrator.", ex);
                }
            }
            else // invalid code word
            {
                MessageUserControl.ShowInfoMessage("The access code '" + newWord + "' is not valid. Please ensure the access code is between 6 and 8 letters (no numbers or symbols are permitted).");
            }
        }
        else // no word entered
        {
            MessageUserControl.ShowInfoMessage("No access code word was entered. Please enter a word between 6 and 8 letters (no numbers or symbols are permitted).");
        }
    }
    #endregion

    #region for Searching Access Codes
    /* 
    CREATED:    C. Stanhope         MAR 17 2018
    MODIFIED:   C. Stanhope         MAR 19 2018
        - modified to reflect globalization of accessCodeList
        - added visibility change of update and word list sections
    MODIFIED:   C. Stanhope         APR 5 2018
        - trims whitespace from inputs
    MODIFIED:   C. Stanhope     APR 6 2018
        - added try-catch for database access

    SearchButton_Click()
    Triggered when the "SearchButton" is clicked and is used to collect filtering data from the page and display access codes based on the search parameters.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    AccessCodeController.GetAccessCodesByStatus()
    AccessCodeController.GetAccessCodesByKeyword()
    AccessCodeController.GetAccessCodesByExactMatch()
    AccessCodeController.GetAccessCodesByStartingLetter()
    BindAccessCodeRepeater()
    MessageUserControl.ShowErrorMessage()
    MessageUserControl.ShowInfoMessage()
    */
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        // hide previous search and update results
        AccessCodeTableContainer.Visible = false;
        UpdateAccessCodeContainer.Visible = false;
        selectedAccessCode = null;

        string searchText = SearchKeywordTB.Text;

        if(searchText.Length > 0 && searchText.Trim().Length == 0) // if the only characters entered into the box are spaces
        {
            MessageUserControl.ShowInfoMessage("No words found matching the entered search criteria.");
            SearchKeywordTB.Text = "";
        }
        else // if more than spaces are entered into the textbox. Or of the textbox is empty
        {
            searchText = searchText.Trim();
            SearchKeywordTB.Text = searchText; // clearing whitespace from the textbox

            string searchType = SearchTypeRadioButtonList.SelectedValue;

            bool? activeStatus = null; // if null, show both active and inactive. if false, show inactive only. if true show active only
            #region Setting activeStatus variable
            bool showActive = ShowActiveCheckbox.Checked;
            bool showInactive = ShowInactiveCheckbox.Checked;

            if (showActive && !showInactive)
            {
                activeStatus = true;
            }
            else if (!showActive && showInactive)
            {
                activeStatus = false;
            }
            #endregion

            if (showActive || showInactive) // at least one active status checkbox is selected
            {
                try
                {
                    #region select appropriate search type (keyword, match exactly, starts with)
                    if (searchText.Length == 0) // no search text entered, just active status filtering
                    {
                        accessCodeList = accessCodeController.GetAccessCodesByStatus(activeStatus);
                    }
                    else // some search text exists in textbox
                    {
                        if (searchType.Equals(containsRadioButtonValue)) // keyword
                        {
                            accessCodeList = accessCodeController.GetAccessCodesByKeyword(searchText, activeStatus);
                        }
                        else if (searchType.Equals(matchExactlyRadioButtonValue)) // match exactly
                        {
                            accessCodeList = accessCodeController.GetAccessCodesByExactMatch(searchText, activeStatus);
                        }
                        else if (searchType.Equals(startsWithRadioButtonValue)) // starts with
                        {
                            accessCodeList = accessCodeController.GetAccessCodesByStartingLetter(searchText, activeStatus);
                        }
                        else // something broke
                        {
                            MessageUserControl.ShowErrorMessage("Search type does not exist. Please report to administrator.");
                            accessCodeList.Clear();
                        }
                    }
                    #endregion

                    if (accessCodeList.Count() > 0)
                    {
                        // display access code table
                        AccessCodeTableContainer.Visible = true;

                        // when new data is received, bind Repeater
                        BindAccessCodeRepeater();
                    }
                    else // no access codes found matching search
                    {
                        MessageUserControl.ShowInfoMessage("No words found matching the entered search criteria.");
                    }
                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Searching access code in the database failed. Please try again. If error persists, please contact your administrator.", ex);
                }
            }
            else // no active status checkbox is selected
            {
                MessageUserControl.ShowInfoMessage("Please select at least one active status to filter by.");
            }
        }
    }

    /* 
    CREATED:    C. Stanhope         MAR 17 2018
    MODIFIED:   C. Stanhope         MAR 19 2018
        - changed to bind to new repeater
        - changed parameters to reflect globalization of accessCodeList

    BindAccessCodeRepeater()
    Used to bind the accessCodeList to the AccessCodeTableRepeater.

    PARAMETERS:     
    None

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void BindAccessCodeRepeater()
    {
        AccessCodeTableRepeater.DataSource = accessCodeList;
        AccessCodeTableRepeater.DataBind();
    }

    /* 
    CREATED:      C. Stanhope         MAR 13 2018
    MODIFIED:     C. Stanhope         MAR 17 2018
        - changed code to reflect changes in search methods
    MODIFIED:     C. Stanhope         MAR 19 2018
        - fixed bug when selecting new radio button, search works first try now

    LetterLinkButton_Click()
    Triggered when a "LetterLinkButton" is clicked and is used to determine which letter link was clicked on the page and refresh the search and access code list to match.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    SearchButton_Click()
    */
    protected void LetterLinkButton_Click(object sender, EventArgs e)
    {
        // get text of button
        LinkButton letterButton = (LinkButton)sender;
        string selectedLetter = letterButton.Text;

        // set page to match search
        SearchKeywordTB.Text = selectedLetter;

        // have to un-select radio buttons before selecting new button for some reason
        SearchTypeRadioButtonList.Items.FindByValue(matchExactlyRadioButtonValue).Selected = false;
        SearchTypeRadioButtonList.Items.FindByValue(containsRadioButtonValue).Selected = false;
        SearchTypeRadioButtonList.Items.FindByValue(startsWithRadioButtonValue).Selected = true;   

        // click on search button artificially
        SearchButton_Click(sender, e);
    }

    /* 
    CREATED:    C. Stanhope         MAR 13 2018
    MODIFIED:   C. Stanhope         MAR 17 2018
        - moved code to its own method ResetSearchFilters so it can be called by any method

    ClearSearchButton_Click()
    Calls the ResetSearchFilters method to clear all search terms.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    ResetSearchFilters()
    */
    protected void ClearSearchButton_Click(object sender, EventArgs e)
    {
        ResetSearchFilters();
    }
    #endregion

    #region for Updating and Deactivating Access Codes
    /* 
    CREATED:    C. Stanhope         MAR 19 2018
    MODIFIED:   C. Stanhope         MAR 21 2018
        - changed error handling
        - changed selectedAccessCode to reflect globalization of variable
    MODIFIED:   C. Stanhope         APR 6 2018
        - accounting for non-distinct access codes

    AccessCodeWordButton_Click()
    Triggered when a "AccessCodeWordButton" is clicked from the list of code words and is used to determine which code word was clicked on. The Update Access Code Word section is appropriately populated depending on which code was selected.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowErrorMessage()
    */
    protected void AccessCodeWordButton_Click(object sender, EventArgs e)
    {
        #region Getting information from page about selected word
        LinkButton wordButton = (LinkButton)sender;
        string selectedWord = wordButton.Text;
        bool selectedWordStatus;

        if (wordButton.CssClass.Contains("false"))
        {
            selectedWordStatus = false;
        }
        else
        {
            selectedWordStatus = true;
        }
        #endregion

        // make sure accessCodeList isn't empty and isn't null, which should never happen
        if (accessCodeList != null && accessCodeList.Count() != 0)
        {
            // find item in code word array
            var selectedAccessCodeList = from item in accessCodeList
                                         where item.accesscodeword == selectedWord && item.activeyn == selectedWordStatus
                                         select item;

            if (selectedAccessCodeList.Count() > 0) // make sure access code is found (should always find at least one)
            {
                selectedAccessCode = selectedAccessCodeList.First();

                #region populate update textbox and radio buttons to match selected access code
                UpdateAccessCodeTextBox.Text = selectedWord;

                if (selectedAccessCode.activeyn) // if word is active
                {
                    UpdateAccessActiveStatusCodeRadioButtonList.Items.FindByText("inactive").Selected = false;
                    UpdateAccessActiveStatusCodeRadioButtonList.Items.FindByText("active").Selected = true;
                }
                else
                {
                    UpdateAccessActiveStatusCodeRadioButtonList.Items.FindByText("active").Selected = false;
                    UpdateAccessActiveStatusCodeRadioButtonList.Items.FindByText("inactive").Selected = true;
                }

                // display update box
                UpdateAccessCodeContainer.Visible = true;
                #endregion
            }
            else // if no access code matches
            {
                MessageUserControl.ShowErrorMessage("The selected access code could not be found in the access code list. Please clear the search and try again. If error persists, please contact your administrator.");
            }
        }
        else // if accessCodeList is empty or null
        {
            MessageUserControl.ShowErrorMessage("An access code was selected but no access codes are being displayed. Please try again. If error persists, please contact your administrator.");
        }
    }

    /* 
    CREATED:    C. Stanhope     MAR 21 2018
    MODIFIED:   C. Stanhope     APR 5 2018
        - new word trims whitespace
    MODIFIED:   C. Stanhope     APR 6 2018
        - added try-catch for database access

    UpdateAccessCodeButton_Click()
    Triggered when the "UpdateAccessCodeButton" is clicked and is used to update the selected access code's word and status to whatever the user changed it to.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    ValidateAccessCodeWord()
    AccessCodeController.UpdateAccessCode()
    ClearPage()
    ResetSearchFilters()
    MessageUserControl.ShowInfoMessage()
    MessageUserControl.ShowErrorMessage()
    */
    protected void UpdateAccessCodeButton_Click(object sender, EventArgs e)
    {
        #region getting values from page
        string newWord = UpdateAccessCodeTextBox.Text.ToLower().Trim();
        bool newStatus;
        if (UpdateAccessActiveStatusCodeRadioButtonList.SelectedItem.Value.Equals("y"))
        {
            newStatus = true;
        }
        else
        {
            newStatus = false;
        }
        #endregion

        if(newWord.Equals(selectedAccessCode.accesscodeword) && newStatus == selectedAccessCode.activeyn) // no changes made
        {
            MessageUserControl.ShowInfoMessage("No changes to update.");
        }
        else if (newWord.Equals(selectedAccessCode.accesscodeword)) // status changed but word did not
        {
            selectedAccessCode.activeyn = newStatus;
            try
            {
                accessCodeController.UpdateAccessCode(selectedAccessCode);

                if(newStatus == true)
                {
                    MessageUserControl.ShowSuccessMessage("Access code '" + newWord + "' was marked as active.");
                }
                else
                {
                    MessageUserControl.ShowSuccessMessage("Access code '" + newWord + "' was marked as inactive.");
                }
                
                ClearPage();
                ResetSearchFilters();
            }
            catch (Exception ex)
            {
                MessageUserControl.ShowErrorMessage("Updating access code failed. Please try again. If error persists, please contact your administrator.", ex);
            }
        }
        else // word was changed
        {
            if (ValidateAccessCodeWord(newWord))
            {
                selectedAccessCode.accesscodeword = newWord;
                selectedAccessCode.activeyn = newStatus;

                try
                {
                    accessCodeController.UpdateAccessCode(selectedAccessCode);

                    MessageUserControl.ShowSuccessMessage("Access code '" + newWord + "' updated.");
                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Updating access code failed. Please try again. If error persists, please contact your administrator.", ex);
                }

                ClearPage();
                ResetSearchFilters();
            }
            else // not a valid word
            {
                MessageUserControl.ShowInfoMessage("The access code '" + newWord + "' is not valid. Please ensure the access code is between 6 and 8 letters (no numbers or symbols are permitted).");
            }
        }
    }
    #endregion

    #region for Validation
    /* 
    CREATED:    C. Stanhope         MAR 21 2018  
    MODIFIED:   C. Stanhope         MAR 22 2018
        - added Regex for validation
    MODIFIED:   C. Stanhope         APR 5 2018
        - removed check if word already exists and put in a new method

    ValidateAccessCodeWord()
    Checks if the word passed is between 6 and 8 characters and only contains letters.

    PARAMETERS:
    string word - the access code word to validate

    RETURNS: 
    bool - returns "true" if word is valid, "false" if invalid

    ODEV METHOD CALLS: 
    None
    */
    protected bool ValidateAccessCodeWord(string word)
    {
        bool isValid = true;
        Regex wordRegex = new Regex(@"^[A-z]{6,8}$"); // only letters, 6 to 8 characters long

        if(!wordRegex.IsMatch(word))
        {
            isValid = false;
        }

        return isValid;
    }

    #endregion

    #region for Clearing and Resetting Page
    /* 
    CREATED:    C. Stanhope         MAR 13 2018  

    ClearPage()
    Clears all text boxes on the page.

    PARAMETERS:     
    None

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void ClearPage()
    {
        AddAccessCodeTB.Text = "";
        SearchKeywordTB.Text = "";
    }

    /* 
    CREATED:    C. Stanhope         MAR 17 2018
    MODIFIED:   C. Stanhope         MAR 19 2018
        - fixed bug when resetting radio button selection, should work now
        - added clearing and hiding of update section

    ResetSearchFilters()
    Used to clear the search keyword textbox and set the search type to "contains" and show active to "true" and show inactive to "false". Also clears the update section. 

    PARAMETERS:     
    None

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void ResetSearchFilters()
    {
        #region Clear search section
        SearchKeywordTB.Text = "";

        // have to un-select radio buttons before selecting new button for some reason
        SearchTypeRadioButtonList.Items.FindByValue(matchExactlyRadioButtonValue).Selected = false;
        SearchTypeRadioButtonList.Items.FindByValue(startsWithRadioButtonValue).Selected = false;
        SearchTypeRadioButtonList.Items.FindByValue(containsRadioButtonValue).Selected = true;

        ShowActiveCheckbox.Checked = true;
        ShowInactiveCheckbox.Checked = false;
        #endregion

        #region Clear update section
        UpdateAccessCodeTextBox.Text = "";

        UpdateAccessActiveStatusCodeRadioButtonList.Items.FindByText("active").Selected = false;
        UpdateAccessActiveStatusCodeRadioButtonList.Items.FindByText("inactive").Selected = false;
        #endregion

        #region Hide update and code list sections
        AccessCodeTableContainer.Visible = false;
        UpdateAccessCodeContainer.Visible = false;
        #endregion

        // clear code list and bind repeater
        accessCodeList.Clear();
        BindAccessCodeRepeater();
    }
    #endregion
}
