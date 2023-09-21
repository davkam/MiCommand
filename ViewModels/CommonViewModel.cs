using System.ComponentModel;
using System.Windows.Input;
using MiCommand.Commands.CommonViewCommands;

namespace MiCommand.ViewModels
{
    public class CommonViewModel : INotifyPropertyChanged
    {
        public static CommonViewModel Instance { get; private set; }

        #region Properties
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

        public string BasicButtonText { get; set; }
        public string NetButtonText { get; set; }
        public string FileButtonText { get; set; }
        public string MediaButtonText { get; set; }
        public string MiscButtonText { get; set; }
        public string BasicButtonTooltip { get; set; }
        public string NetButtonTooltip { get; set; }
        public string FileButtonTooltip { get; set; }
        public string MediaButtonTooltip { get; set; }
        public string MiscButtonTooltip { get; set; }
        #endregion

        #region Commands
        public ICommand BasicButtonCommand { get; private set; }
        public ICommand NetButtonCommand { get; private set; }
        public ICommand FileButtonCommand { get; private set; }
        public ICommand MediaButtonCommand { get; private set; }
        public ICommand MiscButtonCommand { get; private set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public CommonViewModel()
        {
            Instance = this;

            BasicButtonCommand = new BasicButtonCommand();
            NetButtonCommand = new NetButtonCommand();
            FileButtonCommand = new FileButtonCommand();
            MediaButtonCommand = new MediaButtonCommand();
            MiscButtonCommand = new MiscButtonCommand();

            SetEnglishLanguage();
        }

        #region Public Methods
        public void SetEnglishLanguage()
        {
            CommonCommandsText = "COMMON COMMANDS";

            BasicButtonText = "BASIC";
            NetButtonText = "NET";
            FileButtonText = "FILE";
            MediaButtonText = "MEDIA";
            MiscButtonText = "MISC";

            BasicButtonTooltip = "Show basic commands";
            NetButtonTooltip = "Show network commands";
            FileButtonTooltip = "Show file commands";
            MediaButtonTooltip = "Show media commands";
            MiscButtonTooltip = "Show miscellaneous commands";

            InvokePropertyChanged();
        }
        public void SetSwedishLanguage()
        {
            CommonCommandsText = "VANLIGA KOMMANDON";

            BasicButtonText = "GRUND";
            NetButtonText = "NÄT";
            FileButtonText = "FIL";
            MediaButtonText = "MEDIA";
            MiscButtonText = "ÖVRIG";

            BasicButtonTooltip = "Visa grundläggande kommandon";
            NetButtonTooltip = "Visa nätverk kommandon";
            FileButtonTooltip = "Visa fil kommandon";
            MediaButtonTooltip = "Visa media kommandon";
            MiscButtonTooltip = "Visa övriga kommandon";

            InvokePropertyChanged();
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InvokePropertyChanged()
        {
            OnPropertyChanged(nameof(BasicButtonText));
            OnPropertyChanged(nameof(NetButtonText));
            OnPropertyChanged(nameof(FileButtonText));
            OnPropertyChanged(nameof(MediaButtonText));
            OnPropertyChanged(nameof(MiscButtonText));

            OnPropertyChanged(nameof(BasicButtonTooltip));
            OnPropertyChanged(nameof(NetButtonTooltip));
            OnPropertyChanged(nameof(FileButtonTooltip));
            OnPropertyChanged(nameof(MediaButtonTooltip));
            OnPropertyChanged(nameof(MiscButtonTooltip));
        }
    }
}
