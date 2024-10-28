using System.Collections;
using System.Text.RegularExpressions;

namespace Lab6Act1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Hashtable productionRulez = new Hashtable();
        Hashtable firstSets = new Hashtable();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            productionRulez.Clear();
            firstSets.Clear();
            richTextBox2.Text = "";

            //flag used to check for small letters non-terminals
            bool flag = true;

            //array of all the production rules
            String[] productionRules = richTextBox1.Text.Split('\n');

            //loop through all the production rules
            foreach (var productionRule in productionRules)
            {
                //seperate the left and right part of the rule
                String[] splittedRule = productionRule.Split('>');
                //if rule of a non terminal is not in hashtable, then put it in the hashtable
                if (!productionRulez.Contains(splittedRule[0]))
                {
                    productionRulez.Add(splittedRule[0], splittedRule[1]);
                    var te = splittedRule[0];
                    if (!(new Regex(@"^(([A-Z]+)|([A-Z]+[-]*[A-Z]+))[`]?$")).Match(te + "").Success)
                    {
                        flag = false;
                        MessageBox.Show("Non terminals cant be small letters");
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
                    richTextBox2.AppendText("First(" + x.Key.ToString() + ") = " + "{" + x.Value.ToString() + "}\n");
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

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


