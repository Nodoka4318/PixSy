namespace PixSy.Views {
    partial class InputBox {
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
            submitButton = new Button();
            textBox = new TextBox();
            SuspendLayout();
            // 
            // submitButton
            // 
            submitButton.Location = new Point(12, 84);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(350, 74);
            submitButton.TabIndex = 0;
            submitButton.Text = "OK";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += submitButton_Click;
            // 
            // textBox
            // 
            textBox.Location = new Point(12, 21);
            textBox.Name = "textBox";
            textBox.Size = new Size(350, 39);
            textBox.TabIndex = 1;
            // 
            // InputBox
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 179);
            Controls.Add(textBox);
            Controls.Add(submitButton);
            MaximizeBox = false;
            MaximumSize = new Size(400, 250);
            MinimizeBox = false;
            MinimumSize = new Size(400, 250);
            Name = "InputBox";
            Text = "InputBox";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button submitButton;
        private TextBox textBox;
    }
}