<%@ Page Title="Contact Requests" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="contact_requests.aspx.cs" Inherits="Management_contact_requests" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Contact Requests</h2>

    <p class="note">If you have processed a contact request please re-retrieve the results.</p>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="UpdatePanel">
        <ContentTemplate>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <div class="form-group row top-space">
		        <label for="CareSiteDDL" class="col-sm-2 col-form-label right-text">Care Site</label>
		        <div class="col-sm-10">
                    <asp:DropDownList ID="CareSiteDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="GetCareSitesODS" 
                            DataTextField="caresitename" DataValueField="caresiteid" OnSelectedIndexChanged="RetrieveUnitList_Select" AutoPostBack="true">
                        <asp:ListItem Value="0">All Care Sites</asp:ListItem>
                    </asp:DropDownList>
		        </div>
	        </div>

            <div class="form-group row" id="unitsdiv" runat="server">
		        <label id="UnitLable" class="col-sm-2 col-form-label right-text" runat="server">Unit</label>
		        <div class="col-sm-10">
                    <asp:Repeater ID="UnitRepeater" runat="server" ItemType="MSS.Data.DTOs.UnitDTO" >
                        <HeaderTemplate>
                            <div class="row asp-check-and-radio">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="HiddenUnitID" Value="<%# Item.unitid %>" />
                            <div class="col-sm-2" runat="server" visible="<%# Item.unitname.Length <= 10 %>">
                                <asp:CheckBox ID="CheckBox" runat="server" Text="<%# Item.unitname %>" Checked="true" CssClass="check-and-radio-space" /></div>
                            <div class="col-sm-3" runat="server" visible="<%# Item.unitname.Length > 10 %>">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="<%# Item.unitname %>" Checked="true" CssClass="check-and-radio-space" />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate></div></FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

             <p class="form-hint">Dates are inclusive. If no dates are entered, all responses will be retrieved.<br />
                 End date defaults to today's date if left blank.<br />
                 Start date defaults to the earliest available survey date if left blank.
             </p>

	        <div class="form-group row">
		        <label for="date" class="col-sm-2 col-form-label right-text">Date Range</label>
		        <div class="col-sm-3">
			        <input  id="dateStart" name="dateStart" class="form-control" type="text" placeholder="mm/dd/yyyy" runat="server" maxlength="10">
		        </div>
                <label class="col-form-label">to</label>
		        <div class="col-sm-3">
			        <input id="dateEnd" name="dateEnd" class="form-control" type="text" runat="server" placeholder="mm/dd/yyyy" maxlength="10">
		        </div> 
	        </div>

            <div class="form-group row">
		        <label id="RespondentLable" class="col-sm-2 col-form-label right-text" runat="server">Respondent Type</label>
		        <div class="col-sm-10">
                    <asp:Repeater ID="RespondentTypeRepeater" runat="server" DataSourceID="GetRespondentTypesODS" ItemType="MSS.Data.DTOs.RespondentDTO" >
                        <HeaderTemplate>
                            <div class="row asp-check-and-radio">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="HiddenRespondentID" Value="<%# Item.respondenttypeid %>" />
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
		        <label for="GenderCheckboxes" class="col-sm-2 col-form-label right-text">Gender</label>
		        <div class="col-sm-10">
                    <asp:Repeater ID="GenderRepeater" runat="server" DataSourceID="GetGenderODS" ItemType="MSS.Data.DTOs.GenderDTO" >
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
		        <label for="AgeCheckBoxes" class="col-sm-2 col-form-label right-text">Age Range</label>
		        <div class="col-sm-10">
                    <asp:Repeater ID="AgeRepeater" runat="server" DataSourceID="GetAgeGroupODS" ItemType="MSS.Data.DTOs.AgeGroupDTO" >
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
            <div class="form-group row">
		        <label for="StatusCheckBoxes" class="col-sm-2 col-form-label right-text">Status</label>
		        <div class="col-sm-10">
                    <div class="row asp-check-and-radio">
                        <div class="col-sm-2">
                            <asp:CheckBox ID="ProcessedY" runat="server" Text="Processed" Checked="true" value="y" CssClass="check-and-radio-space" />
                        </div>
                        <div class="col-sm-3">
                            <asp:CheckBox ID="ProcessedN" runat="server" Text="Not processed" Checked="true" value="n" CssClass="check-and-radio-space" />
                        </div>
                    </div>
                </div>
            </div>

	        <div class="form-group row justify-content-center top-space bottom-space">
                <asp:LinkButton ID="RetrieveContactRequests" runat="server" CssClass="button button-info contact-req-btn" OnClick="RetrieveContactRequests_Click">Retrieve Contact Requests</asp:LinkButton>
                <asp:LinkButton ID="ResetPage" runat="server" CssClass="button button-plain second-button contact-req-btn" OnClick="ResetPage_Click"  OnClientClick="return confirm('Resetting the filters will remove the current results. Do you want to continue?')">Reset Filters</asp:LinkButton>
	        </div>

	        <h3>Results: <asp:Label ID="ResultCountLabel" runat="server" Text="-"></asp:Label></h3>

            <asp:Repeater ID="ContactRequestsRepeater" runat="server" ItemType="MSS.Data.DTOs.ContactRequestDTO">

                <HeaderTemplate>
                    <table class="table table-striped contact-req">
                        <thead>
		                    <tr>
			                    <th>Processed</th>
			                    <th>Survey ID</th>
			                    <th>Care Site</th>
			                    <th>Unit</th>
			                    <th>Date</th>
			                    <th>Contact Method</th>
			                    <th>Name</th>
			                    <th></th>
		                    </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
                        <td><asp:CheckBox ID="ContactCheckBox" Checked=" <%# (Item.contactedyn == true) ? true : false %> " runat="server" Enabled="false" /></td>
			            <td><%# Item.surveyid %></td>
			            <td><%# Item.caresitename %></td> 
			            <td><%# Item.unitname %></td>
			            <td><%# Item.date.ToString("MMM d, yyyy") %></td>
			            <td><%# Item.preferredcontact %></td>
			            <td><%# Item.firstname%></td>
			            <td><a href="survey_details.aspx?id=<%# Item.surveyid %>" target="_blank">View Survey</a></td>
		            </tr>
                </ItemTemplate>

                <FooterTemplate>
                        </tbody>
	                </table>
                </FooterTemplate>

            </asp:Repeater>

        </ContentTemplate>
    </asp:UpdatePanel>
    
    <%-- Object Data Sources --%>
    <asp:ObjectDataSource ID="GetCareSitesODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCareSites" TypeName="MSS.System.BLL.CareSiteController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="GetRespondentTypesODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllRespondentTypes" TypeName="MSS.System.BLL.RespondentTypeController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="GetAgeGroupODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllAges" TypeName="MSS.System.BLL.AgeController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="GetGenderODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllGenders" TypeName="MSS.System.BLL.GenderController"></asp:ObjectDataSource>
</asp:Content>