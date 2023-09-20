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
                string startOutput = selectedTab.Command.GetStartOutput();

                selectedTab.Content.Text = "";
                selectedTab.Content.AppendText(startOutput);
            }
            InputViewModel.Instance.InputText = "";
        }
    }
}
