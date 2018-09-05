<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Management_index" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In to the MSS</title>

    <!-- Basic Page Needs –––––––––––––––––––––––––––––––––––––––––––––––––– -->
    <meta charset="utf-8" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <!-- Bootstrap ––––––––––––––––––––––––––––––––––––––– Bootstrap -->
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap.min.css" />

    <!-- CSS –––––––––––––––––––––––––––––––––––––––––––––––––– -->
    <link rel="stylesheet" type="text/css" href="css/styles.css" />

    <!-- Favicon –––––––––––––––––––––––––––––––––––––––––––––––––– -->
    <link rel="icon" type="image/png" href="img/favicon.ico" />

</head>
<body>
    <header>
        <nav>
            <ul>
                <li>
                    <img src="img/logo_small.png" alt="Covenant Health Logo" />Covenant Health Meal Satisfaction System
                </li>
            </ul>
        </nav>
    </header>
    <main runat="server">
        <h2>Survey Management Login</h2>

        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

        <div class="centre">
            <form class="login-form" runat="server" defaultbutton="LogInButton">
                <div class="form-group row">
                    <label for="username" class="col-sm-4 col-form-label right-text">Username</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control" placeholder="Enter username here" MaxLength="100" />
                    </div>
                </div>
                <div class="form-group row bottom-space">
                    <label for="pass" class="col-sm-4 col-form-label right-text">Password</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password here" MaxLength="100" />
                    </div>
                </div>
                <div class="form-group row justify-content-center">
                    <asp:LinkButton ID="LogInButton" runat="server" CssClass="button button-info col-sm-3" OnClick="LogIn">Log In</asp:LinkButton>
                </div>
            </form>
        </div>
        
    </main>
    <footer>
        <p>&copy; Covenant Health 2018</p>
    </footer>
</body>
</html>
