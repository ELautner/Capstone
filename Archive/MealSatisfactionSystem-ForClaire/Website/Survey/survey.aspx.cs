using MSS.Data.DTOs;
using MSS.System.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Survey_survey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CareSiteAccessController csaController = new CareSiteAccessController();
        CareSiteController csController = new CareSiteController();
        
        //string codeWord = Request.QueryString["accessCode"];

        //try
        //{
        //    var careSiteId = csaController.GetCareSiteID(codeWord);
        //    string careSiteName = csController.GetCareSiteName(careSiteId);

        //    SiteNameLabel.Text = careSiteName;
        //}
        //catch (Exception ex)
        //{
        //    Server.Transfer("index.aspx?errorMSG=That code is not active, please check your meal or tray ticket", true);
        //}

        //TODO: Populate the Units DDL from the database


    }


    //TODO: ON_SUBMIT - Check that any field has any kind of text in it
    //TODO: insert into the database

}