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
        enum FirstOctave {C, Csharp, D, Dsharp, E, F, Fsharp, G, Gsharp, A, Asharp, B}
        string[] FirstOctaveString = { "C", "Csharp", "D", "Dsharp", "E", "F", "Fsharp", "G", "Gsharp", "A", "Asharp", "B" };
        int[] FirstOctaveMin = {66, 70, 74, 79, 84, 88, 94, 99, 104, 110, 119, 126};
        int[] FirstOctaveMax = {69, 72, 77, 82, 87, 93, 98, 103, 109, 114, 121, 129};

        enum SecondOctave {C, Csharp, D, Dsharp, E, F, Fsharp, G, Gsharp, A, Asharp, B }
        string[] SecondOctaveString = { "C", "Csharp", "D", "Dsharp", "E", "F", "Fsharp", "G", "Gsharp", "A", "Asharp", "B" };
        int[] SecondOctaveMin = { 66, 70, 74, 79, 84, 88, 94, 99, 104, 110, 119, 126 };
        int[] SecondOctaveMax = { 69, 72, 77, 82, 87, 93, 98, 103, 109, 114, 121, 129 };
        

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
            AddText(ABCfile, "C:Trad. (or Hila Shmuel)" + endl);
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

        private int detect_note(int pitch)
        {
            foreach (FirstOctave note in Enum.GetValues(typeof(FirstOctave)))
            {
                if (FirstOctaveMin[(int)note] <= pitch && pitch <= FirstOctaveMax[(int)note])
                    return (int)note;
            }
            return -1;
        }

        // TODO: change this!! o work with Notes
        /// <summary>
        /// get a pitch, and add it to music sheet if found fit
        /// return true if any changes made to the music sheet
        /// </summary>
        /// <param name="pitch"></param>
        public bool addNoteToMusicSheet(string note)
        {
            // add some logic with counters
                ABCfile = System.IO.File.Open(ABCfileName, System.IO.FileMode.Append);
                AddText(ABCfile, note + " ");
                ABCfile.Close();
                return true;



            // convert from number to A B C...

            // write note

            //string note = pitch.ToString() + "\r\n";
            
            //return false;
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
