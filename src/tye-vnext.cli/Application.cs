namespace tye_vnext.cli;

public class Application
{
    public Application(
        string name,
        FileInfo source,
        Dictionary<string, Service> services
    )
    {
        Name = name;
        Source = source.FullName;
        ContextDirectory = source.DirectoryName!;
        Services = services;
    }

    public string Id { get; } = Guid.NewGuid().ToString();

    public string Name { get; }

    public string Source { get; }

    public string ContextDirectory { get; }

    public Dictionary<string, Service> Services { get; }
}