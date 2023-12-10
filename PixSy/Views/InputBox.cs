using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixSy.Views {
    public partial class InputBox : Form {
        public string InputText {
            get {
                return _inputText;
            }
            set {
                textBox.Text = value;
                _inputText = value;
            }
        }

        private string _inputText;

        public InputBox() {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e) {
            _inputText = textBox.Text;
            Close();
        }
    }
}
