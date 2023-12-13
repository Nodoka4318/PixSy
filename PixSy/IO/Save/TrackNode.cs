using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.IO.Save {
    public class TrackNode {
        public List<WrapperNote>? Notes { get; set; }
        public int TrackNumber { get; set; }
        public int StartBar { get; set; }
    }
}
