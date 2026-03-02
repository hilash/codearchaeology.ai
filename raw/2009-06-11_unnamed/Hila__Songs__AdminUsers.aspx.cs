using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class AdminUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Service s = new Service();
            HttpCookie cookie = Request.Cookies["Details"];
            Panel1.Visible = false;
            //Retrieving cookie
            if (cookie == null)
            {
                UserMsg.Text = "אתה לא מחובר לאתר אולי תירשם או תיכנס";
            }
            else if (!s.IsAdmin ( int.Parse(cookie["ID"].ToString())) )
            {
                UserMsg.Text = "אינך מנהל";
            }
            else
            {
                UserMsg.Text = ".פה תוכל לערוך ולמחוק את פרטי המשתמשים באתר";
                Panel1.Visible = true;

               // DataTable dt = s.GetUsersDT();
               // GridView_Users.DataSource = dt;
              //  GridView_Users.DataBind();
              //  Session["data"] = dt;
            }
        }
    }
    protected void GridView_Users_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // gets the id of the user to be delted
        Service s = new Service();
        DataTable dt = (DataTable)Session["data"];
        int i = e.RowIndex;
 
        dt.Rows[i].Delete();
        //dt.Rows.RemoveAt(i);

        Response.Write(i);
        Response.Write("the num of rows in dt " + dt.Rows.Count);


        //GridView_Users.DataSource = dt;
        //GridView_Users.DataBind();

        Session["data"] = dt;
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        //Service s = new Service();
       // DataTable dt = (DataTable)Session["data"];
       // dt.TableName = "tblUser";
       // s.UpdateDataTable(dt);
    }
    //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    // gets the id of the user to be deleted
    //    //Service s = new Service();
    //    int i = e.RowIndex;
    //    DataTable dt = (DataTable)GridView1.DataSource;
    //    GridView2.DataSource = dt;
    //    GridView2.DataBind();
    //    //int User_ID = int.Parse(dt.Rows[i]["User_ID"].ToString());
    //    //int User_ID = int.Parse(((DataTable)GridView1.DataSource).Rows[e.RowIndex]["User_ID"].ToString());
    //    //UserMsg.Text = User_ID.ToString();
    //    UserMsg.Text = i.ToString();
    //}
}
