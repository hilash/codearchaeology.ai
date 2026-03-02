using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Songs : System.Web.UI.Page
{
    int User_ID;
    SongsPage SP;

    protected void Page_Load(object sender, EventArgs e)
    {
        Service s = new Service();

        SP = s.SongsData();

        HttpCookie cookie = Request.Cookies["Details"];

        Songs_Pop.DataSource = SP.pop;
        Songs_new.DataSource = SP.news;
        Songs_random.DataSource = SP.random;


        if (cookie != null)
            User_ID = int.Parse(cookie["ID"]);
        else
        {
            Songs_Pop.Columns.RemoveAt(5);
            Songs_new.Columns.RemoveAt(3);
            Songs_random.Columns.RemoveAt(3);
        }

        Songs_Pop.DataBind();
        Songs_new.DataBind();
        Songs_random.DataBind();
    }

    protected void Songs_Pop_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Like_Song"))
        {
            Service s = new Service();
            int Song_ID = int.Parse(((DataTable)Songs_Pop.DataSource).Rows[int.Parse(e.CommandArgument.ToString())]["Song_ID"].ToString());
            s.InsertUserSongsList(User_ID, Song_ID);
            Response.Redirect("Songs.aspx");
        }
    }
}
