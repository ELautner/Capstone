<%@ Page Title="Meal Survey" Language="C#" MasterPageFile="~/Survey/SurveyMasterPage.master" AutoEventWireup="true" CodeFile="survey.aspx.cs" Inherits="Survey_survey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
   <%-- <main class="row">--%>

      <section class="twelve columns enterForm">
        <form action="thankYou.aspx" method="post" class="surveyForm u-full-width  u-cf" runat="server">
            <section class="row">
                   <asp:Label class="one-third column" for="siteName" runat="server">
                       <p>Your site:</p>
                   </asp:Label>
                <asp:Label runat="server" ID="SiteNameLabel" class="one-third column" name="siteName" Text="">
                  <%--  <p>Misericordia</p>--%>
                </asp:Label>
            </section>

            <section class="row">
              <asp:Label runat="server" class="one-third column" for="unitName">
                  <p>Your unit:</p>
              </asp:Label>
                <%--<asp:DropDownList ID="CareSiteDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="CareSiteODS" DataTextField="sitename" DataValueField="caresiteid" OnSelectedIndexChanged="RetrieveUnitList_Select" AutoPostBack="true">--%>
                <asp:DropDownList ID="UnitDDL" CssClass="one-third column" runat="server" AppendDataBoundItems="false"  DataTextField="unitname" DataValueField="unitid" AutoPostBack="true">
                <asp:ListItem Value="0">Select...</asp:ListItem>
            </asp:DropDownList>
                <%--Maybe get this from the database?--%>
             <%-- <select class="one-third column" name="unitName">
                <option value="0">Please select a unit</option>
				<option value="8E">8 East</option>
                <option value="7E">7 East</option>
                <option value="7W">7 West</option>
                 <option value="6E">6 East</option>
                 <option value="6W">6 West</option>
                 <option value="5E">5 East</option>
                 <option value="5W">5 West</option>
                 <option value="3E">3 East</option>
                 <option value="3N">3 North</option>
                 <option value="ED">ED</option>
              </select>--%>
            </section>
            <section class="row">
                <asp:Label runat="server" class="one-third column" for="consumerType">
                    <p>Which type of consumer are you:</p>
                </asp:Label>
              <select class="one-third column" name="consumerType">
                <option value="-1">Please select your role</option>
                <option value="1">Patient</option>
                <option value="2">Physician/Doctor</option>
                <option value="3">Staff Member</option>
                <option value="4">Visitor</option>
                <option value="5">Volunteer</option>
                  <option value="6">Other</option> <%--WHY IS THIS HERE?--%>
                  <%--<option value="0">Unknown</option>--%>
              </select>
            </section>
            <section class="row">
                <asp:Label ID="Q1Label" runat="server" class="twelve columns">
                    <p>
                        1. During this hospital stay, how would you describe the following features of your meals?
                    </p>
                </asp:Label>
              
              <section class="inset">
                  <asp:Label ID="Q1ALabel" runat="server" for="variety">
                     <p>The variety of food in your daily meals</p>
                  </asp:Label>
                
                <select name="variety">
                    <option value="Unselected">Please select an option</option>
                    <option value="Very Good">Very Good</option>
                    <option value="Good">Good</option>
                    <option value="Fair">Fair</option>
                    <option value="Poor">Poor</option>
                    <option value="Don't Know / No Opinion">Don't Know / No Opinion</option>
                </select>
              </section>

              <section class="inset">
                  <asp:Label ID="Q1BLabel" runat="server" for="taste">
                      <p>
                        The taste and flavour of your meals
                      </p>  
                  </asp:Label>
                <select name="taste">
                  <option value="Unselected">Please select an option</option>
                    <option value="Very Good">Very Good</option>
                    <option value="Good">Good</option>
                    <option value="Fair">Fair</option>
                    <option value="Poor">Poor</option>
                    <option value="Don't Know / No Opinion">Don't Know / No Opinion</option>
                </select>
              </section>

              <section class="inset">
                  <asp:Label ID="Q1CLabel" runat="server" for="temperature">
                      <p>
                            The temperature of your hot food
                      </p>
                  </asp:Label>
                <select name="temperature">
                  <option value="Unselected">Please select an option</option>
                  <option value="Very Good">Very Good</option>
                    <option value="Good">Good</option>
                    <option value="Fair">Fair</option>
                    <option value="Poor">Poor</option>
                    <option value="Don't Know / No Opinion">Don't Know / No Opinion</option>
                </select>
              </section>

              <section class="inset">
                  <asp:Label ID="QDLabel" runat="server" for="appearance">
                      <p>
                          The overall appearance of your meal
                      </p>
                  </asp:Label>
                <select name="appearance">
                  <option value="Unselected">Please select an option</option>
                  <option value="Very Good">Very Good</option>
                    <option value="Good">Good</option>
                    <option value="Fair">Fair</option>
                    <option value="Poor">Poor</option>
                    <option value="Don't Know / No Opinion">Don't Know / No Opinion</option>
                </select>
              </section>

              <section class="inset">
                  <asp:Label ID="Q1DLabel" runat="server"  for="staff">
                      <p>
                          The helpfulness of the staff who deliver your meals
                      </p>
                  </asp:Label>
                <select name="staff">
                  <option value="Unselected">Please select an option</option>
                  <option value="Very Good">Very Good</option>
                    <option value="Good">Good</option>
                    <option value="Fair">Fair</option>
                    <option value="Poor">Poor</option>
                    <option value="Don't Know / No Opinion">Don't Know / No Opinion</option>
                </select>
              </section>
            </section>

            <asp:Label ID="Q2Label" runat="server" for="satisfaction">
                <p>
                    2. How satisfied are you with the portion sizes of your meals?
                </p>
            </asp:Label>
                <select name="satisfaction">
                  <option value="Unselected">Please select an option</option>
                  <option value="" selected="selected">Portion sizes are too small</option>
                  <option value="">Portion sizes are just right</option>
                  <option value="">Portion sizes are too large</option>
                </select>

            <asp:Label ID="Q3Label" runat="server" for="diet">
                <p>
                    3. Do your meals take into account your specific diet requirements?
                </p>
            </asp:Label>
                <select name="diet">
                    <option value="Unselected">Please select an option</option>
                    <option value="">Always</option>
                    <option value="">Usually</option>
                    <option value="">Occasionally</option>
                    <option value="">Never</option>
                    <option value="">No specific dietary requirements</option>
                </select>

            <asp:Label ID="Q4Label" runat="server" for="mealExperience">
                <p>
                    4. Overall, how would you rate your meal experience?
                </p>
            </asp:Label>
                <select name="mealExperience">
                  <option value="Unselected">Please select an option</option>
                  <option value="Very Good">Very Good</option>
                    <option value="Good">Good</option>
                    <option value="Fair">Fair</option>
                    <option value="Poor">Poor</option>
                    <option value="Don't Know / No Opinion">Don't Know / No Opinion</option>
                </select>

              <section class="row">
                  <asp:Label ID="Q5Label" runat="server" for="messageBox">
                      <p>
                          5. Is there anything else you would like to share about your meal experience?
                        </p>
                  </asp:Label>
                <textarea name="messageBox"></textarea>
              </section>

             
    <%--Customer Profile Region--%>
        <section class="twelve columns welcomeBlurb">
            <h3>Customer Profile Questions</h3>
            <p>
              The questions in this section give us a better understanding of who is completing our survey. All demographic questions are <span class="boldRed">optional</span>. 
            </p>
          </section>

            <section class="twelve columns">
            <%--<form class="u-full-width consSpace" method="post" action="thankYou.aspx">--%>
              <div class="row">
                <fieldset class="twelve columns">
                  <legend class="dkBlTxt">Tell Us About Yourself:</legend>
                    <asp:Label runat="server" class="twelve columns" for="ageRange">
                        <p>What is your age?</p>
                    </asp:Label>
                  <select class="twelve columns" name="ageRange">
                    <option value="-1">Please select an option</option>
                    <option value="0-17">0 - 17 years old</option>
                    <option value="18-34">18 - 34 years old</option>
                    <option value="35-54">35 - 54 years old</option>
                    <option value="55-74">55 - 74 years old</option>
                    <option value="75+">75+ years old</option>
                    <option value="0">Prefer not to answer</option>
                  </select>

                    <asp:Label runat="server" class="twelve columns" for="gender">
                        <p>What is your gender?</p>
                    </asp:Label>
                  <select class="twelve columns" name="gender">
                      <option value="-1">Please select an option</option>              
                      <option value="F">Female</option>
                      <option value="M">Male</option>
                      <option value="O">Other</option>
                      <option value="N">Prefer not to answer</option>
                  </select>
                </fieldset>
              </div>

              <div class="row enterForm">
		            <h3>Contact Me</h3>
                <p class="twelve columns">If you would like to talk to a food services staff member about your recent meal experience, please fill in the fields below.</p>


                <fieldset class="twelve columns">
                    <legend runat="server" class="dkBlTxt">
                        Preferred Contact Method:
                    </legend>
                  <!-- <select id="purpose"> -->
                  <select id="contactOps" name="contactOptions">
                    <option value="-1">Please select an option</option>
                    <option value="1">In person</option>
                    <option value="2">Over the phone</option>
                    <option value="3">I don't want to be contacted</option>
                  </select>

                  <section id="box1 box" class="person hidden">
                      <asp:Label runat="server" for="firstName">First name:</asp:Label>
                    <input type="text" name="firstName"/>

                      <asp:Label runat="server" for="lastName">Last name:</asp:Label>
                    <input type="text" name="lastName"/>

                      <asp:Label runat="server" for="unit">Room number:</asp:Label>
                    <input type="text" name="unit"/>

                      <asp:Label runat="server" for="bed">Bed number:</asp:Label>
                    <input type="text" name="bed"/>
                  </section>

                  <section id="box2 box" class="phone hidden">
                      <asp:Label runat="server" for="firstName">First name:</asp:Label>
                    <input type="text" name="firstName"/>

                      <asp:Label runat="server" for="lastName">Last name:</asp:Label>
                    <input type="text" name="lastName"/>

                      <asp:Label runat="server" for="phoneNumber">Phone number:</asp:Label>
                    <input type="text" name="phoneNumber"/>
                  </section>  
                       <input type="submit" name="submit" value="Submit Survey" class="button-primary  u-pull-right"/>       
                </fieldset>
              </div>
          
            </form>
          </section>
    <%--END OF CUSTOMER PROFILES SECTION--%>
<%--</main>--%>
    
    <%--JAVASCRIPT FOR DDL--%>
    <script type="text/javascript" src="js/hideDiv.js"></script>

    <%--OBJECT DATA CSOURCES--%>
    <asp:ObjectDataSource runat="server" ID="CareSiteODS" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCareSites" TypeName="MSS.System.BLL.CareSiteController"></asp:ObjectDataSource>
</asp:Content>

