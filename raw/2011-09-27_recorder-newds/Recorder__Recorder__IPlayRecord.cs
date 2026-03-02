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
    /// <summary>
    /// A generic interface for playing music
    /// </summary>
    public interface IPlayNotes
    {
        void AddNotes(List<Note> additionalNotes);
        void Play();
        void Pause();
        void Stop();
        void Delete();
    }

    /// <summary>
    /// an abstarct class for the genering playing music interface,
    /// used to contain some implemantation
    /// </summary>
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

    /// <summary>
    /// Given notes, play them via the computer speaker
    /// </summary>
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
                    Console.Beep((int)note.Frequency, note.Duration);
                }
            }
        }
    }

    /// <summary>
    /// Given notes, synthesized a waveform to each note
    /// </summary>
    public class PlayNotesByWave : PlayNotes
    {
        private List<string> tempFiles = new List<string>();

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
                    PlayNoteWAVE(note);
                }
            }
        }

        public override void Stop()
        {
            _isPlaying = false;
            _noteIndex = 0;

            foreach (string file in tempFiles)
            {
                File.Delete(file);
            }
        }

        private void PlayNoteWAVE(Note note)
        {
            // generate a wave sound buffer
            int SamplesPerSec = 11025;
            double samplesPerMillisecond = SamplesPerSec / 1000.0;
            int totalNumberOfSamples = (int)((float)note.Duration * samplesPerMillisecond);
            double phaseAngle = 0;
            byte[] waveArray = new byte[totalNumberOfSamples];
            string tempWaveFilePath = Path.GetTempFileName() + "COOL.wav";
            FileStream waveStream = new FileStream(tempWaveFilePath, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(waveStream);

            tempFiles.Add(tempWaveFilePath);

            // generate a sine wave with the note frequency
            for (int i = 0; i < totalNumberOfSamples; i++)
            {
                double sample = ((double)byte.MaxValue) / 2.0 * (Math.Sin(phaseAngle) + 1.0);
                waveArray[i] = (byte)sample;

                phaseAngle += 2 * Math.PI * note.Frequency / SamplesPerSec;
                if (2 * Math.PI < phaseAngle)
                {
                    phaseAngle -= 2 * Math.PI;
                }
            }

            // Write out the header information - for 8-bit 1 channel output
            WriteChars(binaryWriter, "RIFF");
            binaryWriter.Write((uint)(totalNumberOfSamples + 44 - 8)); // size of header + size of wave array - 8 (first 8 bytes)
            WriteChars(binaryWriter, "WAVEfmt ");
            binaryWriter.Write((int)16);                    // size of the WAVEFORMATEX struct that follows
            binaryWriter.Write((ushort)Win32.WinMM.WAVE_FORMAT_PCM);
            binaryWriter.Write((ushort)1);                  //chanels number
            binaryWriter.Write((uint)SamplesPerSec);              //SamplesPerSec
            binaryWriter.Write((uint)SamplesPerSec);              // AvgBytesPerSec
            binaryWriter.Write((ushort)1);                  //BlockAlign
            binaryWriter.Write((ushort)8);                  //BitsPerSample
            WriteChars(binaryWriter, "data");
            binaryWriter.Write((uint)totalNumberOfSamples);  // size of buffer - unknown at this point
            binaryWriter.Write(waveArray);
            waveStream.Close();

            // play the note
            lock (this)
            {
                SoundPlayer soundPlayer = new SoundPlayer(tempWaveFilePath);
                soundPlayer.Play();
                Thread.Sleep(note.Duration);
            }
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

    /// <summary>
    /// WAVE file player
    /// </summary>
    public class PlayNotesByWaveFile : PlayNotes
    {
        private string waveFilePath;
        SoundPlayer soundPlayer;
        bool isPlaying;

        /// <summary>
        /// Wave file player
        /// </summary>
        /// <param name="_waveFilePath">path of the wave filr to play</param>
        public PlayNotesByWaveFile(string _waveFilePath)
        {
            waveFilePath = _waveFilePath;
            soundPlayer = new SoundPlayer(waveFilePath);
            //isPlaying = false;
        }

        /// <summary>
        /// Play or Resume
        /// </summary>
        public override void Play()
        {
            
            soundPlayer.Play();
            //if (isPlaying)
            {
                //MCI.ResumePlayingFile();
            }
            //else
            {
                //isPlaying = true;
                //MCI.StartPlayingFile(waveFilePath);
            }
        }

        public override void Pause()
        {
            soundPlayer.Stop();
            //MCI.PausePlayingFile();
        }

        public override void Stop()
        {
            soundPlayer.Stop();
            //isPlaying = false;
            //MCI.StopPlayingFile();
        }
    }
}
