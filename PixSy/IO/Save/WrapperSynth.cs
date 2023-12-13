using NAudio.Wave.SampleProviders;
using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.IO.Save {
    public class WrapperSynth {
        public string Name { get; set; }
        public string Id { get; set; }
        public SignalGeneratorType Type { get; set; }

        public static WrapperSynth Wrap(Synth synth) {
            return new WrapperSynth {
                Name = synth.Name,
                Id = synth.Id,
                Type = synth.Type
            };
        }

        public Synth GetSynth() {
            return new Synth(Name, Id) {
                Type = Type
            };
        }
    }
}
