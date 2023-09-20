using MiCommand.Models;
using MiCommand.ViewModels;

namespace MiCommand.Commands
{
    public class EnterCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            Tab selectedTab = OutputViewModel.Instance.SelectedTab;

            if (selectedTab != null)
            {
                string inputCommand = InputViewModel.Instance.InputText;
                selectedTab.Command.RunCommand(inputCommand);
            }
            InputViewModel.Instance.InputText = "";
        }
    }
}
