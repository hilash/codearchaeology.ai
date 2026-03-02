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
using System.Text.RegularExpressions;
using Notes;

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
        

        public FingeringsPicture()
        {
            instrumentPicture = Recorder.Properties.Resources.RecorderPicture;
            recorderFingerings = new RecorderFingerings(Recorder.Properties.Resources.recorderFingering);
            recorderHoles = new RecorderHoles(Recorder.Properties.Resources.recorderPictureHoles);    
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
                    else if (fingerings[holeIndex] == HoleStatus.HalfCovered)
                    {
                        ImageGraphics.FillPie(brush, recorderHoles.getHoleLocation(holeIndex),-90, 180);
                    }
                    else if (fingerings[holeIndex] == HoleStatus.Uncovered)
                    {
                        // do nothing
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

        public RecorderFingerings(string recorderFingerings)
        {
            fingeringsTable = new HoleStatus[MaxMIDI][];

            string[] lines = Regex.Split(recorderFingerings, "\r\n");

            foreach (string noteFingering in lines)
            {
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
            if (null == note)
            {
                return null;
            }
            else if (0 <= note.MIDI && note.MIDI < fingeringsTable.Length)
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

        public RecorderHoles(string recorderHoles)
        {
            holesLocationsTable = new Rectangle[recorderHolesNumber];

            string[] lines = Regex.Split(recorderHoles, "\r\n");
            foreach (string holeLocation in lines)
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
