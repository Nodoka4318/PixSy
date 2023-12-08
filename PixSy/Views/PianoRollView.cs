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

namespace PixSy.Views {
    public partial class PianoRollView : Form {
        public PianoRoll PianoRoll => pianoRoll;

        public PianoRollView(PianoRoll pianoRoll) {
            InitializeComponent(pianoRoll);

            selectToolStripMenuItem.Checked = true;
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

        }
    }
}
