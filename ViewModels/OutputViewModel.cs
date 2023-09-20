using System.Collections.ObjectModel;
using System.ComponentModel;
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
            SelectedTab.Command.Process.Close();
            TabItems.Remove(SelectedTab);
        }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
