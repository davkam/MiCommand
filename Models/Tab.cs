using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MiCommand.ViewModels;

namespace MiCommand.Models
{
    public class Tab : INotifyPropertyChanged
    {
        private static bool firstTab = true;

        #region Properties
        public int TabIndex { get; set; }

        private string _header;
        public string Header 
        { 
            get {  return _header; }
            set
            {
                if (value !=  _header)
                {
                    _header = value;
                    OnPropertyChanged(nameof(Header));
                }
            }
        }
        public TextBox Content { get; set; }
        public Command Command { get; set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public Tab()
        {
            SetTabIndex();
            SetHeader();
            SetContent();

            Command = new Command();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Private Methods
        private void SetTabIndex()
        {
            if (OutputViewModel.Instance.TabItems.Count == 0)
            {
                TabIndex++;
            }
            else
            {
                TabIndex = OutputViewModel.Instance.TabItems.Last().TabIndex + 1;
            }
        }
        private void SetHeader()
        {
            if (firstTab)
            {
                Header = $"Tab {TabIndex}";
                firstTab = false;
            }
            else
            {
                if (MainViewModel.Instance.EnglishChecked && OutputViewModel.Instance.LargeTabs)
                {
                    Header = $"Tab {TabIndex}";
                }
                else if (MainViewModel.Instance.SwedishChecked && OutputViewModel.Instance.LargeTabs)
                {
                    Header = $"Flik {TabIndex}";
                }
                else
                {
                    Header = TabIndex.ToString();
                }
            }
        }
        private void SetContent()
        {
            Content = GetTextBox();
        }
        private TextBox GetTextBox()
        {
            TextBox newTextBox = new TextBox()
            {
                IsReadOnly = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Background = Brushes.Transparent,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#edf2f4")),
                BorderThickness = new Thickness(0),
                Margin = new Thickness(10),
                TextWrapping = TextWrapping.Wrap
            };

            return newTextBox;
        }
        #endregion
    }
}
