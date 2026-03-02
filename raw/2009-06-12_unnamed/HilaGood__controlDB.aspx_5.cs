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

public partial class controlDB : System.Web.UI.Page
{
    int ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ID = int.Parse(Request.QueryString["ID"]);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
    }
    protected void db_insert_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Redirect("Default.aspx?ID=" + ID);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
    }
    protected void db_update_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Update.aspx?ID=" + ID);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
    }
    protected void db_delete_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Delete.aspx?ID=" + ID);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
    }
    protected void db_select_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Select.aspx?ID=" + ID);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
    }
}
