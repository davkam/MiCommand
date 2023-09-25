using System.Windows;
using System.Windows.Controls;
using MiCommand.ViewModels;

namespace MiCommand.Views
{
    public partial class OutputView : UserControl
    {
        public OutputView()
        {
            InitializeComponent();

            DataContext = new OutputViewModel();

            OutputViewModel.Instance.TabControl = tabControl;
        }

        private void OnTabControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            OutputViewModel.Instance.CheckAvailableTabSpace();
        }
    }
}
