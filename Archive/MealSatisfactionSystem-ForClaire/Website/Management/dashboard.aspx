<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="Management_dashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

    <h2>Welcome to the Meal Satisfaction System</h2>
    <asp:Label ID="ErrorMessage" runat="server" Text="" CssClass="text-danger bold" />
	<div class="row landing-row">
		<div class="col-sm-6">
			<h3>Reporting</h3>
            <ul class="landing-nav">
				<li><a href="reporting.aspx">View Reports</a></li>
				<li><a href="trends.aspx">View Trends</a></li>
			</ul>
		</div>
		<div class="col-sm-6">
			<h3>Administration</h3>
			<ul class="landing-nav">
				<li><a href="account_add.aspx">Add a New Account</a></li>
				<li><a href="accounts.aspx">Update or Delete an Account</a></li>
				<li><a href="manage_units.aspx">Manage Units</a></li>
                <li><a href="manage_access_codes.aspx">Manage Access Codes</a></li>
			</ul>
		</div>
	</div>
	<div class="row landing-row">
		<div class="col-sm-6">
			<h3>Survey</h3>
			<ul class="landing-nav">
				<li><a href="access_codes.aspx">View Survey Access Codes</a></li>
				<li><a href="surveys.aspx">View Surveys</a></li>
				<li><a href="contact_requests.aspx">View Contact Requests</a></li>
			</ul>
		</div>

        <%-- TODO: delete me when no longer needed --%>
        <div class="col-sm-6">
			<h3>Orphaned pages</h3>
            <p>Delete these later</p>
			<ul class="landing-nav">
				<li><a href="survey_details.aspx?id=1">Survey Details (id=1)</a></li>
			</ul>
		</div>
	</div>

</asp:Content>