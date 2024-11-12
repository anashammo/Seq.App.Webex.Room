namespace Seq.App.Webex.Room.Models.Alerts;

public record AlertRoot
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

    public Properties Properties { get; set; }
}

public class Properties
{
    public string NamespacedAlertTitle { get; set; }

    public Alert Alert { get; set; }

    public Source Source { get; set; }

    public string SuppressedUntil { get; set; }

    public List<string> Failures { get; set; }
}

public class Alert
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Url { get; set; }

    public object OwnerUsername { get; set; }

    public string SignalExpressionDescription { get; set; }

    public string Query { get; set; }

    public string HavingClause { get; set; }

    public string TimeGrouping { get; set; }
}

public class Source
{
    public string RangeStart { get; set; }

    public string RangeEnd { get; set; }

    public string ResultsUrl { get; set; }

    public List<Result> Results { get; set; }

    public List<ContributingEvent> ContributingEvents { get; set; }
}

public class Result
{
    public string Time { get; set; }

    public int Count { get; set; }
}

public class ContributingEvent
{
    public string Id { get; set; }

    public string Timestamp { get; set; }

    public string Message { get; set; }
}