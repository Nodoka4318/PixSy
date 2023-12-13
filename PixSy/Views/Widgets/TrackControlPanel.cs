using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixSy.Views.Widgets {
    public partial class TrackControlPanel : UserControl {
        public int TrackNumber { get; set; }
        public bool IsSolo { get; set; }
        public bool IsMute { get; set; }
        public Synth Synth {
            get => _synth;
            set {
                if (value != null) {
                    _synth = value;
                } else {
                    _synth = Synth.DefaultSynth;
                }

                synthButton.Text = _synth.Name;

                var tElems = PixSyApp.MainView.TrackElements.Where(e => e.TrackNumber == TrackNumber);
                if (tElems.Any()) {
                    foreach (var elem in tElems) {
                        elem.PianoRoll.TrackControl = this;
                    }
                }
            }
        }

        private Synth _synth;


        public TrackControlPanel() {
            InitializeComponent();

            Width = TrackRoll.TrackHeight;
            Height = TrackRoll.TrackHeight;

            soloButton.Size = new Size(Width / 2, Height / 2);
            muteButton.Size = new Size(Width / 2, Height / 2);
            panel1.Size = new Size(Width, Height / 2);
            synthButton.Size = new Size(Width, Height / 2);

            _synth = Synth.DefaultSynth;
            synthButton.Text = _synth.Name;
        }

        private void ToggleSolo(bool val) {
            IsSolo = val;

            if (val && IsMute) {
                ToggleMute(!val);
            }

            soloButton.BackColor = val ? Color.LightGreen : Color.WhiteSmoke;
        }

        private void ToggleMute(bool val) {
            IsMute = val;

            if (val && IsSolo) {
                ToggleSolo(!val);
            }

            muteButton.BackColor = val ? Color.LightCoral : Color.WhiteSmoke;
        }

        private void soloButton_Click(object sender, EventArgs e) {
            ToggleSolo(!IsSolo);
        }

        private void muteButton_Click(object sender, EventArgs e) {
            ToggleMute(!IsMute);
        }

        private void synthButton_Click(object sender, EventArgs e) {
            using (var dlg = new ListSelectBox<Synth>()) {
                dlg.Text = "音色を選択";
                dlg.SetItems(PixSyApp.MainView.Synths);

                if (dlg.ShowDialog() == DialogResult.OK) {
                    Synth = dlg.Selected;
                } else {
                    Synth = Synth.DefaultSynth;
                }
            }
        }
    }
}
