using System.Drawing;
using System.Windows.Forms;

namespace AgentApp
{
    partial class Form1OLD
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            label1 = new Label();
            btnFileIndexer = new Button();
            btnDbIndexer = new Button();
            listBoxLogs = new ListBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(126, 32);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(314, 23);
            textBox1.TabIndex = 0;
            textBox1.Text = "6dcbd2d4-0f34-4bc9-a482-17a8c7b45f4f";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 40);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 1;
            label1.Text = "Enter UUID";
            // 
            // btnFileIndexer
            // 
            btnFileIndexer.Location = new Point(126, 112);
            btnFileIndexer.Name = "btnFileIndexer";
            btnFileIndexer.Size = new Size(136, 93);
            btnFileIndexer.TabIndex = 2;
            btnFileIndexer.Text = "File Indexer";
            btnFileIndexer.UseVisualStyleBackColor = true;
            btnFileIndexer.Click += btnFileIndexer_Click;
            // 
            // btnDbIndexer
            // 
            btnDbIndexer.Location = new Point(304, 112);
            btnDbIndexer.Name = "btnDbIndexer";
            btnDbIndexer.Size = new Size(136, 93);
            btnDbIndexer.TabIndex = 3;
            btnDbIndexer.Text = "Database Indexer";
            btnDbIndexer.UseVisualStyleBackColor = true;
            btnDbIndexer.Click += btnDbIndexer_Click;
            // 
            // listBoxLogs
            // 
            listBoxLogs.FormattingEnabled = true;
            listBoxLogs.ItemHeight = 15;
            listBoxLogs.Location = new Point(470, 32);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(629, 244);
            listBoxLogs.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1111, 306);
            Controls.Add(listBoxLogs);
            Controls.Add(btnDbIndexer);
            Controls.Add(btnFileIndexer);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Button btnFileIndexer;
        private Button btnDbIndexer;
        private ListBox listBoxLogs;
    }
}
