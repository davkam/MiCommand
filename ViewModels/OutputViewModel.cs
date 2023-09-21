using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using MiCommand.Commands;
using MiCommand.Models;

namespace MiCommand.ViewModels
{
    public class OutputViewModel : INotifyPropertyChanged
    {
        public static OutputViewModel Instance { get; private set; }

        #region Properties
        private ObservableCollection<Tab> _tabItems;
        public ObservableCollection<Tab> TabItems
        {
            get { return _tabItems; }
            set
            {
                if (value != _tabItems)
                {
                    _tabItems = value;
                    OnPropertyChanged(nameof(TabItems));
                }
            }
        }

        private Tab _selectedTab;
        public Tab SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (value != _selectedTab)
                {
                    _selectedTab = value;
                    OnPropertyChanged($"{nameof(SelectedTab)}");
                }
            }
        }

        private string _addTabTooltip;
        public string AddTabTooltip 
        { 
            get { return _addTabTooltip; }
            set
            {
                if (value != _addTabTooltip)
                {
                    _addTabTooltip = value;
                    OnPropertyChanged(nameof(AddTabTooltip));
                }
            }
        }
        private string _removeTabTooltip;
        public string RemoveTabTooltip
        {
            get { return _removeTabTooltip; }
            set
            {
                if (value != _removeTabTooltip)
                {
                    _removeTabTooltip = value;
                    OnPropertyChanged(nameof(RemoveTabTooltip));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand AddTabCommand { get; private set; }
        public ICommand RemoveTabCommand { get; private set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public OutputViewModel()
        {
            Instance = this;

            TabItems = new ObservableCollection<Tab>();

            AddTabCommand = new AddTabCommand();
            RemoveTabCommand = new RemoveTabCommand();

            SetEnglishLanguage();

            AddTab();
        }

        #region Public Methods
        public void AddTab()
        {
            SelectedTab = new Tab();
            TabItems.Add(SelectedTab);
        }
        public void RemoveTab()
        {
            if (SelectedTab != null)
            {
                SelectedTab.Command.EndProcess();
                TabItems.Remove(SelectedTab);
            }
        }
        public void SetEnglishLanguage()
        {
            AddTabTooltip = "Add tab";
            RemoveTabTooltip = "Remove tab";

            foreach (Tab tab in TabItems)
            {
                char tabIndex = tab.Header.Last();
                tab.Header = "Tab " + tabIndex;
            }
        }
        public void SetSwedishLanguage()
        {
            AddTabTooltip = "Lägg till flik";
            RemoveTabTooltip = "Ta bort flik";

            foreach (Tab tab in TabItems)
            {
                char tabIndex = tab.Header.Last();
                tab.Header = "Flik " + tabIndex;
            }
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
