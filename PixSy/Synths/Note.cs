using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.Synths {
    public class Note {
        public float Length { get; set; }
        public float Frequency { 
            get {
                return VPosToFreq(VPos);
            }
        }

        /// <summary>
        /// C4=0
        /// </summary>
        public int VPos { get; set; }
        public string Pitch {
            get {
                return VPosToPitch(VPos);
            }

            set {
                VPos = PitchToVPos(value);
            }
        }

        public float StartF { get; set; }
        public float EndF { get; set; }

        public static readonly string[] PitchNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        const float Tuning = 440f;

        public Note(string pitch, float length, float start, float end) {

        }

        public Note(float pos, float length, float start, float end) {

        }

        private static float VPosToFreq(int vPos) {
            return (float)(Tuning * Math.Pow(2.0, vPos / 12.0));
        }

        private static int PitchToVPos(string pitch) {
            throw new NotImplementedException();
        }

        public static string VPosToPitch(int vPos) {
            int index = vPos < 0 ? (-vPos % 12 == 0 ? 0 : 12 - (-vPos) % 12) : vPos % 12;
            int octave = 4 + (vPos / 12);
            var pitchName = PitchNames[index] + octave;

            return pitchName;
        }
    }
}
