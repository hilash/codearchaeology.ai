using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using MusicNotation;


namespace MIDI
{
    static class MIDI
    {
        static public void PlayNotes(List<Note> notes)
        {
            foreach (Note note in notes)
            {
                //SystemSounds.Beep.Play();
                //SoundPlayer simpleSound = new SoundPlayer(@"C:\Temp\bla2.wav");
                //simpleSound.Play();

                Console.Beep((int)note.Frequency, (int)400);
            }
        }
    }
}
