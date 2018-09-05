<%@ Page Title="Manage Access Codes" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="manage_access_codes.aspx.cs" Inherits="Management_manage_access_codes" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Manage Access Codes</h2>
	
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <div class="row">
        <%-- Left column - Searching --%>
        <div class="col-sm-7">
            <h3>Search Access Code Words</h3>
            <div class="row">
		        <div class="col-sm-7">
                    <asp:TextBox ID="SearchKeywordTB" runat="server" CssClass="form-control search-bar" placeholder="Search..." AutoPostBack="true"></asp:TextBox>
		        </div>
		        <div class="col-sm-5">
                    <asp:LinkButton ID="SearchButton" runat="server" CssClass="button button-info">Search</asp:LinkButton>
                    <asp:LinkButton ID="ClearSearchButton" runat="server" CssClass="button button-plain" OnClick="ClearSearchButton_Click">Clear Search</asp:LinkButton>
		        </div>
                <asp:Repeater ID="LetterListRepeater" runat="server" ItemType="System.Char" DataSourceID="GetAccessCodeStartingLettersODS">
                    <HeaderTemplate>
                        <ul class="access-code-letter-list">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:LinkButton ID="LetterLinkButton" runat="server" OnClick="LetterLinkButton_Click" Text="<%# Item %>" />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>

	        </div>
        </div>
        <%-- Right column - Adding --%>
        <div class="col-sm-5">
             <div class="card border-success">
		        <div class="card-body">
			        <h3>Add New Access Code</h3>
			        <div class="form-group row">
				        <div class="col-sm-7">
                            <asp:TextBox ID="AddAccessCodeTB" runat="server" CssClass="form-control" placeholder="Enter new code word"></asp:TextBox>
				        </div>
                        <asp:LinkButton ID="AddAccessCodeButton" runat="server" CssClass="button button-success col-sm-3" OnClick="AddAccessCodeButton_Click">Add</asp:LinkButton>
			        </div>
		        </div>
	        </div>
        </div>
    </div>

    <br /><%-- CCC: get rid of these breaks cause ur a bad coder--%>

    <div class="row">
        <%-- left columm - access codes list --%>
        <div class="col-sm-7">
            <asp:Repeater ID="AccessCodeListRepeater" runat="server" DataSourceID="GetAccessCodesODS" ItemType="MSS.Data.DTOs.AccessCodeDTO">
                <HeaderTemplate>
                    <ul class="access-code-list">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:LinkButton ID="Button" runat="server" Text="<%# Item.accesscodeword %>" />
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        
        <%-- Right column - Update and delete access codes --%>
        <div class="col-sm-5">

        </div>
    </div>

    <%-- Object Data Sources --%>
    <asp:ObjectDataSource ID="GetAccessCodesODS" runat="server" DataObjectTypeName="MSS.Data.DTOs.AccessCodeDTO" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAccessCodesByKeyword" TypeName="MSS.System.BLL.AccessCodeController">
        <SelectParameters>
            <asp:ControlParameter ControlID="SearchKeywordTB" PropertyName="Text" Name="keyword" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="GetAccessCodeStartingLettersODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAccessCodeStartingLetters" TypeName="MSS.System.BLL.AccessCodeController"></asp:ObjectDataSource>

</asp:Content>

