using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MiCommand.Commands;

namespace MiCommand.ViewModels
{
    public class RecentViewModel : INotifyPropertyChanged
    {
        public static RecentViewModel Instance { get; private set; }

        private List<string> _recentCommands;

        #region Properties
        private string _recentCommmandsText;
        public string RecentCommandsText 
        {
            get { return _recentCommmandsText; } 
            set
            {
                if (value != _recentCommmandsText)
                {
                    _recentCommmandsText = value;
                    OnPropertyChanged(nameof(RecentCommandsText));
                }
            }
        }
        private ItemsControl _firstItemsControl;
        public ItemsControl FirstItemsControl
        {
            get { return _firstItemsControl; }
            set
            {
                if (_firstItemsControl != value)
                {
                    _firstItemsControl = value;
                    OnPropertyChanged(nameof(FirstItemsControl));
                }
            }
        }

        private ItemsControl _secondItemsControl;
        public ItemsControl SecondItemsControl
        {
            get { return _secondItemsControl; }
            set
            {
                if (_secondItemsControl != value)
                {
                    _secondItemsControl = value;
                    OnPropertyChanged(nameof(SecondItemsControl));
                }
            }
        }
        public ObservableCollection<string> FirstRecentCommands { get; private set; }
        public ObservableCollection<string> SecondRecentCommands { get; private set; }
        #endregion

        public ICommand RecentCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RecentViewModel()
        {
            Instance = this;

            _recentCommands = new List<string>();

            RecentCommandsText = "RECENT COMMANDS";

            FirstRecentCommands = new ObservableCollection<string>();
            SecondRecentCommands = new ObservableCollection<string>();

            RecentCommand = new RecentCommand();
        }

        #region Public Methods
        public void AddRecentCommand(string recentCommand)
        {
            string firstCommand = recentCommand.Trim().Split(' ').First();

            if (!_recentCommands.Contains(firstCommand))
            {
                if (CheckAvailableSpace(FirstItemsControl, FirstRecentCommands) || CheckAvailableSpace(SecondItemsControl, SecondRecentCommands))
                {
                    _recentCommands.Add(firstCommand);
                }
                else
                {
                    _recentCommands.RemoveAt(0);
                    _recentCommands.Add(firstCommand);
                }
                SortRecentLists();
            }
        }
        public void SortRecentLists()
        {
            FirstRecentCommands.Clear();
            SecondRecentCommands.Clear();

            foreach (string recentCommand in _recentCommands)
            {
                if (CheckAvailableSpace(FirstItemsControl, FirstRecentCommands))
                {
                    FirstRecentCommands.Add(" > " + recentCommand);
                }
                else if (CheckAvailableSpace(SecondItemsControl, SecondRecentCommands))
                {
                    SecondRecentCommands.Add(" > " + recentCommand);
                }
                else
                {
                    return;
                }
            }
        }
        public void SetEnglishLanguage()
        {
            RecentCommandsText = "RECENT COMMANDS";
        }
        public void SetSwedishLanguage()
        {
            RecentCommandsText = "SENASTE KOMMANDON";
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CheckAvailableSpace(ItemsControl itemControl, ObservableCollection<string> itemCollection)
        {
            double totalSpace = itemControl.ActualHeight;
            double occupiedSpace = itemCollection.Count * 30;
            double availableSpace = totalSpace - occupiedSpace;

            if ((availableSpace / 30) >= 1) return true;
            else return false;
        }
    }
}
