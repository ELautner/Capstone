<%@ Page Title="My Account" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="my_account.aspx.cs" Inherits="Management_my_account" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>My Account</h2>
    	
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <div class="account-form">
        <%-- TODO: get rid of all the "Text = [populate]" tags --%>
	    <div class="form-group row">
		    <label class="col-sm-4 col-form-label right-text">Username</label>
            <asp:Label runat="server" ID="UsernameLabel" CssClass="col-sm-4 col-form-label bold" Text="[populate]" />
	    </div>
	    <div class="form-group row">
		    <label for="fname" class="col-sm-4 col-form-label right-text">First Name</label>
            <asp:Label runat="server" ID="FirstNameLabel" CssClass="col-sm-4 col-form-label bold" Text="[populate]" />
	    </div>
	    <div class="form-group row">
		    <label for="lname" class="col-sm-4 col-form-label right-text">Last Name</label>
            <asp:Label runat="server" ID="LastNameLabel" CssClass="col-sm-4 col-form-label bold" Text="[populate]" />
	    </div>
	    <div class="form-group row">
		    <label for="email" class="col-sm-4 col-form-label right-text">Email</label>
            <asp:Label runat="server" ID="EmailLabel" CssClass="col-sm-4 col-form-label bold" Text="[populate]" />
	    </div>

        <%-- NOTE: okay so I've made these textboxes, but they show the characters of the password. I've made inputs as well and commented them out so if you can get the inputs working then I'd say use them! - Claire --%>
	    <div class="form-group row">
		    <label for="pass" class="col-sm-4 col-form-label right-text">Old Password</label>
		    <div class="col-sm-6">
                <asp:TextBox ID="OldPassTextBox" runat="server" CssClass="form-control" />
                <%--<input id="OldPassInput" runat="server" class="form-control" type="password" />--%>
            </div>
	    </div>
        <div class="form-group row">
		    <label for="pass" class="col-sm-4 col-form-label right-text">New Password</label>
		    <div class="col-sm-6">
                <asp:TextBox ID="NewPassTextBox" runat="server" CssClass="form-control" />
                <%--<input id="NewPassInput" runat="server" class="form-control" type="password" />--%>
            </div>
	    </div>
        <div class="form-group row">
		    <label for="pass" class="col-sm-4 col-form-label right-text">Confirm Password</label>
		    <div class="col-sm-6">
                <asp:TextBox ID="ConfirmPassTextBox" runat="server" CssClass="form-control" />
                <%--<input id="ConfirmPassPassInput" runat="server" class="form-control" type="password" />--%>
            </div>
	    </div>

	    <div class="form-group row justify-content-center">
            <asp:LinkButton ID="UpdatePasswordButton" runat="server" CssClass="button button-primary col-sm-3">Update Password</asp:LinkButton>
	    </div>
    </div>
</asp:Content>

