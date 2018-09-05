<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/MessageUserControl.ascx.cs" Inherits="UserControls_MessageUserControl"%>

<asp:Panel ID="MessagePanel" runat="server">

    <asp:Label ID="MessageTitle" runat="server" CssClass="muc-title" />
    <asp:Label ID="MessageLabel" runat="server" />

</asp:Panel>
		
<%-- Old code: keep in case anything blows up TODO: delete later    


    <div runat="server" id="MessageCard">
        <div class="card-header">
		    <asp:Label ID="MessageTitle" runat="server" />
	    </div>
	    <div class="card-body">
            <asp:Label ID="MessageLabel" runat="server" />
	    </div>
    </div>


    <asp:Panel ID="MessagePanel" runat="server">
        <div class="panel-heading">
            <asp:Label ID="MessageTitle" runat="server" />
        </div>
        <div class="panel-body">
            <asp:Label ID="MessageLabel" runat="server" />
        </div>
    </asp:Panel>


    <asp:Panel ID="MessagePanel" runat="server">
        <div runat="server" id="MessageCard">
            <div class="card-header">
		        <asp:Label ID="MessageTitle" runat="server" />
	        </div>
	        <div class="card-body">
                <asp:Label ID="MessageLabel" runat="server" />
	        </div>
        </div>
    </asp:Panel>


    <asp:Panel ID="MessagePanel" runat="server">
		<div class="card-header">
			<asp:Label ID="Label1" runat="server" />
		</div>
		<div class="card-body">
            <asp:Label ID="Label2" runat="server" />
		</div>
    </asp:Panel>



    
    <div class="panel-heading">
        <asp:Label ID="MessageTitleIcon" runat="server"> </asp:Label>
        <asp:Label ID="MessageTitle2" runat="server" />
    </div>
    <div class="panel-body">
        <asp:Label ID="MessageLabel" runat="server" />
        <asp:Repeater ID="MessageDetailsRepeater" runat="server" EnableViewState="false">
            <headertemplate>
                <ul>
            </headertemplate>
            <footertemplate>
                </ul>
            </footertemplate>
            <itemtemplate>
                <li><%# Eval("Error") %></li>
            </itemtemplate>
        </asp:Repeater>
    </div>--%>
