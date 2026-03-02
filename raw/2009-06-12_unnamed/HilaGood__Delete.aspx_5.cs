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

public partial class Delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void tb_delete_Click(object sender, EventArgs e)
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\HilaGood\App_Data\Students.mdf;Integrated Security=True;User Instance=True";

        try
        {
            int ID_del = int.Parse(delete_ID.Text);

            string SqlString = "delete from StudentsTab where ID='" + ID_del + "'";

            SqlConnection myConn = new SqlConnection(connectionString);
            SqlCommand myCmd = new SqlCommand(SqlString, myConn);
            myConn.Open();
            myCmd.ExecuteNonQuery();
            myConn.Close();
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
    }
}
