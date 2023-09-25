using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        private TabControl _tabControl;
        public TabControl TabControl
        {
            get { return _tabControl; }
            set
            {
                if (value != _tabControl)
                {
                    _tabControl = value;
                    OnPropertyChanged(nameof(TabControl));
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
        public bool LargeTabs { get; private set; } = true;
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

            AddTab(true);
        }

        #region Public Methods
        public void AddTab(bool startTab = false)
        {
            if (startTab)
            {
                SelectedTab = new Tab();
                TabItems.Add(SelectedTab);
            }
            else if (CheckAvailableTabSpace())
            {
                SelectedTab = new Tab();
                TabItems.Add(SelectedTab);
            }
            else
            {
                MessageBox.Show("NO MORE TAB SPACE");
            }
        }
        public void RemoveTab()
        {
            if (SelectedTab != null)
            {
                SelectedTab.Command.EndProcess();
                TabItems.Remove(SelectedTab);

                CheckAvailableTabSpace();
            }
        }
        public bool CheckAvailableTabSpace()
        {
            int tabWidth;

            if (LargeTabs) tabWidth = 40;
            else tabWidth = 20;

            int occupiedWidth = TabItems.Count * tabWidth;
            int actualWidth = (int)TabControl.ActualWidth - 50;
            int availableWidth = actualWidth - occupiedWidth;

            if (availableWidth >= tabWidth && LargeTabs)
            {
                //MainViewModel.Instance.AdjustMinWidth(occupiedWidth + 400);

                return true;
            }
            else if (availableWidth < tabWidth && LargeTabs)
            {
                //MainViewModel.Instance.AdjustMinWidth(occupiedWidth + 400);

                DecreaseTabSize();

                return true;
            }
            else if (availableWidth >= tabWidth && !LargeTabs)
            {
                //MainViewModel.Instance.AdjustMinWidth(occupiedWidth + 400);

                IncreaseTabSize();

                return true;
            }
            else
            {
                return false;
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

        private void IncreaseTabSize()
        {
            if (LargeTabs) return;

            int occupiedWidth = TabItems.Count * 40;
            int actualWidth = (int)TabControl.ActualWidth - 50;
            int availableWidth = actualWidth - occupiedWidth;

            if (availableWidth < 0) return;

            foreach (Tab tab in TabItems)
            {
                if (MainViewModel.Instance.EnglishChecked) 
                {
                    tab.Header = tab.Header.Insert(0, "Tab ");
                }
                else
                {
                    tab.Header = tab.Header.Insert(0, "Flik ");
                }
            }

            LargeTabs = true;
        }
        private void DecreaseTabSize()
        {
            if (!LargeTabs) return;

            foreach (Tab tab in TabItems)
            {
                if (MainViewModel.Instance.EnglishChecked)
                {
                    tab.Header = tab.Header.Remove(0, 4);
                }
                else
                {
                    tab.Header = tab.Header.Remove(0, 5);
                }
            }

            LargeTabs = false;
        }
    }
}
