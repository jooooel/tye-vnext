namespace tye_vnext.cli;

public class ServiceDescription
{
    public string Name { get; }

    public RunInfo? RunInfo { get; set; }
    
    public int Replicas { get; set; } = 1;

    public ServiceDescription(string name, RunInfo? runInfo)
    {
        Name = name;
        RunInfo = runInfo;
    }
}