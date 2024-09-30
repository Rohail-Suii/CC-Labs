using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab5Task1
{
    public partial class Form1 : Form
    {
        private Dictionary<string, SymbolEntry> symbolTable;
        private List<string> keywordList;

        public Form1()
        {
            InitializeComponent();
            InitializeSymbolTable();
        }

        private void InitializeSymbolTable()
        {
            symbolTable = new Dictionary<string, SymbolEntry>();
            keywordList = new List<string> { "int", "float", "while", "main", "if", "else", "new" };
        }

        private void btn_Input_Click(object sender, EventArgs e)
        {
            string userInput = tfInput.Text;

            // Clear previous outputs
            tfTokens.Clear();
            symbolTableGrid.Rows.Clear();

            string[] lines = userInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            int lineNum = 0;
            foreach (string line in lines)
            {
                lineNum++;
                ProcessLine(line, lineNum);
            }

            DisplaySymbolTable();
        }

        private void ProcessLine(string line, int lineNum)
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
                    // Process variable declaration
                    if (i + 1 < lexemes.Length && IsVariable(lexemes[i + 1]))
                    {
                        string variableName = lexemes[i + 1];
                        string variableType = lexeme;
                        ProcessVariable(variableName, variableType, lineNum);
                        i += 2; // Skip over the variable name and the value
                    }
                    tfTokens.AppendText($"<keyword, {lexeme}> ");
                }
                else if (IsVariable(lexeme))
                {
                    // Process variable assignment
                    if (i + 1 < lexemes.Length && lexemes[i + 1] == "=")
                    {
                        string variableName = lexeme;
                        UpdateVariableValue(variableName, lineNum);
                        tfTokens.AppendText($"<var, {variableName}> ");
                        break; // Process the rest of the line as a single assignment
                    }
                    else
                    {
                        ProcessVariable(lexeme, "unknown", lineNum);
                    }
                }
            }
        }

        private void ProcessVariable(string variableName, string variableType, int lineNum)
        {
            if (!symbolTable.ContainsKey(variableName))
            {
                symbolTable[variableName] = new SymbolEntry
                {
                    VariableName = variableName,
                    VariableType = variableType,
                    LineNumber = lineNum
                };
            }
        }

        private void UpdateVariableValue(string variableName, int lineNum)
        {
            if (symbolTable.ContainsKey(variableName))
            {
                symbolTable[variableName].LineNumber = lineNum; // Update line number if necessary
            }
            else
            {
                tfTokens.AppendText($"<error, {variableName} not declared> ");
            }
        }

        private void DisplaySymbolTable()
        {
            foreach (var entry in symbolTable.Values)
            {
                symbolTableGrid.Rows.Add(entry.VariableName, entry.VariableType, entry.LineNumber);
            }
        }

        private bool IsOperator(string s) => Regex.IsMatch(s, @"^[-*+/><&&||=]$");
        private bool IsDigit(string s) => Regex.IsMatch(s, @"^[0-9]+([.][0-9]+)?([e]([+|-])?[0-9]+)?$");
        private bool IsPunctuation(string s) => Regex.IsMatch(s, @"^[.,'\[\]{}();:?]$");
        private bool IsVariable(string s) => Regex.IsMatch(s, @"^[A-Za-z|_][A-Za-z|0-9]*$");
    }

    public class SymbolEntry
    {
        public string VariableName { get; set; }
        public string VariableType { get; set; }
        public int LineNumber { get; set; }
    }
}
