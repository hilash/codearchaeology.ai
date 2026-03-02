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
        
        if (!IsPostBack)
        {
            for (int i = 1; i <= 31; i++)
            {
                ListItem li = new ListItem();
                li.Value = i.ToString();
                User_Day.Items.Add(li);
            }

            for (int i = 1; i <= 12; i++)
            {
                ListItem li = new ListItem();
                li.Value = i.ToString();
                User_Month.Items.Add(li);
            }

            for (int i = 1900; i <= 2008; i++)
            {
                ListItem li = new ListItem();
                li.Value = i.ToString();
                User_Year.Items.Add(li);
            }
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Service s = new Service();
        DateTime dt = new DateTime(int.Parse(User_Year.Text),int.Parse(User_Month.Text),int.Parse(User_Day.Text));
        s.InsertUser(UserID, User_Fname.Text.ToString(), User_Lname.Text.ToString(), dt, "F", User_Pass.Text.ToString(), User_Mail.Text.ToString());
        UserID++;
    }
}
