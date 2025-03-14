using System;
using System.Collections.Generic;

class Program
{
    // Dictionary to store the grammar rules (non-terminal -> list of productions)
    private Dictionary<string, List<string>> productions;
    // Dictionary to store the FIRST sets
    private Dictionary<string, HashSet<string>> firstSets;
    // Dictionary to store the FOLLOW sets
    private Dictionary<string, HashSet<string>> followSets;
    private Dictionary<string, Dictionary<string, string>> parsingTable = new Dictionary<string, Dictionary<string, string>>();


    public Program()
    {
        productions = new Dictionary<string, List<string>>();
        firstSets = new Dictionary<string, HashSet<string>>();
        followSets = new Dictionary<string, HashSet<string>>();
    }

    // Add a production rule for a non-terminal
    public void AddProduction(string nonTerminal, List<string> productionList)
    {
        if (!productions.ContainsKey(nonTerminal))
        {
            productions[nonTerminal] = new List<string>();
        }
        productions[nonTerminal].AddRange(productionList);
    }

    // Helper function to check if a symbol is terminal
    private bool IsTerminal(char symbol)
    {
        // Assuming terminals are lowercase or special characters
        return !char.IsUpper(symbol);
    }

    // Get or compute the FIRST set of a given non-terminal
    // Get or compute the FIRST set of a given non-terminal
    public HashSet<string> GetFirst(string nonTerminal)
    {
        // If the FIRST set is already computed, return it
        if (firstSets.ContainsKey(nonTerminal))
        {
            return firstSets[nonTerminal];
        }

        HashSet<string> firstSet = new HashSet<string>();

        // Check if the nonTerminal is actually a terminal symbol
        if (IsTerminal(nonTerminal[0])) // Handle terminal directly
        {
            firstSet.Add(nonTerminal);
            return firstSet;
        }

        // Check all productions of the non-terminal
        foreach (string production in productions[nonTerminal])
        {
            // Iterate through each symbol in the production
            for (int i = 0; i < production.Length; i++)
            {
                char symbol = production[i];

                // If the symbol is a terminal, add it to the FIRST set and break
                if (IsTerminal(symbol)) // Terminals are non-uppercase symbols
                {
                    firstSet.Add(symbol.ToString());
                    break;
                }

                // If the symbol is a non-terminal, compute its FIRST set recursively
                string nonTerminalSymbol = symbol.ToString();
                HashSet<string> firstOfSymbol = GetFirst(nonTerminalSymbol);

                // Add all symbols except epsilon (ε) from the first of the non-terminal
                firstSet.UnionWith(firstOfSymbol);
                firstSet.Remove("ε");

                // If epsilon (ε) is in the FIRST set of the non-terminal, check the next symbol
                if (!firstOfSymbol.Contains("ε"))
                {
                    break;
                }
            }
        }

        // Store the computed FIRST set
        firstSets[nonTerminal] = firstSet;

        return firstSet;
    }

    // Get or compute the FOLLOW set of a given non-terminal
    public HashSet<string> GetFollow(string nonTerminal)
    {
        // If the FOLLOW set is already computed, return it
        if (followSets.ContainsKey(nonTerminal))
        {
            return followSets[nonTerminal];
        }

        HashSet<string> followSet = new HashSet<string>();

        // Add $ to the FOLLOW set of the start symbol
        if (nonTerminal == "E") // Assuming E is the start symbol
        {
            followSet.Add("$");
        }

        // Iterate through all productions to find occurrences of nonTerminal
        foreach (var entry in productions)
        {
            string lhs = entry.Key;
            List<string> rhsList = entry.Value;

            foreach (string production in rhsList)
            {
                for (int i = 0; i < production.Length; i++)
                {
                    if (production[i].ToString() == nonTerminal)
                    {
                        // If nonTerminal is not the last symbol
                        if (i + 1 < production.Length)
                        {
                            char nextSymbol = production[i + 1];

                            // If nextSymbol is a terminal, add it to FOLLOW(nonTerminal)
                            if (IsTerminal(nextSymbol))
                            {
                                followSet.Add(nextSymbol.ToString());
                            }
                            else
                            {
                                // Add FIRST(nextSymbol) to FOLLOW(nonTerminal), excluding epsilon
                                HashSet<string> firstOfNext = GetFirst(nextSymbol.ToString());
                                followSet.UnionWith(firstOfNext);
                                followSet.Remove("ε");

                                // If FIRST(nextSymbol) contains epsilon, add FOLLOW(lhs) to FOLLOW(nonTerminal)
                                if (firstOfNext.Contains("ε"))
                                {
                                    followSet.UnionWith(GetFollow(lhs));
                                }
                            }
                        }
                        else // If nonTerminal is the last symbol, add FOLLOW(lhs) to FOLLOW(nonTerminal)
                        {
                            if (lhs != nonTerminal) // To avoid immediate self-recursion
                            {
                                followSet.UnionWith(GetFollow(lhs));
                            }
                        }
                    }
                }
            }
        }

        // Store the computed FOLLOW set
        followSets[nonTerminal] = followSet;

        return followSet;
    }


    // Display the FIRST sets for all non-terminals
    public void DisplayFirstSets()
    {
        foreach (var nonTerminal in productions.Keys)
        {
            Console.Write("FIRST(" + nonTerminal + ") = { ");
            Console.WriteLine(string.Join(", ", GetFirst(nonTerminal)) + " }");
        }
    }

    // Display the FOLLOW sets for all non-terminals
    public void DisplayFollowSets()
    {
        foreach (var nonTerminal in productions.Keys)
        {
            Console.Write("FOLLOW(" + nonTerminal + ") = { ");
            Console.WriteLine(string.Join(", ", GetFollow(nonTerminal)) + " }");
        }
    }
public void BuildParsingTable()
{
    foreach (var entry in productions)
    {
        string nonTerminal = entry.Key;
        List<string> rhsList = entry.Value;

        foreach (var production in rhsList)
        {
            HashSet<string> firstSet = GetFirst(production[0].ToString());

            foreach (var terminal in firstSet)
            {
                if (terminal != "ε")
                {
                    if (!parsingTable.ContainsKey(nonTerminal))
                    {
                        parsingTable[nonTerminal] = new Dictionary<string, string>();
                    }
                    parsingTable[nonTerminal][terminal] = production;
                }
                else
                {
                    // Handle epsilon production: Add FOLLOW(nonTerminal) to the parsing table
                    foreach (var followTerminal in GetFollow(nonTerminal))
                    {
                        if (!parsingTable.ContainsKey(nonTerminal))
                        {
                            parsingTable[nonTerminal] = new Dictionary<string, string>();
                        }
                        parsingTable[nonTerminal][followTerminal] = production;
                    }
                }
            }
        }
    }
}
    public void DisplayParsingTable()
    {
        Console.WriteLine("Parsing Table:");
        foreach (var nonTerminal in parsingTable.Keys)
        {
            foreach (var terminal in parsingTable[nonTerminal].Keys)
            {
                Console.WriteLine($"{nonTerminal} -> {terminal} : {parsingTable[nonTerminal][terminal]}");
            }
        }
    }
    // Method to parse input based on the parsing table
    public void ParseInput(string input)
{
    Stack<string> stack = new Stack<string>();
    stack.Push("$"); // End marker for the stack
    stack.Push("E"); // Start symbol

    Queue<string> inputQueue = new Queue<string>(input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
    inputQueue.Enqueue("$"); // Add end marker to the input

    Console.WriteLine("Parsing Actions:");
    Console.WriteLine("Parsing Stack | Input               | Action");

    while (stack.Count > 0)
    {
        string top = stack.Peek();
        string currentInput = inputQueue.Peek();
        string action = "";

        if (top == currentInput)
        {
            action = $"Match: {top}";
            stack.Pop(); // Match the top of the stack with the current input
            inputQueue.Dequeue(); // Remove the matched input
        }
        else if (IsTerminal(top[0])) // If the top of the stack is a terminal
        {
            action = $"Error: Expected {top} but found {currentInput}";
            Console.WriteLine($"{string.Join(" ", stack)} | {string.Join(" ", inputQueue)} | {action}");
            return; // Parsing fails
        }
        else if (parsingTable.ContainsKey(top))
        {
            // Check if the current input matches any production in the parsing table
            if (parsingTable[top].ContainsKey(currentInput))
            {
                string production = parsingTable[top][currentInput];
                action = $"Apply: {top} -> {production}";

                stack.Pop(); // Remove the top of the stack
                // Push the right-hand side of the production onto the stack in reverse order
                if (production != "ε") // Do not push epsilon
                {
                    for (int i = production.Length - 1; i >= 0; i--)
                    {
                        stack.Push(production[i].ToString());
                    }
                }
            }
            else if (top == "B" && GetFollow("B").Contains(currentInput))
            {
                // If B can produce ε and the next input is in FOLLOW(B), pop B
                action = $"Epsilon production: {top} -> ε";
                stack.Pop();
            }
            else
            {
                action = $"Error: No rule for {top} with input {currentInput}";
                Console.WriteLine($"{string.Join(" ", stack)} | {string.Join(" ", inputQueue)} | {action}");
                return; // Parsing fails
            }
        }
        else
        {
            action = $"Error: No rule for {top} with input {currentInput}";
            Console.WriteLine($"{string.Join(" ", stack)} | {string.Join(" ", inputQueue)} | {action}");
            return; // Parsing fails
        }

        // Display the current state of the stack and input with vertical bars
        Console.WriteLine($"{string.Join(" ", stack)} | {string.Join(" ", inputQueue)} | {action}");
    }

    // Console.WriteLine("inputQueue.Count: " + inputQueue.Count);
    // Final success check
    if (inputQueue.Count==0)
    {
        Console.WriteLine("Input parsed successfully!");
    }
    else
    {
        Console.WriteLine("Error: Input not completely parsed.");
    }
}

    // Helper method to determine if a character is a terminal


    static void Main(string[] args)
    {
        Program grammar = new Program();

        // Example grammar (non-terminals are uppercase, terminals are lowercase or special characters)
        grammar.AddProduction("E", new List<string> { "TG" });
        grammar.AddProduction("G", new List<string> { "+TG", "ε" });
        grammar.AddProduction("T", new List<string> { "FB" });
        grammar.AddProduction("B", new List<string> { "*FB", "ε" });
        grammar.AddProduction("F", new List<string> { "i", "(E)" });

        // Compute and display the FIRST sets
        grammar.DisplayFirstSets();
        Console.Write("========================\n");
        // Compute and display the FOLLOW sets
        grammar.DisplayFollowSets();

        Console.Write("========================\n");
        Console.Write("========================\n");
        // // Build the parsing table
        grammar.BuildParsingTable();
        // // Display the parsing table
        grammar.DisplayParsingTable();
        // Example input to parse
        string input = "( i + i ) * i"; // Space-separated input
        grammar.ParseInput(input);

    }
}
