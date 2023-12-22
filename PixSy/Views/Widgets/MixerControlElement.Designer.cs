namespace PixSy.Views.Widgets {
    partial class MixerControlElement {
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
            volumeTrackBar = new TrackBar();
            volumeTrackBarPanel = new Panel();
            volumePanel = new Panel();
            volumeButton = new Button();
            panPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)volumeTrackBar).BeginInit();
            volumeTrackBarPanel.SuspendLayout();
            volumePanel.SuspendLayout();
            SuspendLayout();
            // 
            // volumeTrackBar
            // 
            volumeTrackBar.Dock = DockStyle.Fill;
            volumeTrackBar.Location = new Point(0, 0);
            volumeTrackBar.Maximum = 20;
            volumeTrackBar.Name = "volumeTrackBar";
            volumeTrackBar.Orientation = Orientation.Vertical;
            volumeTrackBar.Size = new Size(68, 357);
            volumeTrackBar.TabIndex = 0;
            volumeTrackBar.TickStyle = TickStyle.Both;
            volumeTrackBar.Value = 10;
            // 
            // volumeTrackBarPanel
            // 
            volumeTrackBarPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            volumeTrackBarPanel.Controls.Add(volumeTrackBar);
            volumeTrackBarPanel.Location = new Point(63, 89);
            volumeTrackBarPanel.Name = "volumeTrackBarPanel";
            volumeTrackBarPanel.Size = new Size(68, 357);
            volumeTrackBarPanel.TabIndex = 1;
            // 
            // volumePanel
            // 
            volumePanel.Controls.Add(volumeButton);
            volumePanel.Controls.Add(volumeTrackBarPanel);
            volumePanel.Location = new Point(0, 154);
            volumePanel.Name = "volumePanel";
            volumePanel.Size = new Size(200, 446);
            volumePanel.TabIndex = 2;
            // 
            // volumeButton
            // 
            volumeButton.Dock = DockStyle.Top;
            volumeButton.Location = new Point(0, 0);
            volumeButton.Name = "volumeButton";
            volumeButton.Size = new Size(200, 83);
            volumeButton.TabIndex = 2;
            volumeButton.Text = "Volume 10";
            volumeButton.UseVisualStyleBackColor = true;
            // 
            // panPanel
            // 
            panPanel.Dock = DockStyle.Top;
            panPanel.Location = new Point(0, 0);
            panPanel.Name = "panPanel";
            panPanel.Size = new Size(200, 155);
            panPanel.TabIndex = 3;
            // 
            // MixerControlElement
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(volumePanel);
            Controls.Add(panPanel);
            Name = "MixerControlElement";
            Size = new Size(200, 600);
            ((System.ComponentModel.ISupportInitialize)volumeTrackBar).EndInit();
            volumeTrackBarPanel.ResumeLayout(false);
            volumeTrackBarPanel.PerformLayout();
            volumePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TrackBar volumeTrackBar;
        private Panel volumeTrackBarPanel;
        private Panel volumePanel;
        private Button volumeButton;
        private Panel panPanel;
    }
}
