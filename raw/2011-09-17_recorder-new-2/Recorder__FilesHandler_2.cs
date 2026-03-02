/*******************************************************************
 * Class Name: FilesHandler
 * Purpose: handle the program's output files formats:
 *          ABC, PostScript, PDF, Wave, MIDI.
 *          
 *          given a list of Notes, outputs the file containing
 *          the corresponding music data.
 *          
 * Author: Hila Shmuel, 
 * Date: 0`4/09/2011
 *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Notes;

namespace Recorder
{
    static class FilesHandler
    {
        static public void CreateABCfile(RecodingData recordingData, string ABCfilePath)
        {
            FileStream ABCfile = System.IO.File.Create(ABCfilePath);

            // Write ABC file header
            string endl = "\r\n";
            AddText(ABCfile, "X:1" + endl);
            AddText(ABCfile, "T:" + recordingData.Title + endl);
            AddText(ABCfile, "C:" + recordingData.Composer + endl);
            AddText(ABCfile, "M:C" + endl);
            AddText(ABCfile, "L:1/4" + endl);
            AddText(ABCfile, "K:C" + endl); // Can be C or F

            // Write ABC file data - notes
            foreach (Note note in recordingData.Notes)
            {
                AddText(ABCfile, note.ABC + " ");
            }

            ABCfile.Close();
        }

        static public void CreatePostScriptFileFromABC(string ABCfilePath, string postScriptFilePath)
        {
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = @"C:\Users\Purple Fire\Documents\Visual Studio 2010\Projects\Recorder2\Recorder\Recorder\Resources\abcm2ps.exe";
            pProcess.StartInfo.Arguments = "\"" + ABCfilePath + "\" -O \"" + postScriptFilePath + "\"";
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = false;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.StartInfo.WorkingDirectory = System.IO.Path.GetTempPath();
            pProcess.Start();
            pProcess.WaitForExit();
        }

        static public void CreatePostScriptfile(RecodingData recordingData, string postScriptFilePath)
        {
            string guid = Guid.NewGuid().ToString();
            string tmpPath = System.IO.Path.GetTempPath();
            string ABCfilePath = tmpPath + guid + ".abc";

            // create abc file, and convert it to postScript
            CreateABCfile(recordingData, ABCfilePath);
            CreatePostScriptFileFromABC(ABCfilePath, postScriptFilePath);

            // remove temp ABC file - TODO
        }

        static public void CreatePDFfileFromPostScript(string postScriptFilePath, string PDFfilePath)
        {
            //TODO
        }

        static public void CreatePDFfileFromABC(string ABCfilePath, string PDFfilePath)
        {
            string guid = Guid.NewGuid().ToString();
            string tmpPath = System.IO.Path.GetTempPath();
            string postScriptFilePath = tmpPath + guid + ".ps";

            CreatePostScriptFileFromABC(ABCfilePath, postScriptFilePath);
            CreatePDFfileFromPostScript(postScriptFilePath, PDFfilePath);

            // TODO - remove postScript file
        }

        static public void CreatePDFfile(RecodingData recordingData, string PDFfilePath)
        {
            string guid = Guid.NewGuid().ToString();
            string tmpPath = System.IO.Path.GetTempPath();
            string postScriptFilePath = tmpPath + guid + ".ps";

            CreatePostScriptfile(recordingData, postScriptFilePath);
            CreatePDFfileFromPostScript(postScriptFilePath, PDFfilePath);
            // TODO - remove postScript file
        }

        private static void AddText(System.IO.FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        /*
        /// <summary>
        /// open temporery ABC file for writing & create the ABC header
        /// </summary>
        public void CreateABCfile()
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
        */
    }
}
