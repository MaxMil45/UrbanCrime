using System;
using System.Windows;

namespace UrbanCrimeFrontend
{
    public partial class MainWindow : Window
    {
        private int clickCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LiveUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            clickCount++;
            LiveTextBlock.Text = $"Button clicked {clickCount} time{(clickCount == 1 ? "" : "s")}";
        }
    }
}
