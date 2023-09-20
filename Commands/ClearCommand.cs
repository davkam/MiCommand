using MiCommand.Models;
using MiCommand.ViewModels;

namespace MiCommand.Commands
{
    public class ClearCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            Tab selectedTab = OutputViewModel.Instance.SelectedTab;

            if (selectedTab != null)
            {
                // TBD! Needs a more efficient solution.
                selectedTab.Content.Text = string.Empty;
                selectedTab.Command.EndProcess();
                selectedTab.Command.StartProcess();
            }
            InputViewModel.Instance.InputText = "";
        }
    }
}
