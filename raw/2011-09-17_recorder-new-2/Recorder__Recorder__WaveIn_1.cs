/*******************************************************************
 * Class Name: WaveIn
 * Purpose: an 8-bit sound recoder.
 *          handle raw sound wave - open device for recording, 
 *          and return buffers conatins the sound wave.
 * Author: Hila Shmuel, 
 * Date:02/08/2011
 *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Win32;
using System.Runtime.InteropServices;
using System.Threading;
using PInvokeLibrary;

namespace Wave
{
    public class WaveInException : System.Exception
    {
        public WaveInException(string reason) : base(reason) { }
    }

    public class WaveIn
    {


        /*
        IntPtr hWaveIn = IntPtr.Zero;
        IntPtr dwCallback = IntPtr.Zero;
        IntPtr dataPtr;

        WAVEINCAPS waveInCaps;
        WAVEFORMATEX waveFormat;
        WAVEHDR waveHdr;
        */

        uint numberOfSamples;
        uint samplesPerSec;
        IntPtr hWaveIn;
        IntPtr waveBufferPtr;
        WAVEHDR waveHdr;
        WAVEFORMATEX waveFormat;
        

        /// <summary>
        /// constructor for WaveIn, a class for recording raw sound wave from microphone
        /// </summary>
        public WaveIn(uint paramNumberOfSamples, uint paramSamplesPerSec)
        {
            // TODO: check params
            numberOfSamples = paramNumberOfSamples;
            samplesPerSec = paramSamplesPerSec;

            hWaveIn = IntPtr.Zero;
            waveBufferPtr = IntPtr.Zero;
            waveHdr = new WAVEHDR();
            waveFormat = new WAVEFORMATEX();


            // make dataPtr a pointer to the buffer array, where the device will put the samples
            //waveBufferPtr = Marshal.UnsafeAddrOfPinnedArrayElement(waveBuffer, 0);
            //unsafe
            //{
            //    fixed (byte* p = waveBuffer)
            //    {
            //        waveBufferPtr = (IntPtr)p;
            //    }
            //}

            waveBufferPtr = Memory.LocalAlloc(Memory.LMEM_FIXED, numberOfSamples);
            if (waveBufferPtr == IntPtr.Zero)
            {
                throw new WaveInException("no memory to allocate buffer.");
            }

            //Marshal.Copy(data, 0, lpData, data.Length);
        }

        /// <summary>
        /// Return a list contains the names of waveform-audio input devices present in the system
        /// If there are no such devices, return an empty list
        /// </summary>
        /// <returns>list of waveform-audio input devices</returns>
        public List<string> getDevices()
        {
            // get the number of waveform-audio input devices present in the system
            int numberOfDevices = WinMM.waveInGetNumDevs();
            // list contains the names of waveform-audio input devices present in the system
            List<string> namesOfDevices = new List<string>();
            // describes the capabilities of a waveform-audio input device
            WAVEINCAPS waveInCaps = new WAVEINCAPS();
            // winmm functions return value
            int result = WinMM.MMSYSERR_NOERROR;

            if (numberOfDevices <= 0)
            {
                throw new WaveInException("No microphone has been detected.\n");
            }
            else
            {
                for (int uDeviceID = 0; uDeviceID < numberOfDevices; uDeviceID++)
                {
                    result = WinMM.waveInGetDevCaps(uDeviceID, ref waveInCaps, Marshal.SizeOf(waveInCaps));
                    if (WinMM.MMSYSERR_NOERROR != result)
                    {
                        throw new WaveInException("Failed to get information about input devices (microphones).\n");
                    }
                    namesOfDevices.Add(waveInCaps.szPname);
                }
            }
            return namesOfDevices;
        }

        /// <summary>
        /// open the waveform-audio input device, and start recording.
        /// At this point, the sound bytes (date) will not reach your program.
        /// </summary>
        /// <param name="uDeviceID"></param>
        public void startDevice(uint uDeviceID)
        {
            // winmm functions return value
            int result = WinMM.MMSYSERR_NOERROR;

            // initialize the WAVEFORMATEX structure
            waveFormat.wFormatTag = WinMM.WAVE_FORMAT_PCM;
            waveFormat.nChannels = 1;
            waveFormat.nSamplesPerSec = samplesPerSec;
            waveFormat.wBitsPerSample = 8;
            waveFormat.nBlockAlign = (short)((waveFormat.nChannels) * (waveFormat.wBitsPerSample / 8));
            waveFormat.nAvgBytesPerSec = (uint)(waveFormat.nSamplesPerSec * waveFormat.nBlockAlign);
            waveFormat.cbSize = 0;
            
            // opens the given waveform-audio input device for recording
            result = WinMM.waveInOpen(ref hWaveIn, uDeviceID, ref waveFormat, 0, 0, 0);
            if (WinMM.MMSYSERR_NOERROR != result)
            {
                throw new WaveInException("Failed to open input device!\n");
            }

            // start the device. At this point, the sound bytes will not reach your program
            result = WinMM.waveInStart(hWaveIn);
            if (WinMM.MMSYSERR_NOERROR != result)
            {
                throw new WaveInException("Failed to start recording!\n");
            }

        }

        /// <summary>
        /// stop the device, and close it
        /// </summary>
        public void stopDevice()
        {
            // winmm functions return value
            int result = WinMM.MMSYSERR_NOERROR;

            result = WinMM.waveInReset(hWaveIn);
            if (WinMM.MMSYSERR_NOERROR != result)
            {
                throw new WaveInException("Failed to reset input device!\n");
            }

            result = WinMM.waveInClose(hWaveIn);
            if (WinMM.MMSYSERR_NOERROR != result)
            {
                throw new WaveInException("Failed to close input device!\n");
            }

            //WinMM.waveInUnprepareHeader(hWaveIn, ref waveHdr, Marshal.SizeOf(waveHdr));
            //if (result != WinMM.MMSYSERR_NOERROR)
            //{
            //    MessageBox.Show("Failed to close the device!", "Recorder By Hila Shmuel");
            //    setControlEnabled(InputDevice, true);
            //    setControlEnabled(start_record, false);
            //    setControlEnabled(stop_record, false);
            //    return;
            //}
        }

        /// <summary>
        /// record sounds from waveform-audio device, and return buffer with samples.
        /// put the thread to sleep while waiting for the audio input to return from the device.
        /// </summary>
        /// <param name="waveArray">array of bytes, with size at least (numberOfSamples), given as WaveIn parameter </param>
        /// <param name="sleepTime">amount of time the thread is put to sleep between each check for input, in milliseconds</param>
        public void recordBuffer(byte[] waveArray, int sleepTime)
        {
            // winmm functions return value
            int result = WinMM.MMSYSERR_NOERROR;

            // parameters validation
            if (numberOfSamples > waveArray.Length)
            {
                string message = "The length of the buffer for recording given is" + waveArray.Length.ToString() +
                    ".\nIt length should be the number of samples, " + numberOfSamples.ToString() + ".\n";
                throw new WaveInException("Failed to close input device!\n");
            }

            waveHdr.lpData = waveBufferPtr;
            waveHdr.dwBufferLength = (int)numberOfSamples * waveFormat.nBlockAlign; // length in bytes
            waveHdr.dwFlags = 0;

            // prepares a buffer for waveform-audio input. 
            result = WinMM.waveInPrepareHeader(hWaveIn, ref waveHdr, Marshal.SizeOf(waveHdr));
            if (WinMM.MMSYSERR_NOERROR != result)
            {
                throw new WaveInException("Failed to prepare audio block header!\n");
            }

            // Call wave InPrepareHeader and  waveInAddBuffer to allow Windows to stream the sound bytes to your own buffer
            //    InputDevice.Enabled = true;
            //    start_record.Enabled = false;
            //    stop_record.Enabled = false;

            // sends an input buffer to the given waveform-audio input device. When the buffer is filled, the application is notified. 
            result = WinMM.waveInAddBuffer(hWaveIn, ref waveHdr, Marshal.SizeOf(waveHdr));
            if (WinMM.MMSYSERR_NOERROR != result)
            {
                throw new WaveInException("Failed to add buffer!]n");
            }

            // wait for the API buffer to fill
            //while (((waveHdr.dwFlags & WinMM.WHDR_DONE) != WinMM.WHDR_DONE) && (hWaveIn.Equals(IntPtr.Zero) == false))
            while ((waveHdr.dwFlags & WinMM.WHDR_DONE) != WinMM.WHDR_DONE)
            {
                Thread.Sleep(sleepTime);
            }

            result = WinMM.waveInUnprepareHeader(hWaveIn, ref waveHdr, Marshal.SizeOf(waveHdr));
            if (WinMM.MMSYSERR_NOERROR != result)
            {
                throw new WaveInException("Failed to unprepare audio block header!\n");
            }

            if (waveBufferPtr != IntPtr.Zero)
            {
                Marshal.Copy(waveBufferPtr, waveArray, 0, (int)numberOfSamples);
            }
            else
            {
                throw new WaveInException("Failed to copy data from buffer!\n");
            }
        }

        /// <summary>
        /// Frees any memory allocated for the buffer.
        /// </summary>
        public void Dispose()
        {
            if (waveBufferPtr != IntPtr.Zero)
                Memory.LocalFree(waveBufferPtr);
        }
    }
}
