using NAudio.Wave.SampleProviders;
using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixSy.Views.Widgets {
    public partial class SynthsPanel : UserControl {
        public List<Synth> Synths => _synths;

        private List<Synth> _synths;

        public SynthsPanel() {
            InitializeComponent();

            _synths = new List<Synth> { // 仮置き
                new Synth() { Name = "Sin", Type = SignalGeneratorType.Sin },
                new Synth() { Name = "Square", Type = SignalGeneratorType.Square },
                new Synth() { Name = "Triangle", Type = SignalGeneratorType.Triangle },
                new Synth() { Name = "Saw", Type = SignalGeneratorType.SawTooth }
            };

            foreach (var sy in _synths) {
                synthsListBox.Items.Add(sy);
            }
        }

        private void editButton_Click(object sender, EventArgs e) {

        }

        private void createButton_Click(object sender, EventArgs e) {

        }

        private void deleteButton_Click(object sender, EventArgs e) {

        }
    }
}
