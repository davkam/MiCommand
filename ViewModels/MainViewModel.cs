using MiCommand.Commands;
using System.ComponentModel;
using System.Windows.Input;

namespace MiCommand.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public static MainViewModel Instance { get; private set; }

        #region Properties
        private bool _englishChecked;
        public bool EnglishChecked 
        { 
            get {  return _englishChecked; } 
            set
            {
                if (value != _englishChecked)
                {
                    _englishChecked = value;
                    OnPropertyChanged(nameof(EnglishChecked));
                }
            } 
        }

        private bool _swedishChecked;
        public bool SwedishChecked
        {
            get { return _swedishChecked; }
            set
            {
                if (value != _swedishChecked)
                {
                    _swedishChecked = value;
                    OnPropertyChanged(nameof(SwedishChecked));
                }
            }
        }

        private int _textSize;
        public int TextSize
        {
            get { return _textSize; }
            set
            {
                if (value != _textSize)
                {
                    _textSize = value;
                    OnPropertyChanged(nameof(TextSize));
                }
            }
        }
        public string LanguageText { get; set; }
        public string IncreaseTextSizeTooltip { get; set; }
        public string DecreaseTextSizeTooltip { get; set; }
        #endregion

        #region Commands
        public ICommand LanguageCommand { get; private set; }
        public ICommand IncreaseTextSizeCommand { get; private set; }
        public ICommand DecreaseTextSizeCommand { get; private set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            Instance = this;

            EnglishChecked = true;
            SwedishChecked = false;

            TextSize = 14;

            LanguageCommand = new LanguageCommand();
            IncreaseTextSizeCommand = new IncreaseTextSizeCommand();
            DecreaseTextSizeCommand = new DecreaseTextSizeCommand();

            SetEnglishLanguage();
        }

        #region Public Methods
        public void SetEnglishLanguage()
        {
            LanguageText = "LANGUAGE:";
            IncreaseTextSizeTooltip = "Increase text size";
            DecreaseTextSizeTooltip = "Decrease text size";

            InvokePropertyChanged();
        }
        public void SetSwedishLanguage()
        {
            LanguageText = "SPRÅK:";
            IncreaseTextSizeTooltip = "Öka textstorlek";
            DecreaseTextSizeTooltip = "Minska textstorlek";

            InvokePropertyChanged();
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InvokePropertyChanged()
        {
            OnPropertyChanged(nameof(LanguageText));
            OnPropertyChanged(nameof(IncreaseTextSizeTooltip));
            OnPropertyChanged(nameof(DecreaseTextSizeTooltip));
        }
    }
}
