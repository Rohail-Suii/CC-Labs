using System;
using System.Windows.Forms;

namespace Lab3Act3
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Ensure that the correct namespace and form class are used
            Application.Run(new Form1());  // This should reference Form1
        }
    }
}
