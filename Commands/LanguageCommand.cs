using MiCommand.ViewModels;
using System;
using System.Windows;

namespace MiCommand.Commands
{
    public class LanguageCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (MainViewModel.Instance.EnglishChecked)
            {
                MainViewModel.Instance.SetEnglishLanguage();
                OutputViewModel.Instance.SetEnglishLanguage();
                InputViewModel.Instance.SetEnglishLanguage();
                CommonViewModel.Instance.SetEnglishLanguage();
                RecentViewModel.Instance.SetEnglishLanguage();
            }
            else
            {
                MainViewModel.Instance.SetSwedishLanguage();
                OutputViewModel.Instance.SetSwedishLanguage();
                InputViewModel.Instance.SetSwedishLanguage();
                CommonViewModel.Instance.SetSwedishLanguage();
                RecentViewModel.Instance.SetSwedishLanguage();
            }
        }
    }
}
