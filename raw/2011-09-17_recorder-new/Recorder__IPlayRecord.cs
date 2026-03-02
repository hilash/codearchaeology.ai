using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Media;
using Notes;

namespace Recorder
{
    static class PlayNote
    {
        public static void PlayNoteBeep(Note note)
        {
            if ((int)note.Frequency < 10)
            {
                Thread.Sleep(1000);
            }
            else
            {
                Console.Beep((int)note.Frequency, 1000);
            }
        }

        public static void PlayNoteMIDI(Note note)
        {
        }

        public static void PlayNoteWAV(Note note)
        {
            // generate a wave sound buffer
            int SamplesPerSec = 11025;
            double samplesPerMillisecond = SamplesPerSec / 1000.0;
            int totalNumberOfSamples = (int)((float)note.Duration * samplesPerMillisecond);
            double phaseAngle = 0;
            byte[] waveArray = new byte[totalNumberOfSamples];
            FileStream waveStream = new FileStream(@"C:\Temp\blaaaaa3.wav", FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(waveStream);

            // generate a sine wave with the note frequency
            for (int i = 0; i < totalNumberOfSamples; i++)
            {
                if (Math.Sin(phaseAngle) < 0)
                {
                    int x = 0;
                }
                double sample = ((double)byte.MaxValue) / 2.0 * (Math.Sin(phaseAngle) + 1.0);
                waveArray[i] = (byte) sample;

                phaseAngle += 2 * Math.PI * note.Frequency / SamplesPerSec;
                if (2 * Math.PI < phaseAngle)
                {
                    phaseAngle -= 2 * Math.PI;
                }
            }

            // Write out the header information - for 8-bit 1 channel output
            WriteChars(binaryWriter, "RIFF");
            binaryWriter.Write(totalNumberOfSamples + 44 - 8);// size of file - 8 (first 8 bytes) - unknown at this point
            WriteChars(binaryWriter, "WAVEfmt ");
            binaryWriter.Write((int)16);                    // size of the WAVEFORMATEX struct that follows
            binaryWriter.Write((ushort)Win32.WinMM.WAVE_FORMAT_PCM);
            binaryWriter.Write((ushort)1);                          //chanels number
            binaryWriter.Write(SamplesPerSec);              //SamplesPerSec
            binaryWriter.Write(SamplesPerSec);              // AvgBytesPerSec
            binaryWriter.Write((ushort)1);                          //BlockAlign
            binaryWriter.Write((ushort)8);                          //BitsPerSample
            WriteChars(binaryWriter, "data");
            binaryWriter.Write((int)totalNumberOfSamples);              // size of buffer - unknown at this point
            binaryWriter.Write(waveArray);

            waveStream.Close();

            // play the note
            SoundPlayer soundPlayer = new SoundPlayer(@"C:\Temp\blaaaaa3.wav");
            soundPlayer.Play();
        }

        /// <summary>
        /// TAKEN FROM PIVOKE LIBARAY EXAMPLES
        /// Save the current record buffers to the specified file.
        /// </summary>
        /// <param name="fileName">Name of file to be saved</param>
        /// <returns>MMSYSERR.NOERROR if successful</returns>
        private static void WriteChars(BinaryWriter wrtr, string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = (char)text[i];
                wrtr.Write(c);
            }
        }
    }

    /*******************************************************************************/
    /*
    public interface IPlayNotes
    {
        void AddNotes(List<Note> additionalNotes);
        void Play();
        void Pause();
        void Stop();
        void Delete();
    }

    public abstract class PlayNotes : IPlayNotes
    {
        // Fields
        protected bool _isPlaying = false;
        protected List<Note> _notes = new List<Note>();
        protected int _noteIndex = 0;

        public virtual void AddNotes(List<Note> additionalNotes)
        {
            _notes.AddRange(additionalNotes.ToList());
        }

        public virtual void Play()
        {
            _isPlaying = true;
            for ( ; _noteIndex < _notes.Count ; _noteIndex++)
            {
                if (false == _isPlaying)
                {
                    break;
                }
                else
                {
                    // Play note
                }
            }
        }

        public virtual void Pause()
        {
            _isPlaying = false;
        }

        public virtual void Stop()
        {
            _isPlaying = false;
            _noteIndex = 0;
        }

        public virtual void Delete()
        {
            Stop();

            _isPlaying = false;
            _noteIndex = 0;
            _notes.Clear();
        }
    }

    public class PlayNotesByBeep : PlayNotes
    {
        public override void Play()
        {
            _isPlaying = true;
            for (; _noteIndex < _notes.Count; _noteIndex++)
            {
                if (false == _isPlaying)
                {
                    break;
                }
                else
                {
                    Note note = _notes[_noteIndex];
                    if ((int)note.Frequency < 10)
                    {
                        Thread.Sleep(1000);
                    }
                    else 
                    {
                        Console.Beep((int)note.Frequency, 1000);
                    }
                }
            }
        }
    } */
}
