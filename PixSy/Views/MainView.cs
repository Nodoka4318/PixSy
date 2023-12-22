using NAudio.Dmo;
using NAudio.Midi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using PixSy.IO.Save;
using PixSy.IO.Save.Extensions;
using PixSy.Synths;
using PixSy.Views.Widgets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
        public TrackRoll TrackRoll => trackRoll;
        public int Bpm {
            get => _bpm;
            set {
                _bpm = value;
                bpmToolStripMenuItem.Text = $"{_bpm} BPM";

                _playTimer.Interval = (int)(60f / (float)_bpm * 100f);
            }
        }

        public int Rhythm {
            get => _rhythm;
            set {
                _rhythm = value;
                rhythmToolStripMenuItem.Text = $"{_rhythm}拍子";

                trackRoll.Rhythm = _rhythm;
            }
        }

        public float Frequency {
            get => _frequency;
            set {
                _frequency = value;
                freqToolStripMenuItem.Text = $"{_frequency.ToString("F2")}Hz";

                Note.Tuning = _frequency;
            }
        }

        private int _bpm = 120;
        private int _rhythm = 4;
        private float _frequency = 440.00f;
        private Timer _playTimer;
        private List<Note> _playingNotes;
        private ISampleProvider? _playCache;
        private WaveOut _playWaveOut;

        public MainView() {
            InitializeComponent();

            Text = $"{PixSyAppInfo.AppName} ({PixSyAppInfo.Version})";
            trackControls.Width = TrackRoll.TrackHeight * 4;
            trackRoll.TrackControls = trackControls;

            _playTimer = new Timer();
            _playingNotes = new List<Note>();

            _playTimer.Interval = (int)(60f / (float)_bpm * 100f);
            _playTimer.Tick += _playTimer_Tick;

            trackRoll.CurrentPlayHPosDragged += TrackRoll_CurrentPlayHPosUpdated;
            trackControls.ValueChanged += TrackControls_ValueChanged;
        }

        private void TrackControls_ValueChanged(object? sender, EventArgs e) {
            UpdatePlaySample();
        }

        private void TrackRoll_CurrentPlayHPosUpdated(object? sender, EventArgs e) {
            UpdatePlaySample();
        }

        private void _playTimer_Tick(object? sender, EventArgs e) {
            /*
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
                //var sound = note.SoundCache;

                Task.Run(async () => {
                    await Synth.PlaySound(sound);
                });
            }

            _playingNotes = currentNotes;
            */
            trackRoll.CurrentPlayHPos += 0.1f;
        }

        public void UpdatePlaySample() {
            if (_playTimer.Enabled) {
                if (_playWaveOut.PlaybackState == PlaybackState.Playing) {
                    _playWaveOut.Stop();
                }

                InitPlaySample();
                InitPlayWaveOut(TimeSpan.FromSeconds(trackRoll.CurrentPlayHPos * 60f / (float)_bpm));

                Task.Run(async () => {
                    _playWaveOut.Play();
                    while (_playWaveOut.PlaybackState == PlaybackState.Playing) {
                        await Task.Delay(1);
                    }
                });
            }
        }

        private void InitPlaySample() {
            var trackSamples = new List<ISampleProvider>();
            IEnumerable<TrackRoll.TrackElement> tracks;

            if (TrackControls.TrackControlPanels.Exists(p => p.IsSolo)) {
                tracks = trackRoll.TrackElements.Where(e => e.PianoRoll.TrackControl.IsSolo);
            } else {
                tracks = trackRoll.TrackElements.Where(e => !e.PianoRoll.TrackControl.IsMute);
            }

            foreach (var t in tracks) {
                var samples = new List<ISampleProvider>();
                foreach (var n in t.PianoRoll.Notes) {
                    var sample = new OffsetSampleProvider(n.GetSoundSignal(_bpm)) {
                        DelayBy = TimeSpan.FromSeconds((n.StartF + t.HPos * _rhythm) * 60f / (float)_bpm)
                    };
                    samples.Add(sample);
                }

                if (samples.Count > 0) {
                    trackSamples.Add(new MixingSampleProvider(samples));
                }
            }

            if (trackSamples.Count == 0) {
                _playCache = null;
            } else {
                _playCache = new MixingSampleProvider(trackSamples);
            }
        }

        private void InitPlayWaveOut(TimeSpan position) {
            _playWaveOut = new WaveOut();

            if (_playCache != null) {
                _playWaveOut.Init(_playCache.Skip(position)); // TODO: 鋳型の位置も変わってしまう
            } else {
                _playWaveOut.Init(new SilenceWaveProvider());
            }
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e) {
            trackRoll.CurrentPlayHPos = trackRoll.CurrentPlayHPos; // 必要
            trackRoll.TrackElements.ForEach(e => e.PianoRoll.IsPlaying = true);

            if (!_playTimer.Enabled) {
                InitPlaySample();
                InitPlayWaveOut(TimeSpan.FromSeconds(trackRoll.CurrentPlayHPos * 60f / (float)_bpm));

                Task.Run(async () => {
                    _playWaveOut.Play();
                    while (_playWaveOut.PlaybackState == PlaybackState.Playing) {
                        await Task.Delay(1);
                    }
                });

                trackRoll.IsPlaying = true;
                _playTimer.Start(DateTime.Now);
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_playTimer.Enabled) {
                trackRoll.IsPlaying = false;
                _playTimer.Stop();

                if (_playWaveOut.PlaybackState == PlaybackState.Playing) {
                    _playWaveOut.Pause();
                }
            }

            trackRoll.TrackElements.ForEach(e => e.PianoRoll.IsPlaying = false);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e) {
            if (_playTimer.Enabled) {
                trackRoll.IsPlaying = false;
                _playTimer.Stop();

                if (_playWaveOut.PlaybackState == PlaybackState.Playing) {
                    _playWaveOut.Stop();
                }
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
            this.Save();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var dlg = new OpenFileDialog()) {
                dlg.Filter = "PixSyプロジェクトファイル|*.pixsyproj|すべてのファイル|*.*";
                dlg.Title = "プロジェクトを開く";

                if (dlg.ShowDialog() == DialogResult.OK) {
                    var saveData = SaveManager.Load(dlg.FileName);

                    if (saveData != null) {
                        this.LoadSaveData(saveData);

                        Invalidate();
                    }
                }
            }
        }
    }
}
