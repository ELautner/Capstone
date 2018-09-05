<%@ Page Title="Take Meal Survey" Language="C#" MasterPageFile="~/Survey/SurveyMasterPage.master" AutoEventWireup="true" CodeFile="survey.aspx.cs" Inherits="Survey_survey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
	<section class="twelve columns enterForm">
            <asp:Label ID="ErrorMSG" class="errMSG" runat="server" Text=""></asp:Label>
		<form method="post" class="surveyForm u-full-width  u-cf" runat="server" autocomplete="off">

			<%--CARE SITE NAME--%>
			<section class="row">
				<asp:Label class="one-third column" for="siteName" runat="server">
					<p>Your site:</p>
				</asp:Label>
				<asp:Label runat="server" ID="SiteNameLabel" CssClass="one-third column colorFix" name="siteName" Text="" ForeColor="Black">
				</asp:Label>
			</section>

			<%--UNIT--%>
			<section class="row">
                <asp:RequiredFieldValidator runat="server" CssClass="errMSG2" ControlToValidate="UnitListDDL" InitialValue="-1" ErrorMessage="You must select a unit"></asp:RequiredFieldValidator>
				<asp:Label runat="server" class="one-third column" for="unitName">
					<p>Your unit:</p>
				</asp:Label>

				<asp:dropdownlist runat="server" cssclass="one-third column" datatextfield="unitname" datavaluefield="unitid" id="UnitListDDL" appenddatabounditems="true" ItemType="MSS.Data.DTOs.UnitDTO">
					<asp:listitem value="-1">Select your unit</asp:listitem>
					</asp:dropdownlist>
			</section>

			<%--CONSUMER TYPE--%>
			<section class="row">
                   <asp:RequiredFieldValidator runat="server" CssClass="errMSG2" ControlToValidate="ConsumerTypeDDL" InitialValue="-1" ErrorMessage="You must select a role"></asp:RequiredFieldValidator>
				<asp:Label runat="server" class="one-third column" for="ConsumerTypeDDL">
					<p>Are you a:</p>
				</asp:Label>

                <asp:DropDownList ID="ConsumerTypeDDL" runat="server" CssClass="one-third column" DataSourceID="CustomerODS" DataTextField="respondenttypename" DataValueField="respondenttypeid" AppendDataBoundItems="true" ItemType="MSS.Data.DTOs.RespondentDTO">
                    <asp:listitem value="-1">Please select your role</asp:listitem>
                </asp:DropDownList>
                
             
			</section>

			<%--START OF SURVEY QUESTIONS--%>
			<%--Q1--%>
			<section class="row">
				<asp:Label ID="Q1Label" runat="server" class="twelve columns">
					<p>
						1. During this hospital stay, how would you describe the following features of your meals?
					</p>
				</asp:Label>
				<section class="inset">
					<asp:Label ID="Q1ALabel" runat="server" for="variety" Text="The variety of food in your daily meals">
					</asp:Label>
                    <asp:DropDownList runat="server" ID="Q1ADDL">
                        <asp:ListItem Value="Unselected">Please select an option</asp:ListItem>
                        <asp:ListItem Value="1">Very Good</asp:ListItem>
                        <asp:ListItem Value="2">Good</asp:ListItem>
                        <asp:ListItem Value="3">Fair</asp:ListItem>
                        <asp:ListItem Value="4">Poor</asp:ListItem>
                        <asp:ListItem Value="5">Don't Know / No Opinion</asp:ListItem>
                    </asp:DropDownList>
				</section>
				<section class="inset">
					<asp:Label ID="Q1BLabel" runat="server" for="taste" Text="The taste and flavour of your meals">
					</asp:Label>
                    <asp:DropDownList runat="server" ID="Q1BDDL">
                        <asp:ListItem Value="Unselected">Please select an option</asp:ListItem>
                        <asp:ListItem Value="6">Very Good</asp:ListItem>
                        <asp:ListItem Value="7">Good</asp:ListItem>
                        <asp:ListItem Value="8">Fair</asp:ListItem>
                        <asp:ListItem Value="9">Poor</asp:ListItem>
                        <asp:ListItem Value="10">Don't Know / No Opinion</asp:ListItem>
                    </asp:DropDownList>
				</section>
				<section class="inset">
					<asp:Label ID="Q1CLabel" runat="server" for="temperature" Text="The temperature of your hot food">
					</asp:Label>
					<asp:DropDownList runat="server" ID="Q1CDDL">
                        <asp:ListItem Value="Unselected">Please select an option</asp:ListItem>
                        <asp:ListItem Value="11">Very Good</asp:ListItem>
                        <asp:ListItem Value="12">Good</asp:ListItem>
                        <asp:ListItem Value="13">Fair</asp:ListItem>
                        <asp:ListItem Value="14">Poor</asp:ListItem>
                        <asp:ListItem Value="15">Don't Know / No Opinion</asp:ListItem>
                    </asp:DropDownList>
				</section>
				<section class="inset">
					<asp:Label ID="Q1DLabel" runat="server" for="appearance" Text="The overall appearance of your meal">
					</asp:Label>
					<asp:DropDownList runat="server" ID="Q1DDDL">
                        <asp:ListItem Value="Unselected">Please select an option</asp:ListItem>
                        <asp:ListItem Value="16">Very Good</asp:ListItem>
                        <asp:ListItem Value="17">Good</asp:ListItem>
                        <asp:ListItem Value="18">Fair</asp:ListItem>
                        <asp:ListItem Value="19">Poor</asp:ListItem>
                        <asp:ListItem Value="20">Don't Know / No Opinion</asp:ListItem>
                    </asp:DropDownList>
				</section>
				<section class="inset">
					<asp:Label ID="Q1ELabel" runat="server"  for="staff" Text="The helpfulness of the staff who deliver your meals">
					</asp:Label>
					<asp:DropDownList runat="server" ID="Q1EDDL">
                        <asp:ListItem Value="Unselected">Please select an option</asp:ListItem>
                        <asp:ListItem Value="21">Very Good</asp:ListItem>
                        <asp:ListItem Value="22">Good</asp:ListItem>
                        <asp:ListItem Value="23">Fair</asp:ListItem>
                        <asp:ListItem Value="24">Poor</asp:ListItem>
                        <asp:ListItem Value="25">Don't Know / No Opinion</asp:ListItem>
                    </asp:DropDownList>
				</section>
			</section>

			<%--Q2--%>
			<section>
				<asp:Label ID="Q2Label" runat="server" for="satisfaction" Text="2. How satisfied are you with the portion sizes of your meals?">
				</asp:Label>
                <asp:DropDownList runat="server" ID="Q2DDL">
                        <asp:ListItem Value="Unselected">Please select an option</asp:ListItem>
                        <asp:ListItem Value="26">Portion sizes are too small</asp:ListItem>
                        <asp:ListItem Value="27">Portion sizes are just right</asp:ListItem>
                        <asp:ListItem Value="28">Portion sizes are too large</asp:ListItem>
                    </asp:DropDownList>
			</section>

			<%--Q3--%>
			<section>
				<asp:Label ID="Q3Label" runat="server" for="diet" Text="3. Do your meals take into account your specific diet requirements?">
				</asp:Label>
                <asp:DropDownList runat="server" ID="Q3DDL">
                        <asp:ListItem Value="Unselected">Please select an option</asp:ListItem>
                        <asp:ListItem Value="29">Always</asp:ListItem>
                        <asp:ListItem Value="30">Usually</asp:ListItem>
                        <asp:ListItem Value="31">Occasionally</asp:ListItem>
                        <asp:ListItem Value="32">Never</asp:ListItem>
                        <asp:ListItem Value="33">I do not have any specific diet requirements</asp:ListItem>
                    </asp:DropDownList>
			</section>

			<%--Q4--%>
			<section>
				<asp:Label ID="Q4Label" runat="server" for="mealExperience" Text="4. Overall, how would you rate your meal experience?">
				</asp:Label>

                <asp:DropDownList runat="server" ID="Q4DDL">
                        <asp:ListItem Value="Unselected">Please select an option</asp:ListItem>
                        <asp:ListItem Value="34">Very Good</asp:ListItem>
                        <asp:ListItem Value="35">Good</asp:ListItem>
                        <asp:ListItem Value="36">Fair</asp:ListItem>
                        <asp:ListItem Value="37">Poor</asp:ListItem>
                        <asp:ListItem Value="38">Don't Know / No Opinion</asp:ListItem>
                    </asp:DropDownList>
			</section>

			<%--Q5--%>
			<section class="row">
				<asp:Label ID="Q5Label" runat="server" for="messageBox" Text="5. Is there anything else you would like to share about your meal experience?">
				</asp:Label>
				
                <asp:TextBox runat="server" ID="Q5Text" Width="100%" Rows="5" TextMode="MultiLine" Text="" ValidateRequestMode="Disabled"></asp:TextBox>
			</section>
			<%--END OF SURVEY QUESTIONS--%>

			<%--START OF CUSTOMER PROFILE QUESTIONS--%>
			<%--GREETING TEXT - "Header"--%>
			<section class="twelve columns welcomeBlurb">
				<h3>Customer Profile</h3>
				<p>
					The questions in this section help us better understand our customer’s needs and improve our services. All customer profile questions are <span class="errMSG">optional</span>. 
				</p>
			</section>

			<section class="twelve columns">
				<%--AGE AND GENDER--%>
				<section class="row">
					<fieldset class="twelve columns">
						<legend class="dkBlTxt">Tell us about yourself:</legend>
						<asp:Label runat="server" class="twelve columns" for="ageRange">
							<p>What is your age?</p>
						</asp:Label>
                        <%--ADD REQUIRED FIELD--%>	
                        <asp:DropDownList runat="server" ID="AgeDDL" AppendDataBoundItems="true" DataSourceID="AgesODS" DataTextField="agegroupname" DataValueField="agegroupid">
                            <asp:ListItem Value="-1">Please select an option</asp:ListItem>
                        </asp:DropDownList>
                         					
						<asp:Label runat="server" class="twelve columns" for="gender">
							<p>What is your gender?</p>
						</asp:Label>
                        <%--ADD REQUIRED FIELD--%>	
                        <asp:DropDownList runat="server" ID="GenderDDL" AppendDataBoundItems="true" DataSourceID="GendersODS" DataTextField="gendername" DataValueField="genderid">
                            <asp:ListItem Value="-1">Please select an option</asp:ListItem>
                        </asp:DropDownList>
					</fieldset>
				</section>

				<%--CONTACT ME "HEADER"--%>
				<section class="row enterForm welcomeBlurb">
					<h3>Contact Me</h3>
					<p class="twelve columns">If you would like to talk to a food services staff member about your recent meal experience, please fill in the fields below. Please remember that these questions are <span class="errMSG">optional</span>.</p>
				</section> 

                <section class="row contactErrorMsg" > 
                     <asp:Label ID="ContactErr" class="errMSG" runat="server" Text=""></asp:Label> 
                </section>  

				<%--CONTACT OPTIONS--%>
				<section>
					<fieldset class="twelve columns">
						<legend runat="server" class="dkBlTxt">
							Preferred contact method:
						</legend>
					
                        <asp:DropDownList runat="server" ID="ContactDDL" OnSelectedIndexChanged="ContactDDL_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="-1">Please select an option</asp:ListItem>
                            <asp:ListItem Value="In Person">In person</asp:ListItem>
                            <asp:ListItem Value="Over the Phone">Over the phone</asp:ListItem>
                            <asp:ListItem Value="Prefer Not to Be Contacted">I don't want to be contacted</asp:ListItem>
                        </asp:DropDownList>
						
                        <section id="box box1 box2 FirstNameBox" class="person phone">
                            <asp:Label ID="FirstNameLbl" runat="server" for="firstName">First name:</asp:Label>
							<input id="FirstName" runat="server" type="text" name="firstName"/>
                        </section>


						<section id="box1 box" class="person">
							<asp:Label ID="RoomNumberLbl" runat="server" for="unit">Room number:</asp:Label>
							<input id="RoomNumber" runat="server" type="text" name="unit" maxlength="5"/>
							<asp:Label ID="BedNumberLbl" runat="server" for="bed">Bed number:</asp:Label>
							<input id="BedNumber" runat="server" type="text" name="bed" maxlength="4"/>
						</section>

						<section id="box2 box" class="phone">
							<asp:Label ID="PhoneNumberLbl" runat="server" for="phoneNumber">Phone number:</asp:Label>
							<input id="PhoneNumber" runat="server" type="text" name="phoneNumber"/>
						</section>
						<asp:Button runat="server" ID="Submit" class="button-primary  u-pull-right" Text="Submit Survey" OnClick="Submit_Click" />
					</fieldset>
				</section>
			</section>
		</form>
	</section>

    <%--ODS FOR GENDER AND AGE RESPECTIVELY --%>
    <asp:ObjectDataSource ID="GendersODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetActiveGenders" TypeName="MSS.System.BLL.GenderController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="AgesODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetActiveAges" TypeName="MSS.System.BLL.AgeController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="CustomerODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetRespondentTypes" TypeName="MSS.System.BLL.RespondentTypeController"></asp:ObjectDataSource>
</asp:Content>