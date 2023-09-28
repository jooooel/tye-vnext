using System.Collections.Concurrent;
using System.ComponentModel;

namespace tye_vnext.cli;

public class Service
{
    public Service(ServiceDescription description)
    {
        Description = description;
    }

    public ServiceDescription Description { get; }

    public int Restarts { get; set; }

    public ServiceType ServiceType
    {
        get
        {
            if (Description.RunInfo is ProjectRunInfo)
            {
                return ServiceType.Project;
            }

            throw new InvalidEnumArgumentException($"Unknown run info: {Description.RunInfo?.GetType().FullName}");

        }
    }

    public ServiceStatus Status { get; set; } = new ServiceStatus();

    public ConcurrentDictionary<string, ReplicaStatus> Replicas { get; set; } = new ConcurrentDictionary<string, ReplicaStatus>();

    public Dictionary<object, object> Items { get; } = new Dictionary<object, object>();

    public ConcurrentQueue<string> CachedLogs { get; } = new ConcurrentQueue<string>();

    // public Subject<string> Logs { get; } = new Subject<string>();
    //
    // public Subject<ReplicaEvent> ReplicaEvents { get; } = new Subject<ReplicaEvent>();
}