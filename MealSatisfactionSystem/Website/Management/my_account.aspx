<%@ Page Title="My Account" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="my_account.aspx.cs" ValidateRequest="false" Inherits="Management_my_account" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <h2>My Account</h2>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="UpdatePanel">
        <ContentTemplate>

            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <div class="centre-form">
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label right-text">Username</label>
                    <asp:Label runat="server" ID="UsernameLabel" CssClass="col-sm-4 col-form-label bold" />
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label right-text">First Name</label>
                    <asp:Label runat="server" ID="FirstNameLabel" CssClass="col-sm-4 col-form-label bold" />
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label right-text">Last Name</label>
                    <asp:Label runat="server" ID="LastNameLabel" CssClass="col-sm-4 col-form-label bold" />
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label right-text">Email</label>
                    <asp:Label runat="server" ID="EmailLabel" CssClass="col-sm-4 col-form-label bold" />
                </div>

                <div class="form-group row">
                    <label class="col-sm-4 col-form-label right-text">Current Password</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="OldPassTextBox" TextMode="Password" runat="server" CssClass="form-control" MaxLength="16" placeholder="Enter current password here" />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 col-form-label right-text">New Password</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="NewPassTextBox"
                            TextMode="Password" runat="server" CssClass="form-control" MaxLength="16" placeholder="Enter new password here"/>
                    </div>
                </div>
                <div class="form-group row bottom-space">
                    <label class="col-sm-4 col-form-label right-text">Confirm Password</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="ConfirmPassTextBox"
                            TextMode="Password" runat="server" CssClass="form-control" MaxLength="16" placeholder="Re-enter new password here" />
                    </div>
                </div>

                <div class="form-group row justify-content-center bottom-space">
                    <asp:LinkButton ID="UpdatePasswordButton"
                        runat="server" OnClick="SetPassword_Click" CssClass="button button-primary col-sm-3">Update Password</asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

