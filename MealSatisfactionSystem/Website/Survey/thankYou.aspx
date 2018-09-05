<%@ Page Title="Thank You" Language="C#" MasterPageFile="~/Survey/SurveyMasterPage.master" AutoEventWireup="true" CodeFile="thankYou.aspx.cs" Inherits="Survey_thankYou" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="0"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
   <section class="twelve columns welcomeBlurb">
        <h3>Thank You!</h3>
        <p>Thank you for taking the time to complete our meal satisfaction survey. Your feedback helps us to continually improve our meal services.</p>
      </section>
      <section class="twelve columns goodbye">
        <h4 class="twelve columns">Have a great day!</h4>
    </section>


  <%--TESTING AREA--%>
    <section class="testArea">
      
                <asp:Label ID="TestLabel" runat="server" CssClass="errMSG" Text=""></asp:Label>
    </section>

</asp:Content>

