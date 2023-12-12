namespace PixSy.Views.Widgets {
    partial class SynthsPanel {
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
            synthsListBox = new ListBox();
            deleteButton = new Button();
            createButton = new Button();
            editButton = new Button();
            SuspendLayout();
            // 
            // synthsListBox
            // 
            synthsListBox.Dock = DockStyle.Fill;
            synthsListBox.FormattingEnabled = true;
            synthsListBox.ItemHeight = 32;
            synthsListBox.Location = new Point(0, 0);
            synthsListBox.Name = "synthsListBox";
            synthsListBox.Size = new Size(513, 587);
            synthsListBox.TabIndex = 0;
            // 
            // deleteButton
            // 
            deleteButton.Dock = DockStyle.Bottom;
            deleteButton.ForeColor = Color.Red;
            deleteButton.Location = new Point(0, 755);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(513, 84);
            deleteButton.TabIndex = 1;
            deleteButton.Text = "削除";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // createButton
            // 
            createButton.Dock = DockStyle.Bottom;
            createButton.Location = new Point(0, 671);
            createButton.Name = "createButton";
            createButton.Size = new Size(513, 84);
            createButton.TabIndex = 2;
            createButton.Text = "新規作成";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // editButton
            // 
            editButton.Dock = DockStyle.Bottom;
            editButton.Location = new Point(0, 587);
            editButton.Name = "editButton";
            editButton.Size = new Size(513, 84);
            editButton.TabIndex = 3;
            editButton.Text = "編集";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += editButton_Click;
            // 
            // SynthsPanel
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(synthsListBox);
            Controls.Add(editButton);
            Controls.Add(createButton);
            Controls.Add(deleteButton);
            Name = "SynthsPanel";
            Size = new Size(513, 839);
            ResumeLayout(false);
        }

        #endregion

        private ListBox synthsListBox;
        private Button deleteButton;
        private Button createButton;
        private Button editButton;
    }
}
