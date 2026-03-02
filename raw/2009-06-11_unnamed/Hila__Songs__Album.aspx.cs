using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Album : System.Web.UI.Page
{
    int ID1;
    AlbumPage AP;

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
        AP = S.AlbumData(ID1);
        //AP_Artist_ID.Text = AP.Artist_ID.ToString();
        AP_Artist_Name.Text = AP.Artist_Name;
        AP_Album_Name.Text = AP.Album_Name;
        AP_Album_Pic.ImageUrl = AP.Album_Pic.ToString();
        AP_Album_Year.Text = AP.Album_Year.ToString();
        //AP_Album_ID.Text = AP.Album_ID.ToString();

        AP_Songs.DataSource = AP.songs;

        AP_Songs.DataBind();
    }
    protected void AP_Artist_Name_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Artist.aspx?ID=" + AP.Artist_ID);
        }
        catch (Exception Ex)
        {
            Response.Write(Ex.ToString());
        }
    }
}
