/*******************************************************************
 * Class Name: ABChandler
 * Purpose: handle ABC music sheets:
 *              - open a temporary ABC music sheet
 *              - add notes to it
 *              - convert the music sheet from .ABC to .pdf temporary file
 *              - delete the file in the end
 * Author: Hila Shmuel, 
 * Date: 24/04/2011
 *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recorder
{
    class ABCHandler
    {
        string ABCfileName;
        string PSfileName;
        string PDFfileName;
        System.IO.FileStream ABCfile;

        // Enums for recorder pitches
        enum FirstOctave
        {
            C_min = 0,  C_max = 0,
            Csharp_min = 0,  Csharp_max = 0,
            D_min = 0,  D_max = 0,
            Dsharp_min = 0,  Dsharp_max = 0,
            E_min = 0,  E_max = 0,
            Esharp_min = 0,  Esharp_max = 0,
            F_min = 0,  F_max = 0,
            Fsharp_min = 0,  Fsharp_max = 0,
            G_min = 0,  G_max = 0,
            Gsharp_min = 0,  Gsharp_max = 0,
            A_min = 0,  A_max = 0,
            Asharp_min = 0,  Asharp_max = 0,
            B_min = 0,  B_max = 0,
            Bsharp_min = 0,  Bsharp_max = 0,
        }

        enum SecondOctave
        {
            C_min = 0, C_max = 0,
            Csharp_min = 0, Csharp_max = 0,
            D_min = 0, D_max = 0,
            Dsharp_min = 0, Dsharp_max = 0,
            E_min = 0, E_max = 0,
            Esharp_min = 0, Esharp_max = 0,
            F_min = 0, F_max = 0,
            Fsharp_min = 0, Fsharp_max = 0,
            G_min = 0, G_max = 0,
            Gsharp_min = 0, Gsharp_max = 0,
            A_min = 0, A_max = 0,
            Asharp_min = 0, Asharp_max = 0,
            B_min = 0, B_max = 0,
            Bsharp_min = 0, Bsharp_max = 0,
        }

        enum ThirdOctave
        {
            C_min = 0, C_max = 0,
            Csharp_min = 0, Csharp_max = 0,
            D_min = 0, D_max = 0,
            Dsharp_min = 0, Dsharp_max = 0,
        }

        public ABCHandler()
        {
            ABCfileName = "";
        }

        /// <summary>
        /// open temporery ABC file for writing & create the ABC header
        /// </summary>
        public void init()
        {
            // create a unique temporary file & open for writing
            string guid = Guid.NewGuid().ToString();
            string tmp_path = System.IO.Path.GetTempPath();

            ABCfileName = tmp_path + guid + ".abc";
            PSfileName = tmp_path + guid + ".ps";
            PDFfileName = tmp_path + guid + ".pdf ";
            ABCfile = System.IO.File.Create(ABCfileName);

            // write ABC header
            string endl = "\r\n";
            AddText(ABCfile, "X:1" + endl);
            AddText(ABCfile, "T:Paddy O'Rafferty" + endl);
            AddText(ABCfile, "C:Trad." + endl);
            AddText(ABCfile, "M:C" + endl);
            AddText(ABCfile, "L:1/4" + endl);

            // Can be C or F
            AddText(ABCfile, "K:C" + endl);

            ABCfile.Close();
        }

        /// <summary>
        /// add text to file in UTF8
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="value"></param>
        private static void AddText(System.IO.FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        /// <summary>
        /// get a pitch, and add it to music sheet if found fit
        /// return true if any changes made to the music sheet
        /// </summary>
        /// <param name="pitch"></param>
        public bool addNoteToMusicSheet(int pitch)
        {
            // add some logic with counters


            // convert from number to A B C...

            // write note

            string note = pitch.ToString() + "\r\n";
            //AddText(ABCfile, note);
            return true;
        }

        /// <summary>
        ///  create a pdf file with the music sheet. return the file path.
        /// </summary>
        public string ConvertToPDF()
        {
            // Step 1: .ABC --> .PS
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "C:\\Users\\Purple Fire\\Downloads\\abcm2ps-5.9.23-win32\\abcm2ps-5.9.23\\abcm2ps.exe";
            pProcess.StartInfo.Arguments = "\"" + ABCfileName + "\" -O \"" + PSfileName + "\"";
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = false;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.StartInfo.WorkingDirectory = System.IO.Path.GetTempPath();
            pProcess.Start();
            pProcess.WaitForExit();
           
            // Step2: .PS --> .PDF
            


            return PDFfileName;
        }

        public void deleteABCfile()
        {
            ABCfile.Close();
            //System.IO.File.Delete(ABCfileName);
        }
    }
}
