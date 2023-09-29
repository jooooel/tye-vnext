using System.CommandLine.IO;

namespace tye_vnext.cli.Core;

public class NullStandardStreamWriter : IStandardStreamWriter
{
    public void Write(string? value)
    {
    }
}