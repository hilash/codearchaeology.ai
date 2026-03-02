using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using Notes;


namespace MIDI
{
    static class MIDI
    {
        static public void Play(List<Note> notes)
        {
            foreach (Note note in notes)
            {
                //SystemSounds.Beep.Play();
                //SoundPlayer simpleSound = new SoundPlayer(@"C:\Temp\bla4.wav");
                //simpleSound.Play();

                //Console.Beep((int)note.Frequency, (int)400);
            }
        }
    }
}
