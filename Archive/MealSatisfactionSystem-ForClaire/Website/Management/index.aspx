<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Management_index" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In To the MSS</title>

    <!-- Basic Page Needs –––––––––––––––––––––––––––––––––––––––––––––––––– -->
	<meta charset="utf-8" />
	<meta name="description" content="" />
	<meta name="author" content="" />

	<!-- Boostrap ––––––––––––––––––––––––––––––––––––––– Bootstrap -->
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
	
	<!-- CSS –––––––––––––––––––––––––––––––––––––––––––––––––– -->
	<link rel="stylesheet" type="text/css" href="css/styles.css" />

	<!-- Favicon –––––––––––––––––––––––––––––––––––––––––––––––––– -->
	<link rel="icon" type="image/png" href="img/favicon.png" />

</head>
<body>
    <header>
	    <nav>
		    <ul>
			    <li><img src="img/chLogo_transparent_icon.png" alt="Covenant Health Logo" />Covenant Health Meal Satisfaction System</li>
		    </ul>
	    </nav>
    </header>
	<main runat="server">
		<h2>Survey Management Login</h2>
        	
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

		<div class="centre">
			<form class="login-form" runat="server">
				<div class="form-group row">
					<label for="username" class="col-sm-4 col-form-label right-text">Username</label>
					<div class="col-sm-8">
                        <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control" placeholder="Enter username here" />
					</div>
				</div>
				<div class="form-group row">
					<label for="pass" class="col-sm-4 col-form-label right-text">Password</label>
					<div class="col-sm-8">
                        <%-- TODO: make the password dots or something --%>
                        <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="form-control" placeholder="Enter password here" />
					</div>
				</div>
				<div class="form-group row justify-content-center">
                    <asp:LinkButton ID="LogInButton" runat="server" CssClass="btn btn-info col-sm-3" >Log In</asp:LinkButton>
				</div>
                <%-- TODO: remove this link --%>
                <a href="dashboard.aspx">Click here to get to dashboard, will be deleted later</a>
			</form>
		</div>
	</main>
    <footer>
	    <p>&copy; Covenant Health 2018</p>
    </footer>
</body>
</html>
