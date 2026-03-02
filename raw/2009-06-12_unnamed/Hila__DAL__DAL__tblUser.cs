using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class tblUser : SqlHelper
    {
        private static string tblName
        {
            get
            {
                return "tblUser";
            }
        }

        private static int InsertUser(
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

        private static int UpdateUser(
            int User_ID,
            string User_Fname,
            string User_Lname,
            string User_Bday,
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
            if (!((User_Bday == "") || (User_Bday == null)))
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

        private static int DeleteUser(int User_ID)
        {
            return FullConnected("DELETE FROM tblUser WHERE User_ID='" + User_ID + "'");
        }

        private static string GetInfo(string output_type, string where_type, string where_what)
        {
            string SS = "SELECT * FROM tblUser WHERE ";
            SS += where_type;
            SS += "='" + where_what + "'";
            Console.WriteLine(SS);
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0][output_type].ToString();
        }

        private static bool UserLogIn(int User_ID, string User_Pass)
        {
            string SqlString = "SELECT (User_Pass) FROM tblUser  WHERE User_ID='" + User_ID + "'";
            DataTable dt = GetDataTable(SqlString, tblName);
            return (dt.Rows[0]["User_Pass"].ToString()==User_Pass)?true:false;
        }

        /*
        static void Main(string[] args)
        {
            //InsertUser(4, "LLIILII", "Shmuel","", "Female", "12345678", "");
            //InsertUser(5, "Liave", "Shmuel", "", "Male", "12345678", "");
            //UpdateUser(1, "Mirit", "","", "", "1111", "");
            //DeleteUser(3);
            Console.WriteLine(UserLogIn(1, "1112"));

            Console.ReadKey();
        }*/
    }
}
