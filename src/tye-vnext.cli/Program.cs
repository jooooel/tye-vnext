using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using tye_vnext.cli;

var registrations = new ServiceCollection();
// registrations.AddSingleton<IGreeter, HelloWorldGreeter>();

// Create a type registrar and register any dependencies.
// A type registrar is an adapter for a DI framework.
var registrar = new TypeRegistrar(registrations);

var app = new CommandApp<RunCommand>(registrar);
return app.Run(args);

internal sealed class RunCommand : Command<RunCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("Path to tye.yaml. Defaults to current directory.")]
        [CommandArgument(0, "[path]")]
        public string? Path { get; init; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        var fileInfo = TryParsePath(settings.Path);

        if (fileInfo is null || !fileInfo.Exists)
        {
            AnsiConsole.MarkupLine($"[red]File not found {settings.Path}[/]");
            return -1;
        }

        AnsiConsole.MarkupLine($"Running Tye from [green]{fileInfo.FullName}[/]");
        
        var app = new Application(application.Name, application.Source, application.DashboardPort, services, application.ContainerEngine) { Network = application.Network };
        return 0;
    }

    private static FileInfo? TryParsePath(string? path)
    {
        if (string.IsNullOrEmpty(path))
        {
            path = ".";
        }

        if (File.Exists(path))
        {
            return new FileInfo(path);
        }

        if (Directory.Exists(path))
        {
            if (ConfigFileFinder.TryFindSupportedFile(path, out var filePath, out var errorMessage))
            {
                return new FileInfo(filePath!);
            }

            return default;
        }

        return default;
    }
}