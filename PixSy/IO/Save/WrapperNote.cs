using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.IO.Save {
    public class WrapperNote {
        public int VPos { get; set; }
        public float StartF { get; set; }
        public float EndF { get; set; }

        public static WrapperNote Wrap(Note note) {
            return new WrapperNote() {
                VPos = note.VPos,
                StartF = note.StartF,
                EndF = note.EndF
            };
        }

        public Note GetNote(int id) {
            return new Note(VPos, StartF, EndF, id);
        }
    }
}
