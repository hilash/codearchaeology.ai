using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        // set the birthday drop-down-lists
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

    protected void ButtonSignUp_Click(object sender, EventArgs e)
    {
        Service s = new Service();
        if (s.IsEmailInDB(User_Mail.Text))
            Email_DB.Visible = true;
        else
        {
            MultiView1.ActiveViewIndex = 1;
            s.InsertUser(
                User_Fname.Text,
                User_Lname.Text,
                int.Parse(User_Year.SelectedValue.ToString()),
                int.Parse(User_Month.SelectedValue.ToString()),
                int.Parse(User_Day.SelectedValue.ToString()),
                User_Gender.Text,
                User_Pass.Text,
                User_Mail.Text
                );
            Response.AddHeader("REFRESH", "3;URL=Default.aspx");
            //Response.Redirect("Default.aspx?");
        }
    }
}
