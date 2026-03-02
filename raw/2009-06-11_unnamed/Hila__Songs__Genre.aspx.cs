using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class Genre : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Genre;
        DataTable dt; 
        try
        {
            Genre = Request.QueryString["Genre"];
            Service s = new Service();
            SongsOfGenre.DataSource = dt = s.SongsByGenre(Genre);
            if (dt.Rows.Count < 1)
                MultiView1.ActiveViewIndex = 0;
            else
            {
                MultiView1.ActiveViewIndex = 1;
                Genre_Name.Text = Genre;
                SongsOfGenre.DataBind();
            }

        }
        catch (Exception Ex)
        {
            MultiView1.ActiveViewIndex = 0;
            Response.Write(Ex.ToString());
        }
    }
}
