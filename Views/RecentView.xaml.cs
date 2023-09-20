using System.Windows;
using System.Windows.Controls;
using MiCommand.ViewModels;

namespace MiCommand.Views
{
    public partial class RecentView : UserControl
    {
        public RecentView()
        {
            InitializeComponent();

            DataContext = new RecentViewModel();

            RecentViewModel viewModel = (RecentViewModel)DataContext;

            viewModel.FirstItemsControl = firstItemsControl;
            viewModel.SecondItemsControl = secondItemsControl;
        }

        private void OnItemsControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RecentViewModel.Instance.SortRecentLists();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                RecentViewModel.Instance.RecentCommand.Execute(button.Content);
            }
        }
    }
}
