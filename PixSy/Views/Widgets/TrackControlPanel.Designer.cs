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
            panel1.SuspendLayout();
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
            // TrackControlPanel
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(synthButton);
            Controls.Add(panel1);
            Name = "TrackControlPanel";
            Size = new Size(200, 200);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button muteButton;
        private Button soloButton;
        private Button synthButton;
    }
}
