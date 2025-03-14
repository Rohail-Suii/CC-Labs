using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace Lab7Act1
{
    public partial class Form1 : Form
    {
        // Fields
        private Hashtable productionRulez = new Hashtable();
        private Hashtable followSets = new Hashtable();
        private Hashtable firstSets = new Hashtable();
        private Dictionary<string, Dictionary<string, string>> parsingTable = new Dictionary<string, Dictionary<string, string>>();

        // UI Controls
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox3;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox inputStringTextBox;
        private Button parseButton;
        private RichTextBox parseOutputTextBox;
        private Label lblGrammar;
        private Label lblInput;
        private Label lblParseOutput;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.inputStringTextBox = new System.Windows.Forms.TextBox();
            this.parseButton = new System.Windows.Forms.Button();
            this.parseOutputTextBox = new System.Windows.Forms.RichTextBox();
            this.lblGrammar = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.lblParseOutput = new System.Windows.Forms.Label();

            // Form settings
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "Grammar Parser";

            // Grammar Input Label
            this.lblGrammar.AutoSize = true;
            this.lblGrammar.Location = new System.Drawing.Point(12, 9);
            this.lblGrammar.Name = "lblGrammar";
            this.lblGrammar.Size = new System.Drawing.Size(100, 13);
            this.lblGrammar.Text = "Enter Grammar Rules:";

            // Grammar Input TextBox
            this.richTextBox1.Location = new System.Drawing.Point(12, 25);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(300, 200);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";

            // Follow Sets Button
            this.button1.Location = new System.Drawing.Point(12, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Follow Sets";
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // First Sets Button
            this.button2.Location = new System.Drawing.Point(112, 235);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "First Sets";
            this.button2.Click += new System.EventHandler(this.button2_Click);

            // Create Parsing Table Button
            this.button3.Location = new System.Drawing.Point(212, 235);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Create Table";
            this.button3.Click += new System.EventHandler(this.button3_Click);

            // Sets Output
            this.richTextBox2.Location = new System.Drawing.Point(330, 25);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(200, 200);
            this.richTextBox2.TabIndex = 4;
            this.richTextBox2.Text = "";

            // Parsing Table Output
            this.richTextBox3.Location = new System.Drawing.Point(540, 25);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(240, 200);
            this.richTextBox3.TabIndex = 5;
            this.richTextBox3.Text = "";

            // Input String Label
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(12, 270);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(100, 13);
            this.lblInput.Text = "Enter Input String:";

            // Input String TextBox
            this.inputStringTextBox.Location = new System.Drawing.Point(12, 290);
            this.inputStringTextBox.Name = "inputStringTextBox";
            this.inputStringTextBox.Size = new System.Drawing.Size(300, 20);
            this.inputStringTextBox.TabIndex = 6;

            // Parse Button
            this.parseButton.Location = new System.Drawing.Point(320, 288);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(75, 23);
            this.parseButton.TabIndex = 7;
            this.parseButton.Text = "Parse";
            this.parseButton.Click += new System.EventHandler(this.ParseButton_Click);

            // Parse Output Label
            this.lblParseOutput.AutoSize = true;
            this.lblParseOutput.Location = new System.Drawing.Point(12, 320);
            this.lblParseOutput.Name = "lblParseOutput";
            this.lblParseOutput.Size = new System.Drawing.Size(100, 13);
            this.lblParseOutput.Text = "Parsing Output:";

            // Parse Output TextBox
            this.parseOutputTextBox.Location = new System.Drawing.Point(12, 340);
            this.parseOutputTextBox.Name = "parseOutputTextBox";
            this.parseOutputTextBox.Size = new System.Drawing.Size(768, 240);
            this.parseOutputTextBox.TabIndex = 8;
            this.parseOutputTextBox.Text = "";

            // Add controls to form
            this.Controls.Add(this.lblGrammar);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.inputStringTextBox);
            this.Controls.Add(this.parseButton);
            this.Controls.Add(this.lblParseOutput);
            this.Controls.Add(this.parseOutputTextBox);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            productionRulez.Clear();
            followSets.Clear();
            richTextBox2.Text = "";

            string[] productionRules = richTextBox1.Text.Split('\n');

            foreach (var productionRule in productionRules)
            {
                string[] splittedRule = productionRule.Split('>');
                if (!productionRulez.Contains(splittedRule[0]))
                {
                    productionRulez.Add(splittedRule[0], splittedRule[1]);
                }
                else
                {
                    productionRulez[splittedRule[0]] += "|" + splittedRule[1];
                }
            }

            foreach (DictionaryEntry rule in productionRulez)
            {
                followSets[rule.Key] = "";
            }

            followSets[productionRules[0].Split('>')[0]] = "$";

            bool added;
            do
            {
                added = false;
                foreach (DictionaryEntry rule in productionRulez)
                {
                    string nonTerminal = rule.Key.ToString();
                    string[] rules = rule.Value.ToString().Split('|');

                    foreach (var r in rules)
                    {
                        string[] symbols = r.Split(' ');

                        for (int i = 0; i < symbols.Length; i++)
                        {
                            if (!productionRulez.Contains(symbols[i])) continue;

                            string follow = "";
                            if (i + 1 < symbols.Length)
                            {
                                follow = GetFirstSet(symbols[i + 1], productionRulez, new HashSet<string>());
                                if (follow.Contains("epsilon"))
                                {
                                    follow = follow.Replace("epsilon", "") + "," + followSets[nonTerminal];
                                }
                            }
                            else
                            {
                                follow = followSets[nonTerminal].ToString();
                            }

                            string currentFollow = followSets[symbols[i]].ToString();
                            foreach (var item in follow.Split(','))
                            {
                                if (!string.IsNullOrEmpty(item) && !currentFollow.Contains(item))
                                {
                                    followSets[symbols[i]] = currentFollow + (string.IsNullOrEmpty(currentFollow) ? "" : ",") + item;
                                    added = true;
                                }
                            }
                        }
                    }
                }
            } while (added);

            foreach (DictionaryEntry x in followSets)
            {
                richTextBox2.AppendText("Follow(" + x.Key.ToString() + ") = " + "{" + x.Value.ToString() + "}\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            productionRulez.Clear();
            firstSets.Clear();
            richTextBox2.Text = "";

            bool flag = true;

            string[] productionRules = richTextBox1.Text.Split('\n');

            foreach (var productionRule in productionRules)
            {
                string[] splittedRule = productionRule.Split('>');

                if (splittedRule.Length < 2)
                {
                    MessageBox.Show("Invalid production rule format. Each rule must contain a '>' symbol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                if (!productionRulez.Contains(splittedRule[0]))
                {
                    productionRulez.Add(splittedRule[0], splittedRule[1]);
                    var te = splittedRule[0];

                    if (!new Regex(@"^(([A-Z]+)|([A-Z]+[-]*[A-Z]+))[`]?$").IsMatch(te))
                    {
                        flag = false;
                        MessageBox.Show("Non-terminals can't be lowercase letters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    productionRulez[splittedRule[0]] += "|" + splittedRule[1];
                }
            }

            if (flag)
            {
                foreach (DictionaryEntry rule in productionRulez)
                {
                    getFirstSet(rule.Key.ToString());
                }

                foreach (DictionaryEntry x in firstSets)
                {
                    richTextBox2.AppendText("First(" + x.Key.ToString() + ") = {" + x.Value.ToString() + "}\n");
                }
            }
        }

        private string GetFirstSet(string symbol, Hashtable rules, HashSet<string> visited)
        {
            if (!rules.Contains(symbol))
            {
                return symbol;
            }

            if (visited.Contains(symbol))
            {
                return "";
            }
            visited.Add(symbol);

            string result = "";
            string[] productions = rules[symbol].ToString().Split('|');
            foreach (string production in productions)
            {
                string[] symbols = production.Trim().Split(' ');
                if (symbols[0] == "epsilon")
                {
                    result += "epsilon,";
                }
                else
                {
                    foreach (string s in symbols)
                    {
                        string firstSet = GetFirstSet(s, rules, new HashSet<string>(visited));
                        result += firstSet + ",";
                        if (!firstSet.Contains("epsilon"))
                        {
                            break;
                        }
                    }
                }
            }

            result = result.TrimEnd(',');
            return string.Join(",", result.Split(',').Distinct());
        }

        private void getFirstSet(string nonTerminal)
        {
            List<String[]> rules = new List<String[]>();

            foreach (String rul in productionRulez[nonTerminal].ToString().Split('|'))
            {
                rules.Add(removeEmptyStringsFromArr(rul.Split(' ')));
            }
            foreach (String[] rul in rules)
            {
                if (rul[0] != nonTerminal)
                {
                    if (!firstSets.Contains(nonTerminal))
                    {
                        firstSets.Add(nonTerminal, refineFirstSet(calculateFirst(rul, 0)));
                    }
                    else
                    {
                        firstSets[nonTerminal] += "," + refineFirstSet(calculateFirst(rul, 0));
                    }
                }
            }
        }

        private string calculateFirst(String[] rule, int index)
        {
            if (!productionRulez.Contains(rule[0]) && rule[0] != "epsilon")
            {
                return rule[0];
            }
            else if (rule[0] != "epsilon" && rule.Length >= 1 && index < rule.Length)
            {
                string fsOfNt = nonTerminalCase(rule[index]);

                if (fsOfNt.Contains("epsilon"))
                {
                    return fsOfNt + calculateFirst(rule, index + 1);
                }
                else
                {
                    return fsOfNt;
                }
            }

            return "epsilon";
        }

        private string nonTerminalCase(string nonTerminal)
        {
            List<String[]> rules = new List<String[]>();

            foreach (String rul in productionRulez[nonTerminal].ToString().Split('|'))
            {
                rules.Add(removeEmptyStringsFromArr(rul.Split(' ')));
            }

            string firstSet = "";

            foreach (String[] rul in rules)
            {
                if (rul[0] != nonTerminal)
                {
                    string fs = calculateFirst(rul, 0);
                    firstSet += fs + ",";
                }
            }

            return firstSet;
        }

        private String[] removeEmptyStringsFromArr(string[] arr)
        {
            return arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        private string refineFirstSet(string firstSet)
        {
            // Removes duplicates from the first set
            return string.Join(",", firstSet.Split(',').Distinct());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Code for creating the parsing table
            parsingTable.Clear();
            richTextBox3.Text = "";

            foreach (DictionaryEntry rule in productionRulez)
            {
                string nonTerminal = rule.Key.ToString();
                string[] rules = rule.Value.ToString().Split('|');

                foreach (var production in rules)
                {
                    string[] symbols = production.Split(' ');
                    string first = GetFirstSet(symbols[0], productionRulez, new HashSet<string>());

                    foreach (var term in first.Split(','))
                    {
                        if (term != "epsilon")
                        {
                            if (!parsingTable.ContainsKey(nonTerminal))
                            {
                                parsingTable[nonTerminal] = new Dictionary<string, string>();
                            }

                            if (!parsingTable[nonTerminal].ContainsKey(term))
                            {
                                parsingTable[nonTerminal][term] = production;
                            }
                        }
                        else
                        {
                            foreach (var follow in followSets[nonTerminal].ToString().Split(','))
                            {
                                if (!parsingTable.ContainsKey(nonTerminal))
                                {
                                    parsingTable[nonTerminal] = new Dictionary<string, string>();
                                }

                                parsingTable[nonTerminal][follow] = production;
                            }
                        }
                    }
                }
            }

            foreach (var row in parsingTable)
            {
                richTextBox3.AppendText(row.Key + " -> ");
                foreach (var col in row.Value)
                {
                    richTextBox3.AppendText($"[{col.Key}: {col.Value}] ");
                }
                richTextBox3.AppendText("\n");
            }
        }

        private void ParseButton_Click(object sender, EventArgs e)
        {
            string inputString = inputStringTextBox.Text + "$";
            Stack<string> stack = new Stack<string>();
            stack.Push("$");
            stack.Push(productionRulez.Keys.Cast<string>().First());

            int index = 0;
            parseOutputTextBox.Clear();

            // Add header
            parseOutputTextBox.AppendText("Parsing Actions:\n");
            parseOutputTextBox.AppendText("Parsing Stack".PadRight(25) + "| " +
                                         "Input".PadRight(20) + "| " +
                                         "Action\n");
            parseOutputTextBox.AppendText(new string('-', 80) + "\n");

            while (stack.Count > 0)
            {
                string top = stack.Peek();
                string currentSymbol = index < inputString.Length ? inputString[index].ToString() : "$";

                // Get remaining input
                string remainingInput = inputString.Substring(index);

                // Get stack contents as string
                string stackContent = string.Join(" ", stack.Reverse());

                if (top == "$" && currentSymbol == "$")
                {
                    // Format the final step
                    parseOutputTextBox.AppendText(stackContent.PadRight(25) + "| " +
                                                remainingInput.PadRight(20) + "| " +
                                                "Accept\n");
                    parseOutputTextBox.AppendText("\nParsing successful! Input is accepted.\n");
                    return;
                }
                else if (!productionRulez.Contains(top))
                {
                    if (top == currentSymbol)
                    {
                        // Format match action
                        parseOutputTextBox.AppendText(stackContent.PadRight(25) + "| " +
                                                    remainingInput.PadRight(20) + "| " +
                                                    $"Match: {top}\n");
                        stack.Pop();
                        index++;
                    }
                    else
                    {
                        // Format error
                        parseOutputTextBox.AppendText(stackContent.PadRight(25) + "| " +
                                                    remainingInput.PadRight(20) + "| " +
                                                    $"Error: Expected {top} but found {currentSymbol}\n");
                        return;
                    }
                }
                else if (parsingTable.ContainsKey(top) && parsingTable[top].ContainsKey(currentSymbol))
                {
                    string production = parsingTable[top][currentSymbol];
                    // Format production application
                    parseOutputTextBox.AppendText(stackContent.PadRight(25) + "| " +
                                                remainingInput.PadRight(20) + "| " +
                                                $"Apply: {top} -> {production}\n");

                    stack.Pop();
                    if (production != "epsilon")
                    {
                        string[] symbols = production.Split(' ');
                        for (int i = symbols.Length - 1; i >= 0; i--)
                        {
                            stack.Push(symbols[i]);
                        }
                    }
                }
                else
                {
                    // Format error
                    parseOutputTextBox.AppendText(stackContent.PadRight(25) + "| " +
                                                remainingInput.PadRight(20) + "| " +
                                                $"Error: No rule for {top} with input {currentSymbol}\n");
                    return;
                }
            }

            parseOutputTextBox.AppendText("\nParsing completed.\n");
        }
    }
}
