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
        public PianoRoll? Parent { get; private set; }

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

        private int _id;

        public static readonly string[] PitchNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        const float Tuning = 440f;

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
            return (float) (Tuning * Math.Pow(2.0, vPos / 12.0));
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

        public override bool Equals(object? obj) {
            if (obj is Note note) {
                return note.Id == Id;
            }
            return false;
        }

        public void PlaySoundSync() {
            var mStrm = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(mStrm);
            UInt16 volume = 16383;

            const double TAU = 2 * Math.PI;
            int formatChunkSize = 16;
            int headerSize = 8;
            short formatType = 1;
            short tracks = 1;
            int samplesPerSecond = 44100;
            short bitsPerSample = 16;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int samples = (int)(samplesPerSecond * (EndF - StartF));
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            // var encoding = new System.Text.UTF8Encoding();
            writer.Write(0x46464952); // = encoding.GetBytes("RIFF")
            writer.Write(fileSize);
            writer.Write(0x45564157); // = encoding.GetBytes("WAVE")
            writer.Write(0x20746D66); // = encoding.GetBytes("fmt ")
            writer.Write(formatChunkSize);
            writer.Write(formatType);
            writer.Write(tracks);
            writer.Write(samplesPerSecond);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(bitsPerSample);
            writer.Write(0x61746164); // = encoding.GetBytes("data")
            writer.Write(dataChunkSize);
            {
                double theta = Frequency * TAU / (double)samplesPerSecond;
                // 'volume' is UInt16 with range 0 thru Uint16.MaxValue ( = 65 535)
                // we need 'amp' to have the range of 0 thru Int16.MaxValue ( = 32 767)
                double amp = volume >> 2; // so we simply set amp = volume / 2
                for (int step = 0; step < samples; step++) {
                    short s = (short)(amp * Math.Sin(theta * (double)step));
                    writer.Write(s);
                }
            }

            mStrm.Seek(0, SeekOrigin.Begin);
            new System.Media.SoundPlayer(mStrm).PlaySync();
            writer.Close();
            mStrm.Close();
        }
    }
}
