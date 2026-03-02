using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Search : System.Web.UI.Page
{
    string Q;
    DataSet DS3;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Q = Request.QueryString["Q"];
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }

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
}
