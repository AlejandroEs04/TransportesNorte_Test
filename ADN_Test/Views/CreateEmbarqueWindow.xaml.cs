using ADN_Test.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ADN_Test.Views
{
    public partial class CreateEmbarqueWindow : Window
    {
        public CreateEmbarqueWindow(CreateEmbarqueViewModel viewModel)
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

            string fullText = GetProposedText(textBox, e.Text);
            Regex regex = new(@"^\d*\.?\d*$");
            e.Handled = !regex.IsMatch(fullText);
        }

        private string GetProposedText(TextBox textBox, string newText)
        {
            string text = textBox.Text;
            if (textBox.SelectionLength > 0)
            {
                text = text.Remove(textBox.SelectionStart, textBox.SelectionLength);
            }
            return text.Insert(textBox.SelectionStart, newText);
        }
    }
}
