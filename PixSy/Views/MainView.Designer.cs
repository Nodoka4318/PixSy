﻿namespace PixSy.Views {
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
            this.pianoRoll1 = new PixSy.Views.Widgets.PianoRoll();
            this.SuspendLayout();
            // 
            // pianoRoll1
            // 
            this.pianoRoll1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pianoRoll1.Location = new System.Drawing.Point(0, 0);
            this.pianoRoll1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pianoRoll1.Name = "pianoRoll1";
            this.pianoRoll1.Size = new System.Drawing.Size(560, 270);
            this.pianoRoll1.TabIndex = 0;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 270);
            this.Controls.Add(this.pianoRoll1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainView";
            this.Text = "MainView";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Widgets.PianoRoll pianoRoll1;
    }
}