using PixSy.Views;
using PixSy.Views.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static PixSy.Views.Widgets.TrackRoll;

namespace PixSy.IO.Save.Extensions {
    public static class MainViewExtension {
        public static void LoadSaveData(this MainView mainView, ISaveData saveData) {
            mainView.Bpm = saveData.GetBpm();
            mainView.Rhythm = saveData.GetRhythm();
            mainView.Frequency = saveData.GetTuningFrequency();

            mainView.Synths.Clear();
            mainView.Synths.AddRange(saveData.GetSynths().Select(s => s.GetSynth()));

            mainView.TrackControls.Clear();
            saveData.GetTrackStates().OrderBy(s => s.TrackNumber).ToList().ForEach(s => {
                var ctrlPanel = new TrackControlPanel() {
                    Synth = mainView.Synths.FirstOrDefault(sy => sy.Id == s.SynthId),
                    IsMute = s.IsMute,
                    IsSolo = s.IsSolo,
                    TrackNumber = s.TrackNumber,
                    Volume = s.Volume,
                    Pan = s.Pan
                };
                ctrlPanel.ValueChanged += (s, e) => mainView.UpdatePlaySample();

                mainView.TrackControls.Add(ctrlPanel);
            });
            mainView.TrackControls.Init();

            mainView.TrackRoll.TrackElements.Clear();
            saveData.GetTrackNodes().ForEach(n => {
                mainView.TrackRoll.AddNewTrackElement(n.Notes, n.TrackNumber, n.StartBar);
            });

            mainView.TrackRoll.Rhythm = mainView.Rhythm;
        }

        public static void Save(this MainView mainView) {
            using (var dlg = new SaveFileDialog()) {
                dlg.Filter = "PixSyプロジェクトファイル|*.pixsyproj|すべてのファイル|*.*";
                dlg.Title = "名前を付けて保存";

                if (dlg.ShowDialog() == DialogResult.OK) {
                    var saveData = new ImplSaveData() {
                        projectFormatRevision = PixSyAppInfo.ProjectFormatRevision,
                        bpm = mainView.Bpm,
                        rhythm = mainView.Rhythm,
                        tuningFrequency = mainView.Frequency,
                        synths = mainView.Synths.Select(s => WrapperSynth.Wrap(s)).ToList(),
                        trackNodes = mainView.TrackElements.Select(e => new TrackNode() {
                            Notes = e.PianoRoll.Notes.Select(n => WrapperNote.Wrap(n)).ToList(),
                            StartBar = e.StartBar,
                            TrackNumber = e.TrackNumber
                        }).ToList(),

                        trackStates = mainView.TrackControls.TrackControlPanels.Select(c => new TrackState() {
                            TrackNumber = c.TrackNumber,
                            SynthId = c.Synth.Id,
                            IsSolo = c.IsSolo,
                            IsMute = c.IsMute,
                            Volume = c.Volume,
                            Pan = c.Pan
                        }).ToList()
                    };

                    SaveManager.Save(dlg.FileName, saveData);
                }
            }
        }
    }
}
