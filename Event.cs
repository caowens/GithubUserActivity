public class Event
{
    public required string type { get; set; }
    public required Repository repo {get; set; }
    public required Payload payload { get; set; }
}

public class Repository
{
    public required string name { get; set; }
    public required string url { get; set; }
}

public class Payload
{
    public List<Object>? commits { get; set; }
    public string? ref_type { get; set; }
    public Comment? comment { get; set; }
    public string? action { get; set; }
    public PullRequest? pull_request { get; set; }
    public Fork? forkee { get; set; }
}

public class Comment 
{
    public string? body { get; set; }
}

public class PullRequest
{
    public string? title { get; set; }
}

public class Fork
{
    public string? full_name { get; set; }
}