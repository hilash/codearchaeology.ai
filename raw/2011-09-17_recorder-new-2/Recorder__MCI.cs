using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Recorder
{
    /// <summary>
    /// MCI exception class
    /// </summary>
    public class MCIException : System.Exception
    {
        /// <summary>
        /// MCI exception
        /// </summary>
        /// <param name="reason">String describing the cause of the error</param>
        public MCIException(string reason) : base(reason) { }
    }

    /// <summary>
    /// simple C# Wrapper for the windows API MCI -  Media Control Interface.
    /// MCI provides standard commands for playing multimedia devices and 
    /// recording multimedia resource files. These commands are a generic
    /// interface to nearly every kind of multimedia device.
    /// </summary>
    static class MCI
    {
        /// <summary>
        /// Start recording from microphone
        /// </summary>
        public static void StartRecording()
        {
            uint result = 0;
            StringBuilder errorTest = new StringBuilder(256);

            result = mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            if (0 != result)
            {
                string reason = "MCI: start recording has failed (1).\n" 
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                mciGetErrorString(result, errorTest, errorTest.Length);
                throw new MCIException(reason);
            }

            result = mciSendString("record recsound", "", 0, 0);
            if (0 != result)
            {
                string reason = "MCI: start recording has failed (2).\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                mciGetErrorString(result, errorTest, errorTest.Length);
                throw new MCIException(reason);
            }
        }

        /// <summary>
        /// Stop recording from microphone
        /// </summary>
        /// <param name="filePath">where to save the recording</param>
        public static void StopRecording(string filePath)
        {
            uint result = 0;
            StringBuilder errorTest = new StringBuilder(256);

            string guid = Guid.NewGuid().ToString();
            string filePath2 = @"C:\temp\cool" + guid + ".wav";

            result = mciSendString("save recsound " + filePath2, "", 0, 0);
            //result = mciSendString("save recsound  \"C:\\temp\\cool" + guid + ".wav\"", "", 0, 0);
            if (0 != result)
            {
                string reason = "MCI: stop recording has failed (1).\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                mciGetErrorString(result, errorTest, errorTest.Length);
                throw new MCIException(reason);
            }

            result = mciSendString("close recsound ", "", 0, 0);
            if (0 != result)
            {
                string reason = "MCI: stop recording has failed (2).\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                mciGetErrorString(result, errorTest, errorTest.Length);
                throw new MCIException(reason);
            }
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern uint mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern int mciGetErrorString(uint errorCode, StringBuilder errorText, int errorTextSize);

    }
}
