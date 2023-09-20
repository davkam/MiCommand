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
        private string _enterButton;
        public string EnterButton
        {
            get { return _enterButton; }
            set
            {
                if (value != _enterButton)
                {
                    _enterButton = value;
                    OnPropertyChanged(nameof(EnterButton));
                }
            }
        }
        private string _clearButton;
        public string ClearButton
        {
            get { return _clearButton; }
            set
            {
                if (value != _clearButton)
                {
                    _clearButton = value;
                    OnPropertyChanged(nameof(ClearButton));
                }
            }
        }
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

            EnterButton = "ENTER";
            ClearButton = "CLEAR";

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
        public void SetEnglishLanguage()
        {
            EnterButton = "ENTER";
            ClearButton = "CLEAR";
        }
        public void SetSwedishLanguage()
        {
            EnterButton = "ANGE";
            ClearButton = "RENSA";
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
