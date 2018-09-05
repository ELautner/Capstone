<%@ Page Title="Survey Details" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="survey_details.aspx.cs" Inherits="Management_survey_details" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Survey Details</h2>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="UpdatePanel">
        <ContentTemplate>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <div class="form-group row">
                <label class="col-sm-2 col-form-label right-text">Survey ID</label>
                <asp:Label ID="SurveyIDLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>

		        <label class="col-sm-2 col-form-label right-text">Date Taken</label>
                <asp:Label ID="DateLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>
	        </div>

	        <div class="form-group row">
		        <label class="col-sm-2 col-form-label right-text">Unit</label>
                <asp:Label ID="UnitLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>
        
                <label class="col-sm-2 col-form-label right-text">Care Site</label>
                <asp:Label ID="CareSiteLabel" runat="server" Text="[populate]" CssClass="col-sm-5 col-form-label bold"></asp:Label>
	        </div>

	        <div class="form-group row">
		        <label class="col-sm-2 col-form-label right-text">Respondent Type</label>
                <asp:Label ID="RespondentTypeLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>

                <label class="col-sm-2 col-form-label right-text">Age Range</label>
                <asp:Label ID="AgeLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>

		        <label class="col-sm-2 col-form-label right-text">Gender</label>
                <asp:Label ID="GenderLabel" runat="server" Text="[populate]" CssClass="col-sm-2 col-form-label bold"></asp:Label>
	        </div>

	        <div id="ContactSection" class="card card-secondary bottom-space">
		        <div class="card-header contact-header">
                    Contact Request for <asp:Label ID="Name" runat="server" Text="FirstName LastName" CssClass="bold"></asp:Label>
                    <asp:LinkButton ID="ProcessRequestButton" runat="server" CssClass="button button-primary col-sm-2 pull-right" OnClick="ProcessRequestButton_Click">Process Request</asp:LinkButton>
		        </div>
		        <div class="card-body">
			        <div class="row">
                        <asp:Label ID="BedNumberLabel" runat="server" CssClass="col-sm-2 col-form-label right-text">Bed Number</asp:Label>
                        <asp:Label ID="BedNumber" runat="server" CssClass="col-sm-2 col-form-label bold"></asp:Label>

                        <asp:Label ID="PhoneNumberLabel" runat="server" class="col-sm-2 col-form-label right-text">Phone Number</asp:Label>
                        <asp:Label ID="PhoneNumber" runat="server" CssClass="col-sm-2 col-form-label bold"></asp:Label>

                        <label class="col-sm-2 col-form-label right-text">Preferred Contact</label>
                        <asp:Label ID="PreferredContactLabel" runat="server" CssClass="col-sm-2 col-form-label bold"></asp:Label>
                
                        <asp:label ID="ProcessedStatusLabel" runat="server" CssClass="col-sm-2 col-form-label right-text">Status</asp:label>
                        <asp:Label ID="ProcessedStatus" runat="server" CssClass="col-sm-2 col-form-label bold"></asp:Label>
			        </div>				
		        </div>
	        </div>

	        <h3>Question Responses</h3>

	        <ol class="question-responses">
		        <li>
			        During this hospital stay, how would you describe the following features of your meals?
			        <ol type="a">
				        <li><asp:Label ID="Question1aLabel" runat="server" Text=""></asp:Label></li>
                        <asp:Label ID="Answer1" runat="server" Text="- No response -" CssClass="question-answer"></asp:Label>
				
                        <li><asp:Label ID="Question1bLabel" runat="server" Text=""></asp:Label></li>
                        <asp:Label ID="Answer2" runat="server" Text="- No response -" CssClass="question-answer"></asp:Label>
				
                        <li><asp:Label ID="Question1cLabel" runat="server" Text=""></asp:Label></li>
                        <asp:Label ID="Answer3" runat="server" Text="- No response -" CssClass="question-answer"></asp:Label>
				
				        <li><asp:Label ID="Question1dLabel" runat="server" Text=""></asp:Label></li>
				        <asp:Label ID="Answer4" runat="server" Text="- No response -" CssClass="question-answer"></asp:Label>
				
				        <li><asp:Label ID="Question1eLabel" runat="server" Text=""></asp:Label></li>
				        <asp:Label ID="Answer5" runat="server" Text="- No response -" CssClass="question-answer"></asp:Label>
			        </ol>
		        </li>

		        <li><asp:Label ID="Question2Label" runat="server" Text=""></asp:Label></li>
		        <asp:Label ID="Answer6" runat="server" Text="- No response -" CssClass="question-answer"></asp:Label>

		        <li><asp:Label ID="Question3Label" runat="server" Text=""></asp:Label></li>
		        <asp:Label ID="Answer7" runat="server" Text="- No response -" CssClass="question-answer"></asp:Label>

		        <li><asp:Label ID="Question4Label" runat="server" Text=""></asp:Label></li>
		        <asp:Label ID="Answer8" runat="server" Text="- No response -" CssClass="question-answer"></asp:Label>

		        <li><asp:Label ID="Question5Label" runat="server" Text=""></asp:Label></li>
		        <asp:Label ID="Answer9" runat="server" Text="- No response -" CssClass="question-answer"></asp:Label>
	        </ol>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>