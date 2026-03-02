using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Search : System.Web.UI.Page
{
    DataSet DS3;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string Q = TextBox1.Text;
        Service S = new Service();
        DataSet DS1 = S.SearchSong(Q);
        DataSet DS2 = S.SearchArtist(Q);
        //DataSet
        DS3 = S.SearchAlbum(Q);
        GridView1.DataSource = DS1.Tables[0];
        GridView2.DataSource = DS2.Tables[0];
        GridView3.DataSource = DS3.Tables[0];
        GridView1.DataBind();
        GridView2.DataBind();
        GridView3.DataBind();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    /*
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int ID = int.Parse(TextBox2.Text);
        
        try
        {
            Response.Redirect("Song.aspx?ID=" + ID);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }

    }*/

    /*
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("btnAction"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            string details = "";
            for (int i=0; i<DS3.Tables[0].Columns.Count; i++)
            {
                details+=DS3.Tables[0].Rows[index][i].ToString() + " ";
            }

            Response.Write(details);
        }

    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }*/
    protected void GridView3_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
}
