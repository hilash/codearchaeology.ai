using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    class Song
    {
        public static string BL_InsertSong(
            int Song_ID,
            string Song_Name,
            int Album_ID,
            int Artist_ID,
            string Song_Lyrics,
            string Song_Clip,
            DateTime Song_Length,
            string Song_Genre,
            string Song_Pic
            )
        {
            string ErrorString="";
            if (DAL.tblSong.IsSongInDB(Song_ID) == true)
                ErrorString += "Song_ID:    this song ID is already in the data base\n";
            else if ( (Song_Name == "") || (Song_Name == null) )
                ErrorString += "Song_Name:  you haven't inserted a song name\n";
            else if (DAL.tblAlbum.IsAlbumInDB(Album_ID) == true)
                ErrorString += "Album_ID:   this album ID is already in the data base\n";
            else if (DAL.tblArtist.IsArtistInDB(Artist_ID) == true)
                ErrorString += "Artist_ID:   this artist ID is already in the data base\n";
            //if (IsWebAdress() == false)
            //    ErrorString += "Artist_ID:   this artist ID is already in the data base\n";
            else
            {
                DAL.tblSong.InsertSong(Song_ID, Song_Name, Album_ID, Artist_ID, Song_Lyrics, Song_Clip, Song_Length, Song_Genre, Song_Pic);
                ErrorString += "OK\n";
            }
            return ErrorString;
        }
        public static void Main()
        {
            BL_InsertSong(4, "The KKK Took My Baby Away", 1, 1, "ss", "as", new DateTime(), "Rock", "aa");
        }

    }
}
