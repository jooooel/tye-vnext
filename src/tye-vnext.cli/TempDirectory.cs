using System.Runtime.InteropServices;

namespace tye_vnext.cli;

public class TempDirectory : IDisposable
{
    public static TempDirectory Create(bool preferUserDirectoryOnMacOs = false)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && preferUserDirectoryOnMacOs)
        {
            var baseDirectory = Environment.GetEnvironmentVariable("HOME") ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            baseDirectory = Path.Combine(baseDirectory, ".tye");
            Directory.CreateDirectory(baseDirectory);

            var directoryPath = Path.Combine(baseDirectory, Path.GetRandomFileName());
            var directoryInfo = Directory.CreateDirectory(directoryPath);
            return new TempDirectory(directoryInfo);
        }
        else
        {
            var directoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var directoryInfo = Directory.CreateDirectory(directoryPath);
            return new TempDirectory(directoryInfo);
        }
    }

    public TempDirectory(DirectoryInfo directoryInfo)
    {
        DirectoryInfo = directoryInfo;

        DirectoryPath = directoryInfo.FullName;
    }

    public string DirectoryPath { get; }
    public DirectoryInfo DirectoryInfo { get; }

    public void Dispose()
    {
        DirectoryExtensions.DeleteDirectory(DirectoryPath);
    }
}