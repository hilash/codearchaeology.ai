using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class LogIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Retrieving cookie
        HttpCookie cookie = Request.Cookies["Details"];
        if (cookie != null)
            msg.Text = "אתה כבר רשום באתר!";
        else
        {
            //Log In
            msg.Text = "הירשם לאתר!";
            Email.Visible = true;
            Pass.Visible = true;
            TBmail.Visible = true;
            Tbpass.Visible = true;
        } 

    }
    protected void Button_Click(object sender, EventArgs e)
    {
        if (U)
        {
            Response.Write("the input is wrong");
        }
        else
        {
                Response.Redirect("controlDB.aspx?ID=" + ID);

        }
        
    }
}
