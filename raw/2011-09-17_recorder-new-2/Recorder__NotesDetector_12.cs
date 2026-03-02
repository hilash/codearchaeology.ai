/*******************************************************************
 * Class Name: NotesDetector
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

namespace Recorder
{
    public class Note
    {
        public string note;
        public short octave;
        public float frequency;
        public byte MIDI;
        public string alternative_note;
        public string notation;
        public string ABC;
        public bool sharp;
        public byte index;

        public Note(string _note, short _octave, float _frequency, byte _MIDI,  byte _index, bool _sharp = false, string _alternative_note = "", string _notation = "", string _ABC = "")
        {
            note = _note;
            octave = _octave;
            frequency = _frequency;
            MIDI = _MIDI;
            alternative_note = _alternative_note;
            notation = _notation;
            ABC = _ABC;
            sharp = _sharp;
            index = _index;
        }

        
    }

    public class NoteComparer : IComparer<Note>
    {
        public int Compare(Note x, Note y)
        {
            return x.frequency.CompareTo(y.frequency);
        }
    }
    
    class NotesDetector
    {
        List<Note> notesTable;
        NoteComparer notesComparer;

        /// <summary>
        /// reads from file notes information, in the following format:
        /// each text line contains:    note; octave; frequency; MIDI; alternative_note; noteation; ABC
        /// for example (without ABC field):                 F#;-1;11.560000;6;Gb;F#''';
        /// </summary>
        /// <param name="NotesTableFilePath"></param>
        public NotesDetector(string _notesTableFilePath)
        {
            notesTable = CreateNotesTable();
            notesComparer = new NoteComparer();

            /*
            string[] notesText = File.ReadAllLines(_notesTableFilePath);
            foreach (string noteDescription in notesText)
            {
                notesTable.Add(parseNoteLine(noteDescription));
            }
            notesTable.Sort(notesComparer);
            */
            /*string[] notesDescription = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            int MIDI = 0;
            double frequency = 0;
            byte noteDescriptionIndex = 0;
            string noteDescription;
            while (MIDI < 128)
            {
                frequency = 440.0 * Math.Pow(2, (MIDI - 69.0) / 12.0);
                noteDescription = notesDescription[noteDescriptionIndex];
            }
            */
        }

        public List<Note> CreateNotesTable()
        {
            List<Note> notesTable = new List<Note>();
            string[] notesDescription = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            bool[] sharp = { false, true, false, true, false, false, true, false, true, false, true, false };
            int MIDI = 0;
            double frequency = 0;
            int noteIndex = 0;
            int octave = 0;
            string noteDescription = "";
            byte noteCounter = 0; // another counter for notes, not including the sharps
            bool isSharp = false;
            while (MIDI < 128)
            {
                noteIndex = MIDI % notesDescription.Length;
                octave = MIDI / notesDescription.Length - 1;
                frequency = 440.0 * Math.Pow(2, (MIDI - 69.0) / 12.0);
                noteDescription = notesDescription[noteIndex];
                isSharp = sharp[noteIndex];
                notesTable.Add(new Note(noteDescription, (short)octave, (float)frequency, (byte)MIDI, noteCounter, isSharp, "", "", "" ));
                MIDI++;
                if (!isSharp)
                {
                    noteCounter++;
                }
            }
            return notesTable;
        }

        /// <summary>
        /// get line of the format:
        ///             note; octave; frequency; MIDI; alternative_note; noteation; ABC
        /// and return a Note object
        /// </summary>
        /// <param name="noteDescription"></param>
        /// <returns></returns>
        /*static private Note parseNoteLine(string noteDescription)
        {
            string[] tokens = noteDescription.Split(';');
            string note = tokens[0];
            short octave = Convert.ToInt16(tokens[1]);
            float frequency = Convert.ToSingle(tokens[2]);
            byte MIDI  = Convert.ToByte(tokens[3]);
            string alternative_note =tokens[4];
            string notation = tokens[5];
            string ABC = tokens[6];
            return new Note(note, octave, frequency, MIDI, alternative_note, notation, ABC);
        }
        */
        /// <summary>
        /// find note base on given frequency
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public Note FindNote(float frequency)
        {
            Note note = new Note("", 0, frequency, 0, 0);
            int index = notesTable.BinarySearch(note, notesComparer);

            if (0 <= index)
            {
                return notesTable[index];
            }

            index = ~index;

            if ((0 <= index) && (index < notesTable.Count))
            {
                if (index == 0)
                {
                    return notesTable[0];
                }
                else if (Math.Abs(notesTable[index - 1].frequency - frequency) >= Math.Abs(notesTable[index].frequency - frequency))
                {
                    return notesTable[index];
                }
                else
                {
                    return notesTable[index - 1];
                }
            }
            return new Note("", 0, 0, 0, 0);
        }
    }
}
