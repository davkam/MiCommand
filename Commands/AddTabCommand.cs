using MiCommand.ViewModels;

namespace MiCommand.Commands
{
    public class AddTabCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            OutputViewModel.Instance.AddTab();
        }
    }
}
