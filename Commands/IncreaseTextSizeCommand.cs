using MiCommand.ViewModels;

namespace MiCommand.Commands
{
    public class IncreaseTextSizeCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (MainViewModel.Instance.TextSize < 18)
            {
                MainViewModel.Instance.TextSize++;
                InputViewModel.Instance.TextSize++;
            }
        }
    }
}
