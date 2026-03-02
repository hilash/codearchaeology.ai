using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Artist : System.Web.UI.Page
{
    int ID1;
    ArtistPage AP;

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
        AP = S.ArtistData(ID1);
        // AP_Artist_ID.Text = AP.Artist_ID.ToString();
        AP_Artist_Name.Text = AP.Artist_Name;
        AP_Artist_Type.Text = AP.Artist_Type;
        AP_Artist_Bio.Text = AP.Artist_Bio;
        AP_Artist_Pop.Text = AP.Artist_Pop.ToString();
        AP_Artist_Pic.ImageUrl = AP.Artist_Pic.ToString();

        AP_Albums.DataSource = AP.albums;
        AP_Songs.DataSource = AP.songs;

        AP_Albums.DataBind();
        AP_Songs.DataBind();

    }
}
