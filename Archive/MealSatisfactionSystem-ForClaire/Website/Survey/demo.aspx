<%@ Page Title="" Language="C#" MasterPageFile="~/Survey/SurveyMasterPage.master" AutoEventWireup="true" CodeFile="demo.aspx.cs" Inherits="Survey_demo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <main class="row ltBlue">
      <section class="twelve columns welcomeBlurb">
        <h3>Customer Profile Questions</h3>
        <p>
          The questions in this section give us a better understanding of who is completing our survey. All demographic questions are <span class="boldRed">optional</span>. 
        </p>
      </section>

        <section class="twelve columns">
        <form class="u-full-width consSpace" method="post" action="thankYou.aspx">
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
                    What is your gender?
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
                <option value="3" selected="selected">I don't want to be contacted</option>
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
</main>
</asp:Content>

