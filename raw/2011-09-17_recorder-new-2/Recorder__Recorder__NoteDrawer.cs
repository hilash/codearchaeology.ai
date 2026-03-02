using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MusicNotation
{
    public class NoteDrawerException : System.Exception
    {
        public NoteDrawerException(string reason) : base(reason) { }
    }

    class NoteDrawer
    {
        const uint numberOfLines = 5;
        const float lengthBetweenLines = 15;
        const float heigthBetweenStaves = 100;
        const float stavesMarginsSize = 20;
        const float spaceBetweenNotes = 15;
        const float spaceAfterClef = 10;
        const float widthOfNote = lengthBetweenLines;
        float width = 0;
        float height = 0;
        float widthOfStave = 0;
        float heightOfStave = (numberOfLines - 1) * lengthBetweenLines;

        /// <summary>
        /// the locations of each stave on the graphics control
        /// </summary>
        List<PointF> stavesLocation;

        /// <summary>
        /// the locations of all the places notes can be printed on the graphics control
        /// </summary>
        List<PointF> NotesLocation;

        /// <summary>
        /// buffer of all the notes that currently shown on the graphics control
        /// </summary>
        List<Note> DrawenNotes;

        // graphics components
        Image bufferImage;
        Graphics bufferGraphics;
        Image GClef;

        public NoteDrawer()
        {
            stavesLocation = new List<PointF>();
            NotesLocation = new List<PointF>();
            DrawenNotes = new List<Note>();
            bufferImage = new Bitmap(1, 1);
            bufferGraphics = Graphics.FromImage(bufferImage);
            Image GClefSource = new Bitmap("C:\\Users\\Purple Fire\\Documents\\Visual Studio 2010\\Projects\\Recorder2\\Recorder\\Recorder\\Resources\\GClef.png");
            GClef = new Bitmap(GClefSource, (int)(heightOfStave * 0.5), (int)(heightOfStave * 1.3));
        }

        public void Draw(Graphics graphics, Brush brush, Note note = null)
        {
            // Redraw music sheet if graphics has changes
            if ((graphics.VisibleClipBounds.Width != width) || (graphics.VisibleClipBounds.Height != height))
            {
                DrawEmptyMusicSheet(graphics, brush);
                
                // If there are too many notes and not enough space for them, remove the oldest notes
                int notesToRemove = (int)DrawenNotes.Count - (int)NotesLocation.Count;
                if (notesToRemove > 0)
                {
                    DrawenNotes.RemoveRange(0, notesToRemove);
                }

                // draw old notes
                for (int noteIndex = 0; noteIndex < DrawenNotes.Count; noteIndex++)
                {
                    Note oldNote = DrawenNotes[noteIndex];
                    DrawNote(bufferGraphics, brush, NotesLocation[noteIndex], oldNote);
                }
            }

            // Draw a new Note
            if (null != note)
            {
                // Draw a new sheet if current one is full
                if (DrawenNotes.Count >= NotesLocation.Count)
                {
                    DrawEmptyMusicSheet(graphics, brush);
                    DrawenNotes.Clear();
                }

                int noteIndex = DrawenNotes.Count;
                DrawNote(bufferGraphics, brush, NotesLocation[noteIndex], note);
                DrawenNotes.Add(note);
            }


            graphics.DrawImage(bufferImage, 0, 0);
        }


        /// <summary>
        /// Draw a plain music sheet, contains only stavs.
        /// Also, update the class varibales, to fit the current graphics objects.
        /// (This method migth be called in two cases:
        ///     1. resize - need to change the sheet size & redraw it (including old notes).
        ///     2. new page - create a blank sheet.
        /// </summary>
        /// <param name="graphics">new graphics object</param>
        private void DrawEmptyMusicSheet(Graphics graphics, Brush brush)
        {
            width = graphics.VisibleClipBounds.Width;
            height = graphics.VisibleClipBounds.Height;
            widthOfStave = width - 2 * stavesMarginsSize;
            // double buffeing
            bufferImage = new Bitmap((int)width, (int)height);
            bufferGraphics = Graphics.FromImage(bufferImage);
            bufferGraphics.Clear(Color.White);

            stavesLocation.Clear();
            NotesLocation.Clear();

            // Calculate new Staves and Note Locations.
            PointF currentStaveLocation = new PointF(stavesMarginsSize, heigthBetweenStaves);
            while (currentStaveLocation.Y + heightOfStave + heigthBetweenStaves <= height)
            {
                stavesLocation.Add(currentStaveLocation);

                PointF currentNoteLocation = new PointF(currentStaveLocation.X + GClef.Width + spaceAfterClef,
                                                        currentStaveLocation.Y);
                while (currentNoteLocation.X + widthOfNote + spaceBetweenNotes < width - stavesMarginsSize)
                {
                    NotesLocation.Add(currentNoteLocation);
                    currentNoteLocation.X += widthOfNote + spaceBetweenNotes;
                }
                currentStaveLocation.Y += heightOfStave + heigthBetweenStaves;
            }

            // print staves
            foreach (PointF staveLocation in stavesLocation)
            {
                DrawStave(bufferGraphics, brush, staveLocation);
            }
        }

        private void DrawNote(Graphics graph, Brush brush, PointF point, Note note)
        {
            /* The starting position is E' (inner index 51).
             * Based on the current note distance from E", we will set it's location. */
            int distanceFromE = note.LineIndex - 51;
            PointF ellipseBase = new PointF(point.X, point.Y - distanceFromE * lengthBetweenLines / 2F);
            SizeF ellipseSize = new SizeF(widthOfNote, lengthBetweenLines);
            RectangleF ellipse = new RectangleF(ellipseBase, ellipseSize);

            PointF LineStart, LineEnd;
            if (note.LineIndex > 48)
            {
                LineStart = new PointF(ellipse.X, ellipse.Y + lengthBetweenLines / 2F);
                LineEnd = new PointF(LineStart.X, LineStart.Y + 3.5F * lengthBetweenLines);
            }
            else
            {
                LineStart = new PointF(ellipse.X + widthOfNote, ellipse.Y + lengthBetweenLines / 2F);
                LineEnd = new PointF(LineStart.X, LineStart.Y - 3.5F * lengthBetweenLines);
            }
            PointF[] noteLine = { LineStart , LineEnd};


            graph.DrawLines(new Pen(brush, 2F), noteLine);
            graph.DrawEllipse(new Pen(brush, 1F), ellipse);
            graph.FillEllipse(brush, ellipse);
        }

        private void DrawStave(Graphics graph, Brush brush, PointF point)
        {
            Pen pen = new Pen(brush);

            // draw vertical lines, at the begining and end of the stave
            graph.DrawLine(pen,
                point,
                new PointF(point.X, point.Y + heightOfStave));

            graph.DrawLine(pen,
                new PointF(point.X + widthOfStave, point.Y),
                new PointF(point.X + widthOfStave, point.Y + heightOfStave));

            // draw G-clefs
            graph.DrawImage(GClef, new PointF(point.X, point.Y));

            // draw stave's lines
            for (int i = 0; i < numberOfLines; i++)
            {
                graph.DrawLine(pen,
                    new PointF(point.X, point.Y + i * lengthBetweenLines),
                    new PointF(point.X + widthOfStave, point.Y + i * lengthBetweenLines));
            }
        }
    }
}
