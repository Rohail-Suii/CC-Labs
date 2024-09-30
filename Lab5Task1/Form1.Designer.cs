namespace Lab5Task1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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
            this.tfInput = new System.Windows.Forms.TextBox();
            this.btn_Input = new System.Windows.Forms.Button();
            this.tfTokens = new System.Windows.Forms.TextBox();
            this.symbolTableGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.symbolTableGrid)).BeginInit();
            this.SuspendLayout();

            // 
            // tfInput
            // 
            this.tfInput.Location = new System.Drawing.Point(12, 12);
            this.tfInput.Multiline = true;
            this.tfInput.Name = "tfInput";
            this.tfInput.Size = new System.Drawing.Size(776, 100);
            this.tfInput.TabIndex = 0;

            // 
            // btn_Input
            // 
            this.btn_Input.Location = new System.Drawing.Point(12, 118);
            this.btn_Input.Name = "btn_Input";
            this.btn_Input.Size = new System.Drawing.Size(75, 23);
            this.btn_Input.TabIndex = 1;
            this.btn_Input.Text = "Submit";
            this.btn_Input.UseVisualStyleBackColor = true;
            this.btn_Input.Click += new System.EventHandler(this.btn_Input_Click);

            // 
            // tfTokens
            // 
            this.tfTokens.Location = new System.Drawing.Point(12, 147);
            this.tfTokens.Multiline = true;
            this.tfTokens.Name = "tfTokens";
            this.tfTokens.ReadOnly = true;
            this.tfTokens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tfTokens.Size = new System.Drawing.Size(776, 100);
            this.tfTokens.TabIndex = 2;

            // 
            // symbolTableGrid
            // 
            this.symbolTableGrid.AllowUserToAddRows = false;
            this.symbolTableGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.symbolTableGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Variable", Name = "VariableColumn" },
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Type", Name = "TypeColumn" },
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Line Number", Name = "LineNumberColumn" }
            });
            this.symbolTableGrid.Location = new System.Drawing.Point(12, 253);
            this.symbolTableGrid.Name = "symbolTableGrid";
            this.symbolTableGrid.ReadOnly = true;
            this.symbolTableGrid.Size = new System.Drawing.Size(776, 200);
            this.symbolTableGrid.TabIndex = 3;

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.symbolTableGrid);
            this.Controls.Add(this.tfTokens);
            this.Controls.Add(this.btn_Input);
            this.Controls.Add(this.tfInput);
            this.Name = "Form1";
            this.Text = "Symbol Table Example";
            ((System.ComponentModel.ISupportInitialize)(this.symbolTableGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox tfInput;
        private System.Windows.Forms.Button btn_Input;
        private System.Windows.Forms.TextBox tfTokens;
        private System.Windows.Forms.DataGridView symbolTableGrid;
    }
}
