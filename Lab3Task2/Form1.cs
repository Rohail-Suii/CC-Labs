using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab3Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

         private void button1_Click(object sender, EventArgs e)
        {
            // Clear DataGridView before each run
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            
            // Add columns to the DataGridView
            dataGridView1.Columns.Add("Expression", "Expression");
            dataGridView1.Columns.Add("Exponent", "Exponent");
            dataGridView1.Columns.Add("Result", "Result");

            // Regular expression for scientific notation format
            Regex regex = new Regex(@"^([+-]?\d+)[eE]([+-]?\d+)$");

            // Take input from a RichTextBox or TextBox
            string inputText = richTextBox1.Text;

            // Split the input on the basis of space or other delimiters
            string[] words = inputText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                Match match = regex.Match(word);

                if (match.Success)
                {
                    // Get the base value and exponent value from the match
                    string baseValue = match.Groups[1].Value;
                    string exponentValue = match.Groups[2].Value;

                    // Parse the base and exponent
                    double baseNum = double.Parse(baseValue);
                    int exponentNum = int.Parse(exponentValue);

                    // Calculate the result as base^exponent
                    double result = Math.Pow(baseNum, exponentNum);

                    // Add the expression, exponent, and result to the DataGridView
                    dataGridView1.Rows.Add(word, exponentNum, result);
                }
                else
                {
                    // Show message for invalid input
                    MessageBox.Show("Invalid input: " + word);
                }
            }
        }
            
    }
}
