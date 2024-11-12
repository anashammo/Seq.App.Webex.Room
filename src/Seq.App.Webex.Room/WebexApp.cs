using System.Dynamic;
using System.Text.Json;
using Seq.Api;
using Seq.Api.Model.Events;
using Seq.Apps;
using Seq.Apps.LogEvents;

namespace Seq.App.Webex.Room;


[SeqApp("Webex Room", Description = "Send events and notifications to a Webex configured Room")]
public class WebexApp : SeqApp, ISubscribeToAsync<LogEventData>
{
    [SeqAppSetting]
    public string UsedByAlerts { get; set; }
    
    [SeqAppSetting]
    public string RoomId { get; set; }
    
    [SeqAppSetting]
    public string AuthorizationBearerToken { get; set; }
    
    [SeqAppSetting]
    public string ApiKey { get; set; }
    
    [SeqAppSetting]
    public string UseProxy { get; set; }
    
    [SeqAppSetting]
    public string ProxyAddress { get; set; }
    
    [SeqAppSetting]
    public string ProxyPort { get; set; }
    
    [SeqAppSetting]
    public string AuthenticatedProxy { get; set; }
    
    [SeqAppSetting]
    public string ProxyUsername { get; set; }
    
    [SeqAppSetting]
    public string ProxyPassword { get; set; }
    
    public WebexApp()
    {
        
    }
    
    protected override void OnAttached()
    {
        
    }
    
    public async Task OnAsync(Event<LogEventData> evt)
    {
        var eventAsJson = System.Text.Json.JsonSerializer.Serialize(evt, new JsonSerializerOptions()
        {
            
        });
        
        // var properties = (IDictionary<string,object?>) ToDynamic(evt.Data.Properties ?? new Dictionary<string, object?>());
        //
        // var payload = (IDictionary<string,object?>) ToDynamic(new Dictionary<string, object?>
        // {
        //     { "Id",                  evt.Id },
        //     { "Timestamp",           evt.Timestamp },
        //     { "LocalTimestamp",      evt.Data.LocalTimestamp },
        //     { "Level",               evt.Data.Level },
        //     { "MessageTemplate",     evt.Data.MessageTemplate },
        //     { "Message",             evt.Data.RenderedMessage },
        //     { "Exception",           evt.Data.Exception },
        //     { "Properties",          properties },
        //     { "EventType",           "$" + evt.EventType.ToString("X8") },
        //     { "Instance",            Host.InstanceName },
        //     { "ServerUri",           Host.BaseUri },
        //     // Note, this will only be valid when events are streamed directly to the app, and not when the app is sending an alert notification.
        //     { "EventUri",            string.Concat(Host.BaseUri, "#/events?filter=@Id%20%3D%20'", evt.Id, "'&amp;show=expanded") }
        // });
        //
        // foreach (var property in properties)
        // {
        //     if (property.Key.Equals("Alert", StringComparison.OrdinalIgnoreCase))
        //     {
        //         var alertData = (List<object?>) ToDynamic(property.Value ?? new List<object?>());
        //
        //         payload[property.Key] = alertData;
        //     }
        //     else if (property.Key.Equals("Source", StringComparison.OrdinalIgnoreCase))
        //     {
        //         var sourceData = (List<object?>) ToDynamic(property.Value ?? new List<object?>());
        //         
        //         payload[property.Key] = sourceData;
        //         
        //     }
        //     else if (property.Key.Equals("Failures", StringComparison.OrdinalIgnoreCase))
        //     {
        //         var failuresData = (List<object?>) ToDynamic(property.Value ?? new List<object?>());
        //         
        //         payload[property.Key] = failuresData;
        //     }
        //     else
        //     {
        //         payload[property.Key] = property.Value;
        //     }
        // }
        
        var logger = Log
            .ForContext<WebexApp>()
            .ForContext("Method", "OnAsync");
            
        logger.Information("Event detected: {Event}", eventAsJson);
        
        var seqEvent = await GetEventById(evt.Id);

        if (seqEvent == null)
        {
            return;
        }
    }
    
    private async Task<EventEntity?> GetEventById(string eventId)
    {
        try
        {
            using var connection = new SeqConnection(Host.BaseUri, ApiKey);
            
            return await connection.Events.FindAsync(eventId);
        }
        catch (Exception ex)
        {
            var logger = Log
                .ForContext<WebexApp>()
                .ForContext("Method", "GetEventById");
            
            logger.Error(ex, "Failed to retrieve event with ID {eventId}", eventId);
        }

        return default;
    }
    
    private object ToDynamic(object o)
    {
        if (o is IEnumerable<KeyValuePair<string, object>> dictionary)
        {
            var result = new ExpandoObject();
            var asDict = (IDictionary<string, object?>) result;
            foreach (var kvp in dictionary)
                asDict.Add(kvp.Key, ToDynamic(kvp.Value));
            return result;
        }

        if (o is IEnumerable<object> enumerable)
        {
            return enumerable.Select(ToDynamic).ToArray();
        }

        return o;
    }
}