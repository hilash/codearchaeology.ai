using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class Join
    {
        //SORT BY NAME, ARTIST, ALBUM, POP
        /// <summary>
        /// get a user ID, return a datatable with all the songs he likes
        /// </summary>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public static DataTable getSongsByUser(int User_ID)
        {
         
            string sqlString = "select tblSong.Song_Pic, tblSong.Song_ID, tblSong.Song_Name, tblArtist.Artist_Name, tblAlbum.Album_Name, ";
            sqlString += "tblSong.Artist_ID, tblSong.Album_ID, tblSong.Song_Genre, tblSong.Song_Length,";
            sqlString += "tblSong.Song_Lyrics, tblSong.Song_Clip, tblSong.Song_Pop ";
            sqlString += "from tblArtist inner join tblSong on tblArtist.Artist_ID = tblSong.Artist_ID ";
            sqlString += "inner join tblAlbum on tblAlbum.Album_ID = tblSong.Album_ID ";
            sqlString += "inner join tblUserSongsList on tblUserSongsList.Song_ID = tblSong.Song_ID ";
            sqlString += "where tblUserSongsList.User_ID =" + User_ID;

            return SqlHelper.GetDataTable(sqlString, "songsByUser");
        }

        public static DataTable getArtistsByUser(int User_ID)
        {
            string sqlString = "select *";
            sqlString += "from tblArtist inner join tblUserArtistsList on tblUserArtistsList.Artist_ID = tblArtist.Artist_ID ";
            sqlString += "where tblUserArtistsList.User_ID =" + User_ID;

            return SqlHelper.GetDataTable(sqlString, "songsByUser");
        }
    }
}
