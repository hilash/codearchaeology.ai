using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class SearchPage
    {
        public static DataSet SearchSong(string Q)
        {
            DataSet DS = new DataSet();
            DataTable DT = DAL.tblSong.SearchSongName(Q);
            DS.Tables.Add(DT);
            return DS;
        }
    }
}
