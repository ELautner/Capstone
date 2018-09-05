<%@ Page Title="Access Codes" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="access_codes.aspx.cs" Inherits="Management_access_codes" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Access Codes</h2>

    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <div class="form-group row">
		<label for="careSite" class="col-sm-4 col-form-label right-text">Select a Care Site</label>
		<div class="col-sm-4">
            <%-- TODO: change this DDL to only show care sites the user has access to --%>
            <asp:DropDownList ID="CareSiteDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="GetCareSitesODS" DataTextField="caresitename" DataValueField="caresiteid" OnSelectedIndexChanged="CareSiteDDL_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="">Select...</asp:ListItem> 
            </asp:DropDownList>
		</div>
	</div>

	<br />

	<div class="row justify-content-around" id="CodeCards" runat="server">
		<div class="col-sm-4">
			<div class="card border-success">
				<div class="card-header">
					<h3 class="card-title">Today's Code:</h3>
				</div>
				<div class="card-body">
                    <asp:Label ID="TodayCodeLabel" runat="server" Text="populate code here" CssClass="access-code text-success"></asp:Label>
                    <br />
                    <p>Valid for the full 24 hours of:</p>
                    <asp:Label runat="server" ID="TodayDateLabel" CssClass="bold" />
				</div>
			</div>
		</div>
		<div class="col-sm-4">
			<div class="card border-secondary">
				<div class="card-header">
					<h3 class="card-title">Tomorrow's Code:</h3>
				</div>
				<div class="card-body">
					<asp:Label ID="TomorrowCodeLabel" runat="server" Text="populate code here" CssClass="access-code text-secondary"></asp:Label>
                    <br />
                    <p>Valid for the full 24 hours of:</p>
                    <asp:Label runat="server" ID="TomorrowDateLabel" CssClass="bold" />
				</div>
			</div>
		</div>
	</div>

     <%-- Object Data Sources --%>
    <asp:ObjectDataSource ID="GetCareSitesODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCareSites" TypeName="MSS.System.BLL.CareSiteController"></asp:ObjectDataSource>
</asp:Content>