namespace PixSy.Views.Widgets {
    partial class TrackControlPanel {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            panel1 = new Panel();
            muteButton = new Button();
            soloButton = new Button();
            synthButton = new Button();
            soloMuteSynthPanel = new Panel();
            volumePanel = new Panel();
            volumeTrackBar = new TrackBar();
            volumeButton = new Button();
            panPanel = new Panel();
            panTrackBar = new TrackBar();
            panButton = new Button();
            panel1.SuspendLayout();
            soloMuteSynthPanel.SuspendLayout();
            volumePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeTrackBar).BeginInit();
            panPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panTrackBar).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(muteButton);
            panel1.Controls.Add(soloButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 100);
            panel1.TabIndex = 0;
            // 
            // muteButton
            // 
            muteButton.BackColor = Color.WhiteSmoke;
            muteButton.Dock = DockStyle.Right;
            muteButton.ForeColor = Color.Red;
            muteButton.Location = new Point(100, 0);
            muteButton.Name = "muteButton";
            muteButton.Size = new Size(100, 100);
            muteButton.TabIndex = 1;
            muteButton.Text = "M";
            muteButton.UseVisualStyleBackColor = false;
            muteButton.Click += muteButton_Click;
            // 
            // soloButton
            // 
            soloButton.BackColor = Color.WhiteSmoke;
            soloButton.Dock = DockStyle.Left;
            soloButton.ForeColor = Color.Green;
            soloButton.Location = new Point(0, 0);
            soloButton.Name = "soloButton";
            soloButton.Size = new Size(100, 100);
            soloButton.TabIndex = 0;
            soloButton.Text = "S";
            soloButton.UseVisualStyleBackColor = false;
            soloButton.Click += soloButton_Click;
            // 
            // synthButton
            // 
            synthButton.Dock = DockStyle.Bottom;
            synthButton.Location = new Point(0, 100);
            synthButton.Name = "synthButton";
            synthButton.Size = new Size(200, 100);
            synthButton.TabIndex = 1;
            synthButton.Text = "Synth";
            synthButton.UseVisualStyleBackColor = true;
            synthButton.Click += synthButton_Click;
            // 
            // soloMuteSynthPanel
            // 
            soloMuteSynthPanel.Controls.Add(panel1);
            soloMuteSynthPanel.Controls.Add(synthButton);
            soloMuteSynthPanel.Dock = DockStyle.Right;
            soloMuteSynthPanel.Location = new Point(600, 0);
            soloMuteSynthPanel.Name = "soloMuteSynthPanel";
            soloMuteSynthPanel.Size = new Size(200, 200);
            soloMuteSynthPanel.TabIndex = 2;
            // 
            // volumePanel
            // 
            volumePanel.Controls.Add(volumeTrackBar);
            volumePanel.Controls.Add(volumeButton);
            volumePanel.Dock = DockStyle.Top;
            volumePanel.Location = new Point(0, 0);
            volumePanel.Name = "volumePanel";
            volumePanel.Size = new Size(600, 100);
            volumePanel.TabIndex = 3;
            // 
            // volumeTrackBar
            // 
            volumeTrackBar.BackColor = Color.Lavender;
            volumeTrackBar.Dock = DockStyle.Fill;
            volumeTrackBar.Location = new Point(150, 0);
            volumeTrackBar.Maximum = 15;
            volumeTrackBar.Name = "volumeTrackBar";
            volumeTrackBar.Size = new Size(450, 100);
            volumeTrackBar.TabIndex = 1;
            volumeTrackBar.TickStyle = TickStyle.Both;
            volumeTrackBar.Value = 10;
            // 
            // volumeButton
            // 
            volumeButton.BackColor = Color.Lavender;
            volumeButton.Dock = DockStyle.Left;
            volumeButton.Location = new Point(0, 0);
            volumeButton.Name = "volumeButton";
            volumeButton.Size = new Size(150, 100);
            volumeButton.TabIndex = 0;
            volumeButton.Text = "Level 10";
            volumeButton.UseVisualStyleBackColor = false;
            volumeButton.Click += volumeButton_Click;
            // 
            // panPanel
            // 
            panPanel.Controls.Add(panTrackBar);
            panPanel.Controls.Add(panButton);
            panPanel.Dock = DockStyle.Bottom;
            panPanel.Location = new Point(0, 100);
            panPanel.Name = "panPanel";
            panPanel.Size = new Size(600, 100);
            panPanel.TabIndex = 4;
            // 
            // panTrackBar
            // 
            panTrackBar.BackColor = Color.Linen;
            panTrackBar.Dock = DockStyle.Fill;
            panTrackBar.Location = new Point(150, 0);
            panTrackBar.Minimum = -10;
            panTrackBar.Name = "panTrackBar";
            panTrackBar.Size = new Size(450, 100);
            panTrackBar.TabIndex = 1;
            panTrackBar.TickStyle = TickStyle.Both;
            // 
            // panButton
            // 
            panButton.BackColor = Color.Linen;
            panButton.Dock = DockStyle.Left;
            panButton.Location = new Point(0, 0);
            panButton.Name = "panButton";
            panButton.Size = new Size(150, 100);
            panButton.TabIndex = 0;
            panButton.Text = "Pan 0";
            panButton.UseVisualStyleBackColor = false;
            panButton.Click += panButton_Click;
            // 
            // TrackControlPanel
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panPanel);
            Controls.Add(volumePanel);
            Controls.Add(soloMuteSynthPanel);
            Name = "TrackControlPanel";
            Size = new Size(800, 200);
            panel1.ResumeLayout(false);
            soloMuteSynthPanel.ResumeLayout(false);
            volumePanel.ResumeLayout(false);
            volumePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)volumeTrackBar).EndInit();
            panPanel.ResumeLayout(false);
            panPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panTrackBar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button muteButton;
        private Button soloButton;
        private Button synthButton;
        private Panel soloMuteSynthPanel;
        private Panel volumePanel;
        private TrackBar volumeTrackBar;
        private Button volumeButton;
        private Panel panPanel;
        private TrackBar panTrackBar;
        private Button panButton;
    }
}
