<%@ Page Title="Trends" Language="C#" MasterPageFile="~/Management/ManagementMasterPage.master" AutoEventWireup="true" CodeFile="trends.aspx.cs" Inherits="Management_trends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">
    <h2>Trends</h2>
    	
    <asp:Label ID="ErrorMessage" runat="server" Text="" CssClass="text-danger bold" />
    <asp:Label ID="UserMessage" runat="server" Text="" CssClass="text-info bold" />

    <div class="alert alert-info alert-dismissible fade show" role="alert">
		<button type="button" class="close" data-dismiss="alert" aria-label="Close">
		<span aria-hidden="true">&times;</span>
		</button>
		<p><strong>I made dis 4 u TJ</strong></p>
        <p>It's just straight up copied and pasted from reporting... ¯\_(ツ)_/¯</p>
	</div>

    <div class="alert alert-warning alert-dismissible fade show" role="alert">
		<button type="button" class="close" data-dismiss="alert" aria-label="Close">
		<span aria-hidden="true">&times;</span>
		</button>
		<p><strong>Note:</strong> I've removed this part of the form because I'm still fixing the "master version" (which can be found <a href="contact_requests.aspx">here</a>)</p>
        <p>I'm hoping it will be here soon, but until then please refer to the Contact Requests page for an idea of what the upper half of this page will look like</p>
        <p>The table underneath this section is fair game though!</p>
	</div>

    <div class="form-group row">
		<label for="QuestionCheckBoxes" class="col-sm-2 col-form-label right-text">Questions</label>
		<div class="col-sm-10">
            <div class="row asp-checkboxes">
                <div class="col-sm-1">
                    <asp:CheckBox ID="Question1a" runat="server" Text="1a" Checked="true" value="1a" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-1">
                    <asp:CheckBox ID="Question1b" runat="server" Text="1b" Checked="true" value="1b" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-1">
                    <asp:CheckBox ID="Question1c" runat="server" Text="1c" Checked="true" value="1c" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-1">
                    <asp:CheckBox ID="Question1d" runat="server" Text="1d" Checked="true" value="1d" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-1">
                    <asp:CheckBox ID="Question1e" runat="server" Text="1e" Checked="true" value="1e" CssClass="checkbox-space" />
                </div>
            </div>
            <div class="row asp-checkboxes">
                <div class="col-sm-1">
                    <asp:CheckBox ID="Question2" runat="server" Text="2" Checked="true" value="2" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-1">
                    <asp:CheckBox ID="Question3" runat="server" Text="3" Checked="true" value="3" CssClass="checkbox-space" />
                </div>
                <div class="col-sm-1">
                    <asp:CheckBox ID="Question4" runat="server" Text="4" Checked="true" value="4" CssClass="checkbox-space" />
                </div>
            </div>
        </div>
    </div>

	<div class="form-group row justify-content-center">
		<a href="#" class="button button-secondary">Generate Report</a>
	</div>
		
	<div class="row justify-content-center">
		<img src="img/Q1AReport.PNG">
		<img hidden src="img/Q1ATimeReport.PNG">
	</div>
</asp:Content>

<%--  Old code: CCC: claire delete later

    NO ASPX CHECKBOXES:

    <div class="form-group row">
		<label class="col-sm-2 col-form-label right-text">Questions</label>
		
        <div class="col-sm-10 asp-checkboxes">
            <div class="row">
                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" ID="Question1a" value="1a" runat="server" checked="checked">
                    <label class="form-check-label" for="Question1a">1a</label>
                </div>

                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" ID="Question1b" value="1b" runat="server" checked="checked">
                    <label class="form-check-label" for="Question1b">1b</label>
                </div>

                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" ID="Question1c" value="1c" runat="server" checked="checked">
                    <label class="form-check-label" for="Question1c">1c</label>
                </div>

                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" ID="Question1d" value="1d" runat="server" checked="checked">
                    <label class="form-check-label" for="Question1d">1d</label>
                </div>

                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" ID="Question1e" value="1e" runat="server" checked="checked">
                    <label class="form-check-label" for="Question1e">1e</label>
                </div>
            </div>
            <div class="row">
                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" ID="Question2" value="2" runat="server" checked="checked">
                    <label class="form-check-label" for="Question2">2</label>
                </div>

                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" ID="Question3" value="3" runat="server" checked="checked">
                    <label class="form-check-label" for="Question3">3</label>
                </div>

                <div class="form-check form-check-inline">
                    <input class="form-check-input" type="checkbox" ID="Question4" value="4" runat="server" checked="checked">
                    <label class="form-check-label" for="Question4">4</label>
                </div>
            </div>
        </div>
	</div>
    
    SAMPLE DATA:

    <div class="col-sm-3">
		<select class="form-control">
			<option value="none">All Questions</option>
			<option value="1a">Question 1 A</option>
			<option value="1b">Question 1 B</option>
			<option value="1c">Question 1 C</option>
			<option value="1d">Question 1 D</option>
			<option value="1e">Question 1 E</option>
			<option value="2">Question 2</option>
			<option value="3">Question 3</option>
			<option value="4">Question 4</option>
		</select>
	</div>
    
     --%>