using System.Windows.Controls;
using MiCommand.ViewModels;

namespace MiCommand.Views
{
    public partial class CommonView : UserControl
    {
        public CommonView()
        {
            InitializeComponent();

            DataContext = new CommonViewModel();
        }
    }
}
