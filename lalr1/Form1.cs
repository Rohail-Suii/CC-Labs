using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lalr1
{
    public partial class Form1 : Form
    {
        private Dictionary<string, List<string>> grammar;
        private HashSet<string> nonTerminals;
        private HashSet<string> terminals;
        private Dictionary<int, List<LR1Item>> dfaStates;
        private List<List<LR1Item>> canonicalCollection;
        private Dictionary<Tuple<int, string>, string> parsingTable;
        private Button btnAddProduction;
        private Button btnGenerate;
        private TextBox txtNonTerminal;
        private TextBox txtProduction;

        public Form1()
        {
            InitializeComponent();
            InitializeData();
            SetupEventHandlers();
        }

        private void InitializeData()
        {
            grammar = new Dictionary<string, List<string>>();
            nonTerminals = new HashSet<string>();
            terminals = new HashSet<string>();
            dfaStates = new Dictionary<int, List<LR1Item>>();
            canonicalCollection = new List<List<LR1Item>>();
            parsingTable = new Dictionary<Tuple<int, string>, string>();
        }

        private void SetupEventHandlers()
        {
            btnAddProduction.Click += BtnAddProduction_Click;
            btnGenerate.Click += BtnGenerate_Click;
        }

        private void BtnAddProduction_Click(object sender, EventArgs e)
        {
            string nonTerminal = txtNonTerminal.Text.Trim();
            string production = txtProduction.Text.Trim();

            if (string.IsNullOrEmpty(nonTerminal) || string.IsNullOrEmpty(production))
            {
                MessageBox.Show("Please enter both non-terminal and production.");
                return;
            }

            // Add to grammar
            if (!grammar.ContainsKey(nonTerminal))
            {
                grammar[nonTerminal] = new List<string>();
                nonTerminals.Add(nonTerminal);
            }
            grammar[nonTerminal].Add(production);

            // Extract terminals
            foreach (char c in production)
            {
                string symbol = c.ToString();
                if (!nonTerminals.Contains(symbol) && symbol != "ε")
                {
                    terminals.Add(symbol);
                }
            }

            UpdateGrammarGrid();
            txtProduction.Clear();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (grammar.Count == 0)
            {
                MessageBox.Show("Please add grammar productions first.");
                return;
            }

            GenerateLALR1Parser();
            DisplayParsingTable();
        }

        private void UpdateGrammarGrid()
        {
            dgvGrammar.Rows.Clear();
            foreach (var entry in grammar)
            {
                foreach (var production in entry.Value)
                {
                    dgvGrammar.Rows.Add(entry.Key, "→", production);
                }
            }
        }

        private HashSet<string> First(string input)
        {
            var result = new HashSet<string>();
            
            if (string.IsNullOrEmpty(input))
            {
                result.Add("ε");
                return result;
            }

            var firstSymbol = input[0].ToString();
            
            if (terminals.Contains(firstSymbol))
            {
                result.Add(firstSymbol);
                return result;
            }

            if (nonTerminals.Contains(firstSymbol))
            {
                foreach (var production in grammar[firstSymbol])
                {
                    if (production == "ε")
                    {
                        if (input.Length > 1)
                        {
                            result.UnionWith(First(input.Substring(1)));
                        }
                        else
                        {
                            result.Add("ε");
                        }
                    }
                    else
                    {
                        result.UnionWith(First(production));
                    }
                }
            }

            return result;
        }

        private List<LR1Item> Closure(List<LR1Item> items)
        {
            var closure = new List<LR1Item>(items);
            bool changed;
            do
            {
                changed = false;
                var newItems = new List<LR1Item>();

                foreach (var item in closure)
                {
                    if (item.DotPosition < item.Production.Length)
                    {
                        string nextSymbol = item.Production[item.DotPosition].ToString();
                        if (nonTerminals.Contains(nextSymbol))
                        {
                            foreach (var production in grammar[nextSymbol])
                            {
                                var remainingString = item.Production.Substring(item.DotPosition + 1) + item.Lookahead;
                                var lookAheads = First(remainingString);
                                foreach (var lookahead in lookAheads)
                                {
                                    var newItem = new LR1Item(nextSymbol, production, 0, lookahead);
                                    if (!closure.Contains(newItem) && !newItems.Contains(newItem))
                                    {
                                        newItems.Add(newItem);
                                        changed = true;
                                    }
                                }
                            }
                        }
                    }
                }
                closure.AddRange(newItems);
            } while (changed);

            return closure;
        }

        private List<LR1Item> Goto(List<LR1Item> items, string symbol)
        {
            var gotoItems = new List<LR1Item>();
            foreach (var item in items)
            {
                if (item.DotPosition < item.Production.Length &&
                    item.Production[item.DotPosition].ToString() == symbol)
                {
                    gotoItems.Add(new LR1Item(item.NonTerminal, item.Production,
                        item.DotPosition + 1, item.Lookahead));
                }
            }
            return Closure(gotoItems);
        }

        private void GenerateLALR1Parser()
        {
            // Add augmented grammar production
            string startSymbol = grammar.First().Key;
            var augmentedStartSymbol = startSymbol + "'";
            grammar[augmentedStartSymbol] = new List<string> { startSymbol };
            nonTerminals.Add(augmentedStartSymbol);

            // Initialize first set with augmented start production
            var initialItem = new LR1Item(augmentedStartSymbol, startSymbol, 0, "$");
            var initialState = Closure(new List<LR1Item> { initialItem });
            canonicalCollection.Add(initialState);
            dfaStates[0] = initialState;

            // Generate all states
            for (int i = 0; i < canonicalCollection.Count; i++)
            {
                foreach (var symbol in terminals.Union(nonTerminals))
                {
                    var gotoSet = Goto(canonicalCollection[i], symbol);
                    if (gotoSet.Count > 0)
                    {
                        int existingStateIndex = -1;
                        for (int j = 0; j < canonicalCollection.Count; j++)
                        {
                            if (canonicalCollection[j].Count == gotoSet.Count &&
                                canonicalCollection[j].All(item => gotoSet.Any(g => 
                                    item.NonTerminal == g.NonTerminal &&
                                    item.Production == g.Production &&
                                    item.DotPosition == g.DotPosition)))
                            {
                                existingStateIndex = j;
                                break;
                            }
                        }

                        if (existingStateIndex == -1)
                        {
                            canonicalCollection.Add(gotoSet);
                            dfaStates[canonicalCollection.Count - 1] = gotoSet;
                            parsingTable[Tuple.Create(i, symbol)] = (canonicalCollection.Count - 1).ToString();
                        }
                        else
                        {
                            parsingTable[Tuple.Create(i, symbol)] = existingStateIndex.ToString();
                        }
                    }
                }
            }

            MergeStates();
            GenerateParsingTable();
        }

        private void MergeStates()
        {
            var coreToStateMap = new Dictionary<string, int>();
            var newDfaStates = new Dictionary<int, List<LR1Item>>();
            var stateMapping = new Dictionary<int, int>();

            foreach (var state in dfaStates)
            {
                var core = string.Join("|", state.Value.Select(item =>
                    $"{item.NonTerminal}->{item.Production},{item.DotPosition}"));

                if (coreToStateMap.ContainsKey(core))
                {
                    int existingState = coreToStateMap[core];
                    foreach (var item in state.Value)
                    {
                        if (!newDfaStates[existingState].Contains(item))
                        {
                            newDfaStates[existingState].Add(item);
                        }
                    }
                    stateMapping[state.Key] = existingState;
                }
                else
                {
                    coreToStateMap[core] = state.Key;
                    newDfaStates[state.Key] = new List<LR1Item>(state.Value);
                    stateMapping[state.Key] = state.Key;
                }
            }

            dfaStates = newDfaStates;

            // Update parsing table with merged states
            var newParsingTable = new Dictionary<Tuple<int, string>, string>();
            foreach (var entry in parsingTable)
            {
                var newState = stateMapping[entry.Key.Item1];
                newParsingTable[Tuple.Create(newState, entry.Key.Item2)] = entry.Value;
            }
            parsingTable = newParsingTable;
        }

        private void GenerateParsingTable()
        {
            var newParsingTable = new Dictionary<Tuple<int, string>, string>();

            foreach (var state in dfaStates)
            {
                foreach (var item in state.Value)
                {
                    if (item.DotPosition < item.Production.Length)
                    {
                        string symbol = item.Production[item.DotPosition].ToString();
                        if (parsingTable.ContainsKey(Tuple.Create(state.Key, symbol)))
                        {
                            string action = parsingTable[Tuple.Create(state.Key, symbol)];
                            if (terminals.Contains(symbol))
                            {
                                newParsingTable[Tuple.Create(state.Key, symbol)] = "s" + action;
                            }
                            else
                            {
                                newParsingTable[Tuple.Create(state.Key, symbol)] = action;
                            }
                        }
                    }
                    else
                    {
                        // Reduce actions
                        if (item.NonTerminal == grammar.Keys.First() && item.Lookahead == "$")
                        {
                            newParsingTable[Tuple.Create(state.Key, "$")] = "acc";
                        }
                        else
                        {
                            int productionIndex = GetProductionIndex(item);
                            if (productionIndex != -1)
                            {
                                newParsingTable[Tuple.Create(state.Key, item.Lookahead)] = "r" + productionIndex;
                            }
                        }
                    }
                }
            }

            parsingTable = newParsingTable;
        }

        private int GetProductionIndex(LR1Item item)
        {
            int index = 0;
            foreach (var entry in grammar)
            {
                foreach (var production in entry.Value)
                {
                    if (entry.Key == item.NonTerminal && production == item.Production)
                    {
                        return index;
                    }
                    index++;
                }
            }
            return -1;
        }

        private void DisplayParsingTable()
        {
            dgvParsingTable.Columns.Clear();
            dgvParsingTable.Columns.Add("State", "State");

            // Add terminal columns (including $)
            foreach (var terminal in terminals.Union(new[] { "$" }))
            {
                dgvParsingTable.Columns.Add(terminal, terminal);
            }

            // Add non-terminal columns
            foreach (var nonTerminal in nonTerminals)
            {
                dgvParsingTable.Columns.Add(nonTerminal, nonTerminal);
            }

            // Add rows
            for (int state = 0; state < dfaStates.Count; state++)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dgvParsingTable);
                row.Cells[0].Value = state.ToString();

                int colIndex = 1;
                foreach (var terminal in terminals.Union(new[] { "$" }))
                {
                    var key = Tuple.Create(state, terminal);
                    row.Cells[colIndex].Value = parsingTable.ContainsKey(key) ? parsingTable[key] : "";
                    colIndex++;
                }

                foreach (var nonTerminal in nonTerminals)
                {
                    var key = Tuple.Create(state, nonTerminal);
                    row.Cells[colIndex].Value = parsingTable.ContainsKey(key) ? parsingTable[key] : "";
                    colIndex++;
                }

                dgvParsingTable.Rows.Add(row);
            }
        }
    }

    public class LR1Item
    {
        public string NonTerminal { get; }
        public string Production { get; }
        public int DotPosition { get; }
        public string Lookahead { get; }

        public LR1Item(string nonTerminal, string production, int dotPosition, string lookahead)
        {
            NonTerminal = nonTerminal;
            Production = production;
            DotPosition = dotPosition;
            Lookahead = lookahead;
        }

        public override bool Equals(object obj)
        {
            if (obj is LR1Item other)
            {
                return NonTerminal == other.NonTerminal &&
                       Production == other.Production &&
                       DotPosition == other.DotPosition &&
                       Lookahead == other.Lookahead;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NonTerminal, Production, DotPosition, Lookahead);
        }

        public override string ToString()
        {
            return $"{NonTerminal} → {Production.Insert(DotPosition, "•")}, {Lookahead}";
        }
    }
}