using System;
using System.Collections;

namespace CollectionsApplication
{
   class Program
   {
      static void Main(string[] args)
      {
         Stack st = new Stack();
         bool continueInput = true;

         Console.WriteLine("Stack Operations:");
         Console.WriteLine("1. Push (Add) a value");
         Console.WriteLine("2. Pop (Remove) the top value");
         Console.WriteLine("3. Peek (View) the top value");
         Console.WriteLine("4. Display the stack");
         Console.WriteLine("5. Exit");

         while (continueInput)
         {
            Console.Write("\nChoose an operation (1-5): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
               case "1": // Push operation
                  Console.Write("Enter a character to push onto the stack: ");
                  char value;
                  if (char.TryParse(Console.ReadLine(), out value))
                  {
                     st.Push(value);
                     Console.WriteLine($"{value} has been added to the stack.");
                  }
                  else
                  {
                     Console.WriteLine("Invalid input. Please enter a single character.");
                  }
                  break;

               case "2": // Pop operation
                  if (st.Count > 0)
                  {
                     Console.WriteLine($"Popped value: {st.Pop()}");
                  }
                  else
                  {
                     Console.WriteLine("The stack is empty. Nothing to pop.");
                  }
                  break;

               case "3": // Peek operation
                  if (st.Count > 0)
                  {
                     Console.WriteLine($"The next poppable value in stack: {st.Peek()}");
                  }
                  else
                  {
                     Console.WriteLine("The stack is empty. Nothing to peek.");
                  }
                  break;

               case "4": // Display the stack
                  if (st.Count > 0)
                  {
                     Console.WriteLine("Current stack: ");
                     foreach (char c in st)
                     {
                        Console.Write(c + " ");
                     }
                     Console.WriteLine();
                  }
                  else
                  {
                     Console.WriteLine("The stack is empty.");
                  }
                  break;

               case "5": // Exit
                  continueInput = false;
                  Console.WriteLine("Exiting...");
                  break;

               default:
                  Console.WriteLine("Invalid choice. Please choose a number between 1 and 5.");
                  break;
            }
         }
      }
   }
}