using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using MiCommand.ViewModels;

namespace MiCommand.Models
{
    public class Command
    {
        private static int initialOutput = 0;

        private bool _validInputCommand;
        private int _recentCommandIndex;

        public Process CommandProcess { get; private set; }
        public List<string> RecentInputs { get; private set; }
        public List<string> RecentOutputs { get; private set; }

        public Command()
        {
            _validInputCommand = false;
            _recentCommandIndex = 0;

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

            Thread.Sleep(100);
            if (_validInputCommand)
            {
                if (inputCommand.Trim() != "")
                {
                    RecentViewModel.Instance.AddRecentCommand(inputCommand.Trim());

                    _validInputCommand = false;
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
                _validInputCommand = true;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (RecentOutputs.Count == 2) OutputViewModel.Instance.SelectedTab.Content.AppendText("\n");

                    RecentOutputs.Add(e.Data);
                    OutputViewModel.Instance.SelectedTab.Content.AppendText(e.Data + "\n");
                    OutputViewModel.Instance.SelectedTab.Content.ScrollToEnd();
                });
            }
            else initialOutput++;
        }
        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                _validInputCommand = false;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    OutputViewModel.Instance.SelectedTab.Content.AppendText(e.Data + "\n");
                    OutputViewModel.Instance.SelectedTab.Content.ScrollToEnd();
                });
            }
        }
        #endregion
    }
}
