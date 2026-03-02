using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using BL;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool Login(int username,string pass)
    {
        return DAL.tblUser.UserLogIn(username,pass);
    }

    [WebMethod]
    public  DataSet SearchSong(string Q)
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

    [WebMethod]
    public SongPage SongData(int ID)
    {
        SongPage SP = new SongPage();
        SP.getSongData(ID);
        return SP;   
    }

    [WebMethod]
    public ArtistPage ArtistData(int ID)
    {
        ArtistPage AP = new ArtistPage();
        AP.getArtistData(ID);
        return AP;
    }

    [WebMethod]
    public AlbumPage AlbumData(int ID)
    {
        AlbumPage AP = new AlbumPage();
        AP.getAlbumData(ID);
        return AP;
    }

    [WebMethod]
    public UserPage UserData(int ID)
    {
        UserPage UP = new UserPage();
        UP.getUserData(ID);
        return UP;
    }

    
}
