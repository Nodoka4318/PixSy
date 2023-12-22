using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PixSy.IO.Save {
    [XmlRoot("PixSyProj")]
    public class ImplSaveData : ISaveData {
        public int projectFormatRevision;
        public int bpm = 120;
        public string? id = Guid.Empty.ToString();
        public string? projectName = "MySong";
        public int rhythm = 4;
        public List<WrapperSynth>? synths;
        public List<TrackNode>? trackNodes;
        public List<TrackState>? trackStates;
        public float tuningFrequency = 440f;

        public int GetBpm() {
            return bpm;
        }

        public string? GetId() {
            return id;
        }

        public int GetProjectFormatRevision() {
            return projectFormatRevision;
        }

        public string? GetProjectName() {
            return projectName;
        }

        public int GetRhythm() {
            return rhythm;
        }

        public List<WrapperSynth>? GetSynths() {
            return synths;
        }

        public List<TrackNode>? GetTrackNodes() {
            return trackNodes;
        }

        public List<TrackState>? GetTrackStates() {
            return trackStates;
        }

        public float GetTuningFrequency() {
            return tuningFrequency;
        }
    }
}
