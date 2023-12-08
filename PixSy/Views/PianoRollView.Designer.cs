using PixSy.Views.Widgets;

namespace PixSy.Views {
    partial class PianoRollView {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(PianoRoll pr) {
            pianoRoll = pr;
            menuStrip = new MenuStrip();
            modesToolStripMenuItem = new ToolStripMenuItem();
            selectToolStripMenuItem = new ToolStripMenuItem();
            penToolStripMenuItem = new ToolStripMenuItem();
            playToolStripMenuItem = new ToolStripMenuItem();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pianoRoll
            // 
            pianoRoll.CurrentPlayHPos = 0F;
            pianoRoll.Dock = DockStyle.Fill;
            pianoRoll.IsPlaying = false;
            pianoRoll.Location = new Point(0, 40);
            pianoRoll.Margin = new Padding(6, 6, 6, 6);
            pianoRoll.Mode = Widgets.PianoRoll.EditMode.Select;
            pianoRoll.Name = "pianoRoll";
            pianoRoll.Size = new Size(1174, 689);
            pianoRoll.TabIndex = 0;
            pianoRoll.Load += pianoRoll_Load;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(32, 32);
            menuStrip.Items.AddRange(new ToolStripItem[] { modesToolStripMenuItem, playToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1174, 40);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // modesToolStripMenuItem
            // 
            modesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { selectToolStripMenuItem, penToolStripMenuItem });
            modesToolStripMenuItem.Name = "modesToolStripMenuItem";
            modesToolStripMenuItem.Size = new Size(84, 36);
            modesToolStripMenuItem.Text = "モード";
            // 
            // selectToolStripMenuItem
            // 
            selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            selectToolStripMenuItem.Size = new Size(195, 44);
            selectToolStripMenuItem.Text = "選択";
            selectToolStripMenuItem.Click += selectToolStripMenuItem_Click;
            // 
            // penToolStripMenuItem
            // 
            penToolStripMenuItem.Name = "penToolStripMenuItem";
            penToolStripMenuItem.Size = new Size(195, 44);
            penToolStripMenuItem.Text = "ペン";
            penToolStripMenuItem.Click += penToolStripMenuItem_Click;
            // 
            // playToolStripMenuItem
            // 
            playToolStripMenuItem.Name = "playToolStripMenuItem";
            playToolStripMenuItem.Size = new Size(82, 36);
            playToolStripMenuItem.Text = "再生";
            playToolStripMenuItem.Click += playToolStripMenuItem_Click;
            // 
            // PianoRollView
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 729);
            Controls.Add(pianoRoll);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "PianoRollView";
            Text = "PianoRollView";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Widgets.PianoRoll pianoRoll;
        private MenuStrip menuStrip;
        private ToolStripMenuItem modesToolStripMenuItem;
        private ToolStripMenuItem selectToolStripMenuItem;
        private ToolStripMenuItem penToolStripMenuItem;
        private ToolStripMenuItem playToolStripMenuItem;
    }
}