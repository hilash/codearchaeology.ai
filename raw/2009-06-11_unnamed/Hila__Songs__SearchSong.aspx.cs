using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class SearchSong : System.Web.UI.Page
{
    string Q;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Q = Request.QueryString["Q"];
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
            Q = "";
        }

        Service S = new Service();
        DataSet DS1 = S.SearchSong(Q);
        //foreach (DataRow dr in DS1.Tables[0].Rows)
        //{ 
        //    DateTime shortDate = DateTime.Parse(dr["Song_Length"].ToString());
        //    dr["Song_Length"] = shortDate.Day +"/"+shortDate.Month+"/"+shortDate.Year;
        //    Response.Write(dr["Song_Length"].ToString());
        //}
        //DataSet
        tbl_song_search.DataSource = DS1.Tables[0];
        tbl_song_search.DataBind();

    }
}
