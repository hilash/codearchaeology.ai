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

        public Note(string _note, short _octave, float _frequency, byte _MIDI, string _alternative_note = "", string _notation = "", string _ABC = "")
        {
            note = _note;
            octave = _octave;
            frequency = _frequency;
            MIDI = _MIDI;
            alternative_note = _alternative_note;
            notation = _notation;
            ABC = _ABC;
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
            notesTable = new List<Note>();
            notesComparer = new NoteComparer();

            string[] notesText = File.ReadAllLines(_notesTableFilePath);
            foreach (string noteDescription in notesText)
            {
                notesTable.Add(parseNoteLine(noteDescription));
            }
            notesTable.Sort(notesComparer);
        }

        /// <summary>
        /// get line of the format:
        ///             note; octave; frequency; MIDI; alternative_note; noteation; ABC
        /// and return a Note object
        /// </summary>
        /// <param name="noteDescription"></param>
        /// <returns></returns>
        static private Note parseNoteLine(string noteDescription)
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
        
        /// <summary>
        /// find note base on given frequency
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public Note FindNote(float frequency)
        {
            Note note = new Note("", 0, frequency, 0);
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
            return new Note("", 0, 0, 0);
        }
    }
}
