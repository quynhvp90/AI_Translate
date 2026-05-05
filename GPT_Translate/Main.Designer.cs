namespace GPT_Translate
{
    partial class Main
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
            richTextBoxInput = new RichTextBox();
            richTextBoxOutput = new RichTextBox();
            buttonTranslate = new Button();
            label1 = new Label();
            label2 = new Label();
            buttonEncrypted = new Button();
            buttonDecrypt = new Button();
            SuspendLayout();
            // 
            // richTextBoxInput
            // 
            richTextBoxInput.Location = new Point(12, 27);
            richTextBoxInput.Name = "richTextBoxInput";
            richTextBoxInput.Size = new Size(339, 231);
            richTextBoxInput.TabIndex = 0;
            richTextBoxInput.Text = "";
            // 
            // richTextBoxOutput
            // 
            richTextBoxOutput.Location = new Point(439, 27);
            richTextBoxOutput.Name = "richTextBoxOutput";
            richTextBoxOutput.Size = new Size(349, 231);
            richTextBoxOutput.TabIndex = 0;
            richTextBoxOutput.Text = "";
            // 
            // buttonTranslate
            // 
            buttonTranslate.Location = new Point(357, 91);
            buttonTranslate.Name = "buttonTranslate";
            buttonTranslate.Size = new Size(76, 35);
            buttonTranslate.TabIndex = 1;
            buttonTranslate.Text = "Translate";
            buttonTranslate.UseVisualStyleBackColor = true;
            buttonTranslate.Click += buttonTranslate_ClickAsync;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 2;
            label1.Text = "Input";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(439, 9);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 2;
            label2.Text = "Output";
            // 
            // buttonEncrypted
            // 
            buttonEncrypted.Location = new Point(357, 132);
            buttonEncrypted.Name = "buttonEncrypted";
            buttonEncrypted.Size = new Size(76, 35);
            buttonEncrypted.TabIndex = 1;
            buttonEncrypted.Text = "Encrypted";
            buttonEncrypted.UseVisualStyleBackColor = true;
            buttonEncrypted.Click += buttonEncrypted_Click;
            // 
            // buttonDecrypt
            // 
            buttonDecrypt.Location = new Point(357, 173);
            buttonDecrypt.Name = "buttonDecrypt";
            buttonDecrypt.Size = new Size(76, 35);
            buttonDecrypt.TabIndex = 1;
            buttonDecrypt.Text = "Decrypt";
            buttonDecrypt.UseVisualStyleBackColor = true;
            buttonDecrypt.Click += buttonDecrypt_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 306);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonDecrypt);
            Controls.Add(buttonEncrypted);
            Controls.Add(buttonTranslate);
            Controls.Add(richTextBoxOutput);
            Controls.Add(richTextBoxInput);
            Name = "Main";
            Text = "Main";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBoxInput;
        private RichTextBox richTextBoxOutput;
        private Button buttonTranslate;
        private Label label1;
        private Label label2;
        private Button buttonEncrypted;
        private Button buttonDecrypt;
    }
}
