using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Albums : System.Web.UI.Page
{
    AlbumsPage AP;

    protected void Page_Load(object sender, EventArgs e)
    {
        Service S = new Service();
        AP = S.AlbumsData();

       GridView_20New.DataSource = AP.NewestAlbums;
       GridView_Last.DataSource = AP.LastAlbumAdded;
       GridView_Random.DataSource = AP.RandomAlbums;
       GridView_Top20.DataSource = AP.TOPAlbums;

        GridView_20New.DataBind();
       GridView_Last.DataBind();
       GridView_Random.DataBind();
       GridView_Top20.DataBind();


    }
}
