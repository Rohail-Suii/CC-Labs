using System;
using System.Windows.Forms;
using System.Text;
namespace labAct2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Set up DataGridView columns
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Product ID";
            dataGridView1.Columns[1].Name = "Product Name";
            dataGridView1.Columns[2].Name = "Product Price";

            // Add rows to the DataGridView
            string[] row = new string[] { "1", "Product 1", "1000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "2", "Product 2", "2000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "3", "Product 3", "3000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "4", "Product 4", "4000" };
            dataGridView1.Rows.Add(row);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RetrieveData();
        }

        private void RetrieveData()
        {
            StringBuilder retrievedData = new StringBuilder();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                    {
                        retrievedData.Append(cell.Value.ToString()).Append(" ");
                    }
                }
                retrievedData.AppendLine();
            }

            // Display the retrieved data in a MessageBox
            MessageBox.Show(retrievedData.ToString(), "Retrieved Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
