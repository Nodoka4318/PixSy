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

namespace PixSy.Views {
    public partial class ListSelectBox<T> : Form {
        public T Selected { get; set; }

        public ListSelectBox() {
            InitializeComponent();
        }

        public void SetItems(IEnumerable<T> items) {
            listBox.Items.Clear();

            foreach (var item in items) {
                listBox.Items.Add(item);
            }
        }

        private void submitButton_Click(object sender, EventArgs e) {
            var selected = listBox.SelectedItem;

            if (selected is T s) {
                Selected = s;
                DialogResult = DialogResult.OK;   
            }

            Close();
        }
    }
}
