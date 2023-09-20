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

        private string _languageText;
        public string LanguageText
        {
            get { return _languageText; }
            set
            {
                if (value != _languageText)
                {
                    _languageText = value;
                    OnPropertyChanged(nameof(LanguageText));
                }
            }
        }
        #endregion

        public ICommand LanguageCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            Instance = this;

            EnglishChecked = true;
            SwedishChecked = false;

            LanguageText = "LANGUAGE:";

            LanguageCommand = new LanguageCommand();
        }

        #region Public Methods
        public void SetEnglishLanguage()
        {
            LanguageText = "LANGUAGE:";
        }
        public void SetSwedishLanguage()
        {
            LanguageText = "SPRÅK:";
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
