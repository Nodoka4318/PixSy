using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixSy.Views.Widgets {
    public partial class TrackRoll : UserControl {
        public class TrackElement {
            public PianoRoll PianoRoll { get; set; }
            public int TrackNumber { get; set; }
            public int StartBar { get; set; }
            public int VPos {
                get => TrackNumber - 1; // 座標ではなくトラック番号で管理したいので
                set {
                    TrackNumber = value + 1;
                }
            }

            public int HPos {
                get => StartBar - 1;
                set {
                    StartBar = value + 1;
                }
            }

            public TrackElement(PianoRoll pianoRoll, int trackNumber, int startBar) {
                PianoRoll = pianoRoll;
                TrackNumber = trackNumber;
                StartBar = startBar;
            }
        }

        public int HPos {
            get => _hPos;
            set {
                _hPos = value;
                hScrollBar.Value = value;

                Invalidate();
            }
        }

        public int VPos {
            get => _vPos;
            set {
                _vPos = value;
                vScrollBar.Value = value;

                Invalidate();
            }
        }

        private List<TrackElement> _trackElements;
        private int _hPos;
        private int _vPos; // 一番上が0
        private bool _isDragging = false;
        private float _dragOffset;
        private TrackElement? _selectedElement;
        private System.Windows.Forms.Timer _mainTimer;

        private readonly int TrackHeight = 80; // トラックの高さ
        private readonly int BarWidth = 120; // 小節の幅
        private readonly Brush[] TrackElementColors = new Brush[] {
            Brushes.Aquamarine,
            Brushes.Coral,
            Brushes.LightGreen,
            Brushes.LightPink,
            Brushes.LightSkyBlue,
            Brushes.LightYellow,
            Brushes.PaleTurquoise,
            Brushes.Wheat,
        };

        public TrackRoll() {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            AutoScroll = false;
            BackColor = Color.LightGray;

            _trackElements = new List<TrackElement>();

            vScrollBar.ValueChanged += VScrollBar_ValueChanged;
            hScrollBar.ValueChanged += HScrollBar_ValueChanged;

            MouseDown += TrackRoll_MouseDown;
            MouseMove += TrackRoll_MouseMove;
            MouseUp += TrackRoll_MouseUp;

            _mainTimer = new System.Windows.Forms.Timer();
            _mainTimer.Interval = 100;
            _mainTimer.Tick += (s, e) => {
                Invalidate();
            };
            _mainTimer.Start();

            // デバッグ用
            var pr = new PianoRoll();
            pr.AddNewNote(0, 0);
            pr.AddNewNote(4, 20);
            pr.AddNewNote(7, 25);

            _trackElements.Add(new TrackElement(pr, 4, 1));
        }

        private void TrackRoll_MouseUp(object? sender, MouseEventArgs e) {
            _isDragging = false;
        }

        private void TrackRoll_MouseMove(object? sender, MouseEventArgs e) {
            var cPoint = PointToClient(Cursor.Position);

            if (_isDragging) {
                if (_selectedElement == null) {
                    return;
                }

                var pointHPos = _hPos + cPoint.X / BarWidth;
                var pointVPos = _vPos + cPoint.Y / TrackHeight;

                var newHPos = pointHPos - _dragOffset;

                if (newHPos < 0) {
                    newHPos = 0;
                }

                _selectedElement.HPos = (int)newHPos;
                _selectedElement.VPos = pointVPos;

                Invalidate();
            }
        }

        private void TrackRoll_MouseDown(object? sender, MouseEventArgs e) {
            var cPoint = PointToClient(Cursor.Position);
            TrackElement? elem;

            if (e.Button == MouseButtons.Left) {
                if (TryGetTrackElementAt(cPoint, out elem)) {
                    _isDragging = true;

                    var pointHPos = _hPos + cPoint.X / BarWidth;
                    _dragOffset = pointHPos - elem.HPos;

                    _selectedElement = elem;

                    Invalidate();
                } else {
                    _selectedElement = null;
                    Invalidate();
                }
            } else if (e.Button == MouseButtons.Right) {
                if (TryGetTrackElementAt(cPoint, out elem)) {
                    var menu = new ContextMenuStrip();
                    var editMenuElement = new ToolStripMenuItem("編集");
                    var deleteMenuElement = new ToolStripMenuItem("削除");

                    editMenuElement.Click += (s, e) => {
                        var dialog = new PianoRollView(elem.PianoRoll);
                        dialog.Show();
                    };

                    deleteMenuElement.Click += (s, e) => {
                        _trackElements.Remove(elem);
                        Invalidate();
                    };

                    menu.Items.Add(editMenuElement);
                    menu.Items.Add(deleteMenuElement);

                    menu.Show(this, cPoint);
                } else {
                    var menu = new ContextMenuStrip();
                    var addMenuElement = new ToolStripMenuItem("追加");

                    addMenuElement.Click += (s, e) => {
                        var pr = new PianoRoll();
                        var dialog = new PianoRollView(pr);
                        var newElem = new TrackElement(pr, _vPos + cPoint.Y / TrackHeight + 1, _hPos + cPoint.X / BarWidth + 1);
                        
                        _trackElements.Add(newElem);
                        Invalidate();

                        dialog.Show();
                    };

                    menu.Items.Add(addMenuElement);

                    menu.Show(this, cPoint);
                }
            }
        }

        private void HScrollBar_ValueChanged(object? sender, EventArgs e) {
            HPos = hScrollBar.Value;
        }

        private void VScrollBar_ValueChanged(object? sender, EventArgs e) {
            VPos = vScrollBar.Value;
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);

            DrawTrackElements(e.Graphics);
            DrawTrackGrids(e.Graphics);
            DrawBarGrids(e.Graphics);
        }

        private void DrawTrackGrids(Graphics g) {
            var pen = new Pen(Color.Black, 1);
            var fontSize = 10;
            for (int i = 0; i * TrackHeight <= Height; i++) {
                g.DrawLine(pen, 0, i * TrackHeight, Width, i * TrackHeight);
                g.DrawString($"Track {_vPos + i + 1}", new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, 0, i * TrackHeight + TrackHeight - fontSize - 5); // 小節番号
            }
        }

        private void DrawTrackElements(Graphics g) {
            foreach (var te in _trackElements) {
                if (te.HPos + te.PianoRoll.GetHLength() <= _hPos) {
                    continue;
                }

                if (te.VPos < _vPos) {
                    continue;
                }

                var elemX = (te.HPos - _hPos) * BarWidth;
                var elemY = (te.VPos - _vPos) * TrackHeight;
                var elemWidth = te.PianoRoll.GetHLength() * BarWidth;

                if (te.HPos < _hPos) {
                    elemX = 0;
                    elemWidth -= (_hPos - te.HPos) * BarWidth;
                }

                var brush = TrackElementColors[te.VPos % TrackElementColors.Length];

                if (_selectedElement != null && _selectedElement.PianoRoll.Id == te.PianoRoll.Id) {
                    var c = ((SolidBrush)brush).Color;
                    brush = new SolidBrush(Color.FromArgb(128, c.R, c.G, c.B));
                }

                g.FillRectangle(brush, elemX, elemY, elemWidth, TrackHeight);
            }
        }

        private void DrawBarGrids(Graphics g) {
            var pen = new Pen(Color.Black, 1) {
                DashStyle = DashStyle.DashDotDot
            };

            for (int i = 0; i * BarWidth < Width; i++) {
                g.DrawLine(pen, i * BarWidth, 0, i * BarWidth, Height);
                g.DrawString($"{_hPos + i + 1}", new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, i * BarWidth, 0); // 小節番号
            }
        }

        public bool TryGetTrackElementAt(Point point, out TrackElement? elem) {
            var pointHPos = _hPos + point.X / BarWidth;
            var pointVPos = _vPos + point.Y / TrackHeight;

            var condition = _trackElements.Where(e => e.VPos == pointVPos && e.HPos <= pointHPos && pointHPos <= e.HPos + e.PianoRoll.GetHLength());

            if (condition.Any()) {
                elem = condition.First();
                return true;
            } else {
                elem = null;
                return false;
            }
        }
    }
}
