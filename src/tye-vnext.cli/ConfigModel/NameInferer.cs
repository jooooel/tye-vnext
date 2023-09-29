namespace tye_vnext.cli.ConfigModel
{
    internal static class NameInferer
    {
        public static string? InferApplicationName(FileInfo? fileInfo)
        {
            if (fileInfo == null)
            {
                return null;
            }

            var extension = fileInfo.Extension;
            if (extension is ".sln" or ".csproj" or ".fsproj")
            {
                return Path.GetFileNameWithoutExtension(fileInfo.Name).ToLowerInvariant();
            }

            return fileInfo.Directory?.Name.ToLowerInvariant();
        }
    }
}
