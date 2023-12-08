namespace PixSy.Views.Widgets {
    partial class TrackRoll {
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
            vScrollBar = new VScrollBar();
            hScrollBar = new HScrollBar();
            SuspendLayout();
            // 
            // vScrollBar
            // 
            vScrollBar.Dock = DockStyle.Right;
            vScrollBar.Location = new Point(133, 0);
            vScrollBar.Name = "vScrollBar";
            vScrollBar.Size = new Size(17, 150);
            vScrollBar.TabIndex = 0;
            // 
            // hScrollBar
            // 
            hScrollBar.Dock = DockStyle.Bottom;
            hScrollBar.Location = new Point(0, 133);
            hScrollBar.Name = "hScrollBar";
            hScrollBar.Size = new Size(133, 17);
            hScrollBar.TabIndex = 1;
            // 
            // TrackRoll
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(hScrollBar);
            Controls.Add(vScrollBar);
            Name = "TrackRoll";
            ResumeLayout(false);
        }

        #endregion

        private VScrollBar vScrollBar;
        private HScrollBar hScrollBar;
    }
}
