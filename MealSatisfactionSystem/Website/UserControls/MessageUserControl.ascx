<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/MessageUserControl.ascx.cs" Inherits="UserControls_MessageUserControl"%>

<asp:Panel ID="MessagePanel" runat="server">

    <asp:Label ID="MessageTitle" runat="server" CssClass="muc-title" />
    <asp:Label ID="MessageLabel" runat="server" />
    <asp:Label ID="SystemError" runat="server" CssClass="muc-sys-error" />
    <ul runat="server" id="MessageList"></ul>
    <asp:Repeater ID="MessageListRepeater" runat="server" ItemType="System.string">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>

        <ItemTemplate>
            <li><%# Item %></li>
        </ItemTemplate>

        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Panel>