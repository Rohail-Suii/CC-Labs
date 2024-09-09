using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab2Task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputText = richTextBox1.Text;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("Input is empty.");
                return;
            }

            string[] words = inputText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Regex operatorRegex = new Regex(@"&&|\|\||!"); 

            Regex outOfBoundsRegex = new Regex(@"(&&{2,}|(\|\|){2,}|!{6,})");

            richTextBox2.Clear();

            bool foundOperator = false;
            bool outOfBounds = false;

            foreach (string word in words)
            {
                if (outOfBoundsRegex.IsMatch(word))
                {
                    MessageBox.Show("Out of bounds: More than 5 consecutive logical operators.");
                    outOfBounds = true;
                    break;
                }

                Match match = operatorRegex.Match(word);

                if (match.Success)
                {
                    richTextBox2.Text += word + " ";
                    foundOperator = true;
                }
            }

            if (!foundOperator && !outOfBounds)
            {
                MessageBox.Show("No logical operators found.");
            }
        }
    }
}
