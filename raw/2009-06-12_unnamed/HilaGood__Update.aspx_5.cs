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

public partial class Update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int ID = int.Parse(Request.QueryString["ID"]);
                tb_ID.Text = ID.ToString();

                // now we got the ID. get the ID table (row) and fill the other text boxes

                string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\HilaGood\App_Data\Students.mdf;Integrated Security=True;User Instance=True";
                string sqlString = "select * from studentsTab where ID=" + ID;
                /*"select ID, firstname, lastname from studentsTab (where mclass='2') and (email='2')";
                "select * from studentsTab where mclass='2'";  brings me all fields in Tab
                string sqlString = "select * from studentsTab where ID='" + uID + "'";
                 */
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlString, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dt.TableName = "DTstudents";
                if (dt.Rows.Count > 0)
                {
                    tb_pass.Text = dt.Rows[0]["password"].ToString();
                    tb_email.Text = dt.Rows[0]["email"].ToString();
                    tb_class.Text = dt.Rows[0]["mclass"].ToString();

                }
            }
            catch (Exception Ex)
            {
                Response.Write(Ex.ToString());
            }
        }
    }
    protected void send_Click(object sender, EventArgs e)
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\HilaGood\App_Data\Students.mdf;Integrated Security=True;User Instance=True";

        int ID_b = int.Parse(tb_ID.Text);
        string Email = tb_email.Text;
        string Password = tb_pass.Text;
        string MClass = tb_class.Text;

        string SqlString = "update StudentsTab set email=('" + Email + "'), password=('" + Password + "'), mclass=('" + MClass + "') where ID='" + ID_b + "'";

        SqlConnection myConn = new SqlConnection(connectionString);
        SqlCommand myCmd = new SqlCommand(SqlString, myConn);
        myConn.Open();
        myCmd.ExecuteNonQuery();
        myConn.Close();
    }
}
