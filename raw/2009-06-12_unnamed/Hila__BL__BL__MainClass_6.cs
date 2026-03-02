using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class MainClass
    {
        public static void Main()
        {
            //DataSet DS = new DataSet();
            //DS = UserPage.getSongsByUser(1);
            //DAL.SqlHelper.PrintDataTable(DS.Tables[0]);

            //int[] arr1 = { 1, 2, 3, 4 };
            //int[] arr2 = { 1, 2, 3, 4 };
            //Console.WriteLine( arr1 == arr2);
            //Console.ReadKey();

            SongPage SP = new SongPage();
            SP.getSongData(2);
            Console.WriteLine(SP.Song_ID);
            //Console.WriteLine(SP.Song_Pop);
            //Console.ReadKey();
            

        }
    }
}
