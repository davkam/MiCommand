using MiCommand.ViewModels;

namespace MiCommand.Commands
{
    public class RecentCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (parameter is string buttonContent) 
            {
                string recentCommand = buttonContent.Trim().Split(' ')[1];

                InputViewModel.Instance.InputText = "";
                InputViewModel.Instance.InputWindow.Focus();
                InputViewModel.Instance.InputWindow.AppendText(recentCommand + " ");
            }
        }
    }
}
