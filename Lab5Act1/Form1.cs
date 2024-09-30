using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab5Act1
{
    public partial class Form1 : Form
    {
        private List<List<string>> symbolTable;
        private List<string> keywordList;

        public Form1()
        {
            InitializeComponent();
            InitializeSymbolTable();
        }

        private void InitializeSymbolTable()
        {
            symbolTable = new List<List<string>>();
            keywordList = new List<string> { "int", "float", "while", "main", "if", "else", "new" };
        }

        private void btn_Input_Click(object sender, EventArgs e)
        {
            string userInput = tfInput.Text;
            int row = 1;
            int count = 1;
            int lineNum = 0;

            tfTokens.Clear();
            symbolTableTextBox.Clear();
            symbolTable.Clear();

            string[] lines = userInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                lineNum++;
                ProcessLine(line, ref row, ref count, lineNum);
                tfTokens.AppendText("\n");
            }

            DisplaySymbolTable();
        }

        private void ProcessLine(string line, ref int row, ref int count, int lineNum)
        {
            string[] lexemes = line.Split(new[] { ' ', '=' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lexemes.Length; i++)
            {
                string lexeme = lexemes[i];

                if (IsOperator(lexeme))
                {
                    tfTokens.AppendText($"< op, {lexeme} > ");
                }
                else if (IsDigit(lexeme))
                {
                    tfTokens.AppendText($"< digit, {lexeme} > ");
                }
                else if (IsPunctuation(lexeme))
                {
                    tfTokens.AppendText($"< punc, {lexeme} > ");
                }
                else if (keywordList.Contains(lexeme))
                {
                    // Process variable declaration (e.g., int a)
                    if (i + 1 < lexemes.Length && IsVariable(lexemes[i + 1]))
                    {
                        string variableName = lexemes[i + 1];
                        string variableType = lexeme;
                        ProcessVariable(variableName, variableType, ref row, ref count, lineNum);
                        i++; // Skip over the variable name
                    }
                    tfTokens.AppendText($"<keyword, {lexeme}> ");
                }
                else if (IsVariable(lexeme))
                {
                    // Process variable assignment (we no longer store values, so skip value processing)
                    if (i + 1 < lexemes.Length && lexemes[i + 1] == "=")
                    {
                        string variableName = lexeme;
                        tfTokens.AppendText($"<var, {variableName} assigned> ");
                        break; // Stop processing the rest of the assignment
                    }
                    else
                    {
                        ProcessVariable(lexeme, "unknown", ref row, ref count, lineNum);
                    }
                }
            }
        }

        private void ProcessVariable(string variableName, string variableType, ref int row, ref int count, int lineNum)
        {
            var existingEntry = symbolTable.Find(entry => entry[1] == variableName);
            if (existingEntry != null)
            {
                tfTokens.AppendText($"<var{existingEntry[0]}, {existingEntry[0]}> ");
            }
            else
            {
                // Add to the symbol table without the value
                symbolTable.Add(new List<string> { count.ToString(), variableName, variableType, lineNum.ToString() });
                tfTokens.AppendText($"<var{count}, {row}> ");
                row++;
                count++;
            }
        }

        private void DisplaySymbolTable()
        {
            // Display symbol table without variable values
            foreach (var entry in symbolTable)
            {
                // Only display variable count, name, type, and line number
                symbolTableTextBox.AppendText($"{entry[0]}\t{entry[1]}\t{entry[2]}\t{entry[3]}\n");
            }
        }

        private bool IsOperator(string s) => Regex.IsMatch(s, @"^[-*+/><&&||=]$");
        private bool IsDigit(string s) => Regex.IsMatch(s, @"^[0-9]+([.][0-9]+)?([e]([+|-])?[0-9]+)?$");
        private bool IsPunctuation(string s) => Regex.IsMatch(s, @"^[.,'\[\]{}();:?]$");
        private bool IsVariable(string s) => Regex.IsMatch(s, @"^[A-Za-z|_][A-Za-z|0-9]*$");
    }
}
