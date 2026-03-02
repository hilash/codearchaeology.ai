using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class Class1
    {
        public static void Main()
        {
            /*
            DateTime today = DateTime.Now;
            DateTime dateOfBirth = new DateTime(1991, 05, 13);
            Console.WriteLine(dateOfBirth.ToShortDateString());
            Console.WriteLine(dateOfBirth.ToShortTimeString());
            Console.WriteLine(dateOfBirth.ToUniversalTime());
            Console.WriteLine(dateOfBirth.ToString());
            Console.WriteLine(today);
            int year = dateOfBirth.Year;
            Console.WriteLine(year);
            TimeSpan ts = new TimeSpan();
            ts = today - dateOfBirth;
            double years = ts.TotalDays/365;
            Console.WriteLine(years);
             */

            DAL.tblAlbum.GetDataTable(
        }
    }
}
