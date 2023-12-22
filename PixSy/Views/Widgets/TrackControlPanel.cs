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
        public bool IsSolo {
            get => _isSolo;
            set {
                _isSolo = value;
                ToggleSolo(value);
            }
        }

        public bool IsMute {
            get => _isMute;
            set {
                _isMute = value;
                ToggleMute(value);
            }
        }

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

        public int Volume {
            get => _volume;
            set {
                _volume = value;
                volumeTrackBar.Value = value;
                volumeButton.Text = $"Level {value}";
            }
        }

        public int Pan {
            get => _pan;
            set {
                _pan = value;
                panTrackBar.Value = value;
                panButton.Text = $"Pan {value}";
            }
        }

        public float NAudioCompatibleVolume => (float)Volume / 15.0f * 1.0f;
        public float NAudioCompatiblePan => (float)Pan / 10.0f;

        private Synth _synth;
        private bool _isSolo;
        private bool _isMute;
        private int _volume = 10;
        private int _pan = 0;

        public TrackControlPanel() {
            InitializeComponent();

            Width = TrackRoll.TrackHeight * 4;
            Height = TrackRoll.TrackHeight;

            soloMuteSynthPanel.Size = new Size(Height, Height);

            soloButton.Size = new Size(Height / 2, Height / 2);
            muteButton.Size = new Size(Height / 2, Height / 2);
            panel1.Size = new Size(Height, Height / 2);
            synthButton.Size = new Size(Height, Height / 2);

            volumeButton.Size = new Size(3 * Height / 4, Height / 2);
            volumePanel.Size = new Size(Width - Height, Height / 2);

            panButton.Size = new Size(3 * Height / 4, Height / 2);
            panPanel.Size = new Size(Width - Height, Height / 2);

            _synth = Synth.DefaultSynth;
            synthButton.Text = _synth.Name;

            volumeTrackBar.ValueChanged += VolumeTrackBar_ValueChanged;
            panTrackBar.ValueChanged += PanTrackBar_ValueChanged;
        }

        private void PanTrackBar_ValueChanged(object? sender, EventArgs e) {
            Pan = panTrackBar.Value;
        }

        private void VolumeTrackBar_ValueChanged(object? sender, EventArgs e) {
            Volume = volumeTrackBar.Value;
        }

        private void ToggleSolo(bool val) {
            _isSolo = val;

            if (val && _isMute) {
                ToggleMute(!val);
            }

            soloButton.BackColor = val ? Color.LightGreen : Color.WhiteSmoke;
        }

        private void ToggleMute(bool val) {
            _isMute = val;

            if (val && _isSolo) {
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

        private void volumeButton_Click(object sender, EventArgs e) {
            using (var dlg = new InputBox()) {
                dlg.Text = "Levelを設定";
                dlg.InputText = _volume.ToString();
                dlg.ShowDialog();

                int volume;

                if (int.TryParse(dlg.InputText, out volume)) {
                    if (!(volume < 0 || volume > 15)) {
                        Volume = volume;
                    } else {
                        MessageBox.Show("Levelは0から15の範囲で設定してください。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else {
                    MessageBox.Show("Levelの設定に失敗しました。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panButton_Click(object sender, EventArgs e) {
            using (var dlg = new InputBox()) {
                dlg.Text = "Panを設定";
                dlg.InputText = _pan.ToString();
                dlg.ShowDialog();

                int pan;

                if (int.TryParse(dlg.InputText, out pan)) {
                    if (!(pan < -10 || pan > 10)) {
                        Pan = pan;
                    } else {
                        MessageBox.Show("Panは-10から10の範囲で設定してください。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else {
                    MessageBox.Show("Panの設定に失敗しました。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
