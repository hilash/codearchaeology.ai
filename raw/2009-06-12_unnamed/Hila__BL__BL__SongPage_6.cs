using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    /// <summary>
    /// In songs page, all the details about the song
    /// </summary>
    public class SongPage
    {

        public int Song_ID;
        public int Song_Pop;
        public string Song_Name;
        public string Album_Name;
        public string Artist_Name;
        public string Song_Lyrics;
        public string Song_Clip;
        public string Song_Genre;
        public string Song_Pic;
        public DateTime Song_Length;
        
        public DataSet getSongData(int Song_ID1)
        {
            DataSet DS = new DataSet();
            DataTable DT = DAL.Join.getSongsByUser(Song_ID1);

            Song_ID = Song_ID1;
            //Song_Pop = int.Parse(DS.Tables[0].Rows[0]["Song_Pop"].ToString());
            //Song_Pop = int.Parse(DT.Rows[1]["Song_Pop"].ToString());


            return DS;
        }
    }
}
