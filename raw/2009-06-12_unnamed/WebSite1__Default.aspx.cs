using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using System.Xml.Linq;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 1; i <= 31; i++)
        {
            ListItem li = new ListItem();
            li.Value = i.ToString();
            day.Items.Add(li);

        }

        for (int i = 1; i <= 12; i++)
        {
            ListItem li = new ListItem();
            li.Value = i.ToString();
            month.Items.Add(li);

        }
        for (int i = 1970; i <= 2008; i++)
        {
            ListItem li = new ListItem();
            li.Value = i.ToString();
            year.Items.Add(li);
        }
    }
    protected void send_Click(object sender, EventArgs e)
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Hila\Documents\Visual Studio 2005\WebSites\WebSite1\App_Data\Students.mdf;Integrated Security=True;User Instance=True";
        string Fname = fname.Text;
        string Lname = lname.Text;
        string Pass1 = pass.Text;
        string Mail = mail.Text;
        string Gender = gender.SelectedValue;
        int Day = int.Parse(day.SelectedValue);
        int Month1 = int.Parse(month.SelectedValue);
        int Year1 = int.Parse(year.SelectedValue);
        string MClass1 = mclass.Text;
        // string SqlString = "insert into StudentsTab(firstname, lastname, password, email, gender, day, month, year, class) values('" + Fname + "','" + Lname + "','" + Pass1 + "','" + Mail + "','" + Gender + "','" + Day + "','" + Month + "','" + Year + "','" + Mclass + "')";
        string SqlString = "insert into StudentsTab(firstname, lastname, password, email, gender, day, month, year, class) values('" + Fname + "','" + Lname + "','" + Pass1 + "','" + Mail + "','" + Gender + "','" + Day + "','" + Month1 + "','" + Year1 + "','" + MClass1 + "')";
        SqlConnection myConn = new SqlConnection(connectionString);
        SqlCommand myCmd = new SqlCommand(SqlString, myConn);
        myConn.Open();
        myCmd.ExecuteNonQuery();
        myConn.Close();
    }
}
