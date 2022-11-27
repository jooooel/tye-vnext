using System.Diagnostics.CodeAnalysis;

namespace tye_vnext.cli
{
    public class ConfigFileFinder
    {
        private static readonly string[] FileFormats = { "*.csproj" };

        public static bool TryFindSupportedFile(string directoryPath, [NotNullWhen(true)] out string? filePath, [MaybeNullWhen(true)] out string? errorMessage, string[]? fileFormats = null)
        {
            fileFormats ??= FileFormats;
            foreach (var format in fileFormats)
            {
                var files = Directory.GetFiles(directoryPath, format);

                switch (files.Length)
                {
                    case 1:
                        errorMessage = null;
                        filePath = files[0];
                        return true;
                    case 0:
                        continue;
                }

                errorMessage = $"More than one matching file was found in directory '{directoryPath}'.";
                filePath = default;
                return false;
            }

            errorMessage = $"No project project file or solution was found in directory '{directoryPath}'.";
            filePath = default;
            return false;
        }
    }
}
