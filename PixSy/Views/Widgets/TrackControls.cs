using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixSy.Views.Widgets {
    public partial class TrackControls : UserControl {
        public int VPos {
            get => _vPos;
            set {
                _vPos = value;
                Init();
            }
        }

        public List<TrackControlPanel> TrackControlPanels => _trackControlPanels;

        private int _vPos = 0;
        private List<TrackControlPanel> _trackControlPanels;

        public TrackControls() {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            AutoScroll = false;

            _trackControlPanels = new List<TrackControlPanel>();
        }

        private void Init() {
            Controls.Clear();

            var trackHeight = TrackRoll.TrackHeight;

            for (int i = 0; ; i++) {
                TrackControlPanel panel;
                var currentTrackNumber = i + _vPos + 1;
                var match = _trackControlPanels.Where(p => p.TrackNumber == currentTrackNumber).ToList();

                if (match.Count == 0) {
                    panel = new TrackControlPanel();

                    panel.Location = new Point(0, i * trackHeight);
                    panel.TrackNumber = i + _vPos + 1;

                    Controls.Add(panel);
                    _trackControlPanels.Add(panel);
                } else {
                    panel = match[0];
                    
                    panel.Location = new Point(0, i * trackHeight);
                    Controls.Add(panel);
                }

                if (i * trackHeight > Height) { // 後ろでやる
                    break;
                }
            }
        }
    }
}
