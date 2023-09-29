using System.CommandLine;
using System.CommandLine.IO;

namespace tye_vnext.cli.Core;

public class NullConsole : IConsole
{
    public IStandardStreamWriter Out { get; } = new NullStandardStreamWriter();
    public bool IsOutputRedirected { get; } = false;
    public IStandardStreamWriter Error { get; } = new NullStandardStreamWriter();
    public bool IsErrorRedirected { get; } = false;
    public bool IsInputRedirected { get; } = false;
}