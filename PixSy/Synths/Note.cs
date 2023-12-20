using NAudio.Wave;
using PixSy.Views.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.Synths {
#pragma warning disable CS0659
    public class Note {
        public float Length => EndF - StartF;
        public float Frequency {
            get {
                return VPosToFreq(VPos);
            }
        }
        public int Id { get => _id; }
        public bool IsPlaying { get; set; }
        public PianoRoll? Parent { get; set; }
        public ISampleProvider SoundCache { get; set; }

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

        public static float Tuning = 440f; // A4

        private int _id;

        public static readonly string[] PitchNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        public Note(string pitch, float length, float start, float end, PianoRoll? parent = null) {
            throw new NotImplementedException();
        }

        public Note(int pos, float start, float end, int id, PianoRoll? parent = null) {
            VPos = pos;
            StartF = start;
            EndF = end;
            Parent = parent;
            _id = id;
        }

        private static float VPosToFreq(int vPos) {
            return (float)(Tuning * Math.Pow(2.0, (vPos - 9) / 12.0)); // C4=0 so -9 to get A4
        }

        private static int PitchToVPos(string pitch) {
            throw new NotImplementedException();
        }

        public static string VPosToPitch(int vPos) {
            int index = vPos < 0 ? (-vPos % 12 == 0 ? 0 : 12 - (-vPos) % 12) : vPos % 12;
            int octave = 4 + ((vPos < 0 ? vPos - 11 : vPos) + 3) / 12; // vPos < 0 のときは-11

            var pitchName = PitchNames[index] + octave;

            return pitchName;
        }

        public override bool Equals(object? obj) {
            if (obj is Note note) {
                return note.Id == Id;
            }
            return false;
        }

        public byte[] GetSoundByte(float bpm) {
            var synth = Parent?.Synth;

            if (synth != null) {
                var freq = Frequency;
                var sec = Length / bpm * 60f;

                return synth.GetSoundByte(freq, sec);
            }

            throw new NullReferenceException("Synth is null");
        }

        public ISampleProvider GetSoundSignal(float bpm) {
            var synth = Parent?.Synth;

            if (synth != null) {
                var freq = Frequency;
                var sec = Length / bpm * 60f;

                return synth.GetSoundSignal(freq, sec);
            }

            throw new NullReferenceException("Synth is null");
        }
    }
}
