/*******************************************************************
 * Class Name: NotesDB
 * Purpose: - notes detection. Given a frequency in Hz, it return
 *          the note (as string). The information is first initialized
 *          from a notes information table.
 *          - given few notes samples, it analyses them and return the note.
 * Author: Hila Shmuel, 
 * Date: 29/07/2011
 *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Notes
{
    /// <summary>
    /// represantation of a musical note
    /// </summary>
    public class Note
    {
        /// <summary>
        /// note scientific name (without ocatve): "A", "A#", "B",...
        /// </summary>
        public string NoteDescription;

        /// <summary>
        /// note's ocatves - from C-1 to G9 (-1,...9)
        /// </summary>
        public short Octave;

        /// <summary>
        /// note's pitch
        /// </summary>
        public float Frequency;

        /// <summary>
        /// note's MIDI number - from 0..127
        /// </summary>
        public byte MIDI;

        /// <summary>
        /// an internal index used for drawing the note on the correct line.
        /// with this numbering, a note "X" and "X#" would have the same value.
        /// example:
        ///     notes - "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
        ///     index -  0,   0,    1,   1,    2,   3,   3,    4,   4,    5,   5,    6
        /// </summary>
        public byte LineIndex;

        /// <summary>
        /// does the note has sharp or flat
        /// </summary>
        public bool IsSharp;

        /// <summary>
        /// the note abc notation
        /// </summary>
        public string ABC;

        /// <summary>
        /// length of the note, in milliseconds
        /// </summary>
        public int Duration;

        public Note(string noteDescription,
            short octave,
            float frequency,
            byte midi,
            byte lineIndex,
            bool isSharp = false,
            string abc = "",
            int duration = 0)
        {
            NoteDescription = noteDescription;
            Octave = octave;
            Frequency = frequency;
            MIDI = midi;
            LineIndex = lineIndex;
            IsSharp = isSharp;
            ABC = abc; // TODO: don't make ABC as deafult, create it in the NoteTable
            Duration = duration;
        }    
    }

    /// <summary>
    /// a comparer for comparing between notes based on their frequencies
    /// </summary>
    public class NoteComparer : IComparer<Note>
    {
        public int Compare(Note x, Note y)
        {
            return x.Frequency.CompareTo(y.Frequency);
        }
    }
    
    /// <summary>
    /// create a Table contains notes and their descriptions,
    /// and allow search note given frequency
    /// </summary>
    class NotesDB
    {
        List<Note> NotesTable;
        NoteComparer NotesComparer;

        public NotesDB()
        {
            NotesTable = CreateNotesTable();
            NotesComparer = new NoteComparer();
        }

        public Note GetNote(byte MIDI)
        {
            if (MIDI < 0 || MIDI > 127)
            {
                return null;
            }
            else
            {
                return NotesTable[MIDI];
            }
        }

        /// <summary>
        /// create a list of notes, with MIDI index 0... 127
        /// </summary>
        /// <returns>list of notes and information</returns>
        private List<Note> CreateNotesTable()
        {
            List<Note> notesTable = new List<Note>();
            string[] notesDescription = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            bool[] sharp = { false, true, false, true, false, false, true, false, true, false, true, false };

            // search for Helmholtz pitch notation
            int germanNotataionMarks = -8; // C,,, C,, C, C c c' c'' c''' c'''' c''''' c''''''
            string noteDescription = "";
            int octave = -1;
            double frequency = 0;
            byte midi = 0;
            byte lineIndex = 0;
            bool isSharp = false;
            string abc = "";
            int noteIndex = 0;

            for (midi = 0; midi < 128; midi++)
            {
                frequency = 440.0 * Math.Pow(2, (midi - 69.0) / 12.0);
                noteIndex = midi % notesDescription.Length;
                octave = midi / notesDescription.Length - 1;
                noteDescription = notesDescription[noteIndex];
                isSharp = sharp[noteIndex];
                
                //ABC
                if (isSharp)
                {
                    abc = "^" + noteDescription[0];
                }
                else
                {
                    abc = noteDescription;
                }

                if (noteIndex == 0)
                {
                    germanNotataionMarks++;
                }
                if (germanNotataionMarks < 0)
                {
                    abc = abc.ToUpper();
                    for (int i = 0; i < Math.Abs(germanNotataionMarks) - 1; i++)
                    {
                        abc += ",";
                    }
                }
                else
                {
                    abc = abc.ToLower();
                    for (int i = 0; i < Math.Abs(germanNotataionMarks); i++)
                    {
                        abc += "'";
                    }
                } 

                notesTable.Add(new Note(noteDescription,
                                        (short)octave,
                                        (float)frequency,
                                        (byte)midi,
                                        lineIndex,
                                        isSharp,
                                        abc));
                if (!isSharp)
                {
                    lineIndex++;
                }
            }

            return notesTable;
        }

        /// <summary>
        /// find note base on given frequency
        /// </summary>
        /// <param name="frequency">pitch, frequency in Hertz</param>
        /// <returns>the note that has the closest pitch to the given frequency</returns>
        public Note FindNote(float frequency)
        {
            Note note = new Note("", 0, frequency, 0, 0);
            int index = NotesTable.BinarySearch(note, NotesComparer);

            if (0 <= index)
            {
                return NotesTable[index];
            }

            index = ~index;

            if ((0 <= index) && (index < NotesTable.Count))
            {
                if (index == 0)
                {
                    return NotesTable[0];
                }
                else if (Math.Abs(NotesTable[index - 1].Frequency - frequency) >= Math.Abs(NotesTable[index].Frequency - frequency))
                {
                    return NotesTable[index];
                }
                else
                {
                    return NotesTable[index - 1];
                }
            }
            return new Note("", 0, 0, 0, 0);
        }
    }
}
