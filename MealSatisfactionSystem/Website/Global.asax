<%@ Application Language="C#" %>
<%@ Import Namespace="Website" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<%@ Import Namespace="MSS.System.BLL.Security" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        RoleManager roleManager = new RoleManager();
        roleManager.AddDefaultRoles();

        UserManager userManager = new UserManager();
        userManager.AddAdministratorAccount();

        
    }

</script>
