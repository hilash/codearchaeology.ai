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

public partial class update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Ben\form\App_Data\Databen.mdf;Integrated Security=True;User Instance=True";

        int ID_b = int.Parse(tb_ID.Text);
        string Email = tb_email.Text;
        string Password = tb_pass.Text;
        string MClass = tb_class.Text;

        string SqlString = "update Students set email=('" + Email + "'), password=('" + Password + "'), mother_class=('" + MClass + "') where ID='" + ID_b + "'";
        
        SqlConnection myConn = new SqlConnection(connectionString);
        SqlCommand myCmd = new SqlCommand(SqlString, myConn);
        myConn.Open();
        myCmd.ExecuteNonQuery();
        myConn.Close();
    }
}