using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Notes;

namespace Recorder
{
    /// <summary>
    /// Given the ABC file with notes, while playing in the recorder,
    /// the class checks whether the note that has been play is the correct one,
    /// and display it. In other words: Like GuitarHero, but for Recorder.
    /// 
    /// the image is updating only when new notes are given,
    /// so it depends in the incoming rate of the notes.
    /// 
    /// </summary>
    class RecorderHero
    {
        /// <summary>
        /// The notes to be played
        /// </summary>
        List<Note> Notes;

        /// <summary>
        /// The note that is currently playing
        /// </summary>
        int CurrentNoteIndex;

        /// <summary>
        /// The time that has passed since the start of the playing
        /// </summary>
        int TimeSinceStart;

        /// <summary>
        /// The time that has passed since the current note (currentNoteIndex) has been started
        /// </summary>
        int TimeSinceNoteStart;

        /// <summary>
        /// The time the recording start
        /// </summary>
        int StartTime;

        /// <summary>
        /// Create a music sheet with the given notes
        /// </summary>
        /// <param name="graphics">Where to paint the music sheet</param>
        /// <param name="abcFilePath">The notes to be played</param>
        public RecorderHero(Graphics graphics, string abcFilePath)
        {
            // convert to notes
            //Notes = FilesHandler.ABC2Notes(abcFilePath);

            // Creating a new music sheet, with first notes in it
             Restart(graphics);
        }

        /// <summary>
        /// restart the RecorderHero - notes index and music sheet.
        /// </summary>
        /// <param name="graphics"></param>
        public void Restart(Graphics graphics)
        {
            // Creating a new music sheet, with first notes in it
            CurrentNoteIndex = 0;
            TimeSinceStart = 0;
            TimeSinceNoteStart = 0;
            StartTime = System.Environment.TickCount;

            // Create new Music Sheet
        }
    }
}
