using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab3Act2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Set up the DataGridView columns
            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Name = "Valid Data Types";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear DataGridView for fresh input
            dataGridView1.Rows.Clear();

            // Take input from the richtextbox
            string inputText = richTextBox1.Text;

            // Split the input on the basis of space
            string[] words = inputText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Regular Expression for matching valid data types (int, float, char)
            Regex regex1 = new Regex(@"^(int|float|char)$");

            // Iterate through each word
            foreach (string word in words)
            {
                Match match = regex1.Match(word);

                if (match.Success)
                {
                    // If the word matches a valid data type, add it to the DataGridView
                    dataGridView1.Rows.Add(word);
                }
                else
                {
                    // Show message for invalid input
                    MessageBox.Show("Invalid input: " + word, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
