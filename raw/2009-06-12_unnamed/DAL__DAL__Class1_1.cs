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

            string dateOfBirthToString = dateOfBirth.ToShortDateString();
            Console.ReadKey();*/

            //DAL.tblSong.InsertSong(888, "Love Of My Life", 1, 2, "sss", "sss", today , "pop", "sss");
            //DAL.tblSong.UpdateSong(555, "Killers BLA", 1, 2, "BBB", "sasa",new DateTime(1991, 05, 13), "rock", "");
            //DAL.tblSong.AddSongPOP(555, 23);
            //Console.WriteLine(DAL.tblSong.SongID("killers BLA"));

            
            /*
            DAL.tblUserSongsList.InsertUserSongsList(1, 1);
            DAL.tblUserSongsList.InsertUserSongsList(1, 2);
            DAL.tblUserSongsList.InsertUserSongsList(1, 3);
            DAL.tblUserSongsList.InsertUserSongsList(2, 2);
            DAL.tblUserSongsList.InsertUserSongsList(3, 2);

            DAL.tblUserSongsList.DeleteUserSongsList(2, 2);*/
            
            //DAL.tblSong.PopSong(0);
            //DAL.tblSong.PopSong(1);
            /*int i =DAL.tblSong.PopSong(2);
            Console.WriteLine("pop 0:");
            Console.WriteLine(i);
            */

            //DAL.tblAlbum.InsertAlbum(4, 2, "Wish", "http://upload.wikimedia.org/wikipedia/en/2/2b/TheCureWish.jpg");
            //DAL.tblAlbum.DeleteAlbum(4);
            //Console.WriteLine(DAL.tblAlbum.AlbumID("Jazz"));
            //Console.WriteLine(DAL.tblAlbum.GetInfo("Album_Name","Album_ID","3"));
            //Console.WriteLine(DAL.tblAlbum.IsAlbumInDB(4));
        }
    }

    //IS USER EXIST?
    //IS SONG ALBUM ETC>
}
