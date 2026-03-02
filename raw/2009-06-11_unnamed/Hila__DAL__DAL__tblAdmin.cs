using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class tblAdmin : SqlHelper
    {

        public static bool IsAdmin(int ID)
        {
            // string SS = "UPDATE tblAdmin SET Admin_ID=" + ID;
            // SS += " WHERE Admin_ID=" + ID;
            // return (FullConnected(SS)>0)?true:false;

            return (FullConnected("UPDATE tblAdmin SET Admin_ID='" + ID + "' WHERE Admin_ID='" + ID + "'") > 0) ? true : false;
        }

        public static DataTable Admins()
        {
            string SS = "SELECT * FROM tblUser INNER JOIN tblAdmin on tblUser.User_ID = tblAdmin.Admin_ID";
            return SqlHelper.GetDataTable(SS, "AdminTable");
        }
    }
}
