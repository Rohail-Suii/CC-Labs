using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab2Act2
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
            string var = richTextBox1.Text;

            // Split the input on the basis of space
            string[] words = var.Split(' ');

            // Regular Expression for operators
            Regex regex1 = new Regex(@"^[+|\-|*|/]$");

            // Clear the second richtextbox before displaying results
            richTextBox2.Clear();

            for (int i = 0; i < words.Length; i++)
            {
                Match match1 = regex1.Match(words[i]);

                if (match1.Success)
                {
                    // Append valid operators to the second richtextbox
                    richTextBox2.Text += words[i] + " ";
                }
                else
                {
                    // Show a message for invalid input
                    MessageBox.Show("Invalid input: " + words[i]);
                }
            }
        }
    }
}
