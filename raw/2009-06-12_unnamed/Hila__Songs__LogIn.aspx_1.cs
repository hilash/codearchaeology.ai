using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class LogIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["Details"];
        //Retrieving cookie
        if (cookie != null)
        {
            msg.Text = "אתה כבר מחובר לאתר!";
            Button.Visible = false;
        }
        else
        {
            //Log In

            msg.Text = "התחבר לאתר!";
            Email.Visible = true;
            Pass.Visible = true;
            TBmail.Visible = true;
            Tbpass.Visible = true;
            logGood.Visible = false;
            Button.Visible = true;

        }

    }
    protected void Button_Click(object sender, EventArgs e)
    {
        Service s = new Service();

        if (!s.ULogin(TBmail.Text, Tbpass.Text))
        {
            VAL.Visible = true;
            logGood.Visible = false;
        }
        else
        {
            VAL.Visible = false;
            logGood.Visible = true;

            //create cookie

            HttpCookie cookie = new HttpCookie("Details");
            cookie["Email"] = TBmail.Text;
            cookie["Pass"] = Tbpass.Text;
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            cookie.Expires = dt.Add(ts);
            Response.Cookies.Add(cookie);
            Response.Redirect("User.aspx");
        }

    }
}
