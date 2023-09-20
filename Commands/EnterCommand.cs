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
                string outputCommand = selectedTab.Command.EnterCommand(inputCommand);

                selectedTab.Content.AppendText(inputCommand + "\n");
                selectedTab.Content.AppendText(outputCommand);
                selectedTab.Content.ScrollToEnd();
            }
            InputViewModel.Instance.InputText = "";
        }
    }
}
