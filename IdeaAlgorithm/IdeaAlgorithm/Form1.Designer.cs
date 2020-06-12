namespace IdeaAlgorithm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inputBox = new System.Windows.Forms.RichTextBox();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.encryptButton = new System.Windows.Forms.Button();
            this.decryptButton = new System.Windows.Forms.Button();
            this.toBytesButton = new System.Windows.Forms.Button();
            this.toASCIIButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(55, 31);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(655, 20);
            this.keyTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Klucz:";
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(55, 89);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(229, 111);
            this.inputBox.TabIndex = 2;
            this.inputBox.Text = "";
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(481, 89);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(229, 111);
            this.outputBox.TabIndex = 3;
            this.outputBox.Text = "";
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(339, 108);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(75, 23);
            this.encryptButton.TabIndex = 4;
            this.encryptButton.Text = "Szyfruj";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(339, 158);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(75, 23);
            this.decryptButton.TabIndex = 5;
            this.decryptButton.Text = "Odszyfruj";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // toBytesButton
            // 
            this.toBytesButton.Location = new System.Drawing.Point(55, 223);
            this.toBytesButton.Name = "toBytesButton";
            this.toBytesButton.Size = new System.Drawing.Size(111, 31);
            this.toBytesButton.TabIndex = 6;
            this.toBytesButton.Text = "Konwertuj na bajty";
            this.toBytesButton.UseVisualStyleBackColor = true;
            this.toBytesButton.Click += new System.EventHandler(this.toBytesButton_Click);
            // 
            // toASCIIButton
            // 
            this.toASCIIButton.Location = new System.Drawing.Point(172, 223);
            this.toASCIIButton.Name = "toASCIIButton";
            this.toASCIIButton.Size = new System.Drawing.Size(111, 31);
            this.toASCIIButton.TabIndex = 7;
            this.toASCIIButton.Text = "Konwertuj na ASCII";
            this.toASCIIButton.UseVisualStyleBackColor = true;
            this.toASCIIButton.Click += new System.EventHandler(this.toASCIIButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 278);
            this.Controls.Add(this.toASCIIButton);
            this.Controls.Add(this.toBytesButton);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keyTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox inputBox;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.Button toBytesButton;
        private System.Windows.Forms.Button toASCIIButton;
    }
}

