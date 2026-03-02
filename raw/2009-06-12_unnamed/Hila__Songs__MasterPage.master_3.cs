using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class MasterPage : System.Web.UI.MasterPage
{
    static bool LogFlag = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        HttpCookie cookie = Request.Cookies["Details"];
        //Retrieving cookie
        if (cookie != null)
        {
            LinkLogOut.Visible = true;
            LinkLogIn.Visible = false;
            LinkSignUp.Visible = false;
            LinkUser.Text = cookie["Email"].ToString();
            LinkUser.Visible = true;
        }
        else
        {
            LinkLogOut.Visible = false;
            LinkLogIn.Visible = true;
            LinkSignUp.Visible = true;
            LinkUser.Visible = false;
        }
        MultiView1.Visible = false;

        if (IsPostBack && LogFlag)
        {
            MultiView1.Visible = true;
            MultiView1.SetActiveView(ViewLogIn);
        }

    }
    protected void LinkLogOut_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("Details");
        cookie.Expires = DateTime.Now.AddDays(-1);
        Response.Cookies.Add(cookie);
        Response.Redirect("Default.aspx?");

    }
    protected void LinkLogIn_Click(object sender, EventArgs e)
    {
        //LinkLogOut.Visible = true;
        //LinkLogIn.Visible = false;
        //LinkSignUp.Visible = false;

        // MultiLogIn.SetActiveView(View1);
        MultiView1.Visible = true;
        MultiView1.SetActiveView(ViewLogIn);
        TBmail.Text = "";
        TBpass.Text = "";
        msg.Text = "";



        //Response.Redirect("LogIn.aspx?");


    }

    protected void ButtonLogIn_Click(object sender, EventArgs e)
    {
        Service s = new Service();

        if (!s.ULogin(TBmail.Text, TBpass.Text))
        {
            msg.Text = "אימייל או סיסמא שגויים!";
            LogFlag = true;
        }
        else
        {
            msg.Text = "התחברת בהצלחה! מועבר..";

            //create cookie

            LogFlag = false;
            HttpCookie cookie = new HttpCookie("Details");
            cookie["Email"] = TBmail.Text;
            cookie["Pass"] = TBpass.Text;
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            cookie.Expires = dt.Add(ts);
            Response.Cookies.Add(cookie);
            Response.Redirect("User.aspx");

        }
    }
    protected void exitLogIn_Click(object sender, EventArgs e)
    {
        MultiView1.Visible = true;
        //MultiView1.SetActiveView(ViewLogIn);
        MultiView1.ActiveViewIndex = -1;
        LogFlag = false;
    }
    protected void Link_Search_Click(object sender, ImageClickEventArgs e)
    {
        if (TB_Search.Text == "חפש שיר, אמן, אלבום")
            Response.Redirect("Search.aspx?Q=" + "");
        else 
        Response.Redirect("Search.aspx?Q=" + TB_Search.Text);
    }
}
