<%@ Page Title="Update Account" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="account_update.aspx.cs" Inherits="Management_account_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Update Account</h2>
	
    <asp:Label ID="ErrorMessage" runat="server" Text="" CssClass="text-danger bold" />
    <asp:Label ID="UserMessage" runat="server" Text="" CssClass="text-info bold" />

    <div class="account-form">

        <div class="form-group row">
			<label for="AccountIDLabel" class="col-sm-4 col-form-label right-text">Account ID</label>
            <asp:Label ID="AccountIDLabel" name="AccountIDLabel" runat="server" CssClass="col-sm-8 col-form-label bold" Text="Populate ID Here"></asp:Label>
		</div>
		<div class="form-group row">
			<label for="UsernameLabel" class="col-sm-4 col-form-label right-text">Username</label>
            <asp:Label ID="UsernameLabel" name="UsernameLabel" runat="server" CssClass="col-sm-8 col-form-label bold" Text="Populate Username Here"></asp:Label>
		</div>
		<div class="form-group row">
			<label for="FirstNameTB" class="col-sm-4 col-form-label right-text">First Name</label>
			<div class="col-sm-8">
                <asp:TextBox ID="FirstNameTB" name="FirstNameTB" runat="server" class="form-control" placeholder="First name populated here" />
			</div>
		</div>
		<div class="form-group row">
			<label for="LastNameTB" class="col-sm-4 col-form-label right-text">Last Name</label>
			<div class="col-sm-8">
                <asp:TextBox ID="LastNameTB" name="LastNameTB" runat="server" class="form-control" placeholder="Last name populated here" />
			</div>
		</div>
		<div class="form-group row">
			<label for="EmailTB" class="col-sm-4 col-form-label right-text">Email</label>
			<div class="col-sm-8">
                <asp:TextBox ID="EmailTB" name="EmailTB" runat="server" class="form-control" placeholder="Email populated here" />
			</div>
		</div>
        <div class="form-group row">
			<label for="PasswordTB" class="col-sm-4 col-form-label right-text">Password</label>
			<div class="col-sm-8">
                <asp:TextBox ID="PasswordTB" name="PasswordTB" runat="server" class="form-control" placeholder="Password populated here" />
			</div>
		</div>

        <%-- TODO: This needs to be turned into a repeater --%>
		<div class="form-group row">
			<label for="accountType" class="col-sm-4 col-form-label right-text">Authorization Level</label>
			<div class="col-sm-8">
				<div class="form-check">
					<input type="radio" name="accountType" class="form-check-input" value="user" id="user" checked>
					<label class="form-check-label" for="user">User</label>
				</div>
				<div class="form-check">
					<input type="radio" name="accountType" class="form-check-input" value="super" id="super">
					<label class="form-check-label" for="super">Super User</label>
				</div>
				<div class="form-check">
					<input type="radio" name="accountType" class="form-check-input" value="admin" id="admin">
					<label class="form-check-label" for="admin">Administrator</label>
				</div>
			</div>
		</div>

        <%-- TODO: This should only show up if "User" is selected as the Authorization level --%>
        <div class="form-group row">
		    <label for="CareSiteDDL" class="col-sm-4 col-form-label right-text">Care Site</label>
		    <div class="col-sm-8">
                <%-- Need to connect to GetCareSitesODS once ODS is set up --%>
                <asp:DropDownList ID="CareSiteDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                    <asp:ListItem Value="">Select...</asp:ListItem>
                </asp:DropDownList>
		    </div>
	    </div>

        <div class="form-group row justify-content-center">
            <asp:LinkButton ID="UpdateAccountButton" runat="server" CssClass="button button-primary">Update Account</asp:LinkButton>&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="DeactivateAccountButton" runat="server" CssClass="button button-danger">Deactivate Account</asp:LinkButton>
		</div>
	</div>

    <%-- Object Data Sources --%>
    <asp:ObjectDataSource ID="GetCareSitesODS" runat="server"></asp:ObjectDataSource>
</asp:Content>

