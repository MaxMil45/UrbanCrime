using System;
using System.Diagnostics;
using System.Windows;

namespace UrbanCrimeFrontend
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckCrime_Click(object sender, RoutedEventArgs e)
        {
            string postcode = PostcodeInput.Text.Trim();
            if (string.IsNullOrEmpty(postcode))
            {
                ResultText.Text = "Please enter a postcode.";
                return;
            }

            // Path to your Python script
            string pythonScript = @"C:\Users\mmurp\UrbanCrime\UrbanCrimeBackend\fetch_crime.py";

            // Setup the process info
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "python";  // or full path to python.exe if not in PATH
            psi.Arguments = $"\"{pythonScript}\" {postcode}";
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            try
            {
                Process proc = Process.Start(psi);

                // Read the output and error
                string output = proc.StandardOutput.ReadToEnd();
                string error = proc.StandardError.ReadToEnd();

                proc.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    ResultText.Text = "Error: " + error;
                }
                else
                {
                    ResultText.Text = output;
                }
            }
            catch (Exception ex)
            {
                ResultText.Text = "Failed to run Python script: " + ex.Message;
            }
        }

        private void PostcodeInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PostcodeInput.Text == "Enter postcode...")
            {
                PostcodeInput.Text = "";
            }
        }

        private void PostcodeInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PostcodeInput.Text))
            {
                PostcodeInput.Text = "Enter postcode...";
            }
        }

    }
}
