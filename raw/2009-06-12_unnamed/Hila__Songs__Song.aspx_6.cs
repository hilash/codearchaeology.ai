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
        SP = new SongPage();
        SP = S.SongData(ID1);
        SP_ID.Text = SP.Song_ID.ToString();
        SP_Album_Name.Text = SP.Album_Name;
    }
}
