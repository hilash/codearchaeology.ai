using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class tblSong : SqlHelper
    {
        public static string tblName
        {
            get
            {
                return "tblSong";
            }
        }

        public static int InsertSong(
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
            return FullConnected("INSERT INTO tblSong(Song_ID, Song_Name, Album_ID, Artist_ID, Song_Lyrics, Song_Clip, Song_Length, Song_Genre,Song_Pic,Song_Pop) values('" + Song_ID + "','" + Song_Name + "','" + Album_ID + "','" + Artist_ID + "','" + Song_Lyrics + "','" + Song_Clip + "','" + Song_Length + "','" + Song_Genre + "','" + Song_Pic + "',0)");
        }

        public static int UpdateSong(
            int Song_ID,
            string Song_Name,
            int Album_ID,
            int Artist_ID,
            string Song_Lyrics,
            string Song_Clip,
            DateTime Song_Length, /*Date is wrong*/
            string Song_Genre,
            string Song_Pic
            )
        {
            DateTime Zero = new DateTime(1, 1,1);
            bool flag = false;
            string SqlString = "UPDATE tblSong ";

            if (!((Song_Name == "") || (Song_Name == null)))
            {
                SqlString += "SET ";
                SqlString += "Song_Name='" + Song_Name + "'";
                flag = true;
            }
            if (!(Album_ID == 0))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Album_ID='" + Album_ID + "'";
                flag = true;
            }
            
            if (!(Artist_ID == 0))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Artist_ID='" + Artist_ID + "'";
                flag = true;
            }
            
            if (!((Song_Lyrics == "") || (Song_Lyrics == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Lyrics='" + Song_Lyrics + "'";
                flag = true;
            }
            
            if (!((Song_Clip == "") || (Song_Clip == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Clip='" + Song_Clip + "'";
                flag = true;
            }
           
            /*
            //if (!(Song_Length.Equals(Zero)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Length='" + Song_Length + "'";
                flag = true;
            }*/
            
            if (!((Song_Genre == "") || (Song_Genre == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Genre='" + Song_Genre + "'";
                flag = true;
            }
            if (!((Song_Pic == "") || (Song_Pic == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Pic='" + Song_Pic + "'";
                flag = true;
            }
            if (flag)
            {
                SqlString += " WHERE Song_ID='" + Song_ID + "'";
                return FullConnected(SqlString);
            }
            else return -1;
        }

        public static int DeleteSong(int Song_ID)
        {
            return FullConnected("DELETE FROM tblSong WHERE Song_ID='" + Song_ID + "'");
        }

        public static string GetInfo(string output_type, string where_type, string where_what)
        {
            string SS = "SELECT * FROM tblSong WHERE ";
            SS += where_type;
            SS += "='" + where_what + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0][output_type].ToString();
        }

        public static void AddSongPOP(int Song_ID, int add)
        {
            int newPop;
            int Song_Pop = int.Parse(GetInfo("Song_Pop", "Song_ID", Song_ID.ToString()));
            if  ((newPop = (Song_Pop + add)) <= 0)
                newPop = 0;
            FullConnected("UPDATE tblSong SET Song_Pop='" + newPop + "'  WHERE Song_ID='" + Song_ID + "'");
        }

        public static int SongID(string Song_Name)
        {
            string SS = "SELECT Song_Id FROM tblSong WHERE Song_Name='" + Song_Name + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return int.Parse(dt.Rows[0]["Song_Id"].ToString());
        }

        public static int PopSong(int number)
        {
            string SS = "SELECT (Song_ID) FROM tblSong ORDER BY Song_Pop DESC ";
            DataTable dt = GetDataTable(SS, "tblSong");
            //PrintDataTable(dt);
            if (number > dt.Rows.Count)
                return -1;
            else return int.Parse(dt.Rows[number]["Song_Id"].ToString());
        } 
    }
}
