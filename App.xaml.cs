using System.Windows;
using MiCommand.Views;

namespace MiCommand
{
    public partial class App : Application
    {
        public App()
        {
            MainWindow = new MainView();
        }
    }
}
