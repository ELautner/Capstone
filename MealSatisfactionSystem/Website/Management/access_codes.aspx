<%@ Page Title="Survey Access Codes" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="access_codes.aspx.cs" Inherits="Management_access_codes" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Survey Access Codes</h2>
    
    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="UpdatePanel">
        <ContentTemplate>

            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <div class="form-group row bottom-space">
		        <label for="careSite" class="col-sm-3 col-form-label right-text">Select a Care Site</label>
		        <div class="col-sm-8">
                    <asp:DropDownList ID="CareSiteDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="GetCareSitesODS" DataTextField="caresitename" DataValueField="caresiteid" 
                        OnSelectedIndexChanged="CareSiteDDL_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0">Select...</asp:ListItem> 
                    </asp:DropDownList>
		        </div>
	        </div>

	        <div class="row justify-content-around" id="CodeCards" runat="server">
		        <div class="col-sm-4" id="TodaysCard" runat="server">
			        <div class="card border-success">
				        <div class="card-header">
					        <h3 class="card-title">Today's Code:</h3>
				        </div>
				        <div class="card-body">
                            <asp:Label ID="TodayCodeLabel" runat="server" Text="populate code here" CssClass="access-code text-success bottom-space"></asp:Label>
                            <p>Valid for: <asp:Label runat="server" ID="TodayDateLabel" CssClass="bold" /></p>
				        </div>
			        </div>
		        </div>
		        <div class="col-sm-4" id="TomorrowsCard" runat="server">
			        <div class="card border-secondary">
				        <div class="card-header">
					        <h3 class="card-title">Tomorrow's Code:</h3>
				        </div>
				        <div class="card-body">
					        <asp:Label ID="TomorrowCodeLabel" runat="server" Text="populate code here" CssClass="access-code text-secondary bottom-space"></asp:Label>
                            <p>Valid for: <asp:Label runat="server" ID="TomorrowDateLabel" CssClass="bold" /></p>
				        </div>
			        </div>
		        </div>
	        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

     <%-- Object Data Sources --%>
    <asp:ObjectDataSource ID="GetCareSitesODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetActiveCareSites" TypeName="MSS.System.BLL.CareSiteController"></asp:ObjectDataSource>
</asp:Content>