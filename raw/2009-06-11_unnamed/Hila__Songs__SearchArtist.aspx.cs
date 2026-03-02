using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class SearchArtist : System.Web.UI.Page
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
        }

        Service S = new Service();

        DataSet DS2 = S.SearchArtist(Q);

        tbl_artist_search.DataSource = DS2.Tables[0];

        tbl_artist_search.DataBind();
    }
}
