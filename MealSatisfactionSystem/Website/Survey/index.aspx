<%@ Page Title="Enter Meal Survey" Language="C#" MasterPageFile="~/Survey/SurveyMasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Survey_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <section class="twelve columns enterForm">
        <h3>Welcome!</h3>
        <p>
          We would love to hear about your recent meal services experience.
        </p>
		<br />
        <p>
		  To begin the survey, please enter the access code found on your meal tray ticket or order chit.        
		</p>
      </section>

      <section class="twelve columns consSpace enterForm">
          <asp:Label ID="ErrorMSG" class="errMSG" runat="server" Text=""></asp:Label>
        <h3>Enter Access Code:</h3>
        <form id="enterSurvey" class="u-full-width" method="post" runat="server">
                   
            <asp:Label runat="server" Text="" AssociatedControlID="AccessText"></asp:Label>
            <asp:TextBox runat="server" ID="AccessText" name="accessCode" placeholder="Enter access code here" ValidateRequestMode="Disabled"></asp:TextBox>

            <asp:Button runat="server" Text="Enter Survey" CommandName="submit" class="button-primary u-pull-right" OnClick="Submit_Click" />
        </form>
</section>
</asp:Content>

