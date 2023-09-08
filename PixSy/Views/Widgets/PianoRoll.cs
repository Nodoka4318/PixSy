using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixSy.Views.Widgets {
    public partial class PianoRoll : UserControl {
        public int Rhythm { get => 4; } // 拍子

        private List<Note> _notes;
        private int _vPos; // 一番上のキーの高さ. 0=C5(midC=C4)
        private int _hPos; // 横スクロールの現在位置 (0=一番左が一拍目)

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

            _notes = new List<Note>();
            _vPos = 12;
            _hPos = 0;
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
            DrawBeatLines(e.Graphics);
            DrawNotes(e.Graphics);
        }

        private void DrawKeyboard(Graphics graphics) {
            // var offset = _pos >= 0 ? KeyHeight - (Math.Abs(_pos) % 20) : (Math.Abs(_pos) % 20);
            var currentPos = _vPos; // 音階と座標の高さは逆だからマイナス

            for (int i = 0; i <= Height; ) {
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

        private void DrawNotes(Graphics graphics) {

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

        public void AddNotes(IEnumerable<Note> notes) {
            _notes.AddRange(notes);
        }

        public void SetNotes(IEnumerable<Note> notes) {
            _notes = notes.ToList();
        }

        private static byte VPosToKeyType(int vPos) {
            return KeyboardSequence[vPos < 0 ? (-vPos % 12 == 0 ? 0 : 12 - (-vPos) % 12) : vPos % 12]; // 12は音階の数
        }
    }
}
