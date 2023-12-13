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
            freqToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveNewToolStripMenuItem = new ToolStripMenuItem();
            createNewToolStripMenuItem = new ToolStripMenuItem();
            openProjectToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            rightTabControl = new TabControl();
            soundsMenu = new TabPage();
            synthsPanel = new Widgets.SynthsPanel();
            trackControls = new Widgets.TrackControls();
            toolMenuStrip.SuspendLayout();
            menuStrip1.SuspendLayout();
            rightTabControl.SuspendLayout();
            soundsMenu.SuspendLayout();
            SuspendLayout();
            // 
            // trackRoll
            // 
            trackRoll.BackColor = Color.LightGray;
            trackRoll.CurrentPlayHPos = 0F;
            trackRoll.Dock = DockStyle.Fill;
            trackRoll.HPos = 0;
            trackRoll.IsPlaying = false;
            trackRoll.Location = new Point(346, 40);
            trackRoll.Name = "trackRoll";
            trackRoll.Rhythm = 4;
            trackRoll.Size = new Size(828, 889);
            trackRoll.TabIndex = 0;
            trackRoll.TrackControls = null;
            trackRoll.VPos = 0;
            // 
            // toolMenuStrip
            // 
            toolMenuStrip.Dock = DockStyle.Left;
            toolMenuStrip.ImageScalingSize = new Size(32, 32);
            toolMenuStrip.Items.AddRange(new ToolStripItem[] { playToolStripMenuItem, pauseToolStripMenuItem, stopToolStripMenuItem, toolStripMenuItem2, bpmToolStripMenuItem, rhythmToolStripMenuItem, freqToolStripMenuItem });
            toolMenuStrip.Location = new Point(0, 40);
            toolMenuStrip.Name = "toolMenuStrip";
            toolMenuStrip.Size = new Size(246, 889);
            toolMenuStrip.TabIndex = 2;
            toolMenuStrip.Text = "menuStrip1";
            // 
            // playToolStripMenuItem
            // 
            playToolStripMenuItem.Name = "playToolStripMenuItem";
            playToolStripMenuItem.Padding = new Padding(8, 70, 8, 0);
            playToolStripMenuItem.Size = new Size(233, 106);
            playToolStripMenuItem.Text = "▶";
            playToolStripMenuItem.TextDirection = ToolStripTextDirection.Horizontal;
            playToolStripMenuItem.Click += playToolStripMenuItem_Click;
            // 
            // pauseToolStripMenuItem
            // 
            pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            pauseToolStripMenuItem.Padding = new Padding(8, 70, 8, 0);
            pauseToolStripMenuItem.Size = new Size(233, 106);
            pauseToolStripMenuItem.Text = "▮▮";
            pauseToolStripMenuItem.Click += pauseToolStripMenuItem_Click;
            // 
            // stopToolStripMenuItem
            // 
            stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            stopToolStripMenuItem.Padding = new Padding(8, 70, 8, 0);
            stopToolStripMenuItem.Size = new Size(233, 106);
            stopToolStripMenuItem.Text = "■";
            stopToolStripMenuItem.Click += stopToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(233, 4);
            // 
            // bpmToolStripMenuItem
            // 
            bpmToolStripMenuItem.Name = "bpmToolStripMenuItem";
            bpmToolStripMenuItem.Padding = new Padding(8, 50, 8, 0);
            bpmToolStripMenuItem.Size = new Size(233, 86);
            bpmToolStripMenuItem.Text = "120 BPM";
            bpmToolStripMenuItem.Click += bpmToolStripMenuItem_Click;
            // 
            // rhythmToolStripMenuItem
            // 
            rhythmToolStripMenuItem.Name = "rhythmToolStripMenuItem";
            rhythmToolStripMenuItem.Padding = new Padding(8, 50, 8, 0);
            rhythmToolStripMenuItem.Size = new Size(233, 86);
            rhythmToolStripMenuItem.Text = "4拍子";
            rhythmToolStripMenuItem.Click += rhythmToolStripMenuItem_Click;
            // 
            // freqToolStripMenuItem
            // 
            freqToolStripMenuItem.Name = "freqToolStripMenuItem";
            freqToolStripMenuItem.Padding = new Padding(8, 50, 8, 0);
            freqToolStripMenuItem.Size = new Size(233, 86);
            freqToolStripMenuItem.Text = "440.00Hz";
            freqToolStripMenuItem.Click += freqToolStripMenuItem_Click;
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
            saveNewToolStripMenuItem.Click += saveNewToolStripMenuItem_Click;
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
            openProjectToolStripMenuItem.Click += openProjectToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(82, 36);
            editToolStripMenuItem.Text = "編集";
            // 
            // rightTabControl
            // 
            rightTabControl.Controls.Add(soundsMenu);
            rightTabControl.Dock = DockStyle.Right;
            rightTabControl.Location = new Point(1174, 40);
            rightTabControl.Name = "rightTabControl";
            rightTabControl.SelectedIndex = 0;
            rightTabControl.Size = new Size(400, 889);
            rightTabControl.TabIndex = 4;
            // 
            // soundsMenu
            // 
            soundsMenu.Controls.Add(synthsPanel);
            soundsMenu.Location = new Point(8, 46);
            soundsMenu.Name = "soundsMenu";
            soundsMenu.Padding = new Padding(3);
            soundsMenu.Size = new Size(384, 835);
            soundsMenu.TabIndex = 0;
            soundsMenu.Text = "Synths";
            soundsMenu.UseVisualStyleBackColor = true;
            // 
            // synthsPanel
            // 
            synthsPanel.Dock = DockStyle.Fill;
            synthsPanel.Location = new Point(3, 3);
            synthsPanel.Name = "synthsPanel";
            synthsPanel.Size = new Size(378, 829);
            synthsPanel.TabIndex = 0;
            // 
            // trackControls
            // 
            trackControls.Dock = DockStyle.Left;
            trackControls.Location = new Point(246, 40);
            trackControls.Name = "trackControls";
            trackControls.Size = new Size(100, 889);
            trackControls.TabIndex = 5;
            trackControls.VPos = 0;
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1574, 929);
            Controls.Add(trackRoll);
            Controls.Add(trackControls);
            Controls.Add(rightTabControl);
            Controls.Add(toolMenuStrip);
            Controls.Add(menuStrip1);
            Name = "MainView";
            Text = "MainView";
            toolMenuStrip.ResumeLayout(false);
            toolMenuStrip.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            rightTabControl.ResumeLayout(false);
            soundsMenu.ResumeLayout(false);
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
        private TabControl rightTabControl;
        private TabPage soundsMenu;
        private Widgets.SynthsPanel synthsPanel;
        private Widgets.TrackControls trackControls;
        private ToolStripMenuItem freqToolStripMenuItem;
    }
}