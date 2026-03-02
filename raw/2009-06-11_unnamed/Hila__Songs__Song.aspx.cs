using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Song : System.Web.UI.Page
{
    int ID1;
    SongPage SP;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ID1 = int.Parse(Request.QueryString["ID"]);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
        Service S = new Service();
        SP = S.SongData(ID1);
        //SP_ID.Text = SP.Song_ID.ToString();
        SP_Song_Name.Text = SP.Song_Name;
        SP_Artist_Name.Text = SP.Artist_Name;
        //SP_Artist_ID.Text = SP.Artist_ID.ToString();
        SP_Album_Name.Text = SP.Album_Name;
        //SP_Album_ID.Text = SP.Album_ID.ToString();
        SP_Lyrics.Text = SP.Song_Lyrics;
        movie.Attributes["value"] = @"http://www.youtube.com/v/" + SP.Song_Clip + "&hl=en&fs=1";
        embedSrc.Attributes["src"] = @"http://www.youtube.com/v/" + SP.Song_Clip + "&hl=en&fs=1";

        SP_Song_Genre.Text = SP.Song_Genre;
        SP_Pic.ImageUrl = SP.Song_Pic;
        SP_Lenght.Text = SP.Song_Length;
        SP_Song_Pop.Text = SP.Song_Pop.ToString();
    }
    protected void SP_Artist_Name_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Redirect("Artist.aspx?ID=" + SP.Artist_ID);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
    }
    protected void SP_Album_Name_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Album.aspx?ID=" + SP.Album_ID);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }

    }
    protected void SP_Song_Genre_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Genre.aspx?Genre=" + SP.Song_Genre);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
    }
}
