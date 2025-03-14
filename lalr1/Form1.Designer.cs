namespace lalr1
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
            this.btnAddProduction = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtNonTerminal = new System.Windows.Forms.TextBox();
            this.txtProduction = new System.Windows.Forms.TextBox();
            this.dgvGrammar = new System.Windows.Forms.DataGridView();
            this.dgvParsingTable = new System.Windows.Forms.DataGridView();
            this.lblNonTerminal = new System.Windows.Forms.Label();
            this.lblProduction = new System.Windows.Forms.Label();
            this.NonTerminalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArrowColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(this.dgvGrammar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParsingTable)).BeginInit();
            this.SuspendLayout();
            
            // btnAddProduction
            this.btnAddProduction.Location = new System.Drawing.Point(420, 20);
            this.btnAddProduction.Name = "btnAddProduction";
            this.btnAddProduction.Size = new System.Drawing.Size(120, 23);
            this.btnAddProduction.TabIndex = 0;
            this.btnAddProduction.Text = "Add Production";
            this.btnAddProduction.UseVisualStyleBackColor = true;
            
            // btnGenerate
            this.btnGenerate.Location = new System.Drawing.Point(420, 50);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(120, 23);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate LALR(1)";
            this.btnGenerate.UseVisualStyleBackColor = true;
            
            // txtNonTerminal
            this.txtNonTerminal.Location = new System.Drawing.Point(100, 20);
            this.txtNonTerminal.Name = "txtNonTerminal";
            this.txtNonTerminal.Size = new System.Drawing.Size(100, 20);
            this.txtNonTerminal.TabIndex = 2;
            
            // txtProduction
            this.txtProduction.Location = new System.Drawing.Point(300, 20);
            this.txtProduction.Name = "txtProduction";
            this.txtProduction.Size = new System.Drawing.Size(100, 20);
            this.txtProduction.TabIndex = 3;
            
            // lblNonTerminal
            this.lblNonTerminal.AutoSize = true;
            this.lblNonTerminal.Location = new System.Drawing.Point(20, 23);
            this.lblNonTerminal.Name = "lblNonTerminal";
            this.lblNonTerminal.Size = new System.Drawing.Size(74, 13);
            this.lblNonTerminal.TabIndex = 4;
            this.lblNonTerminal.Text = "Non-Terminal:";
            
            // lblProduction
            this.lblProduction.AutoSize = true;
            this.lblProduction.Location = new System.Drawing.Point(220, 23);
            this.lblProduction.Name = "lblProduction";
            this.lblProduction.Size = new System.Drawing.Size(62, 13);
            this.lblProduction.TabIndex = 5;
            this.lblProduction.Text = "Production:";
            
            // dgvGrammar
            this.dgvGrammar.AllowUserToAddRows = false;
            this.dgvGrammar.AllowUserToDeleteRows = false;
            this.dgvGrammar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrammar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.NonTerminalColumn,
                this.ArrowColumn,
                this.ProductionColumn});
            this.dgvGrammar.Location = new System.Drawing.Point(20, 90);
            this.dgvGrammar.Name = "dgvGrammar";
            this.dgvGrammar.ReadOnly = true;
            this.dgvGrammar.Size = new System.Drawing.Size(520, 200);
            this.dgvGrammar.TabIndex = 6;
            
            // NonTerminalColumn
            this.NonTerminalColumn.HeaderText = "Non-Terminal";
            this.NonTerminalColumn.Name = "NonTerminalColumn";
            this.NonTerminalColumn.ReadOnly = true;
            this.NonTerminalColumn.Width = 120;
            
            // ArrowColumn
            this.ArrowColumn.HeaderText = "";
            this.ArrowColumn.Name = "ArrowColumn";
            this.ArrowColumn.ReadOnly = true;
            this.ArrowColumn.Width = 40;
            
            // ProductionColumn
            this.ProductionColumn.HeaderText = "Production";
            this.ProductionColumn.Name = "ProductionColumn";
            this.ProductionColumn.ReadOnly = true;
            this.ProductionColumn.Width = 300;
            
            // dgvParsingTable
            this.dgvParsingTable.AllowUserToAddRows = false;
            this.dgvParsingTable.AllowUserToDeleteRows = false;
            this.dgvParsingTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParsingTable.Location = new System.Drawing.Point(20, 310);
            this.dgvParsingTable.Name = "dgvParsingTable";
            this.dgvParsingTable.ReadOnly = true;
            this.dgvParsingTable.Size = new System.Drawing.Size(520, 300);
            this.dgvParsingTable.TabIndex = 7;
            
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 631);
            this.Controls.Add(this.dgvParsingTable);
            this.Controls.Add(this.dgvGrammar);
            this.Controls.Add(this.lblProduction);
            this.Controls.Add(this.lblNonTerminal);
            this.Controls.Add(this.txtProduction);
            this.Controls.Add(this.txtNonTerminal);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnAddProduction);
            this.Name = "Form1";
            this.Text = "LALR(1) Parser Generator";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrammar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParsingTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGrammar;
        private System.Windows.Forms.DataGridView dgvParsingTable;
        private System.Windows.Forms.Label lblNonTerminal;
        private System.Windows.Forms.Label lblProduction;
        private System.Windows.Forms.DataGridViewTextBoxColumn NonTerminalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArrowColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductionColumn;
    }
}