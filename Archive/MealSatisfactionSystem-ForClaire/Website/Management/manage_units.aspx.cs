using MSS.Data;
using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/* 
CREATED:	C. Stanhope		MAR 2 2018

Management_manage_units
Contains methods nessasary to add, update or dectivate units via the manage units page.

CLASS-LEVEL VARIABLES:
internal UnitController unitController = new UnitController(); - allows methods from the controller to be called

METHODS:
protected void Page_Load(object sender, EventArgs e) - this method runs any code it contains when the page loads
protected void RetrieveUnitList_Select(object sender, EventArgs e) - this method retrieves the units of a specified care site 
protected void RetrieveUnit_Select(object sender, EventArgs e) - this method retrieves one unit based on a unit id
protected void UpdateUnitButton_Click(object sender, EventArgs e) - this method allows the user to update a specified unit in the database
protected void DeactivateUnitButton_Click(object sender, EventArgs e) - this method allows the user to deactivate a specified unit in the database
protected void AddUnitButton_Click(object sender, EventArgs e) - this method allows the user to add a unit to the database
protected void ClearPage() - clears the fields on the page
 */
public partial class Management_manage_units : System.Web.UI.Page
{
    internal UnitController unitController = new UnitController();

    /* 
    CREATED: 	C. Stanhope		MAR 2 2018
    MODIFIED: 	A. Valberg		MAR 3 2018
        - Added method body code
    MODIFIED: 	H. Conant		MAR 5 2018
        - Updated method body code


    Page_Load()
    This method runs any code it contains when the page loads.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    None
    */
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Button Methods
    /* protected void RetrieveUnitListButton_Click(object sender, EventArgs e)
     {
         if (CareSiteDDL.SelectedValue == "0")
         {
             UserMessage.Text = "Please select a care site";
         }
         else
         {
             int tempCareSiteId;
             int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
             List<UnitDTO> tempUnitList = unitController.GetCareSiteUnits(tempCareSiteId);
             UnitDDL.DataSource = tempUnitList;
             UnitDDL.DataBind();
             UnitDDL.Items.Insert(0, new ListItem("Select...", "0"));
             UnitDDL.SelectedValue = "0";

             // clear unit selections so user doesn't get confused and edit a unit in another care site
             AddUnitNameTB.Text = "";
             UpdateUnitNameTB.Text = "";
             DeactivateUnitNameLabel.Text = "";
         }
     }*/

    /* 
    CREATED: 	C. Stanhope		MAR 2 2018
    MODIFIED: 	A. Valberg		MAR 3 2018
        - Added method body code
    MODIFIED: 	H. Conant		MAR 5 2018
        - Updated method signature
        - Updated method body code

    RetrieveUnitList_Select()
    This method retrieves the units of a care site specified by the user.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    .Clear()
    .Insert()
    .TryParse()
    .GetCareSiteUnits()
    .DataBind()
    */
    protected void RetrieveUnitList_Select(object sender, EventArgs e)
    {
        if (CareSiteDDL.SelectedValue == "0")
        {
            UserMessage.Text = "Please select a care site";
            UnitDDL.Items.Clear();
            UnitDDL.Items.Insert(0, new ListItem("Select...", "0"));
            AddUnitNameTB.Text = "";
            UpdateUnitNameTB.Text = "";
            DeactivateUnitNameLabel.Text = "";
            UnitHiddenField.Value = ""; // can delete
            HiddenCareSiteID.Value = ""; // can delete
        }
        else
        {
            int tempCareSiteId;
            int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteId);
            List<UnitDTO> tempUnitList = unitController.GetCareSiteUnits(tempCareSiteId);
            UnitDDL.DataSource = tempUnitList;
            UnitDDL.DataBind();
            UnitDDL.Items.Insert(0, new ListItem("Select...", "0"));
            UnitDDL.SelectedValue = "0";

            // clear unit selections so user doesn't get confused and edit a unit in another care site
            AddUnitNameTB.Text = "";
            UpdateUnitNameTB.Text = "";
            DeactivateUnitNameLabel.Text = "";
        }
    }

    /* protected void SelectUnitButton_Click(object sender, EventArgs e)
    {
        if (UnitDDL.SelectedValue == "0")
        {
            // TODO: display a message to the user that they didn't select a unit
        }
        else
        {
            // TODO: add logic that if there's something in the text boxes, user is notified data will be lost if they continue
            int tempUnitId;
            int.TryParse(UnitDDL.SelectedValue, out tempUnitId);
            UnitDTO tempUnit = unitController.GetUnit(tempUnitId);
            UnitHiddenField.Value = tempUnit.unitid.ToString(); // Can delete 
            HiddenCareSiteID.Value = tempUnit.caresiteid.ToString(); // Can delete 
            UpdateUnitNameTB.Text = tempUnit.unitname;
            DeactivateUnitNameLabel.Text = tempUnit.unitname;
        }
    }*/

    /* 
    CREATED: 	C. Stanhope		MAR 2 2018
    MODIFIED: 	A. Valberg		MAR 3 2018
       - Added method body code
    MODIFIED: 	H. Conant		MAR 5 2018
       - Updated method signature
       - Updated method body code

    RetrieveUnit_Select()
    This method retrieves the unit specified by the user.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    .TryParse()
    .GetUnit()
    .ToString()

    .DataBind()
    */
    protected void RetrieveUnit_Select(object sender, EventArgs e)
    {

        if (UnitDDL.SelectedValue == "0")
        {
            UserMessage.Text = "Please select a unit";
            AddUnitNameTB.Text = "";
            UpdateUnitNameTB.Text = "";
            DeactivateUnitNameLabel.Text = "";
            UnitHiddenField.Value = ""; // can delete 
            HiddenCareSiteID.Value = ""; // can deltete 



        }
        else
        {
            // TODO: add logic that if there's something in the text boxes, user is notified data will be lost if they continue
            //   ^ holly dosnt think it is nessassary 
            int tempUnitId;
            int.TryParse(UnitDDL.SelectedValue, out tempUnitId);
            UnitDTO tempUnit = unitController.GetUnit(tempUnitId);
            UnitHiddenField.Value = tempUnit.unitid.ToString(); // Can delete 
            HiddenCareSiteID.Value = tempUnit.caresiteid.ToString(); // Can delete 
            UpdateUnitNameTB.Text = tempUnit.unitname;
            DeactivateUnitNameLabel.Text = tempUnit.unitname;
        }
    }

    /* 
    CREATED: 	C. Stanhope		MAR 2 2018
    MODIFIED: 	A. Valberg		MAR 3 2018
       - Added method body code
    MODIFIED: 	H. Conant		MAR 5 2018
       - Updated method signature
       - Updated method body code

    UpdateUnitButton_Click()
    This method allows the user to update a specified unit in the database.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    .IsNullOrWhiteSpace()
    .TryParse()
    UnitDTO()
    .UpdateUnit()
    ClearPage();
    */
    protected void UpdateUnitButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UpdateUnitNameTB.Text))
        {
            UserMessage.Text = "Please enter a unit name before updating";
        }
        else
        {
            int tempUnitId;
            int.TryParse(UnitDDL.SelectedValue, out tempUnitId); //UnitHiddenField.Value
            UnitDTO tempUnit = new UnitDTO();
            tempUnit.unitid = tempUnitId;
            tempUnit.unitname = UpdateUnitNameTB.Text;
            try
            {
                unitController.UpdateUnit(tempUnit);
                ClearPage();
            } catch (Exception ex)
            {
                ErrorMessage.Text = "Update unit failed. Please contact the system administrator.";
            }
            
        }
    }

    /* 
    CREATED: 	C. Stanhope		MAR 2 2018
    MODIFIED: 	A. Valberg		MAR 3 2018
       - Added method body code
    MODIFIED: 	H. Conant		MAR 5 2018
       - Updated method signature
       - Updated method body code

    DeactivateUnitButton_Click()
    This method allows the user to deactivate a specified unit in the database.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    .IsNullOrWhiteSpace()
    .TryParse()
    UnitDTO()
    .DeactivateUnit()
    ClearPage();
    */
    protected void DeactivateUnitButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(DeactivateUnitNameLabel.Text))
        {
            UserMessage.Text = "Please select a unit";
        }
        else
        {
            int tempUnitId;
            int.TryParse(UnitDDL.SelectedValue, out tempUnitId); //UnitHiddenField.Value
            UnitDTO tempUnit = new UnitDTO();
            tempUnit.unitid = tempUnitId;
            try
            {
                unitController.DeactivateUnit(tempUnit);
                ClearPage();
            } catch (Exception ex)
            {
                ErrorMessage.Text = "Deactivate unit failed. Please contact the system administrator.";
            }

        }
    }

    /* 
    CREATED: 	C. Stanhope		MAR 2 2018
    MODIFIED: 	A. Valberg		MAR 3 2018
        - Added method body code
    MODIFIED: 	H. Conant		MAR 5 2018
        - Updated method signature
        - Updated method body code

    DeactivateUnitButton_Click()
    This method allows the user to deactivate a specified unit in the database.

    PARAMETERS: 	
    object sender - object on the page that is being targeted
    EventArgs e - event that has triggered the method

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    .IsNullOrWhiteSpace()
    .TryParse()
    UnitDTO()
    .DeactivateUnit()
    ClearPage();
    */
    protected void AddUnitButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(AddUnitNameTB.Text))
        {
            UserMessage.Text = "Please enter a unit name";
        }
        else
        {
            int tempCareSiteID;
            int.TryParse(CareSiteDDL.SelectedValue, out tempCareSiteID); //HiddenCareSiteID.Value
            UnitDTO tempUnit = new UnitDTO();
            tempUnit.caresiteid = tempCareSiteID;
            tempUnit.unitname = AddUnitNameTB.Text;
            try
            {
                unitController.AddUnit(tempUnit);
                ClearPage();

            } catch (Exception ex)
            {
                ErrorMessage.Text = "Add unit failed. Please contact the system administrator.";
            }
            
        }
    }

    #endregion

    #region Helper Methods
    /* 
    CREATED: 	C. Stanhope		MAR 2 2018
    MODIFIED: 	A. Valberg		MAR 3 2018
        - Added method body code

    ClearPage()
    Clears the fields on the page.

    PARAMETERS: 	
    None

    RETURNS: 
    void - Nothing is returned

    METHOD CALLS: 
    .Insert()
    .Clear();
    */
    protected void ClearPage()
    {
        CareSiteDDL.SelectedValue = "0";
        UnitDDL.Items.Clear();
        UnitDDL.Items.Insert(0, new ListItem("Select...", "0"));
        AddUnitNameTB.Text = "";
        UpdateUnitNameTB.Text = "";
        DeactivateUnitNameLabel.Text = "";
        UnitHiddenField.Value = ""; // can delete 
        HiddenCareSiteID.Value = ""; // can delete
    }
    #endregion

}