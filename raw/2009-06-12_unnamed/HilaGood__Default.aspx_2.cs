using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
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

        for (int i = 1900; i <= 2008; i++)
        {
            ListItem li = new ListItem();
            li.Value = i.ToString();
            year.Items.Add(li);
        }

    }
    protected void send_Click(object sender, EventArgs e)
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\HilaGood\App_Data\Students.mdf;Integrated Security=True;User Instance=True";
        
        string Fname = first_name.Text;
        
        string Lname = last_name.Text;
        string Pass = password.Text;
        string Email = email.Text;
        string Gender = gender.SelectedValue;
        int Bday = int.Parse(day.SelectedValue);
        int Mday = int.Parse(month.SelectedValue);
        int Yday = int.Parse(year.SelectedValue);
        string Mclass = mclass.Text;
      
       string SqlString = "insert into StudentsTab(firstname,lastname,password,email,gender,day,month,year,mclass) values('" + Fname + "','" + Lname + "','" + Pass + "','" + Email + "','" + Gender + "','" + Bday + "','" + Mday + "','" + Yday + "','" + Mclass + "')";
        //string SqlString = "insert into StudentsTab(firstname) values('" + Fname + "')";
        SqlConnection myConn = new SqlConnection(connectionString);
        SqlCommand myCmd = new SqlCommand(SqlString, myConn);
        myConn.Open();
        myCmd.ExecuteNonQuery();
        myConn.Close();
    }
}
