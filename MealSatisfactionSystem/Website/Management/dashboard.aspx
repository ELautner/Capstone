<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="Management_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

    <h2>Welcome to the Meal Satisfaction System</h2>

    <div id="errordiv" class="dasherror" runat="server">
        <asp:Label runat="server" ID="MessageLabel" CssClass="muc-title">System Error: </asp:Label>
        <asp:Label runat="server" ID="ErrorMsg"></asp:Label>
        <asp:Label runat="server" ID="ErrorMessage" CssClass="bottom-space"></asp:Label>
        <asp:Label runat="server" ID="ErrorMessage2"></asp:Label>
    </div>
    
    <asp:LoginView ID="UserManagementView" runat="server">
        <RoleGroups>
            <asp:RoleGroup Roles="User, Super User">
                <ContentTemplate>
                    <div class="row bottom-space">
		                <div class="col-sm-6">
			                <h3>Reporting</h3>
                            <ul class="landing-nav">
				                <li><a href="reporting.aspx">View Reports</a></li>
				                <li><a href="trends.aspx">View Trends</a></li>
			                </ul>
		                </div>
		                <div class="col-sm-6">
			                <h3>Surveys</h3>
			                <ul class="landing-nav">
				                <li><a href="access_codes.aspx">View Survey Access Codes</a></li>
				                <li><a href="surveys.aspx">View Surveys</a></li>
				                <li><a href="contact_requests.aspx">View Contact Requests</a></li>
			                </ul>
		                </div>
	                </div>
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>
    <asp:LoginView ID="AdminsitrationManagementView" runat="server">
        <RoleGroups>
            <asp:RoleGroup Roles="Administrator">
                <ContentTemplate>
                     <div class="row bottom-space">
		                <div class="col-sm-6">
			                <h3>Reporting</h3>
                            <ul class="landing-nav">
				                <li><a href="reporting.aspx">View Reports</a></li>
				                <li><a href="trends.aspx">View Trends</a></li>
			                </ul>
		                </div>
		                <div class="col-sm-6">
			                <h3>Care Site Administration</h3>
			                <ul class="landing-nav">
				                <li><a href="care_site_add.aspx">Add Care Site</a></li>
				                <li><a href="manage_care_site.aspx">Manage Care Site</a></li>
				                <li><a href="manage_units.aspx">Manage Units</a></li>
			                </ul>
		                </div>
	                </div>
	                <div class="row landing-row">
		                <div class="col-sm-6">
			                <h3>Surveys</h3>
			                <ul class="landing-nav">
				                <li><a href="access_codes.aspx">View Survey Access Codes</a></li>
				                <li><a href="surveys.aspx">View Surveys</a></li>
				                <li><a href="contact_requests.aspx">View Contact Requests</a></li>
			                </ul>
		                </div>

                        <div class="col-sm-6">
			                <h3>System Administration</h3>
			                <ul class="landing-nav">
				                <li><a href="account_add.aspx">Add Account</a></li>
				                <li><a href="accounts.aspx">Manage Accounts</a></li>
                                <li><a href="manage_access_codes.aspx">Manage Survey Access Codes</a></li>
			                </ul>
		                </div>
                    </div>
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
    </asp:LoginView>
</asp:Content>