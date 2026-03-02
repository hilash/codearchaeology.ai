using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MainClass
        {
        public static void Main()
    {

             DAL.SqlHelper.PrintDataTable(Join.getArtistsByUser(1));
        }
}
}
