using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Artists : System.Web.UI.Page
{
    int User_ID;
    ArtistsPage AP;

    protected void Page_Load(object sender, EventArgs e)
    {
        Service s = new Service();

        HttpCookie cookie = Request.Cookies["Details"];

        AP = s.ArtistsData();
        Artists_Pop.DataSource = AP.pop;
        GridView_all.DataSource = AP.all;
        GridView_Random.DataSource = AP.random;

        if (cookie != null)
            User_ID = int.Parse(cookie["ID"]);
        else
            Artists_Pop.Columns.RemoveAt(2);

        Artists_Pop.DataBind();
        GridView_all.DataBind();
        GridView_Random.DataBind();
        
    }

    protected void Artists_Pop_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Like_Artist"))
        {
            Service s = new Service();
            int Artist_ID = int.Parse(((DataTable)Artists_Pop.DataSource).Rows[int.Parse(e.CommandArgument.ToString())]["Artist_ID"].ToString());
            s.InsertUserArtistsList(User_ID, Artist_ID);
            Response.Redirect("Artists.aspx");
        }
    }
}
