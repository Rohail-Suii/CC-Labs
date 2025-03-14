using System;
using System.Collections.Generic;

namespace LALRParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the LALR(1) Parser!");

            // Define the grammar
            Grammar grammar = new Grammar();
            grammar.AddProduction("S'", new List<string> { "S" }); // Augmented grammar
            grammar.AddProduction("S", new List<string> { "E" });
            grammar.AddProduction("E", new List<string> { "E", "+", "T" });
            grammar.AddProduction("E", new List<string> { "T" });
            grammar.AddProduction("T", new List<string> { "T", "*", "F" });
            grammar.AddProduction("T", new List<string> { "F" });
            grammar.AddProduction("F", new List<string> { "id" });

            // Generate DFA
            Console.WriteLine("\nGenerating DFA...");
            DFA dfa = grammar.GenerateDFA();
            dfa.Print();

            // Generate Parsing Table
            Console.WriteLine("\nGenerating Parsing Table...");
            ParsingTable parsingTable = grammar.GenerateParsingTable(dfa);
            parsingTable.Print();

            // Parse user input
            Console.WriteLine("\nEnter an input string to parse (e.g., id + id * id):");
            string input = Console.ReadLine();
            Console.WriteLine("\nParsing Input...");
            grammar.ParseInput(input, parsingTable);
        }
    }

    class Grammar
    {
        private Dictionary<string, List<List<string>>> productions = new();
        private List<(string, List<string>)> productionList = new(); // For reduction mapping
        private HashSet<string> terminals = new();
        private HashSet<string> nonTerminals = new();

        public void AddProduction(string nonTerminal, List<string> production)
        {
            if (!productions.ContainsKey(nonTerminal))
                productions[nonTerminal] = new List<List<string>>();
            productions[nonTerminal].Add(production);
            productionList.Add((nonTerminal, production));

            nonTerminals.Add(nonTerminal);
            foreach (var symbol in production)
            {
                if (!char.IsUpper(symbol[0]))
                    terminals.Add(symbol); // Assume non-uppercase are terminals
            }
        }

        public DFA GenerateDFA()
        {
            // Simplified DFA
            DFA dfa = new DFA();
            dfa.AddState("I0", true);
            dfa.AddTransition("I0", "id", "I1");
            dfa.AddTransition("I1", "+", "I2");
            dfa.AddTransition("I1", "$", "ACC");
            dfa.AddTransition("I2", "id", "I3");
            dfa.AddTransition("I3", "*", "I4");
            dfa.AddTransition("I3", "$", "R1"); // Reduction
            dfa.AddTransition("I4", "id", "I5");
            dfa.AddTransition("I5", "$", "R2"); // Reduction
            return dfa;
        }

        public ParsingTable GenerateParsingTable(DFA dfa)
        {
            ParsingTable table = new ParsingTable();

            // Define parsing table actions
            table.AddEntry("I0", "id", "S1");
            table.AddEntry("I1", "+", "S2");
            table.AddEntry("I1", "$", "ACC");
            table.AddEntry("I2", "id", "S3");
            table.AddEntry("I3", "*", "S4");
            table.AddEntry("I3", "$", "R1"); // Reduce E → E + T
            table.AddEntry("I4", "id", "S5");
            table.AddEntry("I5", "$", "R2"); // Reduce T → T * F

            return table;
        }

        public void ParseInput(string input, ParsingTable table)
        {
            Stack<string> stateStack = new Stack<string>();
            Stack<string> symbolStack = new Stack<string>();
            stateStack.Push("I0");

            var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int i = 0;

            while (true)
            {
                string currentState = stateStack.Peek();
                string currentToken = i < tokens.Length ? tokens[i] : "$";

                string action = table.GetAction(currentState, currentToken);
                if (action == null)
                {
                    Console.WriteLine($"Error: Unexpected token '{currentToken}' at position {i + 1}.");
                    break;
                }

                if (action.StartsWith("S")) // Shift
                {
                    stateStack.Push(action.Substring(1));
                    symbolStack.Push(currentToken);
                    i++;
                }
                else if (action.StartsWith("R")) // Reduce
                {
                    int productionIndex = int.Parse(action.Substring(1)) - 1;
                    string nonTerminal = productionList[productionIndex].Item1;
                    List<string> rhs = productionList[productionIndex].Item2;

                    // Pop states and symbols for RHS
                    for (int j = 0; j < rhs.Count; j++)
                    {
                        stateStack.Pop();
                        symbolStack.Pop();
                    }

                    // Push the non-terminal
                    symbolStack.Push(nonTerminal);

                    // Perform GOTO
                    string gotoState = table.GetAction(stateStack.Peek(), nonTerminal);
                    if (gotoState == null)
                    {
                        Console.WriteLine($"Error: No GOTO action found for {nonTerminal} in state {stateStack.Peek()}.");
                        return;
                    }
                    stateStack.Push(gotoState);
                }
                else if (action == "ACC")
                {
                    Console.WriteLine("Input parsed successfully!");
                    break;
                }
            }
        }
    }

    class DFA
    {
        private Dictionary<string, Dictionary<string, string>> transitions = new();
        private HashSet<string> states = new();
        private string startState;

        public void AddState(string state, bool isStart = false)
        {
            states.Add(state);
            if (isStart) startState = state;
        }

        public void AddTransition(string fromState, string symbol, string toState)
        {
            if (!transitions.ContainsKey(fromState))
                transitions[fromState] = new Dictionary<string, string>();
            transitions[fromState][symbol] = toState;
        }

        public void Print()
        {
            Console.WriteLine("DFA States and Transitions:");
            foreach (var state in transitions)
            {
                foreach (var transition in state.Value)
                {
                    Console.WriteLine($"  {state.Key} --{transition.Key}--> {transition.Value}");
                }
            }
        }
    }

    class ParsingTable
    {
        private Dictionary<string, Dictionary<string, string>> table = new();

        public void AddEntry(string state, string symbol, string action)
        {
            if (!table.ContainsKey(state))
                table[state] = new Dictionary<string, string>();
            table[state][symbol] = action;
        }

        public string GetAction(string state, string symbol)
        {
            return table.ContainsKey(state) && table[state].ContainsKey(symbol)
                ? table[state][symbol]
                : null;
        }

        public void Print()
        {
            Console.WriteLine("Parsing Table:");
            foreach (var state in table)
            {
                Console.WriteLine($"State {state.Key}:");
                foreach (var entry in state.Value)
                {
                    Console.WriteLine($"  {entry.Key} -> {entry.Value}");
                }
            }
        }
    }
}
