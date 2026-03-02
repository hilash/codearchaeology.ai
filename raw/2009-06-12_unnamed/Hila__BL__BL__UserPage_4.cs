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
    ///
    public class UserPage
    {
        public int User_ID;
        public string User_Fname;
        public string User_Lname;
        public DateTime User_Bday;
        public string User_Gender;
        public string User_Pass;
        public string User_Email;
        public DataTable songs;
        public DataTable artists;

        public void getUserData(int User_ID1)
        {
            DataTable dt = DAL.tblUser.GetUserData(User_ID1);

            User_ID = User_ID1;
            User_Fname = dt.Rows[0]["User_Fname"].ToString();
            User_Lname = dt.Rows[0]["User_Lname"].ToString();

            //try { User_Bday = dt.Rows[0]["User_Bday"]; }
            User_Gender = dt.Rows[0]["User_Gender"].ToString();
            User_Pass = dt.Rows[0]["User_Pass"].ToString();
            User_Email = dt.Rows[0]["User_Email"].ToString();


            songs = DAL.Join.getSongsByUser(User_ID);
            artists = DAL.Join.getArtistsByUser(User_ID);

        }
    }
}
