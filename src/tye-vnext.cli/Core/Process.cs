// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace tye_vnext.cli.Core
{
    internal static class Process
    {
        public static async Task<int> ExecuteAsync(
            string command,
            string args,
            string? workingDir = null,
            Action<string>? stdOut = null,
            Action<string>? stdErr = null,
            params (string key, string value)[] environmentVariables)
        {
            return await StartProcess(command, args, workingDir, stdOut, stdErr, environmentVariables).CompleteAsync();
        }

        private static async Task<int> CompleteAsync(
            this System.Diagnostics.Process process,
            CancellationToken? cancellationToken = null) =>
            await Task.Run(() =>
            {
                process.WaitForExit();

                return Task.FromResult(process.ExitCode);
            }, cancellationToken ?? CancellationToken.None);

        private static System.Diagnostics.Process StartProcess(
            string command,
            string args,
            string? workingDir = null,
            Action<string>? stdOut = null,
            Action<string>? stdErr = null,
            params (string key, string value)[] environmentVariables)
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo =
                {
                    Arguments = args,
                    FileName = command,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false
                }
            };

            if (!string.IsNullOrWhiteSpace(workingDir))
            {
                process.StartInfo.WorkingDirectory = workingDir;
            }

            if (environmentVariables.Length > 0)
            {
                foreach (var (key, value) in environmentVariables)
                {
                    process.StartInfo.Environment.Add(key, value);
                }
            }

            if (stdOut != null)
            {
                process.OutputDataReceived += (_, eventArgs) =>
                {
                    if (eventArgs.Data != null)
                    {
                        stdOut(eventArgs.Data);
                    }
                };
            }

            if (stdErr != null)
            {
                process.ErrorDataReceived += (_, eventArgs) =>
                {
                    if (eventArgs.Data != null)
                    {
                        stdErr(eventArgs.Data);
                    }
                };
            }

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return process;
        }
    }
}