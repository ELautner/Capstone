<%@ Page Title="Account Search" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="accounts.aspx.cs" ValidateRequest="false" Inherits="Management_accounts" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <h2>Account Search</h2>

    <p class="hint">
        Account searches can be done with a username (e.g JaneDoe1) or a full name (e.g Jane Doe)<br />
        The username or full name do not have to be fully completed and can be partially entered
    </p>
    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="UpdatePanel">
        <ContentTemplate>

            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <div class="row bottom-space">
                <div class="col-sm-4">
                    <asp:TextBox ID="SearchKeywordTB" runat="server" CssClass="form-control search-bar" placeholder="Enter search text here" MaxLength="85"></asp:TextBox>
                </div>
                <asp:LinkButton ID="SearchButton" runat="server" OnClick="UsersFetch_Click" CssClass="button button-info accounts-btn">Search</asp:LinkButton>
                <asp:LinkButton ID="ClearSearchButton" runat="server" OnClick="ClearSearch_Click" CssClass="button button-plain second-button accounts-btn">Clear Search</asp:LinkButton>
            </div>

            <asp:Repeater ID="AccountRepeater" runat="server" DataSourceID="GetMgmtAccountsODS">
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
                    <tr>
                        <td>
                            <asp:Label Text='<%# Eval("username") %>' runat="server" ID="usernameLabel" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("firstname") + " " + Eval("lastname") %>' runat="server" ID="firstnameLabel" />
                        </td>
                        <td>
                            <a href="manage_account.aspx?id=<%# Eval("username") %>">Update Account</a>
                        </td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                        </tbody>
	                </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--     Object Data Sources --%>
    <asp:ObjectDataSource ID="GetMgmtAccountsODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListApplicationUsers" TypeName="MSS.System.BLL.Security.UserManager">
        <SelectParameters>
            <asp:ControlParameter ControlID="SearchKeywordTB" PropertyName="Text" DefaultValue="" Name="searchResults" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>

