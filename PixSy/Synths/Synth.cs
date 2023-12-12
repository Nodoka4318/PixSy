using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.Synths {
    public class Synth {
        public static readonly Synth DefaultSynth = new Synth("Default");

        public string Name { get; set; }
        public string Id { get; private set; }
        public SignalGeneratorType Type { get; set; } // とりあえず仮

        const int _adjustMilSec = 0; // adjust miliseconds

        public Synth() {
            Name = "MySynth";
            Id = Guid.NewGuid().ToString();

            Type = SignalGeneratorType.Sin;
        }

        public Synth(string name) : this() {
            Name = name;
        }

        public Synth(string name, string id) : this(name) {
            Id = id;
        }

        public static void PlaySoundByte(byte[] soundByte) {
            WaveOut waveOut = new WaveOut();
            waveOut.Init(new RawSourceWaveStream(new MemoryStream(soundByte), new WaveFormat(44100, 8, 1)));
            waveOut.Volume = 0.5f;
            waveOut.Play();
        }

        public byte[] GetSoundByte(float frequency, float seconds) {
            int sampleRate = 44100; // sample rate of 44.1 kHz

            int numSamples = (int)(sampleRate * seconds);
            byte[] soundByte = new byte[numSamples];

            double amplitude = 0.25 * sbyte.MaxValue; // 8-bit signed PCM
            double angularFrequency = frequency * 2 * Math.PI / sampleRate;

            for (int i = 0; i < numSamples; i++) {
                double time = (double)i / sampleRate;
                double value = amplitude * Math.Sin(angularFrequency * time);
                soundByte[i] = (byte)(value + 128); // convert to unsigned byte
            }

            return soundByte;
        }

        public ISampleProvider GetSoundSignal(float frequency, float second) {
            return new SignalGenerator() {
                Gain = 0.2,
                Frequency = frequency,
                Type = Type
            }.Take(TimeSpan.FromMilliseconds((int) (second * 1000) + _adjustMilSec));
        }

        public static async Task PlaySound(ISampleProvider sound) {
            using (var wo = new WaveOutEvent()) {
                wo.Init(sound);
                wo.Play();

                while (wo.PlaybackState == PlaybackState.Playing) {
                    await Task.Delay(100);
                }
            }
        }

        public override string ToString() {
            return Name;
        }
    }
}
