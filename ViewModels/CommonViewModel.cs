using System.ComponentModel;

namespace MiCommand.ViewModels
{
    public class CommonViewModel : INotifyPropertyChanged
    {
        public static CommonViewModel Instance { get; private set; }

        private string _commonCommandsText;
        public string CommonCommandsText 
        { 
            get {  return _commonCommandsText; } 
            set
            {
                if (value != _commonCommandsText)
                {
                    _commonCommandsText = value;
                    OnPropertyChanged(nameof(CommonCommandsText));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CommonViewModel()
        {
            Instance = this;

            CommonCommandsText = "COMMON COMMANDS";
        }

        #region Public Methods
        public void SetEnglishLanguage()
        {
            CommonCommandsText = "COMMON COMMANDS";
        }
        public void SetSwedishLanguage()
        {
            CommonCommandsText = "VANLIGA KOMMANDON";
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
