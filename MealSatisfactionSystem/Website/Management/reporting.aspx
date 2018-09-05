<%@ Page Title="Reporting" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="reporting.aspx.cs" Inherits="Management_reporting" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">

    <h2>Reporting</h2>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <div class="form-group row">
                <label for="CareSiteDDL" class="col-sm-2 col-form-label right-text">Care Site</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="CareSiteDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataTextField="caresitename" DataValueField="caresiteid" OnSelectedIndexChanged="CareSiteDDL_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="">All Care Sites</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group row" id="unitsdiv" runat="server">
                <label id="UnitLabel" runat="server" for="UnitRepeater" class="col-sm-2 col-form-label right-text">Unit</label>
                <div class="col-sm-10">
                    <asp:Repeater ID="UnitRepeater" runat="server" ItemType="MSS.Data.DTOs.UnitDTO">
                        <HeaderTemplate>
                            <div class="row asp-check-and-radio">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="HiddenUnitID" Value="<%# Item.unitid %>" />
                            <div class="col-sm-2" runat="server" visible="<%# Item.unitname.Length <= 10 %>">
                                <asp:CheckBox ID="CheckBox" runat="server" Text="<%# Item.unitname %>" Checked="true" CssClass="check-and-radio-space" />
                            </div>
                            <div class="col-sm-3" runat="server" visible="<%# Item.unitname.Length > 10 %>">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="<%# Item.unitname %>" Checked="true" CssClass="check-and-radio-space" />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate></div></FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <p class="form-hint">
                Dates are inclusive. If no dates are entered, all responses will be retrieved.<br />
                End date defaults to today's date if left blank.<br />
                Start date defaults to the earliest available survey date if left blank.
            </p>

            <div class="form-group row">
                <label for="date" class="col-sm-2 col-form-label right-text">Date Range</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="dateStart" CssClass="form-control" runat="server" placeholder="mm/dd/yyyy" MaxLength="10" ValidateRequestMode="Disabled" />
                </div>
                <label class="col-form-label">to</label>
                <div class="col-sm-3">
                    <asp:TextBox ID="dateEnd" CssClass="form-control" runat="server" placeholder="mm/dd/yyyy" MaxLength="10" ValidateRequestMode="Disabled" />
                </div>
            </div>

            <div class="form-group row">
                <label for="RespondentTypeRepeater" class="col-sm-2 col-form-label right-text">Respondent Type</label>
                <div class="col-sm-10">
                    <asp:Repeater ID="RespondentTypeRepeater" runat="server" ItemType="MSS.Data.DTOs.RespondentDTO">
                        <HeaderTemplate>
                            <div class="row asp-check-and-radio">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="HiddenRespondentTypeID" Value="<%# Item.respondenttypeid %>" />
                            <div runat="server" id="checkboxdiv2" class="col-sm-2" visible="<%# Item.respondenttypename.Length <= 10 %>">
                                <asp:CheckBox ID="CheckBox" runat="server" Text="<%# Item.respondenttypename %>" Checked="true" CssClass="check-and-radio-space" />
                            </div>
                            <div runat="server" id="checkboxdiv3" class="col-sm-3" visible="<%# Item.respondenttypename.Length > 10 %>">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="<%# Item.respondenttypename %>" Checked="true" CssClass="check-and-radio-space" />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate></div></FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="form-group row">
                <label for="GenderRepeater" class="col-sm-2 col-form-label right-text">Gender</label>
                <div class="col-sm-10">
                    <asp:Repeater ID="GenderRepeater" runat="server" ItemType="MSS.Data.DTOs.GenderDTO">
                        <HeaderTemplate>
                            <div class="row asp-check-and-radio">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="HiddenGenderID" Value="<%# Item.genderid %>" />
                            <div runat="server" id="checkboxdiv2" class="col-sm-2" visible="<%# Item.gendername.Length <= 10 %>">
                                <asp:CheckBox ID="CheckBox" runat="server" Text="<%# Item.gendername %>" Checked="true" CssClass="check-and-radio-space" />
                            </div>
                            <div runat="server" id="checkboxdiv3" class="col-sm-3" visible="<%# Item.gendername.Length > 10 %>">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="<%# Item.gendername %>" Checked="true" CssClass="check-and-radio-space" />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate></div></FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="form-group row">
                <label for="AgeGroupRepeater" class="col-sm-2 col-form-label right-text">Age Group</label>
                <div class="col-sm-10">
                    <asp:Repeater ID="AgeGroupRepeater" runat="server" ItemType="MSS.Data.DTOs.AgeGroupDTO">
                        <HeaderTemplate>
                            <div class="row asp-check-and-radio">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="HiddenAgeGroupID" Value="<%# Item.agegroupid %>" />
                            <div runat="server" id="checkboxdiv2" class="col-sm-2" visible="<%# Item.agegroupname.Length <= 10 %>">
                                <asp:CheckBox ID="CheckBox" runat="server" Text="<%# Item.agegroupname %>" Checked="true" CssClass="check-and-radio-space" />
                            </div>
                            <div runat="server" id="checkboxdiv3" class="col-sm-3" visible="<%# Item.agegroupname.Length > 10 %>">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="<%# Item.agegroupname %>" Checked="true" CssClass="check-and-radio-space" />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate></div></FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="form-group row  bottom-space">
                <label for="QuestionDDL" class="col-sm-2 col-form-label right-text">Question</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="QuestionDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="GetActiveQuestionsODS" DataTextField="question" DataValueField="surveyquestionid">
                        <asp:ListItem Value="">Select a Question</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group row justify-content-center bottom-space">
                <asp:LinkButton ID="GenerateReportButton" runat="server" CssClass="button button-info report-btn" OnClick="GenerateReportButton_Click">Generate Report</asp:LinkButton>
                <asp:LinkButton ID="ResetReport" runat="server" CssClass="button button-plain second-button report-btn" OnClick="ResetReport_Click" OnClientClick="return confirm('Resetting the filters will close the current report. Do you want to continue?')">Reset Filters</asp:LinkButton>
            </div>

            <table class="col-sm-10 report-container">
                <tr>
                    <td>
                        <rsweb:ReportViewer Style="margin: 0 auto;" ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" SizeToReportContent="true" Width="100%" ShowPageNavigationControls="false" ShowBackButton="false" ShowRefreshButton="false" ShowFindControls="false" BackColor="White" ToolBarItemBorderColor="White" ToolBarItemHoverBackColor="#e0f9ff" />
                    </td>
                </tr>
            </table>

            <%-- Object Data Sources --%>
            <asp:ObjectDataSource ID="GetActiveQuestionsODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetQuestionsforReports" TypeName="MSS.System.BLL.SurveyQuestionController" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
