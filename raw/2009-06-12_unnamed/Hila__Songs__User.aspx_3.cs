using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class User : System.Web.UI.Page
{
    int ID1;
    UserPage UP;

    protected void Page_Load(object sender, EventArgs e)
    {
        
            HttpCookie cookie = Request.Cookies["Details"];
            //Retrieving cookie
            if (cookie == null)
                Response.Redirect("LogIn.aspx");
            else
            {
                Service s = new Service();
                ID1 = s.GetUserIDByEmail(cookie["Email"].ToString());
                UP = s.UserData(ID1);
                User_Songs.DataSource = UP.songs;
                User_Songs.DataBind();
            }
        
    }
}
