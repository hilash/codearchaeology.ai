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

    }
    protected void LogInButton_Click(object sender, EventArgs e)
    {
        Service s = new Service();
        
        if (!s.UserLogIn(TBmail.Text,TBpass.Text))
        {
            msg.Text = "אימייל או סיסמא שגויים!";
        }
        else
        {
            msg.Text = "התחברת בהצלחה! מועבר..";

            //create cookie

            HttpCookie cookie = new HttpCookie("Details");
            cookie["Email"] = TBmail.Text;
            cookie["Pass"] = TBpass.Text;
            cookie["ID"] = s.GetUserIDByEmail(TBmail.Text).ToString();
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            cookie.Expires = dt.Add(ts);
            Response.Cookies.Add(cookie);
            Response.Redirect("User.aspx");
        }
    }
}
