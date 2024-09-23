namespace Lab4Act1
{
    partial class Form1
    {
        private System.Windows.Forms.Button btn_Input;
        private System.Windows.Forms.RichTextBox tfInput;
        private System.Windows.Forms.RichTextBox tfTokens;
        private System.Windows.Forms.RichTextBox symbolTable;

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
            this.btn_Input = new System.Windows.Forms.Button();
            this.tfInput = new System.Windows.Forms.RichTextBox();
            this.tfTokens = new System.Windows.Forms.RichTextBox();
            this.symbolTable = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            
            // 
            // btn_Input
            // 
            this.btn_Input.Location = new System.Drawing.Point(50, 350);
            this.btn_Input.Name = "btn_Input";
            this.btn_Input.Size = new System.Drawing.Size(200, 40);
            this.btn_Input.TabIndex = 0;
            this.btn_Input.Text = "Process Input";
            this.btn_Input.UseVisualStyleBackColor = true;
            this.btn_Input.Click += new System.EventHandler(this.btn_Input_Click);
            
            // 
            // tfInput
            // 
            this.tfInput.Location = new System.Drawing.Point(50, 50);
            this.tfInput.Name = "tfInput";
            this.tfInput.Size = new System.Drawing.Size(300, 150);
            this.tfInput.TabIndex = 1;
            this.tfInput.Text = "";
            
            // 
            // tfTokens
            // 
            this.tfTokens.Location = new System.Drawing.Point(400, 50);
            this.tfTokens.Name = "tfTokens";
            this.tfTokens.Size = new System.Drawing.Size(300, 150);
            this.tfTokens.TabIndex = 2;
            this.tfTokens.Text = "";
            
            // 
            // symbolTable
            // 
            this.symbolTable.Location = new System.Drawing.Point(400, 250);
            this.symbolTable.Name = "symbolTable";
            this.symbolTable.Size = new System.Drawing.Size(300, 150);
            this.symbolTable.TabIndex = 3;
            this.symbolTable.Text = "";
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Input);
            this.Controls.Add(this.tfInput);
            this.Controls.Add(this.tfTokens);
            this.Controls.Add(this.symbolTable);
            this.Name = "Form1";
            this.Text = "Lexical Analyzer";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
