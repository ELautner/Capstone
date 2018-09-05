<%@ Page Title="Survey Details" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="survey_details.aspx.cs" Inherits="Management_survey_details" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Survey Details: Survey ID <asp:Label ID="SurveyIDLabel" runat="server" Text="[populate]"></asp:Label></h2>
    	
    <asp:Label ID="ErrorMessage" runat="server" Text="" CssClass="text-danger bold" />
    <asp:Label ID="UserMessage" runat="server" Text="" CssClass="text-info bold" />

	<div class="form-group row">
		<label class="col-sm-2 col-form-label right-text">Date Taken</label>
        <asp:Label ID="DateLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>
        
        <label class="col-sm-2 col-form-label right-text">Care Site</label>
        <asp:Label ID="CareSiteLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>

		<label class="col-sm-2 col-form-label right-text">Unit</label>
        <asp:Label ID="UnitLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>
	</div>

	<div class="form-group row">
		<label class="col-sm-2 col-form-label right-text">Respondent Type</label>
        <asp:Label ID="RespondentTypeLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>

        <label class="col-sm-2 col-form-label right-text">Age Range</label>
        <asp:Label ID="AgeLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>

		<label class="col-sm-2 col-form-label right-text">Gender</label>
        <asp:Label ID="GenderLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>
	</div>

	<div class="card card-secondary">
        <%-- NOTE: not sure if it looks best with the name in the title or in the body... Make a decision on this Claire (CCC) --%>
		<div class="card-header">Contact Information: <asp:Label ID="Name" runat="server" Text="Brown, Jackie" CssClass="bold"></asp:Label></div>
		<div class="card-body">

			<div class="row">
                <label class="col-sm-2 col-form-label right-text">Name</label>
                <asp:Label ID="LNameAndFNameLabel" runat="server" Text="Brown, Jackie" CssClass="col-sm-2 col-form-label bold"></asp:Label>

                <%-- Would be cool if either bed or phone number was displayed, not both --%>
                <label class="col-sm-2 col-form-label right-text">Bed Number</label>
                <asp:Label ID="BedNumLabel" runat="server" Text="2" CssClass="col-sm-1 col-form-label bold"></asp:Label>

                <label class="col-sm-2 col-form-label right-text">Phone Number</label>
                <asp:Label ID="PhoneNumLabel" runat="server" Text="7880-435-2355" CssClass="col-sm-2 col-form-label bold"></asp:Label>

                <label class="col-sm-2 col-form-label right-text">Preferred Contact</label>
                <asp:Label ID="Label3" runat="server" Text="I don't even know what this means" CssClass="col-sm-2 col-form-label bold"></asp:Label>

                <%-- It would be cool if this could change colour depending on if it's processed or not... --%>
                <label class="col-sm-2 col-form-label right-text">Status</label>
                <asp:Label ID="ProcessedStatusLabel" runat="server" Text="Processed" CssClass="col-sm-2 col-form-label bold"></asp:Label>
			</div>				
		</div>
	</div>

	<br><br>
	<h3>Question Responses</h3>

    <%-- NOTE: right now, these questions are hard-coded, but should pull the historic question from the database --%>

	<ol class="question-responses">
		<li>
			During this hospital stay, how would you describe the following features of your meals?
			<ol type="a">
				<li>The variety of food in your daily meals</li>
                <asp:Label ID="Question1aLabel" runat="server" Text="[populate]" CssClass="question-answer"></asp:Label>
				
                <li>The taste and flavour of your meals</li>
                <asp:Label ID="Question1bLabel" runat="server" Text="[populate]" CssClass="question-answer"></asp:Label>
				
                <li>The temperature of your hot food</li>
                <asp:Label ID="Question1cLabel" runat="server" Text="[populate]" CssClass="question-answer"></asp:Label>
				
				<li>The overall appearance of your meal</li>
				<asp:Label ID="Question1dLabel" runat="server" Text="[populate]" CssClass="question-answer"></asp:Label>
				
				<li>The helpfulness of the staff who deliver your meals</li>
				<asp:Label ID="Question1eLabel" runat="server" Text="[populate]" CssClass="question-answer"></asp:Label>
			</ol>
		</li>

		<li>How satisfied are you with the portion sizes of your meals?</li>
		<asp:Label ID="Question2" runat="server" Text="[populate]" CssClass="question-answer"></asp:Label>

		<li>
			Do your meals take into account your specific diet requirements?</li>
			<asp:Label ID="Question3" runat="server" Text="[populate]" CssClass="question-answer"></asp:Label>

		<li>
			Overall, how would you rate your meal experience?</li>
			<asp:Label ID="Question4" runat="server" Text="[populate]" CssClass="question-answer"></asp:Label>

		<li>Is there anything else you would like to share about your meal experience?</li>
		<asp:Label ID="Question5" runat="server" Text="[populate]" CssClass="question-answer"></asp:Label>
	</ol>
</asp:Content>

