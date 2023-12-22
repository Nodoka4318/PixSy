using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.IO.Save {
    public interface ISaveData {
        int GetProjectFormatRevision();
        string? GetProjectName();
        string? GetId();
        int GetBpm();
        int GetRhythm();
        float GetTuningFrequency();
        List<TrackNode>? GetTrackNodes();
        List<WrapperSynth>? GetSynths();
        List<TrackState>? GetTrackStates();
    }
}
