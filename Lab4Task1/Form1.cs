using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab4Task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAnalyze_Click(object sender, EventArgs e)
        {
            string documentText = textBoxInput.Text; // Get document text from the TextBox
            int bufferSize = 1024; // Size of each buffer
            char[] buffer1 = new char[bufferSize];
            char[] buffer2 = new char[bufferSize];

            using (StringReader reader = new StringReader(documentText))
            {
                bool useBuffer1 = true;
                int bytesRead1 = reader.Read(buffer1, 0, bufferSize);
                int bytesRead2 = 0;

                while (bytesRead1 > 0 || bytesRead2 > 0)
                {
                    if (useBuffer1)
                    {
                        ProcessBuffer(buffer1, bytesRead1);
                        bytesRead2 = reader.Read(buffer2, 0, bufferSize);
                    }
                    else
                    {
                        ProcessBuffer(buffer2, bytesRead2);
                        bytesRead1 = reader.Read(buffer1, 0, bufferSize);
                    }

                    useBuffer1 = !useBuffer1; // Switch buffers
                }
            }
        }

        private void ProcessBuffer(char[] buffer, int bytesRead)
        {
            if (bytesRead == 0) return; // Nothing to process

            string bufferContent = new string(buffer, 0, bytesRead);
            var tokenCounts = Tokenize(bufferContent);

            // Clear the DataGridView before adding new results
            dataGridViewTokens.Rows.Clear();

            // Add results to the DataGridView
            foreach (var entry in tokenCounts)
            {
                dataGridViewTokens.Rows.Add(entry.Token, entry.Type, entry.Count);
            }
        }

        private List<(string Token, string Type, int Count)> Tokenize(string input)
        {
            var tokens = new Dictionary<string, (string Type, int Count)>();
            var patterns = new Dictionary<string, string>
            {
                { "Identifier", @"\b[A-Za-z_][A-Za-z0-9_]*\b" },
                { "Integer", @"\b\d+\b" },
                { "Float", @"\b\d+\.\d+\b" },
                { "Operator", @"[+\-*/=]" },
                { "Whitespace", @"\s+" }
            };

            foreach (var pattern in patterns)
            {
                Regex regex = new Regex(pattern.Value);
                MatchCollection matches = regex.Matches(input);

                foreach (Match match in matches)
                {
                    if (pattern.Key != "Whitespace") // Ignore whitespaces
                    {
                        if (tokens.ContainsKey(match.Value))
                        {
                            tokens[match.Value] = (pattern.Key, tokens[match.Value].Count + 1);
                        }
                        else
                        {
                            tokens[match.Value] = (pattern.Key, 1);
                        }
                    }
                }
            }

            // Convert dictionary to list for returning
            var tokenList = new List<(string Token, string Type, int Count)>();
            foreach (var entry in tokens)
            {
                tokenList.Add((entry.Key, entry.Value.Type, entry.Value.Count));
            }

            return tokenList;
        }
    }
}