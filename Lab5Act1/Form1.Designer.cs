namespace Lab5Act1
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
            this.tfInput = new System.Windows.Forms.RichTextBox();
            this.tfTokens = new System.Windows.Forms.RichTextBox();
            this.symbolTableTextBox = new System.Windows.Forms.RichTextBox();
            this.btn_Input = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tfInput
            // 
            this.tfInput.Location = new System.Drawing.Point(12, 12);
            this.tfInput.Name = "tfInput";
            this.tfInput.Size = new System.Drawing.Size(300, 200);
            this.tfInput.TabIndex = 0;
            this.tfInput.Text = "";
            // 
            // tfTokens
            // 
            this.tfTokens.Location = new System.Drawing.Point(12, 218);
            this.tfTokens.Name = "tfTokens";
            this.tfTokens.Size = new System.Drawing.Size(300, 200);
            this.tfTokens.TabIndex = 1;
            this.tfTokens.Text = "";
            // 
            // symbolTableTextBox
            // 
            this.symbolTableTextBox.Location = new System.Drawing.Point(318, 12);
            this.symbolTableTextBox.Name = "symbolTableTextBox";
            this.symbolTableTextBox.Size = new System.Drawing.Size(300, 406);
            this.symbolTableTextBox.TabIndex = 2;
            this.symbolTableTextBox.Text = "";
            // 
            // btn_Input
            // 
            this.btn_Input.Location = new System.Drawing.Point(12, 424);
            this.btn_Input.Name = "btn_Input";
            this.btn_Input.Size = new System.Drawing.Size(75, 23);
            this.btn_Input.TabIndex = 3;
            this.btn_Input.Text = "Process";
            this.btn_Input.UseVisualStyleBackColor = true;
            this.btn_Input.Click += new System.EventHandler(this.btn_Input_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 461);
            this.Controls.Add(this.btn_Input);
            this.Controls.Add(this.symbolTableTextBox);
            this.Controls.Add(this.tfTokens);
            this.Controls.Add(this.tfInput);
            this.Name = "Form1";
            this.Text = "Symbol Table Generator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tfInput;
        private System.Windows.Forms.RichTextBox tfTokens;
        private System.Windows.Forms.RichTextBox symbolTableTextBox;
        private System.Windows.Forms.Button btn_Input;
    }
}