using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using MiCommand.Commands;

namespace MiCommand.ViewModels
{
    public class InputViewModel : INotifyPropertyChanged
    {
        public static InputViewModel Instance { get; private set; }

        #region Properties
        private string _inputText;
        public string InputText
        {
            get { return _inputText; } 
            set
            {
                if (value != _inputText)
                {
                    _inputText = value;
                    OnPropertyChanged(nameof(InputText));
                }
            } 
        }
        public TextBox InputWindow { get; set; }
        #endregion

        #region Commands
        public ICommand EnterCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public InputViewModel()
        {
            Instance = this;

            EnterCommand = new EnterCommand();
            ClearCommand = new ClearCommand();
        }

        #region Public Methods
        public void SetPreviousInput()
        {
            string previousInput = OutputViewModel.Instance.SelectedTab.Command.GetPreviousCommand();

            InputWindow.Text = "";
            InputWindow.Focus();
            InputWindow.AppendText(previousInput);
        }
        public void SetNextInput()
        {
            string nextInput = OutputViewModel.Instance.SelectedTab.Command.GetNextCommand();

            InputWindow.Text = "";
            InputWindow.Focus();
            InputWindow.AppendText(nextInput);
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
