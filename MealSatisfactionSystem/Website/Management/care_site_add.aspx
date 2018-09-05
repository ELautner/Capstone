<%@ Page Title="Add Care Site" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="care_site_add.aspx.cs" Inherits="Management_care_site_add" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Add Care Site</h2>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="UpdatePanel">
        <ContentTemplate>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

		    <div class="form-group row">
			    <label for="CareSiteNameTextBox" class="col-sm-3 col-form-label right-text">Care Site Name</label>
			    <div class="col-sm-8">
                    <asp:TextBox ID="CareSiteNameTextBox" runat="server" class="form-control" placeholder="Enter care site name here" ValidateRequestMode="Disabled" />
			    </div>
		    </div>
		    <div class="form-group row">
			    <label for="AddressTextBox" class="col-sm-3 col-form-label right-text">Address</label>
			    <div class="col-sm-8">
                    <asp:TextBox ID="AddressTextBox" runat="server" class="form-control" placeholder="Enter address here" ValidateRequestMode="Disabled" />
			    </div>
		    </div>
		    <div class="form-group row">
			    <label for="CityTextBox" class="col-sm-3 col-form-label right-text">City</label>
			    <div class="col-sm-8">
                    <asp:TextBox ID="CityTextBox" runat="server" class="form-control" placeholder="Enter city here" ValidateRequestMode="Disabled" />
			    </div>
		    </div>
            <div class="form-group row bottom-space">
			    <label for="ProvinceDDL" class="col-sm-3 col-form-label right-text">Province or Territory</label>
			    <div class="col-sm-8">
                    <asp:DropDownList ID="ProvinceDDL" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select a province" Value="0" />
                        <asp:ListItem Text="Alberta" Value="AB" />
                        <asp:ListItem Text="British Columbia" Value="BC" />
                        <asp:ListItem Text="Manitoba" Value="MB" />
                        <asp:ListItem Text="New Brunswick" Value="NB" />
                        <asp:ListItem Text="Newfoundland and Labrador" Value="NL" />
                        <asp:ListItem Text="Northwest Territories" Value="NT" />
                        <asp:ListItem Text="Nova Scotia" Value="NS" />
                        <asp:ListItem Text="Nunavut" Value="NU" />
                        <asp:ListItem Text="Ontario" Value="ON" />
                        <asp:ListItem Text="Prince Edward Island" Value="PE" />
                        <asp:ListItem Text="Quebec" Value="QC" />
                        <asp:ListItem Text="Saskatchewan" Value="SK" />
                        <asp:ListItem Text="Yukon" Value="YT" />
                    </asp:DropDownList>
			    </div>
		    </div>

		    <div class="form-group row justify-content-center">
                <asp:LinkButton ID="AddCareSiteButton" runat="server" CssClass="button button-success" OnClick="AddCareSiteButton_Click">Add Care Site</asp:LinkButton>
		    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

