/*******************************************************************
 * Class Name: RecorderFingerings
 * Purpose: given a note & octave, create a picture of the recorder
 *          with the proper fingerings
 * Author: Hila Shmuel, 
 * Date: 19/08/2011
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using MusicNotation;

namespace Recorder
{
    enum HoleStatus { Covered, HalfCovered, Uncovered };

    public class RecorderFingeringsException : System.Exception
    {
        public RecorderFingeringsException(string reason) : base(reason) { }
    }


    class FingeringsPicture
    {
        const short recorderHolesNumber = 8;

        Image instrumentPicture;
        RecorderFingerings recorderFingerings;
        RecorderHoles recorderHoles;
        

        public FingeringsPicture(string instrumentPicturePath,
                                string recorderFingeringsFilePath,
                                string recorderHolesFilePath)
        {
            instrumentPicture = new Bitmap(instrumentPicturePath);
            recorderFingerings = new RecorderFingerings(recorderFingeringsFilePath);
            recorderHoles = new RecorderHoles(recorderHolesFilePath);    
        }
        public void createPicture(Graphics graph, Note note)
        {
            HoleStatus[] fingerings = recorderFingerings.getNoteFingerings(note);
            Image image = new Bitmap(instrumentPicture);
            Graphics ImageGraphics = Graphics.FromImage(image);
            Brush brush = new SolidBrush(Color.Black);

            if (null != fingerings)
            {
                for (byte holeIndex = 0; holeIndex < recorderHolesNumber; holeIndex++)
                {
                    if (fingerings[holeIndex] == HoleStatus.Covered)
                    {
                        ImageGraphics.FillEllipse(brush, recorderHoles.getHoleLocation(holeIndex));
                    }
                    else if (fingerings[holeIndex] == HoleStatus.Uncovered)
                    {

                    }
                }
            }

            graph.DrawImage(image, 0, 0);
        }
    }

    class RecorderFingerings
    {
        const short MaxMIDI = 100;
        const short recorderHolesNumber = 8;
        HoleStatus[][] fingeringsTable;

        public RecorderFingerings(string recorderFingeringsFilePath)
        {
            fingeringsTable = new HoleStatus[MaxMIDI][];

            string[] notesText = File.ReadAllLines(recorderFingeringsFilePath);
            foreach (string noteFingering in notesText)
            {
                //fingeringsTable.Add(parseNoteLine(noteDescription));
                addNoteFingerings(fingeringsTable, noteFingering);
            }

        }

        static private void addNoteFingerings(HoleStatus[][] fingeringsTable, string noteFingering)
        {
            string[] tokens = noteFingering.Split(';');

            short MIDIindex = Convert.ToInt16(tokens[0]);
            fingeringsTable[MIDIindex] = new HoleStatus[recorderHolesNumber];

            for (int i = 0; i < recorderHolesNumber; i++)
            {
                switch (tokens[i + 1])
                {
                    case "c":
                        fingeringsTable[MIDIindex][i] = HoleStatus.Covered;
                        break;
                    case "h":
                        fingeringsTable[MIDIindex][i] = HoleStatus.HalfCovered;
                        break;
                    case "u":
                        fingeringsTable[MIDIindex][i] = HoleStatus.Uncovered;
                        break;
                    default:
                        throw new RecorderFingeringsException("parsing the fingering input invalid" +
                            "for note number: " + tokens[0] + " at index: " + i.ToString());
                }
            }
        }

        public HoleStatus[] getNoteFingerings(Note note)
        {
            if (0 <= note.MIDI && note.MIDI < fingeringsTable.Length)
            {
                return fingeringsTable[note.MIDI];
            }
            else
            {
                return null;
            }
        }
    }

    class RecorderHoles
    {
        const short recorderHolesNumber = 8;
        Rectangle[] holesLocationsTable;

        public RecorderHoles(string recorderHolesFilePath)
        {
            holesLocationsTable = new Rectangle[recorderHolesNumber];

            string[] holesText = File.ReadAllLines(recorderHolesFilePath);
            foreach (string holeLocation in holesText)
            {
                addHoleLocation(holesLocationsTable, holeLocation);
            }

        }

        static private void addHoleLocation(Rectangle[] holesLocationsTable, string holeLocation)
        {
            string[] tokens = holeLocation.Split(';');

            short holeIndex = Convert.ToInt16(tokens[0]);

            int x = Convert.ToInt16(tokens[1]) -5 ;
            int y = Convert.ToInt16(tokens[2]) -5;
            int width = Convert.ToInt16(tokens[3]);
            int height = Convert.ToInt16(tokens[4]);

            holesLocationsTable[holeIndex] = new Rectangle(x, y, width, height);
        }

        public Rectangle getHoleLocation(short holeIndex)
        {
            return holesLocationsTable[holeIndex];
        }
    }
}
