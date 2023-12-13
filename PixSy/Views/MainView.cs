using NAudio.Midi;
using PixSy.IO.Save;
using PixSy.Synths;
using PixSy.Views.Widgets;
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
using Timer = PixSy.Threading.Timer;

namespace PixSy.Views {
    public partial class MainView : Form {
        public List<Synth> Synths => synthsPanel.Synths;
        public List<TrackRoll.TrackElement> TrackElements => trackRoll.TrackElements;
        public TrackControls TrackControls => trackControls;

        private int _bpm = 120;
        private int _rhythm = 4;
        private float _frequency = 440.00f;
        private Timer _playTimer;
        private List<Note> _playingNotes;

        public MainView() {
            InitializeComponent();

            Text = $"{PixSyAppInfo.AppName} ({PixSyAppInfo.Version})";
            trackControls.Width = TrackRoll.TrackHeight;
            trackRoll.TrackControls = trackControls;

            _playTimer = new Timer();
            _playingNotes = new List<Note>();

            _playTimer.Interval = (int)(60f / (float)_bpm * 100f);
            _playTimer.Tick += _playTimer_Tick;
        }

        private void _playTimer_Tick(object? sender, EventArgs e) {
            var currentNotes = new List<Note>();
            IEnumerable<List<Note>> currentNoteLists;

            if (TrackControls.TrackControlPanels.Exists(p => p.IsSolo)) {
                currentNoteLists = trackRoll.TrackElements
                    .Where(e => e.PianoRoll.TrackControl.IsSolo)
                    .Select(e => e.PianoRoll.GetCurrentNotesToPlay());
            } else {
                currentNoteLists = trackRoll.TrackElements
                    .Where(e => !e.PianoRoll.TrackControl.IsMute)
                    .Select(e => e.PianoRoll.GetCurrentNotesToPlay());
            }

            foreach (var l in currentNoteLists) {
                currentNotes.AddRange(l);
            }

            var notesToPlay = currentNotes.Except(_playingNotes);

            foreach (var note in notesToPlay) {
                var sound = note.GetSoundSignal(_bpm);

                Task.Run(async () => {
                    await Synth.PlaySound(sound);
                });
            }

            _playingNotes = currentNotes;
            trackRoll.CurrentPlayHPos += 0.1f;
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e) {
            trackRoll.CurrentPlayHPos = trackRoll.CurrentPlayHPos; // 必要
            trackRoll.TrackElements.ForEach(e => e.PianoRoll.IsPlaying = true);

            if (!_playTimer.Enabled) {
                trackRoll.IsPlaying = true;
                _playTimer.Start(DateTime.Now);
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_playTimer.Enabled) {
                trackRoll.IsPlaying = false;
                _playTimer.Stop();
            }

            trackRoll.TrackElements.ForEach(e => e.PianoRoll.IsPlaying = false);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_playTimer.Enabled) {
                trackRoll.IsPlaying = false;
                _playTimer.Stop();
            }

            trackRoll.TrackElements.ForEach(e => e.PianoRoll.IsPlaying = false);
            trackRoll.CurrentPlayHPos = 0;
        }

        private void bpmToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var dlg = new InputBox()) {
                dlg.Text = "BPMを設定";
                dlg.InputText = _bpm.ToString();
                dlg.ShowDialog();

                int bpm;

                if (int.TryParse(dlg.InputText, out bpm)) {
                    if (!(bpm < 1 || bpm > 375)) {
                        _bpm = bpm;
                        bpmToolStripMenuItem.Text = $"{_bpm} BPM";

                        _playTimer.Interval = (int)(60f / (float)_bpm * 100f);
                    } else {
                        MessageBox.Show("BPMは1から375の範囲で設定してください。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else {
                    MessageBox.Show("BPMの設定に失敗しました。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rhythmToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var dlg = new InputBox()) {
                dlg.Text = "拍子を設定";
                dlg.InputText = _rhythm.ToString();
                dlg.ShowDialog();

                int rhythm;

                if (int.TryParse(dlg.InputText, out rhythm)) {
                    if (!(rhythm < 1 || rhythm > 999)) {
                        _rhythm = rhythm;
                        rhythmToolStripMenuItem.Text = $"{_rhythm}拍子";

                        trackRoll.Rhythm = _rhythm;
                    } else {
                        MessageBox.Show("拍子は1から999の範囲で設定してください。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else {
                    MessageBox.Show("拍子の設定に失敗しました。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void freqToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var dlg = new InputBox()) {
                dlg.Text = "チューニングを設定";
                dlg.InputText = _frequency.ToString("F2");
                dlg.ShowDialog();

                float freq;

                if (float.TryParse(dlg.InputText, out freq)) {
                    if (!(freq < 1.00f || freq > 999.99f)) {
                        _frequency = freq;
                        freqToolStripMenuItem.Text = $"{_frequency.ToString("F2")}Hz";

                        Note.Tuning = _frequency;
                    } else {
                        MessageBox.Show("チューニング周波数は1.00から999.99の範囲で設定してください。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else {
                    MessageBox.Show("チューニング周波数の設定に失敗しました。", "PixSy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveNewToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var dlg = new SaveFileDialog()) {
                dlg.Filter = "PixSyプロジェクトファイル|*.pixsy";
                dlg.Title = "名前を付けて保存";

                if (dlg.ShowDialog() == DialogResult.OK) {
                    var saveData = new ImplSaveData() {
                        bpm = _bpm,
                        rhythm = _rhythm,
                        tuningFrequency = _frequency,
                        synths = Synths.Select(s => WrapperSynth.Wrap(s)).ToList(),
                        trackNodes = TrackElements.Select(e => new TrackNode() {
                            Notes = e.PianoRoll.Notes.Select(n => WrapperNote.Wrap(n)).ToList(),
                            StartBar = e.StartBar,
                            TrackNumber = e.TrackNumber
                        }).ToList(),

                        trackStates = TrackControls.TrackControlPanels.Select(c => new TrackState() {
                            TrackNumber = c.TrackNumber,
                            SynthId = c.Synth.Id,
                            IsSolo = c.IsSolo,
                            IsMute = c.IsMute
                        }).ToList()


                    };

                    SaveManager.Save(dlg.FileName, saveData);
                }
            }
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var dlg = new OpenFileDialog()) {
                dlg.Filter = "PixSyプロジェクトファイル|*.pixsy";
                dlg.Title = "プロジェクトを開く";

                if (dlg.ShowDialog() == DialogResult.OK) {
                    var saveData = SaveManager.Load(dlg.FileName);

                    if (saveData != null) {
                        _bpm = saveData.GetBpm();
                        _rhythm = saveData.GetRhythm();
                        _frequency = saveData.GetTuningFrequency();

                        bpmToolStripMenuItem.Text = $"{_bpm} BPM";
                        rhythmToolStripMenuItem.Text = $"{_rhythm}拍子";
                        freqToolStripMenuItem.Text = $"{_frequency.ToString("F2")}Hz";

                        Synths.Clear();
                        Synths.AddRange(saveData.GetSynths().Select(s => s.GetSynth()));

                        trackRoll.TrackElements.Clear();
                        saveData.GetTrackNodes().ForEach(n => {
                            trackRoll.AddNewTrackElement(n.Notes, n.TrackNumber, n.StartBar);
                        });

                        trackControls.Clear();
                        saveData.GetTrackStates().OrderBy(s => s.TrackNumber).ToList().ForEach(s => {
                            trackControls.Add(new TrackControlPanel() {
                                Synth = Synths.FirstOrDefault(sy => sy.Id == s.SynthId),
                                IsMute = s.IsMute,
                                IsSolo = s.IsSolo,
                                TrackNumber = s.TrackNumber
                            });
                        });

                        trackControls.Init();

                        trackRoll.Rhythm = _rhythm;

                        Invalidate();
                    }
                }
            }
        }
    }
}
