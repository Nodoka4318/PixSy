namespace PixSy.Views {
    partial class ListSelectBox<T> {
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
            listBox = new ListBox();
            submitButton = new Button();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.Dock = DockStyle.Top;
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 32;
            listBox.Location = new Point(0, 0);
            listBox.Name = "listBox";
            listBox.ScrollAlwaysVisible = true;
            listBox.Size = new Size(474, 516);
            listBox.TabIndex = 0;
            // 
            // submitButton
            // 
            submitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            submitButton.Location = new Point(12, 522);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(450, 79);
            submitButton.TabIndex = 1;
            submitButton.Text = "選択";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += submitButton_Click;
            // 
            // ListSelectBox
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(474, 621);
            Controls.Add(submitButton);
            Controls.Add(listBox);
            MaximizeBox = false;
            MaximumSize = new Size(500, 692);
            MinimizeBox = false;
            MinimumSize = new Size(500, 692);
            Name = "ListSelectBox";
            Text = "ListSelectBox";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox;
        private Button submitButton;
    }
}