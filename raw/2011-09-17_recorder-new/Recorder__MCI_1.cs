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
            StringBuilder errorTest = new StringBuilder(1000);


            result = mciSendString("open new type waveaudio alias capture", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: start recording has failed (1).\n" 
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";   
                throw new MCIException(reason);
            }

            result = mciSendString("record capture", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: start recording has failed (2).\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";  
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
            StringBuilder errorTest = new StringBuilder(1000);

            result = mciSendString("stop capture", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: stop recording has failed (1).\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                throw new MCIException(reason);
            }

            result = mciSendString("save capture  \"" + filePath + "\"", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: stop recording has failed (1).\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                throw new MCIException(reason);
            }

            result = mciSendString("close capture", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: stop recording has failed (2).\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                throw new MCIException(reason);
            }
        }

        public static void StartPlayingFile(string filePath)
        {
            uint result = 0;
            StringBuilder errorTest = new StringBuilder(1000);

            result = mciSendString("open \"" + filePath + "\" alias track", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: open recording has failed.\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                throw new MCIException(reason);
            }

            result = mciSendString("play track", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: start playing recording has failed.\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                throw new MCIException(reason);
            }
        }

        public static void ResumePlayingFile()
        {
            uint result = 0;
            StringBuilder errorTest = new StringBuilder(1000);

            result = mciSendString("resume track", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: resume playing recording has failed.\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                throw new MCIException(reason);
            }
        }

        public static void PausePlayingFile()
        {
            uint result = 0;
            StringBuilder errorTest = new StringBuilder(1000);

            result = mciSendString("pause track", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: pause recording has failed.\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                throw new MCIException(reason);
            }
        }

        public static void StopPlayingFile()
        {
            uint result = 0;
            StringBuilder errorTest = new StringBuilder(1000);

            result = mciSendString("stop track", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: playing recording has failed.\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                throw new MCIException(reason);
            }

            result = mciSendString("close track", null, 0, IntPtr.Zero);
            if (0 != result)
            {
                mciGetErrorString(result, errorTest, errorTest.Capacity);
                string reason = "MCI: stop playing recording has failed.\n"
                    + "MCI error code: " + result.ToString() + "\n"
                    + "MCI error: " + errorTest.ToString() + "\n";
                throw new MCIException(reason);
            }
        }



        //[DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //private static extern uint mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        //[DllImport("winmm.dll")]
        //private static extern int mciGetErrorString(long errorCode, StringBuilder errorText, long errorTextSize);



        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern uint mciSendString([MarshalAs(UnmanagedType.LPTStr)] string command,
                                                 StringBuilder returnValue,
                                                 int returnLength,
                                                 IntPtr winHandle);

        [System.Runtime.InteropServices.DllImport("winmm.dll", EntryPoint = "mciGetErrorString")]
        public static extern bool mciGetErrorString(uint dwError, StringBuilder errorText, int uLength);
        //[DllImport("winmm.dll")]
        //private static extern int mciGetErrorString(uint errorCode, StringBuilder errorText, int errorTextSize);

    }
}
