using MiCommand.ViewModels;

namespace MiCommand.Commands
{
    public class RemoveTabCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            OutputViewModel.Instance.RemoveTab();
        }
    }
}
