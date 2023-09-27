using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MiCommand.ViewModels;

namespace MiCommand.Models
{
    public class Command
    {
        private static int initialOutput = 0;

        #region Private Fields
        private ManualResetEvent outputDataReceivedEvent = new ManualResetEvent(false);
        private ManualResetEvent errorDataReceivedEvent = new ManualResetEvent(false);

        private bool _validInput;
        private int _recentCommandIndex;
        #endregion

        #region Properties
        public Process CommandProcess { get; private set; }
        public Tab CommandTab { get; private set; }
        public List<string> RecentInputs { get; private set; }
        public List<string> RecentOutputs { get; private set; }
        #endregion

        public Command(Tab tab)
        {
            _validInput = false;
            _recentCommandIndex = 0;

            CommandTab = tab;

            RecentInputs = new List<string>();
            RecentOutputs = new List<string>();

            StartProcess();
        }

        #region Public Methods
        public void StartProcess()
        {
            RecentOutputs.Clear();

            CommandProcess = new Process();

            CommandProcess.StartInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            CommandProcess.OutputDataReceived += OnOutputDataReceived;
            CommandProcess.ErrorDataReceived += OnErrorDataReceived;

            CommandProcess.Start();
            CommandProcess.BeginOutputReadLine();
            CommandProcess.BeginErrorReadLine();

            CommandProcess.StandardInput.WriteLine("");
            CommandProcess.StandardInput.Flush();
        }
        public void EndProcess()
        {
            if (CommandProcess != null && !CommandProcess.HasExited)
            {
                CommandProcess.OutputDataReceived -= OnOutputDataReceived;
                CommandProcess.ErrorDataReceived -= OnErrorDataReceived;

                CommandProcess.CancelOutputRead();
                CommandProcess.CancelErrorRead();

                CommandProcess.StandardInput.WriteLine("exit");
                CommandProcess.StandardInput.Flush();
                CommandProcess.StandardInput.Close();
                CommandProcess.WaitForExit();
                CommandProcess.Close();
            }
        }
        public void RunCommand(string inputCommand)
        {
            CommandProcess.StandardInput.WriteLine(inputCommand);
            CommandProcess.StandardInput.Flush();

            RecentInputs.Add(inputCommand);

            OutputViewModel.Instance.SelectedTab.Content.AppendText("\n");


            outputDataReceivedEvent.WaitOne();
            outputDataReceivedEvent.Reset();

            errorDataReceivedEvent.WaitOne(100);
            errorDataReceivedEvent.Reset();

            if (_validInput)
            {
                if (inputCommand.Trim() != "")
                {
                    RecentViewModel.Instance.AddRecentCommand(inputCommand.Trim());

                    _validInput = false;
                }
            }
        }
        public string GetPreviousCommand()
        {
            if (RecentInputs.Count == 0) return null;

            if (_recentCommandIndex > 0)
            {
                _recentCommandIndex--;
            }
            else
            {
                _recentCommandIndex = RecentInputs.Count - 1;
            }

            return RecentInputs[_recentCommandIndex];
        }
        public string GetNextCommand()
        {
            if (RecentInputs.Count == 0) return null;

            if (_recentCommandIndex < RecentInputs.Count - 1)
            {
                _recentCommandIndex++;
            }
            else
            {
                _recentCommandIndex = 0;
            }

            return RecentInputs[_recentCommandIndex];
        }
        #endregion

        #region Private EventListener Methods
        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data) && initialOutput > 3)
            {
                _validInput = true;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (RecentOutputs.Count == 2) OutputViewModel.Instance.SelectedTab.Content.AppendText("\n");

                    RecentOutputs.Add(e.Data);
                    CommandTab.Content.AppendText(e.Data + "\n");
                    CommandTab.Content.ScrollToEnd();
                });
            }
            else initialOutput++;

            outputDataReceivedEvent.Set();
        }
        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                _validInput = false;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    CommandTab.Content.AppendText(e.Data + "\n");
                    CommandTab.Content.ScrollToEnd();
                });
            }

            errorDataReceivedEvent.Set();
        }
        #endregion
    }
}
