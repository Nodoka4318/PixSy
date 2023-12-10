using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using PixSy.Views.Widgets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PixSy.Synths;

namespace PixSy.Views {
    public partial class PianoRollView : Form {
        public PianoRoll PianoRoll => pianoRoll;
        public int Bpm { get; set; }

        private System.Windows.Forms.Timer _playTimer;
        private List<Note> _playingNotes;

        public PianoRollView(PianoRoll pianoRoll) {
            InitializeComponent(pianoRoll);

            selectToolStripMenuItem.Checked = true;
            Bpm = 120;

            _playTimer = new System.Windows.Forms.Timer();
            _playingNotes = new List<Note>();

            _playTimer.Interval = (int) (60f / (float) Bpm * 100f);
            _playTimer.Tick += _playTimer_Tick;

            FormClosing += PianoRollView_FormClosing;
        }

        private void PianoRollView_FormClosing(object? sender, FormClosingEventArgs e) {
            _playTimer.Stop();
        }

        private void _playTimer_Tick(object? sender, EventArgs e) {
            var currentNotes = pianoRoll.GetCurrentNotesToPlay();
            var notesToPlay = currentNotes.Except(_playingNotes);

            foreach (var note in notesToPlay) {
                var sound = note.GetSoundSignal(Bpm);

                Task.Run(async () => {
                    await Synth.PlaySound(sound);
                });
            }

            _playingNotes = currentNotes;
            pianoRoll.CurrentPlayHPos += 0.1f;
        }

        private void pianoRoll_Load(object sender, EventArgs e) {

        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e) {
            pianoRoll.Mode = PianoRoll.EditMode.Select;

            penToolStripMenuItem.Checked = false;
            selectToolStripMenuItem.Checked = true;
        }

        private void penToolStripMenuItem_Click(object sender, EventArgs e) {
            pianoRoll.Mode = PianoRoll.EditMode.Pen;

            penToolStripMenuItem.Checked = true;
            selectToolStripMenuItem.Checked = false;
        }


        private void playToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_playTimer.Enabled) {
                _playTimer.Stop();
                pianoRoll.IsPlaying = false;
                playToolStripMenuItem.Text = "再生";
            } else {
                _playTimer.Start();
                pianoRoll.IsPlaying = true;
                playToolStripMenuItem.Text = "一時停止";
            }
        }
    }
}
