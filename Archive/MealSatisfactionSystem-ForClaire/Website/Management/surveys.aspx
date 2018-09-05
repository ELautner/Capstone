<%@ Page Title="Surveys" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="surveys.aspx.cs" Inherits="Management_surveys" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>View Surveys</h2>
    	
    <asp:Label ID="ErrorMessage" runat="server" Text="" CssClass="text-danger bold" />
    <asp:Label ID="UserMessage" runat="server" Text="" CssClass="text-info bold" />

    <div class="alert alert-warning alert-dismissible fade show" role="alert">
		<button type="button" class="close" data-dismiss="alert" aria-label="Close">
		<span aria-hidden="true">&times;</span>
		</button>
		<p><strong>Note:</strong> I've removed this part of the form because I'm still fixing the "master version" (which can be found <a href="contact_requests.aspx">here</a>)</p>
        <p>I'm hoping it will be here soon, but until then please refer to the Contact Requests page for an idea of what the upper half of this page will look like</p>
        <p>The table underneath this section is fair game though!</p>
	</div>

	<div class="form-group row justify-content-center">
        <asp:LinkButton ID="RetrieveSurveysButton" runat="server" CssClass="button button-secondary">Retrieve Surveys</asp:LinkButton>
	</div>
		
	<h3>Results: <asp:Label ID="ResultCountLabel" runat="server" Text="Populate # of Results Here" /></h3>

    <%-- Connect to ODS --%>
    <asp:Repeater ID="ContactRequestsRepeater" runat="server">

        <HeaderTemplate>
            <table class="table table-striped contact-req">
                <thead>
		            <tr>
			            <th>Survey ID</th>
			            <th>Care Site</th>
			            <th>Unit</th>
			            <th>Date</th>
			            <th></th>
		            </tr>
                </thead>
                <tbody>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <%-- TODO: the "IDs" need to be the actual words --%>
			    
                <%--
                    These give errors right now cause there is no ODS hooked up to the Repeater.
			    <td><%# Item.surveyid %></td>
			    <td><%# Item.caresiteid %></td> 
			    <td><%# Item.unitid %></td>
			    <td><%# Item.date %></td>
			    <td><a href="survey_details.aspx?id=<%# Item.surveyid %>">view survey</a></td>--%>
		    </tr>
        </ItemTemplate>

        <FooterTemplate>
                </tbody>
	        </table>
        </FooterTemplate>
    </asp:Repeater>

    <%-- Object Data Sources --%>

    <%-- TODO: The repeater needs an ODS but I'm not sure which method should be used. Maybe you need to make your own? --%>

</asp:Content>

<%-- 
    
    <table class="table table-striped">
		<tr>
			<th>Survey ID</th>
			<th>Site</th>
			<th>Unit</th>
			<th>Date</th>
			<th></th>
		</tr>
		<tr>
			<td>1</td>
			<td>Misericordia</td>
			<td>8 East</td>
			<td>25/JAN/2018</td>
			<td><a href="survey_details.aspx">view details</a></td>
		</tr>
		<tr>
			<td>2</td>
			<td>Misericordia</td>
			<td>8 East</td>
			<td>25/JAN/2018</td>
			<td><a href="survey_details.aspx">view details</a></td>
		</tr>
		<tr>
			<td>3</td>
			<td>Misericordia</td>
			<td>8 East</td>
			<td>25/JAN/2018</td>
			<td><a href="survey_details.aspx">view details</a></td>
		</tr>
		<tr>
			<td>4</td>
			<td>Misericordia</td>
			<td>8 East</td>
			<td>25/JAN/2018</td>
			<td><a href="survey_details.aspx">view details</a></td>
		</tr>
		<tr>
			<td>5</td>
			<td>Misericordia</td>
			<td>8 East</td>
			<td>25/JAN/2018</td>
			<td><a href="survey_details.aspx">view details</a></td>
		</tr>
		<tr>
			<td>6</td>
			<td>Misericordia</td>
			<td>8 East</td>
			<td>25/JAN/2018</td>
			<td><a href="survey_details.aspx">view details</a></td>
		</tr>
	</table>
    
    
     --%>