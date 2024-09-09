using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab2Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Take input from the first richtextbox
            string inputText = richTextBox1.Text;

            // Check if input is not empty
            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("Input is empty.");
                return;
            }

            // Split the input based on spaces
            string[] words = inputText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Regular Expression for relational operators
            Regex regex = new Regex(@"==|!=|<=|>=|<|>");

            // Clear previous output
            richTextBox2.Clear();

            bool foundOperator = false;

            foreach (string word in words)
            {
                Match match = regex.Match(word);

                if (match.Success)
                {
                    // Append valid relational operators to the second richtextbox
                    richTextBox2.Text += word + " ";
                    foundOperator = true;
                }
            }

            // Show a message if no relational operators are found
            if (!foundOperator)
            {
                MessageBox.Show("No relational operators found.");
            }
        }
    }
}
