<%@ Page Title="Accounts" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="accounts.aspx.cs" Inherits="Management_accounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Account Search</h2>
	
    <asp:Label ID="ErrorMessage" runat="server" Text="" CssClass="text-danger bold" />
    <asp:Label ID="UserMessage" runat="server" Text="" CssClass="text-info bold" />

	<div class="row">
		<div class="col-sm-4">
            <asp:TextBox ID="SearchKeywordTB" runat="server" CssClass="form-control search-bar" placeholder="Enter search text here"></asp:TextBox>
		</div>
		<div class="col-sm-5">
            <asp:LinkButton ID="SearchButton" runat="server" CssClass="button button-info">Search</asp:LinkButton>
		</div>
	</div>

    <br />

    <%-- CCC: delete me later --%>
    <div class="alert alert-warning alert-dismissible fade show" role="alert">
		<button type="button" class="close" data-dismiss="alert" aria-label="Close">
		<span aria-hidden="true">&times;</span>
		</button>
		<p><strong>Note:</strong> nothing is showing up here right now because the repeater needs to be linked to the ODS.</p>
        <p>For more info talk to Claire</p>
	</div>

    <%-- Hook up to ODS when ODS works --%>
    <asp:Repeater ID="AccountRepeater" runat="server">
        <HeaderTemplate>
            <table class="table table-striped">
		        <thead>
			        <tr>
				        <th scope="col">Username</th>
				        <th scope="col">Full Name</th>
				        <th scope="col"></th>
			        </tr>
		        </thead>
		        <tbody>
        </HeaderTemplate>

        <ItemTemplate>

            <%-- CCC: change this to an asp link? --%>
            <tr>

				<%--
                    Uncomment this section when ODS and repeater are hooked up

                <td><%# Item.username %></td>
				<td><%# Item.firstname & " " & Item.lastname %></td>
				<td>
                    <a href="account_update.aspx?id=<%# Item.managementaccountid %>">Update Account</a>
				</td>
                    
                    --%>

			</tr>
        </ItemTemplate>

        <FooterTemplate>
                </tbody>
	        </table>
        </FooterTemplate>
    </asp:Repeater>
    
    <%-- Object Data Sources --%>
    <asp:ObjectDataSource ID="GetMgmtAccountsODS" runat="server"></asp:ObjectDataSource>

</asp:Content>

<%--keep this just in case
    CCC: delete later
    
    <table class="table table-striped">
		<thead>
			<tr>
				<th scope="col">Username</th>
				<th scope="col">Full Name</th>
				<th scope="col"></th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td>lindachowturner</td>
				<td>Linda Chow-Turner</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
			<tr>
				<td>janedoe</td>
				<td>Jane Doe</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
			<tr>
				<td>jackmacdonald</td>
				<td>Jack MacDonald</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
			<tr>
				<td>emmathompson2</td>
				<td>Emma Thompson</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
			<tr>
				<td>henryellis</td>
				<td>Henry Ellis</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
			<tr>
				<td>rebekahdolman</td>
				<td>Rebekah Dolman</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
			<tr>
				<td>martinmcphee</td>
				<td>Martin McPhee</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
			<tr>
				<td>markletestu</td>
				<td>Mark Letestu</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
			<tr>
				<td>daniellebutler3</td>
				<td>Danielle Butler</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
			<tr>
				<td>katealexander</td>
				<td>Kate Alexander</td>
				<td><a href="account_update.aspx">Update Account</a></td>
			</tr>
		</tbody>
	</table>--%>