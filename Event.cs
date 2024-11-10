// public record class Event(string type);

public class Event
{
    public string type { get; set; }
    public Repository repo {get; set; }
    public Payload payload { get; set;}
}

public class Repository
{
    public string name { get; set;}
    public string url { get; set;}
}

public class Payload
{
    public List<Object> commits { get; set;}
}