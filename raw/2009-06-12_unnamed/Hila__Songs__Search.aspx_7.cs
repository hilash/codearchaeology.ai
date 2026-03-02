using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Q = TextBox1.Text;
        Service S = new Service();
        DataSet DS1 = S.SearchSong(Q);
        DataSet DS2 = S.SearchArtist(Q);
        DataSet DS3 = S.SearchAlbum(Q);
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

    }
}
