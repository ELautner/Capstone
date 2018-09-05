<%@ Page Title="Manage Units" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="manage_units.aspx.cs" Inherits="Management_manage_units" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <h2>Manage Units</h2>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

            <%-- Care site DDL section --%>
            <div class="form-group row bottom-space">
                <label for="CareSiteDDL" class="col-sm-3 col-form-label right-text">Select a Care Site</label>
                <div class="col-sm-7">
                    <asp:DropDownList ID="CareSiteDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="CareSiteODS" DataTextField="caresitename" DataValueField="caresiteid" OnSelectedIndexChanged="RetrieveUnitList_Select" AutoPostBack="true">
                        <asp:ListItem Value="0">Please Select a Care Site</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <%-- Add units section --%>
            <div class="row bottom-space" id="AddUnitRow" runat="server">
                <div class="card border-success col-sm-12">
                    <div class="card-body">
                        <h3>Add New Unit</h3>
                        <p class="hint">To add a new unit, enter the unit's name and click "Add Unit".</p>
                        <div class="form-group row">
                            <label for="newUnitName" class="col-sm-2 col-form-label right-text">Unit Name</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="AddUnitNameTB" runat="server" CssClass="form-control" placeholder="Enter unit name here"></asp:TextBox>
                            </div>
                            <asp:LinkButton ID="AddUnitButton" runat="server" CssClass="button button-success man-unit-btn" OnClick="AddUnitButton_Click">Add Unit</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Modify unit section --%>
            <div class="row bottom-space" id="ModifyUnitRow" runat="server">
                <div class="col-sm-12 card clearfix">
                    <div class="card-body">
                        <h3>Modify Unit</h3>
                        <p class="hint">To update or deactivate an existing unit, select the unit's name from the drop-down list below.</p>
                        <div class="form-group row">
                            <label for="unit" class="col-sm-2 col-form-label right-text">Select a Unit</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="UnitDDL" runat="server" AppendDataBoundItems="false" CssClass="form-control" DataTextField="unitname" DataValueField="unitid" OnSelectedIndexChanged="RetrieveUnit_Select" AutoPostBack="true">
                                    <asp:ListItem Value="0">Select...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Update and deactivate section --%>
            <div class="row bottom-space" id="UpdateDeactivateRow" runat="server">
                <div class="card border-primary col-sm-6 update-unit-space">
                    <div class="card-body">
                        <h3>Update Unit</h3>
                        <div class="form-group row">
                            <div class="col-sm-7">
                                <asp:TextBox ID="UpdateUnitNameTB" runat="server" CssClass="form-control" placeholder="Select a unit to update"></asp:TextBox>
                            </div>
                            <asp:LinkButton ID="UpdateUnitButton" runat="server" CssClass="button button-primary man-unit-btn" OnClick="UpdateUnitButton_Click">Update Unit</asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div class="card border-danger col-sm-5 deactivate-unit">
                    <div class="card-body">
                        <h3>Deactivate Unit</h3>
                        <div class="form-group row">
                            <asp:Label ID="DeactivateUnitNameLabel" runat="server" CssClass="col-form-label col-sm-7 bold"></asp:Label>
                            <asp:LinkButton ID="DeactivateUnitButton" runat="server" CssClass="button button-danger man-unit-btn" OnClick="DeactivateUnitButton_Click" OnClientClick="return confirm('Are you sure you want to deactivate the selected unit?');">Deactivate Unit</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- Object Data Sources --%>
    <asp:ObjectDataSource runat="server" ID="CareSiteODS" OldValuesParameterFormatString="original_{0}" SelectMethod="GetActiveCareSites" TypeName="MSS.System.BLL.CareSiteController"></asp:ObjectDataSource>
</asp:Content>
