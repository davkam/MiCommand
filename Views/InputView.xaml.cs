using System.Windows.Controls;
using System.Windows.Input;
using MiCommand.ViewModels;

namespace MiCommand.Views
{
    public partial class InputView : UserControl
    {
        public InputView()
        {
            InitializeComponent();

            DataContext = new InputViewModel();

            InputViewModel.Instance.InputWindow = inputTextBox;
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender is TextBox textBox)
                {
                    InputViewModel.Instance.InputText = textBox.Text;
                    InputViewModel.Instance.EnterCommand.Execute(null);
                }
            }
            else if (e.Key == Key.Up)
            {
                InputViewModel.Instance.SetPreviousInput();
            }
            else if (e.Key == Key.Down)
            {
                InputViewModel.Instance.SetNextInput();
            }
        }
    }
}
