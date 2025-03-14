using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab7Act1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Hashtable productionRulez = new Hashtable();
        Hashtable followSets = new Hashtable();
        Hashtable firstSets = new Hashtable();
        Dictionary<string, Dictionary<string, string>> parsingTable = new Dictionary<string, Dictionary<string, string>>();


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

        private void button2_Click(object sender, EventArgs e)
        {
            productionRulez.Clear();
            firstSets.Clear();
            richTextBox2.Text = "";

            bool flag = true;

            string[] productionRules = richTextBox1.Text.Split('\n');

            foreach (var productionRule in productionRules)
            {
                // Split each rule at '>' and check for the correct format
                string[] splittedRule = productionRule.Split('>');

                if (splittedRule.Length < 2)
                {
                    MessageBox.Show("Invalid production rule format. Each rule must contain a '>' symbol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                // Add to hashtable if not present, and check for lowercase letters in non-terminal
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
                    richTextBox3.AppendText("First(" + x.Key.ToString() + ") = {" + x.Value.ToString() + "}\n");
                }
            }
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
            //if it is a terminal
            if (!productionRulez.Contains(rule[0]) && rule[0] != "epsilon")
            {
                return rule[0];
            }
            //case of non-terminal
            else if (rule[0] != "epsilon" && rule.Length >= 1 && index < rule.Length)
            {

                string fsOfNt = nonTerminalCase(rule[index]);

                if (fsOfNt.Contains("epsilon")) // if NT contains ~ calcaulate first of next Non-terminal
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


        private String[] removeEmptyStringsFromArr(string[] array)
        {
            var temp = new List<string>();
            foreach (string s in array)
            {
                if (!string.IsNullOrEmpty(s))
                    temp.Add(s);
            }
            return temp.ToArray();
        }


        //removing extra commas and repeated terminals from first set
        private string refineFirstSet(string firstset)
        {
            string finalFirstSet = "";
            String[] temparr = firstset.Split(',');

            for (int i = 0; i < temparr.Length; i++)
            {
                string item = temparr[i];
                /* Console.WriteLine(item);
                 Console.WriteLine(finalFirstSet.Contains(item));*/
                if (item != " " && item != "" && !finalFirstSet.Contains(item))
                {
                    if (i != 0)
                    {
                        finalFirstSet += ",";
                    }
                    finalFirstSet += item;
                }
            }

            return finalFirstSet;
        }

        private void printArr(String[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(i + ": " + arr[i]);
            }
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateParsingTable();
        }
        private void CreateParsingTable()
        {
            // Initialize the parsing table
            foreach (DictionaryEntry rule in productionRulez)
            {
                string nonTerminal = (string)rule.Key;
                parsingTable.Add(nonTerminal, new Dictionary<string, string>());
            }

            // Populate the parsing table
            foreach (DictionaryEntry rule in productionRulez)
            {
                string nonTerminal = (string)rule.Key;
                string[] productions = rule.Value.ToString().Split('|');

                foreach (var production in productions)
                {
                    var firstSet = GetFirstSet(production.Trim(), productionRulez, new HashSet<string>()).Split(',');
                    bool containsEpsilon = firstSet.Contains("epsilon");

                    if (containsEpsilon || production.Trim() == "epsilon")
                    {
                        // Add follow set to parsing table if epsilon is in first set or production is epsilon
                        var followSet = followSets[nonTerminal].ToString().Split(',');
                        foreach (var follow in followSet)
                        {
                            parsingTable[nonTerminal][follow] = production.Trim();
                        }
                    }
                    else
                    {
                        // Add first set to parsing table
                        foreach (var first in firstSet)
                        {
                            parsingTable[nonTerminal][first] = production.Trim();
                        }
                    }
                }
            }

            // Optional: Display the parsing table for debugging or inspection
            DisplayParsingTable();
        }

        private void DisplayParsingTable()
        {
            richTextBox3.Clear(); // Clear existing content in richTextBox3

            foreach (var nonTerminal in parsingTable.Keys)
            {
                richTextBox3.AppendText("Non-Terminal: " + nonTerminal + "\n");

                foreach (var terminal in parsingTable[nonTerminal].Keys)
                {
                    string production = parsingTable[nonTerminal][terminal];
                    richTextBox3.AppendText($"  Table[{nonTerminal}, {terminal}] = {production}\n");
                }

                richTextBox3.AppendText("\n"); // Add a new line after each non-terminal's entries
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string input = richTextBox4.Text.Trim(); // User input to be parsed
            string startSymbol = productionRulez.Keys.Cast<string>().First(); // Start symbol for parsing
            bool isValid = ParseInput(input, startSymbol);

            if (isValid)
            {
                MessageBox.Show("The input is valid according to the grammar.", "Parsing Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The input does not conform to the grammar.", "Parsing Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ParseInput(string input, string startSymbol)
        {
            Stack<string> stack = new Stack<string>();
            stack.Push("$"); // End of input symbol
            stack.Push(startSymbol); // Start with the start symbol of the grammar

            Queue<string> tokens = new Queue<string>(input.Split(' ')); // Tokenize the input based on spaces
            tokens.Enqueue("$"); // Add end of input symbol to tokens

            while (stack.Count > 0)
            {
                string top = stack.Peek();
                string token = tokens.Peek();

                // If stack top matches the current token
                if (top == token)
                {
                    stack.Pop();
                    tokens.Dequeue();
                }
                // If stack top is a terminal that doesn't match the input token, return false
                else if (!parsingTable.ContainsKey(top) && top != "$")
                {
                    return false;
                }
                // If top is a non-terminal and has an entry in the parsing table for the token
                else if (parsingTable.ContainsKey(top) && parsingTable[top].ContainsKey(token))
                {
                    stack.Pop(); // Remove the non-terminal

                    string production = parsingTable[top][token];
                    if (production != "epsilon") // Push production symbols in reverse order if not epsilon
                    {
                        var symbols = production.Split(' ').Reverse();
                        foreach (var symbol in symbols)
                        {
                            stack.Push(symbol);
                        }
                    }
                }
                // If no matching rule is found in the parsing table, return false
                else
                {
                    return false;
                }
            }

            // Input is valid if all tokens are processed
            return tokens.Count == 0;
        }
    }
}
