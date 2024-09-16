using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Lab3Task3
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
            dataGridView1.Columns.Add("Element", "Element");
            dataGridView1.Columns.Add("Count", "Count");
            dataGridView1.Columns.Add("Symbols", "Symbols");

            // Take input from a RichTextBox or TextBox
            string inputText = richTextBox1.Text;

            // Count occurrences and symbols for each category
            (int digitCount, Dictionary<string, int> digitSymbols) = CountOccurrencesAndSymbols(inputText, @"\d");
            (int logicalOperatorCount, Dictionary<string, int> logicalOperatorSymbols) = CountOccurrencesAndSymbols(inputText, @"(\&\&|\|\|)");
            (int relationalOperatorCount, Dictionary<string, int> relationalOperatorSymbols) = CountOccurrencesAndSymbols(inputText, @"(\+|-|\*|\/)");
            (int variableCount, Dictionary<string, int> variableSymbols) = CountOccurrencesAndSymbols(inputText, @"\b\w+\b");

            // Display counts in DataGridView
            dataGridView1.Rows.Add("Digits", digitCount, string.Join(", ", digitSymbols.Select(kvp => $"{kvp.Key}: {kvp.Value}")));
            dataGridView1.Rows.Add("Logical Operators", logicalOperatorCount, string.Join(", ", logicalOperatorSymbols.Select(kvp => $"{kvp.Key}: {kvp.Value}")));
            dataGridView1.Rows.Add("Relational Operators", relationalOperatorCount, string.Join(", ", relationalOperatorSymbols.Select(kvp => $"{kvp.Key}: {kvp.Value}")));
            dataGridView1.Rows.Add("Variables", variableCount, string.Join(", ", variableSymbols.Select(kvp => $"{kvp.Key}: {kvp.Value}")));
        }

        private (int count, Dictionary<string, int> symbols) CountOccurrencesAndSymbols(string text, string pattern)
        {
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(text);
            var symbols = new Dictionary<string, int>();

            foreach (Match match in matches)
            {
                string value = match.Value;
                if (symbols.ContainsKey(value))
                {
                    symbols[value]++;
                }
                else
                {
                    symbols[value] = 1;
                }
            }

            return (matches.Count, symbols);
        }
    }
}
