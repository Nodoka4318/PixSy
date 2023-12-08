using PixSy.Synths;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixSy.Views.Widgets {
    public partial class PianoRoll : UserControl {
        public enum EditMode {
            Select, Pen
        }

        public int Rhythm { get => 4; } // 拍子
        public int NextNoteId => _notes.Count == 0 ? 0 : _notes.Select(n => n.Id).Max() + 1;
        public float CurrentPlayHPos {
            get => _currentPlayHPos;
            set {
                _currentPlayHPos = value;

                if (_currentPlayHPos > _hPos + Width / (float)BeatWidth) {
                    var hPos = _hPos + Width / BeatWidth;
                    SetHPos(hPos);

                    if (hPos + 16 > hScrollBar.Maximum) {
                        hScrollBar.Maximum = hPos + 16;
                    }

                    hScrollBar.Value = hPos;
                }

                Invalidate();
            }
        }

        public List<Note> Notes => _notes;
        public EditMode Mode { get; set; }

        public bool IsPlaying { get; set; }
        public string Title { get; set; }
        public int Id { get; private set; }
        public int MinimumBar { get; set; } // TODO: TrackRollとの整合性を取る

        private List<Note> _notes;
        private int _vPos; // 一番上のキーの高さ. 0=C5(midC=C4)
        private int _hPos; // 横スクロールの現在位置 (0=一番左が一拍目)
        private Note? _selectedNote; // 選択中の音符
        private bool _isDragging = false; // 音符をドラッグ中かどうか
        private bool _isResizing = false; // 音符の長さを変更中かどうか
        private byte _draggingEdge = 0; // 0: なし, 1: 左, 2: 右
        private float _dragOffset; // ドラッグ開始時の音符とマウスの差分
        private float _currentPlayHPos; // 再生中の位置

        static readonly Color WhiteKeyColor = Color.Gainsboro;
        static readonly Color BlackKeyColor = Color.DarkGray;
        static readonly byte[] KeyboardSequence = {
            0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0 // 0が白鍵, 1が黒鍵
        };
        const int KeyHeight = 13; // 一つのキーの縦の長さ
        const int BeatWidth = 50; // 一拍の長さ

        public PianoRoll() {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            AutoScroll = false;

            vScrollBar.ValueChanged += VScrollBar_ValueChanged;
            vScrollBar.Minimum = -49 - Height / KeyHeight;
            vScrollBar.Maximum = 39;

            hScrollBar.ValueChanged += HScrollBar_ValueChanged;
            hScrollBar.Minimum = 0;
            hScrollBar.Maximum = 32;

            MouseClick += PianoRoll_Click;

            MouseDown += PianoRoll_MouseDown;
            MouseUp += PianoRoll_MouseUp;
            MouseMove += PianoRoll_MouseMove;

            _notes = new List<Note>();
            _vPos = 12;
            _hPos = 0;

            MinimumBar = 1;

            _currentPlayHPos = 0f;
            Mode = EditMode.Select;
        }

        private void PianoRoll_MouseMove(object? sender, MouseEventArgs e) {
            if (_selectedNote == null) {
                return;
            }

            var cPoint = PointToClient(Cursor.Position);
            var pointHPos = _hPos + cPoint.X / (float)BeatWidth;
            var pointVPos = _vPos - cPoint.Y / KeyHeight;
            var len = _selectedNote.Length; // 先に長さを取得しておく

            if (_isResizing) {
                if (_draggingEdge == 1) {
                    var startF = pointHPos - _dragOffset;
                    _selectedNote.StartF = startF - (startF % 0.1f);
                } else if (_draggingEdge == 2) {
                    _selectedNote.EndF = pointHPos - (pointHPos % 0.1f);
                }

                if (_selectedNote.StartF < 0) {
                    _selectedNote.StartF = 0;
                }

                if (_selectedNote.Length <= 0.1f) {
                    _selectedNote.EndF = _selectedNote.StartF + 0.2f;
                }

                Invalidate();
            } else if (_isDragging) {
                var startF = pointHPos - _dragOffset;

                if (startF < 0) {
                    startF = 0;
                }

                _selectedNote.VPos = pointVPos;
                _selectedNote.StartF = startF - (startF % 0.1f);
                _selectedNote.EndF = _selectedNote.StartF + len;

                Invalidate();
            }
        }

        private void PianoRoll_MouseUp(object? sender, MouseEventArgs e) {
            _isDragging = false;
            _isResizing = false;
        }

        private void PianoRoll_MouseDown(object? sender, MouseEventArgs e) {
            var cPoint = PointToClient(Cursor.Position);
            Note? note;

            if (e.Button == MouseButtons.Right) {
                if (Mode == EditMode.Pen) {
                    if (TryGetNoteAt(cPoint, out note)) {
                        _notes.Remove(note);
                        Invalidate();
                    }
                }
            } else if (e.Button == MouseButtons.Left) {
                if (TryGetNoteAt(cPoint, out note)) {
                    _selectedNote = note;
                    _isDragging = true;

                    var pointHPos = _hPos + cPoint.X / (float)BeatWidth;
                    _dragOffset = pointHPos - _selectedNote.StartF;

                    _draggingEdge = _dragOffset < 0.1f ? (byte)1 : _selectedNote.Length - _dragOffset < 0.1f ? (byte)2 : (byte)0;
                    _isResizing = _draggingEdge != 0;

                    Invalidate();
                } else if (Mode == EditMode.Pen) {
                    var pointHPos = _hPos + cPoint.X / (float)BeatWidth;
                    var pointVPos = _vPos - cPoint.Y / KeyHeight;
                    _selectedNote = AddNewNote(pointVPos, pointHPos);

                    Invalidate();
                } else {
                    _selectedNote = null;

                    Invalidate();
                }
            }
        }

        private void PianoRoll_Click(object? sender, MouseEventArgs e) {

        }

        private void HScrollBar_ValueChanged(object? sender, EventArgs e) {
            SetHPos(hScrollBar.Value);
        }

        private void VScrollBar_ValueChanged(object? sender, EventArgs e) {
            SetVPos(-vScrollBar.Value);
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);

            DrawKeyboard(e.Graphics);
            DrawNotes(e.Graphics);
            DrawBeatLines(e.Graphics);

            if (IsPlaying) {
                DrawPlayLine(e.Graphics);
            }
        }

        private void DrawKeyboard(Graphics graphics) {
            // var offset = _pos >= 0 ? KeyHeight - (Math.Abs(_pos) % 20) : (Math.Abs(_pos) % 20);
            var currentPos = _vPos; // 音階と座標の高さは逆だからマイナス

            for (int i = 0; i <= Height;) {
                var keyType = VPosToKeyType(currentPos);
                var keyColor = keyType == 0 ? WhiteKeyColor : BlackKeyColor;

                if (currentPos % 12 == 0) {
                    keyColor = Color.WhiteSmoke; // Cだけ白にする
                }

                graphics.FillRectangle(new SolidBrush(keyColor), new Rectangle(0, i, Width, KeyHeight));
                graphics.DrawLine(new Pen(BlackKeyColor), 0, i, Width, i); // 境界線
                graphics.DrawString(Note.VPosToPitch(currentPos), new Font(FontFamily.GenericSansSerif, KeyHeight - 4), currentPos == 0 ? Brushes.Red : Brushes.Black, 0, i); // 音階名

                currentPos--;
                i += KeyHeight;
            }
        }

        private void DrawBeatLines(Graphics graphics) {
            var currentPos = _hPos;

            for (int i = 0; i * BeatWidth <= Width; i++) {
                var dashStyle = DashStyle.Dot;

                if (currentPos % Rhythm == 0) {
                    dashStyle = DashStyle.Solid; // 小節の頭は実線
                    graphics.DrawString($"{currentPos / Rhythm + 1}", new Font(FontFamily.GenericSansSerif, KeyHeight - 4), Brushes.Black, i * BeatWidth, 0); // 小節番号
                }

                graphics.DrawLine(new Pen(Brushes.Black) {
                    DashStyle = dashStyle
                }, i * BeatWidth, 0, i * BeatWidth, Height);

                currentPos++;
            }
        }

        private void DrawPlayLine(Graphics graphics) {
            var playLineX = (int)Math.Round(BeatWidth * (_currentPlayHPos - _hPos));
            graphics.DrawLine(new Pen(Brushes.Red) {
                DashStyle = DashStyle.Solid
            }, playLineX, 0, playLineX, Height);
        }

        private void DrawNotes(Graphics graphics) {
            foreach (var note in _notes) {
                if (_hPos >= note.EndF) {
                    continue;
                }

                if (_vPos < note.VPos) {
                    continue;
                }

                var noteX = (int)Math.Round(BeatWidth * (note.StartF - _hPos));
                var noteY = (int)Math.Round((double)KeyHeight * (_vPos - note.VPos));
                var noteWidth = (int)Math.Round(BeatWidth * (note.EndF - note.StartF));
                var noteColor = Brushes.Aqua;

                if (_hPos >= note.StartF) {
                    noteX = 0;
                    noteWidth -= (int)Math.Round(BeatWidth * (_hPos - note.StartF));
                }

                if (note.Equals(_selectedNote)) {
                    noteColor = Brushes.LightBlue;
                }

                if (note.IsPlaying) {
                    noteColor = Brushes.LightPink;
                }

                graphics.FillRectangle(noteColor, new Rectangle(noteX, noteY, noteWidth, KeyHeight)); // TODO: Noteの色
            }
        }

        public bool TryGetNoteAt(Point point, out Note? note) {
            var pointHPos = _hPos + point.X / (float)BeatWidth;
            var pointVPos = _vPos - point.Y / KeyHeight;

            var condition = _notes.Where(n => n.VPos == pointVPos && n.StartF <= pointHPos && pointHPos <= n.EndF);

            if (condition.Any()) {
                note = condition.First();
                return true;
            } else {
                note = null;
                return false;
            }
        }

        public void GoUp(int offset) {
            _vPos += offset;
            Invalidate();
        }

        public void GoDown(int offset) {
            _vPos -= offset;
            Invalidate();
        }

        public void SetVPos(int vPos) {
            _vPos = vPos;
            Invalidate();
        }

        public void SetHPos(int hPos) {
            _hPos = hPos;
            Invalidate();
        }

        public void AddNote(Note note) {
            _notes.Add(note);
        }

        public Note AddNewNote(int vPos, float hPos) {
            var newNote = new Note(vPos, hPos, hPos + 1, NextNoteId, this);
            _notes.Add(newNote);

            if (hPos + 16 > hScrollBar.Maximum) {
                hScrollBar.Maximum = (int)Math.Ceiling(hPos + 16);
            }

            return newNote;
        }

        public void AddNotes(IEnumerable<Note> notes) {
            _notes.AddRange(notes);
        }

        public void SetNotes(IEnumerable<Note> notes) {
            _notes = notes.ToList();
        }

        // TODO: 消す予定
        public void PlaySync() {
            _notes.OrderBy(n => n.StartF).ToList().ForEach(n => n.PlaySoundSync());
        }

        public int GetHLength() {
            var max = _notes.Count == 0 ? 1 : _notes.Select(n => n.EndF).Max();
            return (int) Math.Ceiling(max / Rhythm);
        }

        private static byte VPosToKeyType(int vPos) {
            return KeyboardSequence[vPos < 0 ? (-vPos % 12 == 0 ? 0 : 12 - (-vPos) % 12) : vPos % 12]; // 12は音階の数
        }
    }
}
