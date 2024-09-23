using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab4Act1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Input_Click(object sender, EventArgs e)
        {
            // Clear previous results
            tfTokens.Clear();
            symbolTable.Clear();

            // Taking user input from rich textbox
            string userInput = tfInput.Text;

            // List of keywords which will be used to separate keywords from variables
            List<string> keywordList = new List<string> { "int", "float", "while", "main", "if", "else", "new" };

            // Row is an index counter for symbol table
            int row = 1;

            // Count is a variable to increment variable id in tokens 
            int count = 1;

            // Line number counter for lines in user input 
            int line_num = 0;

            // SymbolTable is a 2D array that has the following structure
            // [Index][Variable Name][type][value][line#]
            string[,] SymbolTable = new string[20, 6];

            // Input Buffering: Split the input by lines
            List<string> finalArray = userInput.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> finalArrayc = new List<string>();

            // Regular Expressions
            Regex variable_Reg = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*$");
            Regex constants_Reg = new Regex(@"^[0-9]+([.][0-9]+)?([eE]([+-])?[0-9]+)?$");
            Regex operators_Reg = new Regex(@"^[-*+/><&&||=]$");
            Regex Special_Reg = new Regex(@"^[.,'\[\]{}();:?]$");

            // Process input
            foreach (string line in finalArray)
            {
                line_num++;
                finalArrayc.Clear();
                StringBuilder currentToken = new StringBuilder();

                foreach (char c in line)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        if (currentToken.Length > 0)
                        {
                            finalArrayc.Add(currentToken.ToString());
                            currentToken.Clear();
                        }
                    }
                    else if (operators_Reg.IsMatch(c.ToString()) || Special_Reg.IsMatch(c.ToString()))
                    {
                        if (currentToken.Length > 0)
                        {
                            finalArrayc.Add(currentToken.ToString());
                            currentToken.Clear();
                        }
                        finalArrayc.Add(c.ToString());
                    }
                    else
                    {
                        currentToken.Append(c);
                    }
                }

                if (currentToken.Length > 0)
                {
                    finalArrayc.Add(currentToken.ToString());
                }

                // Process tokens
                for (int i = 0; i < finalArrayc.Count; i++)
                {
                    string token = finalArrayc[i];
                    if (operators_Reg.IsMatch(token))
                    {
                        tfTokens.AppendText($"< op, {token} > ");
                    }
                    else if (constants_Reg.IsMatch(token))
                    {
                        tfTokens.AppendText($"< digit, {token} > ");
                    }
                    else if (Special_Reg.IsMatch(token))
                    {
                        tfTokens.AppendText($"< punc, {token} > ");
                    }
                    else if (variable_Reg.IsMatch(token))
                    {
                        if (!keywordList.Contains(token))
                        {
                            // Detect variable declaration (supporting both `int` and `float`)
                            if (i >= 1 && (finalArrayc[i - 1] == "int" || finalArrayc[i - 1] == "float"))
                            {
                                // This is a variable declaration and assignment (check if there is a value)
                                string varType = finalArrayc[i - 1];
                                string varValue = (i + 2 < finalArrayc.Count && finalArrayc[i + 1] == "=") ? finalArrayc[i + 2] : "N/A";

                                // Update symbol table and token output
                                if (row < SymbolTable.GetLength(0))
                                {
                                    SymbolTable[row, 0] = row.ToString();  // Index
                                    SymbolTable[row, 1] = token;           // Variable name
                                    SymbolTable[row, 2] = varType;         // Type
                                    SymbolTable[row, 3] = varValue;        // Value
                                    SymbolTable[row, 4] = line_num.ToString();  // Line number

                                    tfTokens.AppendText($"<var{count}, {row}> ");
                                    symbolTable.AppendText($"{row}\t{token}\t{varType}\t{varValue}\t{line_num}\n");
                                    row++;
                                    count++;
                                }
                            }
                            else
                            {
                                // This is just a variable reference
                                tfTokens.AppendText($"<var, {token}> ");
                            }
                        }
                        else
                        {
                            tfTokens.AppendText($"<keyword, {token}> ");
                        }
                    }
                }

                tfTokens.AppendText("\n");
            }
        }
    }
}