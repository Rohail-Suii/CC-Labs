namespace Lab4Task1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonAnalyze;
        private System.Windows.Forms.DataGridView dataGridViewTokens;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.dataGridViewTokens = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokens)).BeginInit();
            this.SuspendLayout();
            
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(12, 12);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(400, 150);
            this.textBoxInput.TabIndex = 0;

            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Location = new System.Drawing.Point(12, 170);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(100, 30);
            this.buttonAnalyze.TabIndex = 1;
            this.buttonAnalyze.Text = "Analyze";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            this.buttonAnalyze.Click += new System.EventHandler(this.buttonAnalyze_Click);

            // 
            // dataGridViewTokens
            // 
            this.dataGridViewTokens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTokens.Location = new System.Drawing.Point(420, 12);
            this.dataGridViewTokens.Name = "dataGridViewTokens";
            this.dataGridViewTokens.Size = new System.Drawing.Size(300, 188);
            this.dataGridViewTokens.TabIndex = 2;

            // Adding columns to the DataGridView
            this.dataGridViewTokens.Columns.Add("Token", "Token");
            this.dataGridViewTokens.Columns.Add("Type", "Type");    
            this.dataGridViewTokens.Columns.Add("Count", "Count");

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(740, 220);
            this.Controls.Add(this.dataGridViewTokens);
            this.Controls.Add(this.buttonAnalyze);
            this.Controls.Add(this.textBoxInput);
            this.Name = "Form1";
            this.Text = "Lexical Analyzer";

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTokens)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}