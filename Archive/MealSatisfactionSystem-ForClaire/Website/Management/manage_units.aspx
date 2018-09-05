<%@ Page Title="Manage Units" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="manage_units.aspx.cs" Inherits="Management_manage_units" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Manage Unit Lists</h2>
	
    <asp:Label ID="ErrorMessage" runat="server" Text="" CssClass="text-danger bold" />
    <asp:Label ID="UserMessage" runat="server" Text="" CssClass="text-info bold" />

	<div class="form-group row">
		<label for="CareSiteDDL" class="col-sm-4 col-form-label right-text">Select a Care Site</label>
		<div class="col-sm-4">
            <asp:DropDownList ID="CareSiteDDL" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="CareSiteODS" DataTextField="caresitename" DataValueField="caresiteid" OnSelectedIndexChanged="RetrieveUnitList_Select" AutoPostBack="true">
                <asp:ListItem Value="0">Select...</asp:ListItem>
            </asp:DropDownList>
		</div>
        <!-- Can delete button and class now -->
       <asp:LinkButton ID="RetrieveUnitListButton" runat="server" CssClass="button button-info col-sm-2" OnClick="RetrieveUnitListButton_Click">Refresh Unit List</asp:LinkButton> 
	</div>

    <div class="form-group row">
         <!-- Can delete hidden and class use now -->
        <asp:HiddenField runat="server" ID="HiddenCareSiteID" Value="" />
		<label for="unit" class="col-sm-4 col-form-label right-text">Select a Unit</label>
		<div class="col-sm-4">
            <asp:DropDownList ID="UnitDDL" runat="server" AppendDataBoundItems="false" CssClass="form-control" DataTextField="unitname" DataValueField="unitid" OnSelectedIndexChanged="RetrieveUnit_Select" AutoPostBack="true">
                <asp:ListItem Value="0">Select...</asp:ListItem>
            </asp:DropDownList>
		</div>
        <!-- Can delete button and class now -->
        <asp:LinkButton ID="SelectUnitButton" runat="server" CssClass="button button-info col-sm-2" OnClick="SelectUnitButton_Click">Select Unit</asp:LinkButton>
	</div>

    <div class="card border-success">
		<div class="card-body">
			<h3>Add New Unit</h3>
            <p class="text-muted">Select a care site from the drop down list above</p>
			<div class="form-group row">
				<label for="newUnitName" class="col-sm-2 col-form-label right-text">Unit Name</label>
				<div class="col-sm-4">
                    <asp:TextBox ID="AddUnitNameTB" runat="server" CssClass="form-control"></asp:TextBox>
				</div>
                <asp:LinkButton ID="AddUnitButton" runat="server" CssClass="button button-success col-sm-3" OnClick="AddUnitButton_Click">Add Unit</asp:LinkButton>
			</div>
		</div>
	</div>

    <br />
	<div class="card border-primary">
		<div class="card-body">
			<h3>Update Unit</h3>
			<p class="text-muted">Select a unit from the drop down list above</p>
			<div class="form-group row">
				<label for="unitName" class="col-sm-2 col-form-label right-text">Unit Name</label>
				<div class="col-sm-4">
                    <!-- Can delete hidden and class use now -->
                    <asp:HiddenField runat="server" ID="UnitHiddenField" Value="" />
                    <asp:TextBox ID="UpdateUnitNameTB" runat="server" CssClass="form-control"></asp:TextBox>
				</div>
                <asp:LinkButton ID="UpdateUnitButton" runat="server" CssClass="button button-primary col-sm-3" OnClick="UpdateUnitButton_Click">Update Unit</asp:LinkButton>
			</div>
		</div>
	</div>

	<br />
	<div class="card border-danger">
		<div class="card-body">
			<h3>Deactivate Unit</h3>
			<p class="text-muted">Select a unit from the drop down list above</p>
			<div class="form-group row">
				<label class="col-sm-2 col-form-label right-text">Unit Name</label>
                <asp:Label ID="DeactivateUnitNameLabel" runat="server" CssClass="col-form-label col-sm-4 bold"></asp:Label>
                <asp:LinkButton ID="DeactivateUnitButton" runat="server" CssClass="button button-danger col-sm-3" OnClick="DeactivateUnitButton_Click">Deactivate Unit</asp:LinkButton>
			</div>
		</div>
	</div>

    <%-- Object Data Sources --%>
    <asp:ObjectDataSource runat="server" ID="CareSiteODS" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCareSites" TypeName="MSS.System.BLL.CareSiteController"></asp:ObjectDataSource>
</asp:Content>



     <%-- Okay so this works, but selecting from the repeater and populating the stuff on the side is getting complicated
         Please leave this code here in case someone wants to spend the time getting it working   
         <div class="col-sm-3">

            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="GetCareSiteUnitsODS" ItemType="MSS.Data.unit">
                
                <HeaderTemplate>
                    <table class="table table-striped scroll">
				        <thead>
					        <tr>
						        <th scope="col">Unit Name</th>
					        </tr>
				        </thead>
                        <tbody> 
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>
						<td><a href="#"><%# Item.unitname %></a></td>
					</tr>
                </ItemTemplate>

                <FooterTemplate>
                            </tbody>
			        </table>
                </FooterTemplate>

            </asp:Repeater>

		</div>
		<div class="col-sm-9">

            <div class="card border-success">
				<div class="card-body">
					<h3>Add New Unit</h3>
					<div class="form-group row">
						<label for="newUnitName" class="col-sm-2 col-form-label right-text">Unit Name</label>
						<div class="col-sm-4">
							<input id="newUnitName" name="newUnitName" class="form-control" type="text">
						</div>
						<a href="#" class="button button-success col-sm-3">Add Unit</a>
					</div>
				</div>
			</div>

            <br />
			<div class="card border-primary">
				<div class="card-body">
					<h3>Update Unit</h3>
					<p class="text-muted">Select a unit from the table on the left</p>
					<div class="form-group row">
						<label for="unitName" class="col-sm-2 col-form-label right-text">Unit Name</label>
						<div class="col-sm-4">
							<input id="unitName" name="newUnitName" class="form-control" type="text">
						</div>
						<a href="#" class="button button-primary col-sm-3">Update Unit</a>	
					</div>
				</div>
			</div>

			<br />
			<div class="card border-danger">
				<div class="card-body">
					<h3>Deactivate Unit</h3>
					<p class="text-muted">Select a unit from the table on the left</p>
					<div class="form-group row">
						<label class="col-sm-2 col-form-label right-text">Unit Name</label>
						<label name="deleteUnitName" class="col-form-label col-sm-4 bold">7 East</label>
						<a href="#" class="button button-danger col-sm-3">Deactivate Unit</a>	
					</div>
				</div>
			</div>
		</div>--%>