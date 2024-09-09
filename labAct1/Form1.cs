using System;
using System.Windows.Forms;

namespace labAct1
{
    public partial class Form1 : Form
    {
        private TextBox txtDisplay;
        private Button[] buttons;
        private string operation = "";
        private double result = 0;
        private bool isOperationPerformed = false;

        public Form1()
        {
            InitializeComponent();
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {
            // Initialize the display TextBox
            this.txtDisplay = new TextBox 
            { 
                ReadOnly = true, 
                Text = "0", 
                Dock = DockStyle.Top, 
                Font = new System.Drawing.Font("Arial", 24F) 
            };
            this.Controls.Add(txtDisplay);

            // Initialize the buttons array and create calculator buttons
            this.buttons = new Button[16];
            string[] buttonTexts = { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", "0", ".", "=", "+" };

            for (int i = 0; i < 16; i++)
            {
                buttons[i] = new Button
                {
                    Text = buttonTexts[i],
                    Font = new System.Drawing.Font("Arial", 18F),
                    Width = 80,
                    Height = 60,
                    Left = (i % 4) * 80,
                    Top = (i / 4) * 60 + 50
                };
                buttons[i].Click += Button_Click;
                this.Controls.Add(buttons[i]);
            }

            // Set the form size
            this.Width = 350;
            this.Height = 400;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // Handle digit and decimal point button clicks
            if (char.IsDigit(button.Text, 0) || button.Text == ".")
            {
                if ((txtDisplay.Text == "0") || isOperationPerformed)
                    txtDisplay.Clear();

                isOperationPerformed = false;
                txtDisplay.Text += button.Text;
            }
            // Handle clear (C) button click
            else if (button.Text == "C")
            {
                txtDisplay.Text = "0";
                result = 0;
                operation = "";
            }
            // Handle equals (=) button click
            else if (button.Text == "=")
            {
                switch (operation)
                {
                    case "+":
                        txtDisplay.Text = (result + double.Parse(txtDisplay.Text)).ToString();
                        break;
                    case "-":
                        txtDisplay.Text = (result - double.Parse(txtDisplay.Text)).ToString();
                        break;
                    case "*":
                        txtDisplay.Text = (result * double.Parse(txtDisplay.Text)).ToString();
                        break;
                    case "/":
                        txtDisplay.Text = (result / double.Parse(txtDisplay.Text)).ToString();
                        break;
                }
                result = double.Parse(txtDisplay.Text);
                operation = "";
            }
            // Handle operator button clicks
            else
            {
                operation = button.Text;
                result = double.Parse(txtDisplay.Text);
                isOperationPerformed = true;
            }
        }
    }
}
