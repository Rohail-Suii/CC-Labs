namespace labTask1
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
            this.txtDisplay = new TextBox
            {
                // Remove ReadOnly so the user can input data
                ReadOnly = false,
                Text = "0",
                Dock = DockStyle.Top,
                Font = new System.Drawing.Font("Arial", 24F)
            };
            this.Controls.Add(txtDisplay);

            this.buttons = new Button[4];
            string[] buttonTexts = { "Sin", "Cos", "Tan", "log" };

            for (int i = 0; i < 4; i++)
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
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            try
            {
                double inputValue = double.Parse(txtDisplay.Text);

                if (button.Text == "Sin")
                {
                    double sin = Math.Sin(inputValue);
                    txtDisplay.Text = sin.ToString();
                }
                else if (button.Text == "Cos")
                {
                    double cos = Math.Cos(inputValue);
                    txtDisplay.Text = cos.ToString();
                }
                else if (button.Text == "Tan")
                {
                    double tan = Math.Tan(inputValue);
                    txtDisplay.Text = tan.ToString();
                }
                else if (button.Text == "log")
                {
                    double log = Math.Log(inputValue);
                    txtDisplay.Text = log.ToString();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
