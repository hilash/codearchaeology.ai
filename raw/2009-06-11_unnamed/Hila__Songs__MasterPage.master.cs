using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
         HttpCookie cookie = Request.Cookies["Details"];

        //Retrieving cookie
        if (cookie != null)
        {
            LinkUser.Visible = true;
            LinkLogOut.Visible = true;
            LinkLogIn.Visible = false;
            LinkSignUp.Visible = false;
            LinkUser.Text = cookie["Email"].ToString();  
        }
        else
        {
            LinkUser.Visible = false;
            LinkLogOut.Visible = false;
            LinkLogIn.Visible = true;
            LinkSignUp.Visible = true;      
        }
    }
    protected void LinkLogOut_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("Details");
        cookie.Expires = DateTime.Now.AddDays(-1);
        Response.Cookies.Add(cookie);
        Response.Redirect("Default.aspx?");
    }
    protected void Link_Search_Click(object sender, ImageClickEventArgs e)
    {
        string Query = DDLsearch.SelectedValue;
        string Text = TB_Search.Text;

        if (string.Equals(Query, "song"))
            Response.Redirect("SearchSong.aspx?Q=" + Text);

        else if (string.Equals(Query, "artist"))
            Response.Redirect("SearchArtist.aspx?Q=" + Text);

        else if (string.Equals(Query, "album"))
            Response.Redirect("SearchAlbum.aspx?Q=" + Text);

        else if (string.Equals(Query, "lyrics"))
            Response.Redirect("SearchLyrics.aspx?Q=" + Text);
    }
}
