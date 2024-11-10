// public record class Event(string type);

public class Event
{
    public required string type { get; set; }
    public required Repository repo {get; set; }
    public required Payload payload { get; set;}
}

public class Repository
{
    public required string name { get; set;}
    public required string url { get; set;}
}

public class Payload
{
    public List<Object>? commits { get; set;}
}