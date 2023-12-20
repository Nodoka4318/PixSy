using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.Synths {
    internal class MixingUtil {
        public static WaveOut MixSamples(params ISampleProvider[] samples) {
            var mixer = new MixingSampleProvider(samples);
            var wav = new WaveOut();
            wav.Init(mixer);

            return wav;
        }
    }
}
