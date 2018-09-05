<%@ Page Title="Manage Access Codes" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="manage_access_codes.aspx.cs" Inherits="Management_manage_access_codes" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

    <h2>Manage Survey Access Codes</h2>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="AddUpdatePanel">
        <ContentTemplate>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <%-- Add Access Code --%>
            <div class="row bottom-space">
                <div class="card border-success col-sm-12">
		            <div class="card-body">
			            <h3>Add New Access Code</h3>
                        <p class="hint">Enter a word from 6 to 8 letters and click the "Add Word" button.</p>
			            <div class="form-group row">
				            <div class="col-sm-4">
                                <asp:TextBox ID="AddAccessCodeTB" runat="server" CssClass="form-control" placeholder="Enter new code word" ValidateRequestMode="Disabled"></asp:TextBox>
				            </div>
                            <div class="col-sm-8">
                                <asp:LinkButton ID="AddAccessCodeButton" runat="server" CssClass="button button-success access-code-page-btn" OnClick="AddAccessCodeButton_Click">Add Word</asp:LinkButton>
                            </div>
			            </div>
		            </div>
	            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="SearchUpdatePanel">
        <ContentTemplate>
            <%-- Search Access Codes --%>
            <div class="row bottom-space">
                <div class="card col-sm-12">
		            <div class="card-body">
                        <h3>Search Access Code Words</h3>
                        <p class="hint">
                            To view access codes, enter the desired search filters and hit the "Search" button. <br />
                            To view all access codes, select the desired active statuses, leave the search box empty, and hit the "Search" button.
                        </p>
                        <%-- Search Type --%>
                        <div class="row">
		                    <label class="col-form-label search-label">Search Type</label>
                            <div class="row asp-check-and-radio search-type-space">
                                <asp:RadioButtonList ID="SearchTypeRadioButtonList" runat="server" CssClass="check-and-radio-space radiobutton-list-spaced" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="contains" Text="contains" />
                                    <asp:ListItem Value="starts" Text="starts with" />
                                    <asp:ListItem Value="match" Text="match exactly" />
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <%-- Active or not checkbox --%>
                        <div class="form-group row">
		                    <label for="ActiveStatus" class="col-form-label search-label">Active Status</label>
                            <div class="asp-check-and-radio">
                                <asp:CheckBox ID="ShowActiveCheckbox" runat="server" Text="show active" CssClass="check-and-radio-space button-label-spaced" />
                                <asp:CheckBox ID="ShowInactiveCheckbox" runat="server" Text="show inactive" CssClass="check-and-radio-space radiobutton-spaced" />
                            </div>
                        </div>

                        <%-- The Search Box and Buttons --%>
                        <div class="row">
		                    <div class="col-sm-4">
                                <asp:TextBox ID="SearchKeywordTB" runat="server" CssClass="form-control search-bar" placeholder="Enter search text here" ValidateRequestMode="Disabled"></asp:TextBox>
		                    </div>
		                    <div class="col-sm-8">
                                <asp:LinkButton ID="SearchButton" runat="server" CssClass="button button-info access-code-page-btn" OnClick="SearchButton_Click">Search</asp:LinkButton>
                                <asp:LinkButton ID="ClearSearchButton" runat="server" CssClass="button button-plain second-button access-code-page-btn" OnClick="ClearSearchButton_Click">Reset Filters</asp:LinkButton>
		                    </div>
	                    </div>

                        <%-- Letters --%>
                        <div class="row">
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
                </div>
            </div>

            <div class="row">
                <%-- left columm - access codes list --%>
                <div class="col-sm-5" runat="server" id="AccessCodeTableContainer">
                    <h3>Access Code Word List</h3>
                    <p class="hint">Click on an access code in the table below to edit that access code word. Inactive access code words are shown with a line through them.</p>
                    <asp:Repeater ID="AccessCodeTableRepeater" runat="server" ItemType="MSS.Data.DTOs.AccessCodeDTO">
                
                        <HeaderTemplate>
                            <table class="table table-striped scroll">
                                <tbody> 
                        </HeaderTemplate>

                        <ItemTemplate>
                            <tr>
						        <td class="access-code-table-<%# Item.activeyn.ToString().ToLower() %>"><asp:LinkButton ID="AccessCodeWordButton" runat="server" Text="<%# Item.accesscodeword %>" OnClick="AccessCodeWordButton_Click" CssClass="<%# Item.activeyn.ToString().ToLower() %>" /></td>
					        </tr>
                        </ItemTemplate>

                        <FooterTemplate>
                                    </tbody>
			                </table>
                        </FooterTemplate>

                    </asp:Repeater>
                </div>
        
                <%-- Right column - Update and delete access codes --%>
                <div class="col-sm-7 update-code-box" runat="server" id="UpdateAccessCodeContainer">
                    <div class="card border-primary col-sm-12">
		                <div class="card-body">
			                <h3>Update Access Code Word</h3>
                            <p class="hint">Enter a new word from 6 to 8 letters and select its active status to update the word.</p>
                            <div class="row asp-check-and-radio flush-with-textbox">
                                <asp:RadioButtonList ID="UpdateAccessActiveStatusCodeRadioButtonList" runat="server" CssClass="check-and-radio-space radiobutton-list-spaced" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="y" Text="active" />
                                    <asp:ListItem Value="n" Text="inactive" />
                                </asp:RadioButtonList>
                            </div>
			                <div class="form-group row">
				                <div class="col-sm-7">
                                    <asp:TextBox ID="UpdateAccessCodeTextBox" runat="server" CssClass="form-control" placeholder="Select an access code" ValidateRequestMode="Disabled"></asp:TextBox>
				                </div>
                                <div class="col-sm-5">
                                    <asp:LinkButton ID="UpdateAccessCodeButton" runat="server" CssClass="button button-primary access-code-page-btn" OnClick="UpdateAccessCodeButton_Click" OnClientClick="runMessage();">Update Word</asp:LinkButton>
                                </div>
			                </div>
		                </div>
	                </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <%-- Object Data Sources --%>
    <asp:ObjectDataSource ID="GetAccessCodeStartingLettersODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAccessCodeStartingLetters" TypeName="MSS.System.BLL.AccessCodeController"></asp:ObjectDataSource>
</asp:Content>