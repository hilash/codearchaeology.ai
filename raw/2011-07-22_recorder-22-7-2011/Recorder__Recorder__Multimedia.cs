using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;


//using HANDLE = System.IntPtr;
//using HWND = System.IntPtr;
//using HDC = System.IntPtr;

namespace Win32
{
    public struct WAVEHDR
    {
        public IntPtr lpData;
        public int dwBufferLength;
        public int dwBytesRecorded;
        public int dwUser;
        public int dwFlags;
        public int dwLoops;
        public int lpNext;
        public int Reserved;
    }

    public struct WAVEINCAPS
    {
        public short wMid;
        public short wPid;
        public int vDriverVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = WinMM.MAXPNAMELEN)]
        public string szPname;
        public int dwFormats;
        public short wChannels;
    }

    public struct WAVEFORMATEX
    {
        public short wFormatTag;
        public short nChannels;
        public uint nSamplesPerSec;
        public uint nAvgBytesPerSec;
        public short nBlockAlign;
        public short wBitsPerSample;
        public short cbSize;
    }

    public abstract class WinMM
    {
        [DllImport("winmm")] public static extern int waveInGetNumDevs();
        [DllImport("winmm")] public static extern int waveInGetDevCaps(int uDeviceID, ref WAVEINCAPS lpCaps, int uSize);
        [DllImport("winmm")] public static extern int waveInOpen(ref IntPtr lphWaveIn, uint DEVICEID, ref WAVEFORMATEX lpWaveFormat, uint dwCallback, uint dwInstance, uint dwFlags);
        [DllImport("winmm")] public static extern int waveInStart(IntPtr hWaveIn);
        [DllImport("winmm")] public static extern int waveInPrepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm")] public static extern int waveInAddBuffer(IntPtr hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm")] public static extern int waveInStop(IntPtr hWaveIn);
        [DllImport("winmm")] public static extern int waveInClose(IntPtr hWaveIn);
        [DllImport("winmm")] public static extern int waveInUnprepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);


        public const int MAXPNAMELEN = 32;
        public const int MMSYSERR_NOERROR = 0;
        public const int CALLBACK_NULL = 0x0;
        public const int WAVE_FORMAT_PCM = 1;
        public const int CALLBACK_WINDOW = 0x10000;
        public const int WHDR_DONE = 0x1; 
    }
}