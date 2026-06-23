using ADN_Test.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ADN_Test.Views
{
    public partial class EmbarqueDetailWindow : Window
    {
        public EmbarqueDetailWindow(EmbarqueDetailViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.RequestClose += (_, _) => Close();
            Owner = Application.Current.MainWindow;
        }

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox == null) return;

            // Combine current text with the proposed new character
            string fullText = GetProposedText(textBox, e.Text);

            // Regex allows digits, and an optional single decimal point
            Regex regex = new(@"^\d*\.?\d*$");

            e.Handled = !regex.IsMatch(fullText);
        }

        private string GetProposedText(TextBox textBox, string newText)
        {
            string text = textBox.Text;

            // Remove highlighted/selected text that will be replaced
            if (textBox.SelectionLength > 0)
            {
                text = text.Remove(textBox.SelectionStart, textBox.SelectionLength);
            }

            return text.Insert(textBox.SelectionStart, newText);
        }
    }
}
