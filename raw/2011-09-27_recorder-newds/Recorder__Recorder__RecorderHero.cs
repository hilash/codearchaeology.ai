using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using Notes;


namespace Recorder
{
    /// <summary>
    /// The RecorderHero class
    /// </summary>
    class RecorderHero : IDisposable
    {
        /// <summary>
        /// Draws the notes on graphics object
        /// </summary>
        private NoteDrawer Drawer;

        /// <summary>
        /// Holds the ABC notes
        /// </summary>
        private RecordingData AbcData;

        /// <summary>
        /// Thread that runs over the notes, and update the graphics
        /// </summary>
        private Thread RecorderHeroThread;

        /// <summary>
        /// Saves the graphics object, should be accsessed by the recorderHeroThread and when resized.
        /// </summary>
        private Graphics NotesGraphics;


        /// <summary>
        /// The colors of the notes
        /// </summary>
        private List<Color> NotesColors;

        /// <summary>
        /// current note to be played
        /// </summary>
        private int CurrentNoteIndex;

        /// <summary>
        /// Number of notes that can be displayed in music sheet
        /// </summary>
        private int NotesPerPage;

        /// <summary>
        /// The first note to be displayed in the current music sheet
        /// </summary>
        private int FirstNoteInPage;

        /// <summary>
        /// Boolean to tell if currently playing.
        /// </summary>
        private bool m_isPlaying;

        /// <summary>
        /// Is the RecorderHero currently active
        /// </summary>
        public bool isPlaying
        {
            get { return m_isPlaying; }
        }

        /// <summary>
        /// Sleep multiplier by the notes duration
        /// </summary>
        private float m_sleepMultiplier = 1f;

        /// <summary>
        /// How mutch to speed up/down the notes duration
        /// </summary>
        public float SleepMultiplier
        {
            get
            {
                return m_sleepMultiplier;
            }
            set
            {
                this.m_sleepMultiplier = value;
            }
        }

        private Color REGULAR_COLOR = Color.Black;
        private Color WRONG_COLOR = Color.FromArgb(240, 230, 53, 98);
        private Color RIGHT_COLOR = Color.FromArgb(240, 33, 250, 98);
        private Color CURRENT_COLOR = Color.FromArgb(240, 63, 149, 220);

        

        /// <summary>
        /// Open a new RecorderHero instance
        /// </summary>
        /// <param name="graphics">The notes music sheet panel</param>
        /// <param name="abcFilePath">ABC File contains notes</param>
        public RecorderHero(Graphics graphics, string abcFilePath)
        {
            Drawer = new NoteDrawer();

            // convert ABC file to notes
            AbcData = AbcParser.AbcToNotes(abcFilePath);

            // Creating a new music sheet, with first notes in it
            Restart(graphics);
        }

        /// <summary>
        /// Starts a new thread that runs over the notes, and display the current note to be played
        /// </summary>
        public void Start()
        {
            m_isPlaying = true;
            RecorderHeroThread = new Thread(new ThreadStart(NotesIteration));
            RecorderHeroThread.Start();
        }

        /// <summary>
        /// Stops the thread that should runs over the notes
        /// </summary>
        public void Stop()
        {
            m_isPlaying = false;
            RecorderHeroThread.Join();
        }

        /// <summary>
        /// Dispose the recorderHero object
        /// </summary>
        public void Dispose()
        {
            Stop();
        }

        /// <summary>
        /// Notify which note was just played
        /// </summary>
        /// <param name="currentNote">Note that was just played</param>
        public void UpdateCurrentNote(Note currentNote)
        {
            lock (this)
            {
                if (CurrentNoteIndex >= AbcData.Notes.Count)
                {
                    return;
                }
                if (currentNote.MIDI == AbcData.Notes[CurrentNoteIndex].MIDI)
                {
                    NotesColors[CurrentNoteIndex] = RIGHT_COLOR;
                    
                    // Draw with current note as correct
                    int a = FirstNoteInPage;
                    int b = Math.Min(FirstNoteInPage + NotesPerPage, AbcData.Notes.Count)
                        - FirstNoteInPage;

                    NotesPerPage = Drawer.DrawNotes(NotesGraphics,
                        Color.White,
                        new SolidBrush(Color.Black),
                        AbcData.Notes.GetRange(a, b),
                        NotesColors.GetRange(a, b));
                }
            }
        }

        /// <summary>
        /// When the music sheet panel changes, change the internal graphics object & repaint notes
        /// </summary>
        /// <param name="graphics"></param>
        public void OnPaint(Graphics graphics)
        {
            lock (this)
            {
                NotesGraphics = graphics;

                // Create new Music Sheet - get number of notes in current music sheet
                NotesPerPage = Drawer.DrawNotes(graphics,
                    Color.White,
                    new SolidBrush(Color.Black),
                    null,
                    null);

                if (FirstNoteInPage + NotesPerPage <= CurrentNoteIndex)
                {
                    FirstNoteInPage = CurrentNoteIndex;
                }

                // give that number of notes
                int a = FirstNoteInPage;
                int b = Math.Min(FirstNoteInPage + NotesPerPage, AbcData.Notes.Count)
                    - FirstNoteInPage;

                NotesPerPage = Drawer.DrawNotes(graphics,
                    Color.White,
                    new SolidBrush(Color.Black),
                    AbcData.Notes.GetRange(a, b),
                    NotesColors.GetRange(a, b));
            }
        }

        public Note GetCurrentNote()
        {
            if (m_isPlaying && 0 <= CurrentNoteIndex && CurrentNoteIndex < AbcData.Notes.Count)
            {
                return AbcData.Notes[CurrentNoteIndex];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creating a new music sheet with the notes data
        /// </summary>
        /// <param name="graphics"></param>
        private void Restart(Graphics graphics)
        {
            NotesColors = new List<Color>();
            FirstNoteInPage = 0;
            CurrentNoteIndex = 0;
            m_isPlaying = false;
            NotesGraphics = graphics;

            for (int i = 0; AbcData.Notes.Count > i; ++i)
            {
                NotesColors.Add(REGULAR_COLOR);
            }

            OnPaint(graphics);
        }

        /// <summary>
        /// This function should be running in a different thread, and updates the graphics
        /// </summary>
        private void NotesIteration()
        {
            while (m_isPlaying)
            {
                int CurrentNoteDuration = 0;

                lock (this)
                {
                    // Some kind of documentation
                    if (CurrentNoteIndex >= NotesColors.Count)
                    {
                        return;
                    }

                    NotesColors[CurrentNoteIndex] = CURRENT_COLOR;

                    // Check if it's the first note in the page
                    if (FirstNoteInPage == CurrentNoteIndex)
                    {
                        Drawer.Clear(NotesGraphics, Color.White, new SolidBrush(Color.Black));
                    }

                    // Draw notes
                    int a = FirstNoteInPage;
                    int b = Math.Min(FirstNoteInPage + NotesPerPage, AbcData.Notes.Count)
                        - FirstNoteInPage;

                    NotesPerPage = Drawer.DrawNotes(NotesGraphics,
                        Color.White,
                        new SolidBrush(Color.Black),
                        AbcData.Notes.GetRange(a, b),
                        NotesColors.GetRange(a, b));

                    CurrentNoteDuration = AbcData.Notes[CurrentNoteIndex].Duration;
                }

                Thread.Sleep((int)((float)CurrentNoteDuration * m_sleepMultiplier));

                lock (this)
                {
                    // Check if it's the last note in the page
                    if (FirstNoteInPage + NotesPerPage == CurrentNoteIndex)
                    {
                        FirstNoteInPage = CurrentNoteIndex + 1;
                    }

                    // If no right answer yet, color this note as wrong.
                    if (NotesColors[CurrentNoteIndex] != RIGHT_COLOR)
                    {
                        NotesColors[CurrentNoteIndex] = WRONG_COLOR;
                    }

                    // Advance note
                    ++CurrentNoteIndex;

                    // Check if the previous note was the last note
                    if (CurrentNoteIndex == AbcData.Notes.Count)
                    {
                        // Draw again
                        int a = FirstNoteInPage;
                        int b = Math.Min(FirstNoteInPage + NotesPerPage, AbcData.Notes.Count)
                            - FirstNoteInPage;

                        NotesPerPage = Drawer.DrawNotes(NotesGraphics,
                            Color.White,
                            new SolidBrush(Color.Black),
                            AbcData.Notes.GetRange(a, b),
                            NotesColors.GetRange(a, b));

                        // This was last note
                        m_isPlaying = false;
                        return;
                    }
                }
            }
        }
    }
}
