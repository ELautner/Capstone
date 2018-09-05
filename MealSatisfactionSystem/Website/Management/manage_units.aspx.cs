using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

/* 
CREATED:	A. Valberg & H. Conant  	MAR 2 2018

Management_manage_units
Contains methods necessary to add, update, or deactivate units via the manage units page.

FIELDS:
internal UnitController unitController - allows methods from the controller to be called

METHODS:
protected void Page_Load(object sender, EventArgs e) - runs any code it contains when the page loads
protected void RetrieveUnitList_Select(object sender, EventArgs e) - retrieves the units of a specified care site 
protected void RetrieveUnit_Select(object sender, EventArgs e) - retrieves one unit based on a unit ID
protected void UpdateUnitButton_Click(object sender, EventArgs e) - allows the user to update a specified unit in the database
protected void DeactivateUnitButton_Click(object sender, EventArgs e) - allows the user to deactivate a specified unit in the database
protected void AddUnitButton_Click(object sender, EventArgs e) - allows the user to add a unit to the database
protected void ClearPage() - clears the fields on the page
 */
public partial class Management_manage_units : System.Web.UI.Page
{
    internal UnitController unitController = new UnitController();

    /* 
    CREATED: 	A. Valberg		MAR 3 2018
    MODIFIED: 	H. Conant		MAR 5 2018
        - Updated method body code
    MODIFIED:   C. Stanhope     MAR 21 2018
        - Made sections invisible if drop down options are not selected

    Page_Load()
    Triggered on page load and is used to hide sections of the page if the appropriate values are not selected from the drop-down lists on the page.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        if(CareSiteDDL.SelectedIndex == 0)
        {
            AddUnitRow.Visible = false;
            ModifyUnitRow.Visible = false;
            UpdateDeactivateRow.Visible = false;
        }
        else
        {
            AddUnitRow.Visible = true;
            ModifyUnitRow.Visible = true;

            if (UnitDDL.SelectedIndex == 0)
            {
                UpdateDeactivateRow.Visible = false;
            }
            else
            {
                UpdateDeactivateRow.Visible = true;
            }
        }
    }

    #region Button Methods
    /* 
    CREATED: 	A. Valberg		MAR 3 2018
    MODIFIED: 	H. Conant		MAR 5 2018
        - Updated method signature
        - Updated method body code

    RetrieveUnitList_Select()
    This method retrieves the units of a care site specified by the user.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    UnitController.GetActiveCareSiteUnits()
    MessageUserControl.ShowInfoMessage()
    MessageUserControl.ShowErrorMessage()
    */
    protected void RetrieveUnitList_Select(object sender, EventArgs e)
    {
        if (CareSiteDDL.SelectedValue == "0")
        {
            MessageUserControl.ShowInfoMessage("Please select a care site.");
            UnitDDL.Items.Clear();
            UnitDDL.Items.Insert(0, new ListItem("Select...", "0"));
            AddUnitNameTB.Text = "";
            UpdateUnitNameTB.Text = "";
            DeactivateUnitNameLabel.Text = "";

            AddUnitRow.Visible = false;
            ModifyUnitRow.Visible = false;
            UpdateDeactivateRow.Visible = false;
        }
        else
        {
            try
            {
                int tempCareSiteId;
                int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
                List<UnitDTO> tempUnitList = unitController.GetActiveCareSiteUnits(tempCareSiteId);
                UnitDDL.DataSource = tempUnitList;
                UnitDDL.DataBind();
                UnitDDL.Items.Insert(0, new ListItem("Select...", "0"));
                UnitDDL.SelectedValue = "0";

                // clear unit selections so user doesn't get confused and edit a unit in another care site
                AddUnitNameTB.Text = "";
                UpdateUnitNameTB.Text = "";
                DeactivateUnitNameLabel.Text = "";
                UpdateDeactivateRow.Visible = false;
                AddUnitRow.Visible = true;
                ModifyUnitRow.Visible = true;

            }
            catch (Exception ex)
            {
                MessageUserControl.ShowErrorMessage("Retrieving units failed. Please try again. If error persists, please contact your administrator.", ex);
            }
        }
    }

    /* 
    CREATED: 	A. Valberg		MAR 3 2018
    MODIFIED: 	H. Conant		MAR 5 2018
       - Updated method signature
       - Updated method body code
    MODIFIED: 	H. Conant		MAR 27 2018
        - Updated method body code

    RetrieveUnit_Select()
    This method retrieves the unit specified by the user.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    UnitController.GetUnit()
    MessageUserControl.ShowInfoMessage()
    MessageUserControl.ShowErrorMessage()
    */
    protected void RetrieveUnit_Select(object sender, EventArgs e)
    {

        if (UnitDDL.SelectedValue == "0")
        {
           
            MessageUserControl.ShowInfoMessage("Please select a unit name if you wish to update or deactivate a unit.");
            UpdateUnitNameTB.Text = "";
            DeactivateUnitNameLabel.Text = "";
            UpdateDeactivateRow.Visible = false;
            AddUnitRow.Visible = true;

        }
        else
        {
            try
            {
                int tempUnitId;
                int.TryParse(UnitDDL.SelectedValue, out tempUnitId);
                UnitDTO tempUnit = unitController.GetUnit(tempUnitId);
                UpdateUnitNameTB.Text = tempUnit.unitname;
                DeactivateUnitNameLabel.Text = tempUnit.unitname;

                AddUnitRow.Visible = true;
                UpdateDeactivateRow.Visible = true;
            }
            catch (Exception ex)
            {
                MessageUserControl.ShowErrorMessage("Retrieving units failed. Please try again. If error persists, please contact your administrator.", ex);
            }
        }
    }

    /* 
    CREATED: 	A. Valberg		MAR 3 2018
       - Added method body code
    MODIFIED: 	H. Conant		MAR 5 2018
       - Updated method signature
       - Updated method body code
    MODIFIED: 	H. Conant		MAR 27 2018
        - Updated method body code 
    MODIFIED: 	H. L'Heureux	APR 03 2018
        - Updated method body code 

    UpdateUnitButton_Click()
    This method allows the user to update a specified unit in the database.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowInfoMessage()
    UnitController.UpdateUnit()
    MessageUserControl.ShowSuccessMessage()
    MessageUserControl.ShowErrorMessage()
    ClearPage()
    */
    protected void UpdateUnitButton_Click(object sender, EventArgs e)
    {
        string pattern = @"^[A-z 0-9 .-]{1,60}$";

        Regex reg = new Regex(pattern);

        Match unitNameFormat = reg.Match(UpdateUnitNameTB.Text);

        if (string.IsNullOrWhiteSpace(UpdateUnitNameTB.Text) || UpdateUnitNameTB.Text.Length > 60 || !unitNameFormat.Success)
        {
            MessageUserControl.ShowInfoMessage("Please enter a unit name up to 60 characters. Unit names can only contain letters, numbers, and the following symbols: . -");
        }
        else
        {
            int tempUnitId;
            int.TryParse(UnitDDL.SelectedValue, out tempUnitId);
            if (tempUnitId == 0)
            {
                MessageUserControl.ShowInfoMessage("Please select a care site and unit.");
            } else
            {
                UnitDTO tempUnit = new UnitDTO();
                tempUnit.unitid = tempUnitId;
                tempUnit.unitname = UpdateUnitNameTB.Text.Trim();
                try
                {
                    unitController.UpdateUnit(tempUnit);
                    MessageUserControl.ShowSuccessMessage("Unit " + UnitDDL.SelectedItem.Text + " has been updated to " + UpdateUnitNameTB.Text.Trim() + " for the " + CareSiteDDL.SelectedItem.Text + " care site.");
                    ClearPage();
                    
                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Updating unit failed. Please try again. If error persists, please contact your administrator.", ex);
                }
            }
            
        }
    }

    /* 
    CREATED: 	A. Valberg		MAR 3 2018
    MODIFIED: 	H. Conant		MAR 5 2018
       - Updated method signature
       - Updated method body code

    DeactivateUnitButton_Click()
    This method allows the user to deactivate a specified unit in the database.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowInfoMessage()
    UnitController.DeactivateUnit()
    MessageUserControl.ShowSuccessMessage()
    MessageUserControl.ShowErrorMessage()
    ClearPage()
    */
    protected void DeactivateUnitButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(DeactivateUnitNameLabel.Text))
        {
            MessageUserControl.ShowInfoMessage("Please select a care site and unit.");
        } 
        else
        {
            int tempUnitId;
            int.TryParse(UnitDDL.SelectedValue, out tempUnitId);
            if (tempUnitId == 0)
            {
                MessageUserControl.ShowInfoMessage("Please select a care site and unit.");
            } else
            {
                UnitDTO tempUnit = new UnitDTO();
                tempUnit.unitid = tempUnitId;
                try
                {
                    unitController.DeactivateUnit(tempUnit);
                    MessageUserControl.ShowSuccessMessage("Unit " + UnitDDL.SelectedItem.Text + " has been deactivated for the " + CareSiteDDL.SelectedItem.Text + " care site.");
                    ClearPage();
                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Deactivating unit failed. Please try again. If error persists, please contact your administrator.", ex);
                }
            }
        }
    }

    /* 
    CREATED: 	A. Valberg		MAR 3 2018
    MODIFIED: 	H. Conant		MAR 5 2018
        - Updated method signature
        - Updated method body code
    MODIFIED: 	H. Conant		MAR 27 2018
        - Updated method body code
    MODIFIED: 	H. L'Heureux	APR 03 2018
        - Updated method body code

    AddUnitButton_Click()
    This method allows the user to add a specified unit in the database.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    MessageUserControl.ShowInfoMessage()
    UnitController.AddUnit()
    MessageUserControl.ShowSuccessMessage()
    MessageUserControl.ShowErrorMessage()
    ClearPage()
    */
    protected void AddUnitButton_Click(object sender, EventArgs e)
    {
        string pattern = @"^[A-z 0-9 .-]{1,60}$";

        Regex reg = new Regex(pattern);

        Match unitNameFormat = reg.Match(AddUnitNameTB.Text);

        if (string.IsNullOrWhiteSpace(AddUnitNameTB.Text) || AddUnitNameTB.Text.Length > 60 || !unitNameFormat.Success)
        {
            MessageUserControl.ShowInfoMessage("Please enter a unit name up to 60 characters. Unit names can only contain letters, numbers, and the following symbols: . -");
        }
        else
        {
            int tempCareSiteID;
            int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteID);
            if(tempCareSiteID == 0)
            {
                MessageUserControl.ShowInfoMessage("Please select a care site.");
            } else
            {
                UnitDTO tempUnit = new UnitDTO();
                tempUnit.caresiteid = tempCareSiteID;
                tempUnit.unitname = AddUnitNameTB.Text.Trim();
                try
                {
                    unitController.AddUnit(tempUnit);
                    MessageUserControl.ShowSuccessMessage("Unit " + AddUnitNameTB.Text + " has been added to the " + CareSiteDDL.SelectedItem.Text + " care site.");
                    ClearPage();

                }
                catch (Exception ex)
                {
                    MessageUserControl.ShowErrorMessage("Adding unit failed. Please try again. If error persists, please contact your administrator.", ex);
                }
            }
            
        }
    }

    #endregion

    #region Helper Methods
    /* 
    CREATED: 	A. Valberg		MAR 3 2018

    ClearPage()
    Clears the fields on the page.

    PARAMETERS: 	
    None

    RETURNS: 
    void

    ODEV METHOD CALLS:
    None
    */
    protected void ClearPage()
    {
        CareSiteDDL.SelectedValue = "0";
        UnitDDL.Items.Clear();
        UnitDDL.Items.Insert(0, new ListItem("Select...", "0"));
        AddUnitNameTB.Text = "";
        UpdateUnitNameTB.Text = "";
        DeactivateUnitNameLabel.Text = "";

        AddUnitRow.Visible = false;
        ModifyUnitRow.Visible = false;
        UpdateDeactivateRow.Visible = false;
    }
    #endregion

}
