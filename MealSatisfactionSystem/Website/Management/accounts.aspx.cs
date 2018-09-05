using System;

/* 
CREATED:    E. Lautner       MAR 27 2018

Management_accounts
An automatically generated page used to store the account's backend C# code.

FIELDS:
None

METHODS:
protected void UsersFetch_Click(object sender, EventArgs e) - refreshes the repeater with the new search data
protected void ClearSearch_Click(object sender, EventArgs e) - Resets the page
*/
public partial class Management_accounts : System.Web.UI.Page
{
    /* 
    CREATED: 	E. Lautner		MAR 27 2018
       
    UsersFetch_Click()
    This method runs when the search button is clicked. The repeater is forced to databind, which calls the ODS. The ODS uses the string in the search bar for its search.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowErrorMessage()
    MessageUserControl.ShowInfoMessage()
    */
    protected void UsersFetch_Click(object sender, EventArgs e)
    {
        try
        {
            AccountRepeater.DataBind();
        }
        catch (Exception ex)
        {
            MessageUserControl.ShowErrorMessage("Account search failed. Please try again. If error persists, please contact your administrator.", ex);
        }
        if (AccountRepeater.Items.Count < 1)
        {
            MessageUserControl.ShowInfoMessage("No accounts found matching the given search text. Try searching by part of the username or by part of the full name.");
        }
    }

    /* 
    CREATED: 	E. Lautner		APR 5 2018

    ClearSearch_Click()
    This method runs when the clear search button is clicked. Sets the text to "" and then rebinds the repeater.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void ClearSearch_Click(object sender, EventArgs e)
    {
        SearchKeywordTB.Text = "";
        AccountRepeater.DataBind();
    }
}
