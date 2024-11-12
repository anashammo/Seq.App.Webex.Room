namespace Seq.App.Webex.Room.Models.Events;

public class EventRoot
{
    public long EventType { get; set; }

    public string Timestamp { get; set; }

    public Data Data { get; set; }

    public string Id { get; set; }

    public string TimestampUtc { get; set; }
}

public class Data
{
    public string Id { get; set; }

    public string LocalTimestamp { get; set; }

    public int Level { get; set; }

    public string MessageTemplate { get; set; }

    public string RenderedMessage { get; set; }

    public object Exception { get; set; }

    public Dictionary<string, object?> Properties { get; set; }
}