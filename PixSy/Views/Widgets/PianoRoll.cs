using PixSy.Synths;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixSy.Views.Widgets {
    public partial class PianoRoll : UserControl {
        private List<Note> _notes;
        private int _pos; // 一番上のキーの高さ. 0=C5(midC=C4)

        static readonly Color WhiteKeyColor = Color.Gainsboro;
        static readonly Color BlackKeyColor = Color.DarkGray;
        static readonly byte[] KeyboardSequence = {
            0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0 // 0が白鍵, 1が黒鍵
        };
        const int KeyHeight = 13; // 一つのキーの縦の長さ

        public PianoRoll() {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            AutoScroll = false;

            vScrollBar.ValueChanged += VScrollBar_ValueChanged;
            vScrollBar.Minimum = -49 - Height / KeyHeight;
            vScrollBar.Maximum = 39;

            _notes = new List<Note>();
            _pos = 12;
        }

        private void VScrollBar_ValueChanged(object? sender, EventArgs e) {
            SetPos(-vScrollBar.Value);
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
            base.OnPaintBackground(e);
            DrawKeyboard(e.Graphics);
        }

        public void DrawKeyboard(Graphics graphics) {
            // var offset = _pos >= 0 ? KeyHeight - (Math.Abs(_pos) % 20) : (Math.Abs(_pos) % 20);
            var currentPos = _pos; // 音階と座標の高さは逆だからマイナス

            for (int i = 0; i <= Height; ) {
                var keyType = PosToKeyType(currentPos);
                var keyColor = keyType == 0 ? WhiteKeyColor : BlackKeyColor;

                if (currentPos % 12 == 0) {
                    keyColor = Color.WhiteSmoke; // Cだけ白にする
                }

                graphics.FillRectangle(new SolidBrush(keyColor), new Rectangle(0, i, Width, KeyHeight));
                graphics.DrawLine(new Pen(BlackKeyColor), 0, i, Width, i); // 境界線
                graphics.DrawString(Note.PosToPitch(currentPos), new Font(FontFamily.GenericSansSerif, KeyHeight - 4), currentPos == 0 ? Brushes.Red : Brushes.Black, 0, i); // 音階名

                currentPos--;
                i += KeyHeight;
            }
        }

        public void GoUp(int offset) {
            _pos += offset;
            Update();
        }

        public void GoDown(int offset) {
            _pos -= offset;
            Update();
        }

        public void SetPos(int pos) {
            _pos = pos;
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

        private static byte PosToKeyType(int pos) {
            return KeyboardSequence[pos < 0 ? (-pos % 12 == 0 ? 0 : 12 - (-pos) % 12) : pos % 12]; // 12は音階の数
        }
    }
}
