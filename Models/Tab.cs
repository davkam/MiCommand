using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MiCommand.ViewModels;

namespace MiCommand.Models
{
    public class Tab
    {
        #region Properties
        public int TabIndex { get; set; }
        public string Header { get; set; }
        public TextBox Content { get; set; }
        public Command Command { get; set; }
        #endregion

        public Tab()
        {
            Command = new Command();

            SetTabIndex();
            SetHeader();
            SetContent();
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
            Header = $"Tab {TabIndex}";
        }
        private void SetContent()
        {
            Content = GetTextBox();
            Content.AppendText(Command.GetStartOutput());
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
            };

            return newTextBox;
        }
        #endregion
    }
}
