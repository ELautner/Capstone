<%@ Page Title="Contact Requests" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="contact_requests.aspx.cs" Inherits="Management_contact_requests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>View Contact Requests</h2>
	
    <asp:Label ID="ErrorMessage" runat="server" Text="" CssClass="text-danger bold" />
    <asp:Label ID="UserMessage" runat="server" Text="" CssClass="text-info bold" />

    <div class="form-group row">
		<label for="CareSiteDDL" class="col-sm-2 col-form-label right-text">Care Site</label>
		<div class="col-sm-4">
            <asp:DropDownList ID="CareSiteDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="GetCareSitesODS" DataTextField="caresitename" DataValueField="caresiteid">
                <asp:ListItem Value="">All Care Sites</asp:ListItem>
            </asp:DropDownList>
		</div>
        <asp:LinkButton ID="RefreshUnitListButton" runat="server" CssClass="button button-info col-sm-2" OnClick="RetrieveUnitListButton_Click">Refresh Unit List</asp:LinkButton>
	</div>

    <div class="form-group row">
		<label for="UnitRepeater" class="col-sm-2 col-form-label right-text">Select a Unit</label>
		<div class="col-sm-10">
            <asp:Repeater ID="UnitRepeater" runat="server" ItemType="MSS.Data.DTOs.UnitDTO" >
                <HeaderTemplate><div class="row"></HeaderTemplate>
                <ItemTemplate>
                    <div class="col-sm-2">
                        <asp:CheckBox ID="CheckBox" runat="server" Text="<%# Item.unitname %>" Checked="true" CssClass="checkbox-space" />
                        <asp:HiddenField runat="server" ID="HiddenUnitID" Value="<%# Item.unitid %>" />
                    </div>
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

	<div class="form-group row">
		<label for="date" class="col-sm-2 col-form-label right-text">Date Range</label>
		<div class="col-sm-3">
			<input id="dateStart" name="dateStart" class="form-control" type="date" runat="server" value="mm/dd/yyyy">
		</div>
        <label class="col-form-label">to</label>
		<div class="col-sm-3">
			<input id="dateEnd" name="dateEnd" class="form-control" type="date" runat="server" value="mm/dd/yyyy">
		</div>
	</div>

    <div class="form-group row">
		<label for="GenderCheckboxes" class="col-sm-2 col-form-label right-text">Gender</label>
		<div class="col-sm-10">
            <div class="row asp-checkboxes">
                <div class="col-sm-2">
                    <asp:CheckBox ID="GenderFemale" runat="server" Text="Female" Checked="true" value="F" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-2">
                    <asp:CheckBox ID="GenderMale" runat="server" Text="Male" Checked="true" value="M" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-2">
                    <asp:CheckBox ID="GenderOther" runat="server" Text="Other" Checked="true" value="O" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-3">
                    <asp:CheckBox ID="GenderPrefNotAns" runat="server" Text="Prefer not to answer" Checked="true" value="N" CssClass="checkbox-space" />
                </div>
            </div>
        </div>
    </div>

    <div class="form-group row">
		<label for="AgeCheckBoxes" class="col-sm-2 col-form-label right-text">Age Range</label>
		<div class="col-sm-10">
            <div class="row asp-checkboxes">
                <div class="col-sm-2">
                    <asp:CheckBox ID="Age017" runat="server" Text="0 - 17" Checked="true" value="0-17" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-2">
                    <asp:CheckBox ID="Age1834" runat="server" Text="18 - 34" Checked="true" value="18-34" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-2">
                    <asp:CheckBox ID="Age3554" runat="server" Text="35 - 54" Checked="true" value="35-54" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-2">
                    <asp:CheckBox ID="Age5574" runat="server" Text="55 - 74" Checked="true" value="55-74" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-2">
                    <asp:CheckBox ID="Age75" runat="server" Text="75+" Checked="true" value="75+" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-3">
                    <asp:CheckBox ID="Age0" runat="server" Text="Prefer not to answer" Checked="true" value="0" CssClass="checkbox-space" />
                </div>
            </div>
        </div>
    </div>

    <%-- ALTERNATE LAYOUT: WITH FULL AGE NAME ; "YEARS OLD" INCLUDED, TAKES UP MORE SPACE
        
    <div class="form-group row">
		<label for="AgeCheckBoxes" class="col-sm-2 col-form-label right-text">Age Range</label>
		<div class="col-sm-10">
            <div class="row asp-checkboxes">
                <div class="col-sm-3">
                    <asp:CheckBox ID="Age017" runat="server" Text="0 - 17 years old" Checked="true" value="0-17" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-3">
                    <asp:CheckBox ID="Age1834" runat="server" Text="18 - 34 years old" Checked="true" value="18-34" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-3">
                    <asp:CheckBox ID="Age3554" runat="server" Text="35 - 54 years old" Checked="true" value="35-54" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-3">
                    <asp:CheckBox ID="Age5574" runat="server" Text="55 - 74 years old" Checked="true" value="55-74" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-3">
                    <asp:CheckBox ID="Age75" runat="server" Text="75+ years old" Checked="true" value="75+" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-3">
                    <asp:CheckBox ID="Age0" runat="server" Text="Prefer not to answer" Checked="true" value="0" CssClass="checkbox-space" />
                </div>
            </div>
        </div>
    </div>
        
    --%>
    
    <div class="form-group row">
		<label for="StatusCheckBoxes" class="col-sm-2 col-form-label right-text">Status</label>
		<div class="col-sm-10">
            <div class="row asp-checkboxes">
                <div class="col-sm-2">
                    <asp:CheckBox ID="ProcessedY" runat="server" Text="Processed" Checked="true" value="y" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-3">
                    <asp:CheckBox ID="ProcessedN" runat="server" Text="Not processed" Checked="true" value="n" CssClass="checkbox-space" />
                </div>
            </div>
        </div>
    </div>

	<div class="form-group row justify-content-center">
        <asp:LinkButton ID="RetrieveContactRequests" runat="server" CssClass="button button-secondary" OnClick="RetrieveContactRequests_Click">Retrieve Contact Requests</asp:LinkButton>
	</div>

	<h3>Results: <asp:Label ID="ResultCountLabel" runat="server" Text="Populate # of Results Here"></asp:Label></h3>

   
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
			    <td><%# Item.firstname + " " + Item.lastname %></td>
			    <td><a href="survey_details.aspx?id=<%# Item.surveyid %>">view survey</a></td>--%>
		    </tr>
        </ItemTemplate>

        <FooterTemplate>
                </tbody>
	        </table>
        </FooterTemplate>

    </asp:Repeater>

    <%-- Object Data Sources --%>
    <asp:ObjectDataSource ID="GetCareSitesODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCareSites" TypeName="MSS.System.BLL.CareSiteController"></asp:ObjectDataSource>


</asp:Content>

<%-- Old code archive: CCC: claire delete later
    
    UNIT AS A DDL:
    <div class="form-group row">
		<label for="UnitDDL" class="col-sm-2 col-form-label right-text">Select a Unit</label>
		<div class="col-sm-3">
            <asp:DropDownList ID="UnitDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="GetCareSiteUnitsODS" DataTextField="unitname" DataValueField="unitid">
                <asp:ListItem Value="">All Units</asp:ListItem>
            </asp:DropDownList>
		</div>
	</div>


    INDEX CHECKBOXES NOT ASPX:

    <div class="form-group row">
		<label for="AgeCheckBoxes" class="col-sm-2 col-form-label right-text">Age Range</label>
        <div class="col-sm-10 asp-checkboxes">
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" ID="Age018" value="1" runat="server" checked="checked">
                <label class="form-check-label" for="Age018">Under 18 years</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="Age18342" value="2" runat="server" checked="checked">
                <label class="form-check-label" for="Age1834">18 to 34 years</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="Age35542" value="3" runat="server" checked="checked">
                <label class="form-check-label" for="Age3554">35 to 54 years</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="Age55742" value="4" runat="server" checked="checked">
                <label class="form-check-label" for="Age5574">55 to 74 years</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="Age752" value="5" runat="server" checked="checked">
                <label class="form-check-label" for="Age75">75 or older</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="AgePrefNoAnswer" value="6" runat="server" checked="checked">
                <label class="form-check-label" for="AgePrefNoAnswer">Prefer not to answer</label>
            </div>
        </div>
	</div>
    
    <div class="form-group row">
		<label for="GenderCheckBoxes" class="col-sm-2 col-form-label right-text">Gender</label>
        <div class="col-sm-10 asp-checkboxes">
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" ID="GenderFemale2" value="f" runat="server" checked="checked">
                <label class="form-check-label" for="GenderFemale">Female</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="GenderMale2" value="m" runat="server" checked="checked">
                <label class="form-check-label" for="GenderMale">Male</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="GenderOther2" value="o" runat="server" checked="checked">
                <label class="form-check-label" for="GenderOther">Other</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="GenderPrefNotSay" value="p" runat="server" checked="checked">
                <label class="form-check-label" for="GenderPrefNotSay">Prefer not to answer</label>
            </div>
        </div>
	</div>
    
    <div class="form-group row">
		<label for="StatusCheckBoxes" class="col-sm-2 col-form-label right-text">Status</label>
        <div class="col-sm-10 asp-checkboxes">
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" ID="ProcessedY2" value="y" runat="server" checked="checked">
                <label class="form-check-label" for="ProcessedY">Processed</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="ProcessedN2" value="n" runat="server" checked="checked">
                <label class="form-check-label" for="ProcessedN">Not processed</label>
            </div>
        </div>
	</div>
    
    
    SAMPLE DATA CHECKBOXES:
           
    <div class="col-sm-3">
		<select class="form-control">
			<option value="none">All Genders</option>
			<option value="1e">Female</option>
			<option value="1n">Male</option>
			<option value="1w">Prefer not to answer</option>
		</select>
	</div>
        
    <div class="col-sm-3">
		<select class="form-control">
			<option value="none">Any Age</option>
			<option value="1e">0 - 24 years old</option>
			<option value="1e">25 - 44 years old</option>
			<option value="1e">45 - 64 years old</option>
			<option value="1e">65+ years old</option>
			<option value="1e">Prefer not to answer</option>
		</select>
	</div>

    SAMPLE TABLE: 

    <table class="table table-striped contact-req">
        <thead>
		    <tr>
			    <th>Processed</th>
			    <th>Survey ID</th>
			    <th>Site</th>
			    <th>Unit</th>
			    <th>Date</th>
			    <th>Contact Method</th>
			    <th>Name</th>
			    <th></th>
		    </tr>
        </thead>
        <tbody>
		    <tr>
			    <td><input type="checkbox" name="" checked="checked" disabled></td>
			    <td>1</td>
			    <td>Misericordia</td>
			    <td>8 East</td>
			    <td>01/JAN/2018</td>
			    <td>In Person</td>
			    <td>Jane Doe</td>
			    <td><a href="survey_details.aspx">view survey</a></td>
		    </tr>
		    <tr>
			    <td><input type="checkbox" name="" checked="checked" disabled></td>
			    <td>2</td>
			    <td>Misericordia</td>
			    <td>8 East</td>
			    <td>01/JAN/2018</td>
			    <td>Over the Phone</td>
			    <td>Jane Doe</td>
			    <td><a href="survey_details.aspx">view survey</a></td>
		    </tr>
		    <tr>
			    <td><input type="checkbox" name="" disabled></td>
			    <td>3</td>
			    <td>Misericordia</td>
			    <td>8 East</td>
			    <td>01/JAN/2018</td>
			    <td>In Person</td>
			    <td>Jane Doe</td>
			    <td><a href="survey_details.aspx">view survey</a></td>
		    </tr>
		    <tr>
			    <td><input type="checkbox" name="" disabled></td>
			    <td>4</td>
			    <td>Misericordia</td>
			    <td>8 East</td>
			    <td>01/JAN/2018</td>
			    <td>Over the Phone</td>
			    <td>Jane Doe</td>
			    <td><a href="survey_details.aspx">view survey</a></td>
		    </tr>
		    <tr>
			    <td><input type="checkbox" name="" checked="checked" disabled></td>
			    <td>5</td>
			    <td>Misericordia</td>
			    <td>8 East</td>
			    <td>01/JAN/2018</td>
			    <td>Over the Phone</td>
			    <td>Jane Doe</td>
			    <td><a href="survey_details.aspx">view survey</a></td>
		    </tr>
        </tbody>
	</table>
    
    CHECKBOX LISTS:

    <asp:CheckBoxList ID="GenderCheckBoxes" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="form-group">
        <asp:ListItem Value="f" Selected="true">Female</asp:ListItem>
        <asp:ListItem Value="m" Selected="true">Male</asp:ListItem>
        <asp:ListItem Value="o" Selected="true">Other</asp:ListItem>
        <asp:ListItem Value="n" Selected="true">Prefer not to answer</asp:ListItem>
    </asp:CheckBoxList>

    <asp:CheckBoxList ID="AgeCheckBoxes" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="true">0-18</asp:ListItem>
        <asp:ListItem Value="2" Selected="true">19-30</asp:ListItem>
        <asp:ListItem Value="3" Selected="true">31-60</asp:ListItem>
        <asp:ListItem Value="4" Selected="true">Prefer not to answer</asp:ListItem>
    </asp:CheckBoxList>

    <asp:CheckBoxList ID="StatusCheckBoxes" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="y" Selected="true">Processed</asp:ListItem>
        <asp:ListItem Value="n" Selected="true">Not Processed</asp:ListItem>
    </asp:CheckBoxList>
    
     --%>