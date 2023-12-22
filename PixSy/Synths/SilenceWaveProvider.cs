using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.Synths {
    internal class SilenceWaveProvider : IWaveProvider {
        public WaveFormat WaveFormat => new WaveFormat(8000, 8, 1);

        public int Read(byte[] buffer, int offset, int count) {
            for (int n = 0; n < count; n++) buffer[offset++] = 0;
            return count;
        }
    }
}
