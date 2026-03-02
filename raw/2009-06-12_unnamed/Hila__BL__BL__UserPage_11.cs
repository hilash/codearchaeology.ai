using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    /// <summary>
    /// In the user page:
    /// a data table with the songs he likes,
    /// a data table with the artist he likes
    /// </summary>
    class UserPage
    {
        public static DataSet getSongsByUser(int User_ID)
        {
            DataSet DS = new DataSet();
            DS.Tables.Add(DAL.Join.getSongsByUser(User_ID));

            return DS;
        }

        public static DataSet getArtistsByUser(int User_ID)
        {
            DataSet DS = new DataSet();
            DS.Tables.Add(DAL.Join.getArtistsByUser(User_ID));

            return DS;
        }


    }
}
