using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using BL;
using DAL;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /*
   


        [WebMethod]
        public int GetSongNextID()
        {
            return DAL.SqlHelper.GetNextID(DAL.tblSong.GetSongsDT());
        }

        [WebMethod]
        public int GetArtistNextID()
        {
            return DAL.SqlHelper.GetNextID(DAL.tblArtist.GetArtistsDT());
        }

        [WebMethod]
        public int GetAlbumNextID()
        {
            return DAL.SqlHelper.GetNextID(DAL.tblAlbum.GetAlbumsDT());
        }

        [WebMethod]
        public int GetUserNextID()
        {
            return DAL.SqlHelper.GetNextID(DAL.tblUser.GetUsersID_DT());
        }

        [WebMethod]
        public DataTable SongNameLike(string input)
        {
            return DAL.tblSong.SongNameLike(input);
        }



        [WebMethod]
        public DataTable AlbumNameLike(string input)
        {
            return DAL.tblAlbum.AlbumNameLike(input);
        }





       [WebMethod]
       public int DeleteUser(int User_ID)
       {
           return DAL.tblUser.DeleteUser(User_ID);
       }


            [WebMethod]
        public DataTable SongPopTableByUser(int User_ID)
        {
            return BL.SongsPage.SongPopTableWithUserLikes(User_ID);
        }

     * */



    /*********************************** SIGN UP PAGE ************************************************************************************/
    [WebMethod]
    public int InsertUser(
        string User_Fname,
        string User_Lname,
        int Year, int Month, int Day,
        string User_Gender,
        string User_Pass,
        string User_Email
        )
    {
        DateTime dt = new DateTime(Year, Month, Day);
        return DAL.tblUser.InsertUser(DAL.tblUser.GetUserNextID(), User_Fname, User_Lname, dt.ToShortDateString(), User_Gender, User_Pass, User_Email);
    }

    [WebMethod]
    public bool IsEmailInDB(string User_Email)
    {
        return DAL.tblUser.IsEmailInDB(User_Email);
    }


    /*********************************** LOG IN PAGE ************************************************************************************/
    [WebMethod]
    public bool UserLogIn(string mail, string pass)
    {
        return DAL.tblUser.UserLogIn(mail, pass);
    }

    /*********************************** USER PAGE ************************************************************************************/

    [WebMethod]
    public int GetUserIDByEmail(string Email)
    {
        return int.Parse(DAL.tblUser.GetInfo("User_ID", "User_Email", Email).ToString());
    }

    [WebMethod]
    public UserPage UserData(int ID)
    {
        UserPage UP = new UserPage();
        UP.getUserData(ID);
        return UP;
    }

    /*********************************** ADMIN PAGE ************************************************************************************/

    [WebMethod]
    public bool IsAdmin(int ID)
    {
        return DAL.tblAdmin.IsAdmin(ID);
    }

    /*********************************** ADMIN USERS PAGE ************************************************************************************/

    [WebMethod]
    public DataTable GetUsersDT()
    {
        return DAL.tblUser.GetUsersDT();
    }

    [WebMethod]
    public int UpdateDataTable(DataTable dt)
    {
        return DAL.SqlHelper.UpdateDataTable(dt);
    }

    /*********************************** ADMIN REQUESTS PAGE ************************************************************************************/

    [WebMethod]
    public AdminRequests GetRequests()
    {
        AdminRequests AR = new AdminRequests();
        AR.getRequestsTables();
        return AR;
    }

    /*********************************** SEARCH PAGES ************************************************************************************/

    [WebMethod]
    public DataSet SearchSong(string Q)
    {
        return BL.SearchPage.SearchSong(Q);
    }

    [WebMethod]
    public DataSet SearchArtist(string Q)
    {
        return BL.SearchPage.SearchArtist(Q);
    }

    [WebMethod]
    public DataSet SearchAlbum(string Q)
    {
        return BL.SearchPage.SearchAlbum(Q);
    }

    /*********************************** SONG PAGE ************************************************************************************/

    [WebMethod]
    public SongPage SongData(int ID)
    {
        SongPage SP = new SongPage();
        SP.getSongData(ID);
        return SP;
    }

    /*********************************** ARTIST PAGE ************************************************************************************/

    [WebMethod]
    public ArtistPage ArtistData(int ID)
    {
        ArtistPage AP = new ArtistPage();
        AP.getArtistData(ID);
        return AP;
    }

    /*********************************** ALBUM PAGE ************************************************************************************/

    [WebMethod]
    public AlbumPage AlbumData(int ID)
    {
        AlbumPage AP = new AlbumPage();
        AP.getAlbumData(ID);
        return AP;
    }

    /*********************************** GENRE PAGE ************************************************************************************/

    [WebMethod]
    public DataTable SongsByGenre(string Genre)
    {
        return DAL.tblSong.GetSongsOfGenre(Genre);
    }

    /*********************************** ALBUMS PAGE ************************************************************************************/

    [WebMethod]
    public AlbumsPage AlbumsData()
    {
        AlbumsPage AP = new AlbumsPage();
        AP.getData();
        return AP;
    }

    /*********************************** ARTISTS PAGE ************************************************************************************/

    [WebMethod]
    public ArtistsPage ArtistsData()
    {
        ArtistsPage AP = new ArtistsPage();
        AP.getData();
        return AP;
    }

    [WebMethod]
    public int InsertUserArtistsList(int User_ID, int Artist_ID)
    {
        return DAL.tblUserArtistsList.InsertUserArtistsList(User_ID, Artist_ID);
    }

    [WebMethod]
    public int DeleteUserArtistsList(int User_ID, int Artist_ID)
    {
        return DAL.tblUserArtistsList.DeleteUserArtistsList(User_ID, Artist_ID);
    }

    /*********************************** SONGS PAGE ************************************************************************************/


    [WebMethod]
    public SongsPage SongsData()
    {
        SongsPage SP = new SongsPage();
        SP.getData();
        return SP;
    }

    [WebMethod]
    public int InsertUserSongsList(int User_ID, int Song_ID)
    {
        return DAL.tblUserSongsList.InsertUserSongsList(User_ID, Song_ID);
    }

    [WebMethod]
    public int DeleteUserSongsList(int User_ID, int Song_ID)
    {
        return DAL.tblUserSongsList.DeleteUserSongsList(User_ID, Song_ID);
    }

    /*********************************** REQUEST SONG PAGE ************************************************************************************/

    [WebMethod]
    public int InsertRequestSong(
         string Song_Name,
         int Album_ID,
         string Album_Name,
         int Artist_ID,
         string Artist_Name,
         string Song_Lyrics,
         string Song_Clip,
         int Min, int Sec,
         string Song_Genre,
         string Song_Pic,
         string Song_Rtype
         )
    {
        DateTime dt = new DateTime();
        dt.AddMinutes(Min);
        dt.AddSeconds(Sec);
        return DAL.tblRequestSong.InsertRequestSong(DAL.tblRequestSong.GetRequestSongNextID(), Song_Name, Album_ID, Album_Name, Artist_ID,
            Artist_Name, Song_Lyrics, Song_Clip, dt.ToString(), Song_Genre, Song_Pic, Song_Rtype);
    }

    [WebMethod]
    public DataTable ArtistNameLike(string input)
    {
        return DAL.tblArtist.ArtistNameLike(input);
    }

    [WebMethod]
    public DataTable GetAlbumsOfArtist(int Artist_ID)
    {
        return DAL.tblAlbum.GetAlbumsOfArtist(Artist_ID);
    }

    /*********************************** REQUEST ARTIST PAGE ************************************************************************************/

    [WebMethod]
    public int InsertRequestArtist(
         string Artist_Type,
         string Artist_Name,
         string Artist_Bio,
         string Artist_Pic,
         string Artist_Rtype
         )
    {
        return DAL.tblRequestArtist.InsertRequestArtist(DAL.tblRequestArtist.GetRequestArtistNextID(), Artist_Type, Artist_Name, Artist_Bio, Artist_Pic, Artist_Rtype);
    }

    [WebMethod]
    public bool IsArtistNameInDB(string Artist_Name)
    {
        return DAL.tblArtist.IsArtistNameInDB(Artist_Name);
    }

    /*********************************** REQUEST ALBUM PAGE ************************************************************************************/


    [WebMethod]
    public int InsertRequestAlbum(
        string Album_Name,
        int Artist_ID,
        string Artist_Name, 
        string Album_Pic,
        int Album_Year,
        string Album_Rtype
        )
    {
        return DAL.tblRequestAlbum.InsertRequestAlbum(DAL.tblRequestAlbum.GetRequestAlbumNextID(), Album_Name, Artist_ID, Artist_Name, Album_Pic, Album_Year, Album_Rtype);
    }




}
