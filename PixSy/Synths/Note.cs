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
                return PosToFreq(Pos);
            }
        }

        /// <summary>
        /// C4=0
        /// </summary>
        public int Pos { get; set; }
        public string Pitch {
            get {
                return PosToPitch(Pos);
            }

            set {
                Pos = PitchToPos(value);
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

        private static float PosToFreq(int pos) {
            return (float)(Tuning * Math.Pow(2.0, pos / 12.0));
        }

        private static int PitchToPos(string pitch) {
            throw new NotImplementedException();
        }

        public static string PosToPitch(int pos) {
            int index = pos < 0 ? (-pos % 12 == 0 ? 0 : 12 - (-pos) % 12) : pos % 12;
            int octave = 4 + (pos / 12);
            var pitchName = PitchNames[index] + octave;

            return pitchName;
        }
    }
}
