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
            public int Id { get; set; }
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

            public TrackElement(PianoRoll pianoRoll, int trackNumber, int startBar, int id) {
                PianoRoll = pianoRoll;
                TrackNumber = trackNumber;
                StartBar = startBar;
                Id = id;
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

        public bool IsPlaying { get; set; }
        public int Rhythm {
            get => _rhythm;
            set {
                _rhythm = value;
                _trackElements.ForEach(e => e.PianoRoll.Rhythm = value);
            }
        }

        public List<TrackElement> TrackElements => _trackElements;
        public float CurrentPlayHPos {
            get => _currentPlayHPos;
            set {
                _currentPlayHPos = value;

                // TODO:
                /*
                if (_currentPlayHPos / Rhythm > _hPos + Width / BarWidth) {
                    var hPos = _hPos + Width / BarWidth;
                    if (hPos + 4 > hScrollBar.Maximum) {
                        hScrollBar.Maximum = hPos + 4;
                    }

                    _hPos = hPos;
                    hScrollBar.Value = hPos;
                }
                */

                _trackElements.ForEach(e => e.PianoRoll.CurrentPlayHPos = value - e.HPos * Rhythm);
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
        private float _currentPlayHPos; // 再生中の位置
        private int _rhythm;

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
            _currentPlayHPos = 0;
            _rhythm = 4;

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
            } else {
                if (e.Button == MouseButtons.Left) {
                    var pointHPos = _hPos + cPoint.X / (float) BarWidth * Rhythm;
                    _currentPlayHPos = pointHPos < 0 ? 0 : pointHPos - (pointHPos % 0.1f);

                    Invalidate();
                }
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

                    var pointHPos = _hPos + cPoint.X / (float) BarWidth * Rhythm;
                    _currentPlayHPos = pointHPos < 0 ? 0 : pointHPos - (pointHPos % 0.1f);

                    Invalidate();
                }
            } else if (e.Button == MouseButtons.Right) {
                if (TryGetTrackElementAt(cPoint, out elem)) {
                    var menu = new ContextMenuStrip();
                    var editMenuElement = new ToolStripMenuItem("編集");
                    var deleteMenuElement = new ToolStripMenuItem("削除");

                    editMenuElement.Click += (s, e) => {
                        if (elem.PianoRoll.IsDisposed) {
                            var notes = elem.PianoRoll.Notes;

                            elem.PianoRoll = new PianoRoll();
                            elem.PianoRoll.SetNotes(notes);
                            elem.PianoRoll.Rhythm = _rhythm;
                        }

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
                        pr.Rhythm = _rhythm;

                        var newId = _trackElements.Count == 0 ? 0 : _trackElements.Select(e => e.Id).Max() + 1;

                        var newElem = new TrackElement(pr, _vPos + cPoint.Y / TrackHeight + 1, _hPos + cPoint.X / BarWidth + 1, newId);

                        _trackElements.Add(newElem);
                        Invalidate();

                        var dialog = new PianoRollView(pr);
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
            DrawPlayLine(e.Graphics);
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

                if (_selectedElement != null && _selectedElement.Id == te.Id) {
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

        private void DrawPlayLine(Graphics g) {
            var playLineX = (int)Math.Round((BarWidth / Rhythm) * (_currentPlayHPos - _hPos));
            g.DrawLine(new Pen(Brushes.Red) {
                DashStyle = DashStyle.Solid
            }, playLineX, 0, playLineX, Height);
        }

        public bool TryGetTrackElementAt(Point point, out TrackElement? elem) {
            var pointHPos = _hPos + point.X / BarWidth;
            var pointVPos = _vPos + point.Y / TrackHeight;

            var condition = _trackElements.Where(e => e.VPos == pointVPos && e.HPos <= pointHPos && pointHPos <= e.HPos + e.PianoRoll.GetHLength() - 1);

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
