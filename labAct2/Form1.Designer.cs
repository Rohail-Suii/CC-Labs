namespace labAct2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;

        private void InitializeComponent()
        {
            this.dataGridView1 = new DataGridView();
            this.button1 = new Button();
            this.button2 = new Button();

            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(360, 200);
            this.dataGridView1.TabIndex = 0;

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(120, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Retrieve Data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Name = "Form1";
            this.Text = "DataGridView Example";

            // Finalize form initialization
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        }
    }
}
