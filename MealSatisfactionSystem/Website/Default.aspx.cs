using System;
using System.Web.UI;

/* 
CREATED:    C. Stanhope         MAR 29 2018

Default
This page should never be accessed, but if a user navigates to it they will be redirected to the survey home page.

FIELDS:
None

METHODS:
protected void Page_Load(object sender, EventArgs e) - redirects user to the survey home page
*/
public partial class _Default : Page
{

    /* 
    CREATED:    C. Stanhope         MAR 29 2018

    Page_Load()
    Run on page load and is used to redirect to the survey home page.

    PARAMETERS:     
    object sender - references the object that raised the Page_Load event
    EventArgs e - optional class that may be passed that inherits from EventArgs (usually empty)

    RETURNS: 
    void

    ODEV METHOD CALLS: 
    None
    */
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("Survey/index.aspx");
    }
}