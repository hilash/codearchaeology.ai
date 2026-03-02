using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Notes;

namespace Recorder
{
    class FingeringChart
    {
        NoteDrawer noteDrawer;
        List<Note> notes;

        public FingeringChart()
        {
            noteDrawer = new NoteDrawer();
            notes = new List<Note>();

            NotesDB notesDB = new NotesDB();
            for (byte MIDI = 72; MIDI <= 99; MIDI++)
            {
                Note note = notesDB.GetNote(MIDI);
                if (null != note)
                {
                    notes.Add(note);
                }
            }         
        }

        public Note GetNoteOnLocation(Point location)
        {
            return noteDrawer.GetNoteOnLocation(location);
        }

        public void Draw(Graphics graphics, Brush brush)
        {
            Color background = Color.White;
            noteDrawer = new NoteDrawer();
            for (int i = 0; i < notes.Count; i++)
            {
                noteDrawer.Draw(graphics, background, brush, brush, notes[i]);
            }
        }
    }
}
