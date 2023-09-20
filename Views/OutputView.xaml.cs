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
        }
    }
}
