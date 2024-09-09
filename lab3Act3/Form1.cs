using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab3Act3
{
    public partial class Form1 : Form // Inherits from Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Take input from the first richtextbox
            String var = richTextBox1.Text;

            // Split the input on the basis of space
            String[] words = var.Split(' ');

            // Regular Expression for variables (1st character must be letter, followed by up to 24 letters/numbers)
            Regex regex1 = new Regex(@"^[A-Za-z][A-Za-z0-9]{0,5}$");

            for (int i = 0; i < words.Length; i++)
            {
                Match match1 = regex1.Match(words[i]);

                if (match1.Success)
                {
                    // Append valid variable names to the second richtextbox
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
