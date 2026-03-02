using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost;

public partial class User : System.Web.UI.Page
{
    int User_ID;
    UserPage UP;

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["Details"];
        //Retrieving cookie
        if (cookie == null)
        {
            UserMsg.Text = "אתה לא מחובר לאתר אולי תירשם או תיכנס";
        }
        else
        {
            Service s = new Service();

            User_ID = int.Parse(cookie["ID"]);

            UP = s.UserData(User_ID);
            User_Songs.DataSource = UP.songs;
            User_Songs.DataBind();

            User_Artists.DataSource = UP.artists;
            User_Artists.DataBind();

            User_Fname.Text = UP.User_Fname;
            User_Lname.Text = UP.User_Lname;

           // Label1.Text = cookie["Email"].ToString();
           // Label1.Visible = s.IsAdmin(cookie["Email"].ToString());
            Panel_Admin.Visible = s.IsAdmin(User_ID);
        }
    }
    protected void User_Songs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Not_Like_Song"))
        {
            int Song_ID = int.Parse(UP.songs.Rows[int.Parse(e.CommandArgument.ToString())]["Song_ID"].ToString());
            Service s = new Service();
            s.DeleteUserSongsList(User_ID, Song_ID);
            Response.Redirect("User.aspx");
        }
            // DOESNT WORK!
        else if (e.CommandName.Equals("Not_Like_Artist"))
            {
                int Artist_ID = int.Parse(UP.artists.Rows[int.Parse(e.CommandArgument.ToString())]["Artist_ID"].ToString());
                Service s = new Service();
                s.DeleteUserArtistsList(User_ID, Artist_ID);
                Response.Redirect("User.aspx");
            }
    }
}
