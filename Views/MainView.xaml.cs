using System.Windows;
using MiCommand.Models;
using MiCommand.ViewModels;

namespace MiCommand.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

            Closed += OnMainWindowClosed;
        }

        private void OnMainWindowClosed(object sender, System.EventArgs e)
        {
            foreach (Tab tab in OutputViewModel.Instance.TabItems)
            {
                tab.Command.Process.Close();
            }
        }
    }
}
