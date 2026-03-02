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
        try
        {
            ID1 = int.Parse(Request.QueryString["ID"]);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
        Service S = new Service();
        UP = S.UserData(ID1);
        User_Fname.Text = UP.User_Fname;
        User_Lname.Text = UP.User_Lname;



    }
}
