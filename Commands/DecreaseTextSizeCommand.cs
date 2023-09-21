using MiCommand.ViewModels;

namespace MiCommand.Commands
{
    public class DecreaseTextSizeCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (MainViewModel.Instance.TextSize > 12)
            {
                MainViewModel.Instance.TextSize--;
                InputViewModel.Instance.TextSize--;
            }
        }
    }
}
