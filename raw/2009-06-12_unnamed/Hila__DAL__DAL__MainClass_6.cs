using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
