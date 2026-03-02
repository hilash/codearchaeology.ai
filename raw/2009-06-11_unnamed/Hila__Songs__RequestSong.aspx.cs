using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class RequestSong : System.Web.UI.Page
{
    //DOESNT WORK!!!!!!!!!!!!!!!!!!!!!!
    bool ArtistNotInDB, AlbumNotInDB; // 0 if Is in DB, else 1

    protected void Page_Load(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        Validator_Song.Visible = (TB_Song_Name.Text == "") ? true : false;
        Validator_Artist.Visible = ((LB_Artists.SelectedItem == null) && (TB_Other_Artist.Text == "")) ? true : false;

        if (!IsPostBack)
        {
            ArtistNotInDB = AlbumNotInDB = true;
           
            // Show list of artists
            Service s = new Service();
            DataTable dt = s.ArtistNameLike("");

            foreach (DataRow dr in dt.Rows)
            {
                ListItem li = new ListItem();
                li.Value = dr[0].ToString();
                li.Text = dr[1].ToString();
                LB_Artists.Items.Add(li);
            }

            // Show list of minutes & secondes
            for (int i = 0; i < 60; i++)
            {
                ListItem li = new ListItem();
                li.Value = i.ToString();
                DDL_Seconds.Items.Add(li);
            }

            for (int i = 0; i < 60; i++)
            {
                ListItem li = new ListItem();
                li.Value = i.ToString();
                DDL_Minutes.Items.Add(li);
            }
        }
    }
    protected void Send_Click(object sender, ImageClickEventArgs e)
    {

        if ((TB_Song_Name.Text != "") && ((LB_Artists.SelectedItem != null) || (TB_Other_Artist.Text != "")))
        {
            Service s = new Service();
            string Artist_Name = "";
            int Artist_ID = 0;
            string Album_Name = "";
            int Album_ID = 0;
            string Genre;
            int Min = (DDL_Minutes.SelectedItem==null)? 0 : int.Parse(DDL_Minutes.SelectedItem.Text);
            int Sec = (DDL_Seconds.SelectedItem==null)? 0 : int.Parse(DDL_Seconds.SelectedItem.Text);

            //if (TB_Other_Artist.Text != "")
            //    Artist_Name = TB_Other_Artist.Text;
            //else Artist_ID = int.Parse(LB_Artists.SelectedValue);

            if (ArtistNotInDB == true)
            {
                Artist_Name = TB_Other_Artist.Text;
                Artist_ID = 0;
                Album_Name = TB_Other_Album.Text;
                Album_ID = 0;
            }
            else
            {
                Artist_Name = LB_Artists.SelectedItem.Text;
                Artist_ID = int.Parse(LB_Artists.SelectedItem.Value.ToString());
                if (AlbumNotInDB == true)
                {
                    Album_Name = TB_Other_Album.Text;
                    Album_ID = 0;
                }
                else
                {
                    Album_Name = LB_Albums.SelectedItem.Text;
                    Album_ID = int.Parse(LB_Albums.SelectedItem.Value.ToString());
                }
            }

            //if (TB_Other_Album.Text != "")
            //    Album_Name = TB_Other_Album.Text;
            //else Album_ID = int.Parse(LB_Albums.SelectedValue);

            if (TB_Other_Genre.Text != "")
                Genre = TB_Other_Genre.Text;
            else if (DDL_Genre.SelectedItem != null)
                Genre = DDL_Genre.SelectedItem.Text;
            else Genre = "";

            s.InsertRequestSong(TB_Song_Name.Text, Album_ID,
                Album_Name, Artist_ID, Artist_Name, TB_Lyrics.Text, TB_Clip.Text,
                Min, Sec,
                Genre, TB_Pic.Text, "Add");

            MultiView1.ActiveViewIndex = 0;
            Response.AddHeader("REFRESH", "3;URL=Default.aspx");
        }
    }
    protected void TB_Artist_Search_TextChanged(object sender, EventArgs e)
    {
        // If the user search artist - show results in DB
        LB_Artists.Items.Clear();
        string str = TB_Artist_Search.Text;

        Service s = new Service();

        DataTable dt = s.ArtistNameLike(str);

        foreach (DataRow dr in dt.Rows)
        {
            ListItem li = new ListItem();
            li.Value = dr[0].ToString();
            li.Text = dr[1].ToString();
            LB_Artists.Items.Add(li);
        }
        LB_Artists.DataBind();
        LB_Albums.Items.Clear();
    }
    protected void LB_Artists_SelectedIndexChanged(object sender, EventArgs e)
    {
        // if the user chose an artist from DB, show artist's albums
        ArtistNotInDB = false;

        int Artist_ID = int.Parse(LB_Artists.SelectedValue.ToString());

        Service s = new Service();
        DataTable dt = s.GetAlbumsOfArtist(Artist_ID);

        LB_Albums.Items.Clear();

        foreach (DataRow dr in dt.Rows)
        {
            ListItem li = new ListItem();
            li.Value = dr[0].ToString();
            li.Text = dr[1].ToString();
            LB_Albums.Items.Add(li);
        }
        LB_Albums.DataBind();
    }

    // If the user type name of artist not in the database - clear the albums List Box
    protected void TB_Other_Artist_TextChanged(object sender, EventArgs e)
    {
        LB_Albums.Items.Clear();
        LB_Albums.DataBind();

        ArtistNotInDB = true;
    }
    protected void LB_Albums_SelectedIndexChanged(object sender, EventArgs e)
    {
        AlbumNotInDB = false;
    }
    protected void TB_Other_Album_TextChanged(object sender, EventArgs e)
    {
        AlbumNotInDB = true;
    }
}
