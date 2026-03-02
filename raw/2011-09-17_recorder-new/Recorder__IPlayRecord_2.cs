using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Notes;
using System.Threading;

namespace Recorder
{
    public class PlayNote
    {
        public void PlayNoteBeep(Note note)
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

        public void PlayNoteMIDI(Note note)
        {
        }

        public void PlayNoteWAV(Note note)
        {
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
