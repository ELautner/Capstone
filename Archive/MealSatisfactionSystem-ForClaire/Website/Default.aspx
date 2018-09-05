<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>OH HAI</h1>
        <p class="lead">So a thought, this page should have an automatic redirect to the Survey index page. We can do this later, but I thought I'd write it down while I remembered - Claire</p>
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="TestDDL" AppendDataBoundItems="true" DataTextField="unitname" DataValueField="unitid">
            <asp:ListItem Value="0">Select a Unit:</asp:ListItem>
        </asp:DropDownList>
        <asp:ObjectDataSource runat="server" ID="TestDDL" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllUnits" TypeName="MSS.System.BLL.TestController"></asp:ObjectDataSource>

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Reports\TestReport.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource Name="TestDataSet" DataSourceId="TestDataSource"></rsweb:ReportDataSource>
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource runat="server" ID="TestDataSource" ConnectionString='<%$ ConnectionStrings:MSSDB %>' ProviderName='<%$ ConnectionStrings:MSSDB.ProviderName %>' SelectCommand='SELECT * FROM "surveyanswer"'></asp:SqlDataSource>
    </div>

</asp:Content>
