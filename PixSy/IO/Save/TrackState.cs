using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.IO.Save {
    [Serializable]
    public class TrackState {
        public int TrackNumber { get; set; }
        public string? SynthId { get; set; }
        public bool IsSolo { get; set; }
        public bool IsMute { get; set; }
    }
}
