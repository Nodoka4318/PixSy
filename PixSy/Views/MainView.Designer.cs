namespace PixSy.Views {
    partial class MainView {
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
        private void InitializeComponent() {
            trackRoll = new Widgets.TrackRoll();
            toolMenuStrip = new MenuStrip();
            playToolStripMenuItem = new ToolStripMenuItem();
            pauseToolStripMenuItem = new ToolStripMenuItem();
            stopToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            bpmToolStripMenuItem = new ToolStripMenuItem();
            rhythmToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveNewToolStripMenuItem = new ToolStripMenuItem();
            createNewToolStripMenuItem = new ToolStripMenuItem();
            openProjectToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            toolMenuStrip.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // trackRoll
            // 
            trackRoll.BackColor = Color.LightGray;
            trackRoll.Dock = DockStyle.Fill;
            trackRoll.HPos = 0;
            trackRoll.IsPlaying = false;
            trackRoll.Location = new Point(135, 40);
            trackRoll.Name = "trackRoll";
            trackRoll.Size = new Size(1439, 889);
            trackRoll.TabIndex = 0;
            trackRoll.VPos = 0;
            // 
            // toolMenuStrip
            // 
            toolMenuStrip.Dock = DockStyle.Left;
            toolMenuStrip.ImageScalingSize = new Size(32, 32);
            toolMenuStrip.Items.AddRange(new ToolStripItem[] { playToolStripMenuItem, pauseToolStripMenuItem, stopToolStripMenuItem, toolStripMenuItem2, bpmToolStripMenuItem, rhythmToolStripMenuItem });
            toolMenuStrip.Location = new Point(0, 40);
            toolMenuStrip.Name = "toolMenuStrip";
            toolMenuStrip.Size = new Size(135, 889);
            toolMenuStrip.TabIndex = 2;
            toolMenuStrip.Text = "menuStrip1";
            // 
            // playToolStripMenuItem
            // 
            playToolStripMenuItem.Name = "playToolStripMenuItem";
            playToolStripMenuItem.Padding = new Padding(8, 70, 8, 0);
            playToolStripMenuItem.Size = new Size(122, 106);
            playToolStripMenuItem.Text = "▶";
            playToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            playToolStripMenuItem.Click += playToolStripMenuItem_Click;
            // 
            // pauseToolStripMenuItem
            // 
            pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            pauseToolStripMenuItem.Padding = new Padding(8, 70, 8, 0);
            pauseToolStripMenuItem.Size = new Size(122, 106);
            pauseToolStripMenuItem.Text = "▮▮";
            pauseToolStripMenuItem.Click += pauseToolStripMenuItem_Click;
            // 
            // stopToolStripMenuItem
            // 
            stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            stopToolStripMenuItem.Padding = new Padding(8, 70, 8, 0);
            stopToolStripMenuItem.Size = new Size(122, 106);
            stopToolStripMenuItem.Text = "■";
            stopToolStripMenuItem.Click += stopToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(122, 4);
            // 
            // bpmToolStripMenuItem
            // 
            bpmToolStripMenuItem.Name = "bpmToolStripMenuItem";
            bpmToolStripMenuItem.Padding = new Padding(8, 50, 8, 0);
            bpmToolStripMenuItem.Size = new Size(122, 86);
            bpmToolStripMenuItem.Text = "120 BPM";
            bpmToolStripMenuItem.Click += bpmToolStripMenuItem_Click;
            // 
            // rhythmToolStripMenuItem
            // 
            rhythmToolStripMenuItem.Name = "rhythmToolStripMenuItem";
            rhythmToolStripMenuItem.Padding = new Padding(8, 50, 8, 0);
            rhythmToolStripMenuItem.Size = new Size(122, 86);
            rhythmToolStripMenuItem.Text = "4拍子";
            rhythmToolStripMenuItem.Click += rhythmToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1574, 40);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, saveNewToolStripMenuItem, createNewToolStripMenuItem, openProjectToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(102, 36);
            fileToolStripMenuItem.Text = "ファイル";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(376, 44);
            saveToolStripMenuItem.Text = "上書き保存";
            // 
            // saveNewToolStripMenuItem
            // 
            saveNewToolStripMenuItem.Name = "saveNewToolStripMenuItem";
            saveNewToolStripMenuItem.Size = new Size(376, 44);
            saveNewToolStripMenuItem.Text = "名前を付けて保存";
            // 
            // createNewToolStripMenuItem
            // 
            createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            createNewToolStripMenuItem.Size = new Size(376, 44);
            createNewToolStripMenuItem.Text = "新しいプロジェクトを作成";
            // 
            // openProjectToolStripMenuItem
            // 
            openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            openProjectToolStripMenuItem.Size = new Size(376, 44);
            openProjectToolStripMenuItem.Text = "プロジェクトを開く";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(82, 36);
            editToolStripMenuItem.Text = "編集";
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1574, 929);
            Controls.Add(trackRoll);
            Controls.Add(toolMenuStrip);
            Controls.Add(menuStrip1);
            Name = "MainView";
            Text = "MainView";
            toolMenuStrip.ResumeLayout(false);
            toolMenuStrip.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Widgets.TrackRoll trackRoll;
        private MenuStrip toolMenuStrip;
        private ToolStripMenuItem playToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveNewToolStripMenuItem;
        private ToolStripMenuItem pauseToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem createNewToolStripMenuItem;
        private ToolStripMenuItem openProjectToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem bpmToolStripMenuItem;
        private ToolStripMenuItem rhythmToolStripMenuItem;
    }
}