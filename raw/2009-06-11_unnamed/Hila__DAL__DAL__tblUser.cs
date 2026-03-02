using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class tblUser : SqlHelper
    {
        public static string tblName
        {
            get
            {
                return "tblUser";
            }
        }
        /// <summary>
        /// Insert a new user to the database.
        /// returns the numbers of rows affected - if 1, then the insert went well.
        /// if -100 - thats mean there was a problem.
        /// </summary>
        /// <param name="User_ID"></param>
        /// <param name="User_Fname"></param>
        /// <param name="User_Lname"></param>
        /// <param name="User_Bday"></param>
        /// <param name="User_Gender"></param>
        /// <param name="User_Pass"></param>
        /// <param name="User_Email"></param>
        /// <returns></returns>
        public static int InsertUser(
            int User_ID,
            string User_Fname,
            string User_Lname,
            string User_Bday,
            string User_Gender,
            string User_Pass,
            string User_Email
            )
        {
            string SqlString="INSERT INTO tblUser(User_ID, User_Fname, User_Lname, User_Bday, User_Gender, User_Pass, User_Email) ";
            SqlString += "VALUES ('" + User_ID + "','" + User_Fname + "','" + User_Lname + "','" + User_Bday + "','" + User_Gender + "','" + User_Pass + "','" + User_Email + "')";
            return FullConnected(SqlString);
        }

        /// <summary>
        /// returns a data table with all the Id's of the users
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUsersID_DT()
        {
            return GetDataTable("SELECT User_ID FROM tblUser", "tblUser");
        }

        public static bool IsEmailInDB(string User_Email)
        {
            return (FullConnected("UPDATE tblUser SET User_Email='" + User_Email + "' WHERE User_Email='" + User_Email + "'") > 0) ? true : false;
        }

        public static bool UserLogIn(string User_Email, string User_Pass)
        {
            string SqlString = "SELECT (User_Pass) FROM tblUser WHERE User_Email='" + User_Email + "'";
            DataTable dt = GetDataTable(SqlString, tblName);
            if (dt.Rows.Count == 0)
                return false;
            return (string.Equals(dt.Rows[0]["User_Pass"].ToString(),User_Pass)) ? true : false;
        }

        public static DataTable GetUserData(int ID)
        {
            string SS = "SELECT * FROM tblUser WHERE User_ID="+ID;
            return GetDataTable(SS, tblName);
        }

        public static string GetInfo(string output_type, string where_type, string where_what)
        {
            string SS = "SELECT * FROM tblUser WHERE ";
            SS += where_type;
            SS += "='" + where_what + "'";
            //Console.WriteLine(SS);
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0][output_type].ToString();
        }

        public static int GetUserNextID()
        {
            DataTable dt = GetDataTable("SELECT MAX(User_ID) FROM tblUser", "tblmaxuser");
            return int.Parse(dt.Rows[0][0].ToString())+1;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////






        public static int UpdateUser(
            int User_ID,
            string User_Fname,
            string User_Lname,
            DateTime User_Bday,
            string User_Gender,
            string User_Pass,
            string User_Email
            )
        {
            bool flag = false;
            string SqlString = "UPDATE tblUser ";

            if (!((User_Fname == "") || (User_Fname == null)))
            {
                SqlString += "SET ";
                SqlString += "User_Fname='" + User_Fname + "'";
                flag = true;
            }
            if (!((User_Lname == "") || (User_Lname == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "User_Lname='" + User_Lname + "'";
                flag = true;
            }
            if (!(User_Bday.ToString() == "0"))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "User_Bday='" + User_Bday + "'";
                flag = true;
            }
            if (!((User_Gender == "") || (User_Gender == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "User_Gender='" + User_Gender + "'";
                flag = true;
            }
            if (!((User_Pass == "") || (User_Pass == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "User_Pass='" + User_Pass + "'";
                flag = true;
            }
            if (!((User_Email == "") || (User_Email == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "User_Email='" + User_Email + "'";
                flag = true;
            }
            if (flag)
            {
                SqlString += " WHERE User_ID='" + User_ID + "'";
                return FullConnected(SqlString);
            }
            else return -1;
        }

        public static int DeleteUser(int User_ID)
        {
            DAL.tblUserArtistsList.DeleteArtists(User_ID);
            return FullConnected("DELETE FROM tblUser WHERE User_ID='" + User_ID + "'");
        }





        public static string GetUserEmail(int ID)
        {
            string SS = "SELECT User_Email FROM tblUser WHERE User_ID='" + ID + "'";
            DataTable dt = GetDataTable(SS, tblName);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["User_Email"].ToString();
            else return "";
        }

        public static int GetUserID(string Email)
        {

            string SS = "SELECT User_ID FROM tblUser WHERE User_Email='" + Email + "'";

            DataTable dt = GetDataTable(SS, tblName);
            if (dt.Rows.Count > 0)
                return int.Parse(GetDataTable(SS, tblName).Rows[0]["User_ID"].ToString());
            else return -1;
        }

         public static DataTable GetUsersDT()
         {
            return GetDataTable("SELECT * FROM tblUser", "tblUser");
        }

    }
}
