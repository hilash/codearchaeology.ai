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

public partial class select : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int ID = int.Parse(Request.QueryString["ID"]);
                tb_ID.Text = ID.ToString();
            }
            catch (Exception Ex)
            {
                Response.Write(Ex.ToString());
            }
        }

    }
    protected void getData_Click(object sender, EventArgs e)
    {
        string user_ID = tb_ID.Text;
        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\HilaGood\App_Data\Students.mdf;Integrated Security=True;User Instance=True";
        string sqlString = "select * from studentsTab where ID='" + user_ID + "'";
        //"select ID, firstname, lastname from studentsTab (where mclass='2') and (email='2')";
        //"select * from studentsTab where mclass='2'";  brings me all fields in Tab

        SqlConnection conn = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter(sqlString, conn);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        dt.TableName = "DTstudents";
        GridView1.DataSource = dt;
        GridView1.DataBind();
    
    }
}
