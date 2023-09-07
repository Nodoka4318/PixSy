using PixSy.Views.Widgets;
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
    public partial class MainView : Form {
        public MainView() {
            InitializeComponent();

            Text = $"{PixSyAppInfo.AppName} ({PixSyAppInfo.Version})";
        }

        private void MainView_Load(object sender, EventArgs e) {

        }
    }
}
