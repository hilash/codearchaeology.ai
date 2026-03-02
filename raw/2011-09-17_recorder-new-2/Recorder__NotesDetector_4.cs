
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes
{
    /// <summary>
    /// Given last notes samples, detect which (if any) note has been played
    /// </summary>
    class NotesDetector
    {
        /// <summary>
        /// if captured the same samples at least in that time, declare it a note
        /// </summary>
        private const int minimalNoteTime = 70;
        private const int maximalNoteTime = 1000;
        private const int notesSamplesLimit = 5;
        private LimitedList<Note> playedNotes;
        private LimitedList<int> playedNotesTimes;

        /// <summary>
        /// constructor for the class NotesDetector
        /// </summary>
        public NotesDetector()
        {
            playedNotes = new LimitedList<Note>(notesSamplesLimit);
            playedNotesTimes = new LimitedList<int>(notesSamplesLimit);
        }

        /// <summary>
        /// Detect the note based on the last and current samples
        /// This function is used to be called repeatedly, once for every note sample.
        /// given a note, a check is preform based on the last samples, whether this note
        /// is a valid note (with sufficient length) or not.
        /// Based on the detection, if there are enough samples, the function return the 
        /// new note, or the same prevous note with longer duration.
        /// </summary>
        /// <param name="note">A note sample. This may not produce a note, in cases the the note's samples
        /// appears to be a noise</param>
        /// <param name="timePlayed">The time this note has been played</param>
        /// <returns>The Note played</returns>
        public Note DetectNote(Note note, int timePlayed)
        {
            playedNotes.AddAtStart(note);
            playedNotesTimes.AddAtStart(timePlayed);

            if (playedNotes.Count < playedNotes.Limit)
            {
                return null;
            }

            int duration = 0;
            for (int i = playedNotes.Count - 1; i >= 0; i--)
            {
                Note oldNote = playedNotes[i];
                int oldTime = playedNotesTimes[i];

                if (note.MIDI == oldNote.MIDI)
                {
                    duration = timePlayed - oldTime;
                }
                else
                {
                    if (duration < minimalNoteTime)
                    {
                        duration = 0;
                    }
                    break;
                }
            }

            // if duration reached max, clear buffer
            if (maximalNoteTime < duration)
            {
                playedNotes.Clear();
                playedNotesTimes.Clear();
            }

            // add note
            if (0 < duration)
            {
                note.Duration = duration;
                return note;
            }
            else
            {
                return null;
            }     
        }
    }

    /// <summary>
    /// Fixed Size List implematation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LimitedList<T> : List<T>
    {
        private int limit = -1;
        
        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public LimitedList(int limit) : base(limit)
        {
            this.Limit = limit;
        }

        public new void AddAtStart(T item)
        {

            lock (this)
            {
                while (this.Count >= this.Limit)
                {
                    this.RemoveAt(0);
                }
            }

            base.Add(item);
        }
    }
}
