using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class AdminRequests : System.Web.UI.Page
{
    localhost.AdminRequests AR;

    protected void Page_Load(object sender, EventArgs e)
    { 
        Service S = new Service();

        if (!IsPostBack)
        {
            AR = S.GetRequests();

            NUM_Artist.Text = AR.artists.Rows.Count.ToString();
            NUM_Album.Text = AR.albums.Rows.Count.ToString();
            NUM_Song.Text = AR.songs.Rows.Count.ToString();

            //GridView_Artist.DataSource = AR.artists;
            //GridView_Album.DataSource = AR.albums;
            //GridView_Song.DataSource = AR.songs;

            //GridView_Artist.DataBind();
            //GridView_Album.DataBind();
            //GridView_Song.DataBind();

            if (!string.Equals(NUM_Artist.Text, "0"))
                MultiView1.ActiveViewIndex = 0;
            else if (!string.Equals(NUM_Album.Text, "0"))
                MultiView1.ActiveViewIndex = 1;
            else if (!string.Equals(NUM_Song.Text, "0"))
                MultiView1.ActiveViewIndex = 2;
            else MultiView1.ActiveViewIndex = 3;
        }
    }
    protected void GridView_Artist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView_Artist_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}
