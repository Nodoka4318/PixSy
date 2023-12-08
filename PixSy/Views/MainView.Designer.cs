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
            trackRoll1 = new Widgets.TrackRoll();
            SuspendLayout();
            // 
            // trackRoll1
            // 
            trackRoll1.Dock = DockStyle.Fill;
            trackRoll1.Location = new Point(0, 0);
            trackRoll1.Name = "trackRoll1";
            trackRoll1.Size = new Size(800, 450);
            trackRoll1.TabIndex = 0;
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(trackRoll1);
            Name = "MainView";
            Text = "MainView";
            ResumeLayout(false);
        }

        #endregion

        private Widgets.TrackRoll trackRoll1;
    }
}