using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
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
        
    }
    protected void LinkLogOut_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("Details");
        cookie.Expires = DateTime.Now.AddDays(-1);
        Response.Cookies.Add(cookie);
        Response.Redirect("LogIn.aspx?");
        
    }
    protected void LinkLogIn_Click(object sender, EventArgs e)
    {
        //LinkLogOut.Visible = true;
        //LinkLogIn.Visible = false;
        //LinkSignUp.Visible = false;

       // MultiLogIn.SetActiveView(View1);

        Response.Redirect("LogIn.aspx?");


    }
}
