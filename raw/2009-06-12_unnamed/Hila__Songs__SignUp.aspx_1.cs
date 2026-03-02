using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class SignUp : System.Web.UI.Page
{
    private static int UserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Service s = new Service();
       // s.InsertUser(UserID,

    }
}
