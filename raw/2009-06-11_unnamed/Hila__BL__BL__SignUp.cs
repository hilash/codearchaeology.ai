using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class SignUp
    {
        /// <summary>
        /// Insert a new user to the database.
        /// returns the numbers of rows affected - if 1, then the insert went well.
        /// if -100 - thats mean there was a problem.
        /// </summary>
        /// <param name="User_Fname"></param>
        /// <param name="User_Lname"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Day"></param>
        /// <param name="User_Gender"></param>
        /// <param name="User_Pass"></param>
        /// <param name="User_Email"></param>
        /// <returns></returns>
        public static int SignUpUser(
           string User_Fname,
           string User_Lname,
           int Year, int Month, int Day,
           string User_Gender,
           string User_Pass,
           string User_Email
           )
        {
            DateTime dt = new DateTime(Year, Month, Day);
            return DAL.tblUser.InsertUser(DAL.SqlHelper.GetNextID(DAL.tblUser.GetUsersID_DT()), User_Fname, User_Lname, dt.ToShortDateString(), User_Gender, User_Pass, User_Email);
        }
    }
}
