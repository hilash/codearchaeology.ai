using System;
using System.Collections;
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

public partial class SignIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void logIn_Click(object sender, EventArgs e)
    {
        int ID = int.Parse(user_ID.Text);
        string pass = user_pass.Text;

        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\HilaGood\App_Data\Students.mdf;Integrated Security=True;User Instance=True";
        string sqlString = "select * from studentsTab where ID="+ID;
        /*"select ID, firstname, lastname from studentsTab (where mclass='2') and (email='2')";
        "select * from studentsTab where mclass='2'";  brings me all fields in Tab
        string sqlString = "select * from studentsTab where ID='" + uID + "'";
         */
        SqlConnection conn = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter(sqlString, conn);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        dt.TableName = "DTstudents";
        if (dt.Rows.Count == 0)
        {
            Response.Write("the input is wrong");
        }
        else
        {
            if (dt.Rows[0]["password"].ToString().Equals(pass))
                //Response.Write("success!!!");
                Response.Redirect("controlDB.aspx?ID="+ID);
            }
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
    }
}
