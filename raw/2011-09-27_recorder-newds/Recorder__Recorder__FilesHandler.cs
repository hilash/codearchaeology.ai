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
using System.Configuration;
using Notes;

namespace Recorder
{
    /// <summary>
    /// FilesHandler exception class
    /// </summary>
    public class FilesHandlerException : System.Exception
    {
        /// <summary>
        /// FilesHandler exception
        /// </summary>
        /// <param name="reason">String describing the cause of the error</param>
        public FilesHandlerException(string reason) : base(reason) { }
    }

    /// <summary>
    /// Class that Handles converting from .abc file to .ps to  .pdf
    /// </summary>
    static class FilesHandler
    {
        static public void CreateABCfile(RecordingData recordingData, string ABCfilePath)
        {
            try
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
            catch (Exception ex)
            {
                string error_message = "FilesHandler Error: create ABC file failed:\n" + ex.Message;
                throw new FilesHandlerException(error_message);
            }
        }

        static public void CreatePostScriptFileFromABC(string ABCfilePath, string postScriptFilePath)
        {
            try
            {
                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = Properties.Settings.Default.abcm2psPath;
                pProcess.StartInfo.Arguments = "\"" + ABCfilePath + "\" -O \"" + postScriptFilePath + "\"";
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = false;
                pProcess.StartInfo.CreateNoWindow = true;
                pProcess.StartInfo.WorkingDirectory = System.IO.Path.GetTempPath();
                pProcess.Start();
                pProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                string error_message = "FilesHandler Error: Error in convertin from .abc file to .ps file\n" + ex.Message;
                throw new FilesHandlerException(error_message);
            }
        }

        static public void CreatePostScriptfile(RecordingData recordingData, string postScriptFilePath)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                string tmpPath = System.IO.Path.GetTempPath();
                string ABCfilePath = tmpPath + guid + ".abc";

                CreateABCfile(recordingData, ABCfilePath);
                CreatePostScriptFileFromABC(ABCfilePath, postScriptFilePath);

                File.Delete(ABCfilePath);
            }
            catch (Exception ex)
            {
                string error_message = "FilesHandler Error: create PostScript file failed:" + ex.Message;
                throw new FilesHandlerException(error_message);
            }
        }

        static public void CreatePDFfileFromPostScript(string postScriptFilePath, string PDFfilePath)
        {
            try
            {
                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = Properties.Settings.Default.GhostScriptPath;
                pProcess.StartInfo.Arguments = "-sDEVICE=pdfwrite -o \"" + PDFfilePath + "\" \"" + postScriptFilePath + "\"";
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = false;
                pProcess.StartInfo.CreateNoWindow = true;
                pProcess.StartInfo.WorkingDirectory = System.IO.Path.GetTempPath();
                pProcess.Start();
                pProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                string error_message = "FilesHandler Error: Error in convertin from .ps file to .pdf file\n" + ex.Message;
                throw new FilesHandlerException(error_message);
            }
        }

        static public void CreatePDFfileFromABC(string ABCfilePath, string PDFfilePath)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                string tmpPath = System.IO.Path.GetTempPath();
                string postScriptFilePath = tmpPath + guid + ".ps";

                CreatePostScriptFileFromABC(ABCfilePath, postScriptFilePath);
                CreatePDFfileFromPostScript(postScriptFilePath, PDFfilePath);

                File.Delete(postScriptFilePath);
            }
            catch (Exception ex)
            {
                string error_message = "FilesHandler Error: converting from .abc to .pdf failed" + ex.Message;
                throw new FilesHandlerException(error_message);
            }
        }

        static public void CreatePDFfile(RecordingData recordingData, string PDFfilePath)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                string tmpPath = System.IO.Path.GetTempPath();
                string postScriptFilePath = tmpPath + guid + ".ps";

                CreatePostScriptfile(recordingData, postScriptFilePath);
                CreatePDFfileFromPostScript(postScriptFilePath, PDFfilePath);

                File.Delete(postScriptFilePath);
            }
            catch (Exception ex)
            {
                string error_message = "FilesHandler Error: creating PDF file failed" + ex.Message;
                throw new FilesHandlerException(error_message);
            }
        }

        private static void AddText(System.IO.FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
