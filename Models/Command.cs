using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MiCommand.ViewModels;

namespace MiCommand.Models
{
    public class Command
    {
        private int _recentCommandIndex;

        public Process Process { get; set; }
        public List<string> RecentCommands { get; set; }

        public Command()
        {
            RecentCommands = new List<string>();

            StartProcess();
        }

        #region Public Methods
        public string GetStartOutput()
        {
            Process.Start();
            Process.StandardInput.Flush();
            Process.StandardInput.Close();
            Process.WaitForExit();

            return Process.StandardOutput.ReadToEnd();
        }
        public string EnterCommand(string input)
        {
            Process.Start();
            Process.StandardInput.WriteLine($"{input}");
            Process.StandardInput.Flush();
            Process.StandardInput.Close();
            Process.WaitForExit();

            RecentCommands.Add(input);
            _recentCommandIndex = RecentCommands.Count;

            string output = "";
            string standardError = "";
            if ((standardError = Process.StandardError.ReadToEnd()) != "")
            {
                output = "ERROR: " + standardError + "\n";
                output += GetCurrentDirectory();
            }
            else
            {
                int lineNumber = 0;
                string standardOutput = "";
                while ((standardOutput = Process.StandardOutput.ReadLine()) != null)
                {
                    if (lineNumber < 4) lineNumber++;
                    else
                    {
                        output += standardOutput + "\n";
                    }
                }

                if (input.Trim() != "") RecentViewModel.Instance.AddRecentCommand(input);
            }
            return output.TrimEnd('\n');
        }
        public string GetPreviousCommand()
        {
            if (_recentCommandIndex > 0)
            {
                _recentCommandIndex--;
            }
            else
            {
                _recentCommandIndex = RecentCommands.Count - 1;
            }

            return RecentCommands[_recentCommandIndex];
        }
        public string GetNextCommand()
        {
            if (_recentCommandIndex < RecentCommands.Count - 1)
            {
                _recentCommandIndex++;
            }
            else
            {
                _recentCommandIndex = 0;
            }

            return RecentCommands[_recentCommandIndex];
        }
        #endregion

        #region Private Methods
        private void StartProcess()
        {
            Process = new Process();

            Process.StartInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false,
            };
        }
        private string GetCurrentDirectory()
        {
            Process.Start();
            Process.StandardInput.WriteLine($"echo %cd%");
            Process.StandardInput.Flush();
            Process.StandardInput.Close();
            Process.WaitForExit();

            string readOutput = Process.StandardOutput.ReadToEnd();
            string[] outputLines = readOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            return outputLines.Last();
        }
        #endregion
    }
}
