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
        throw new NotImplementedException();
    }
    
    public async Task<EventEntity> GetEventById(string eventId)
    {
        try
        {
            using var connection = new SeqConnection(Host.BaseUri, ApiKey);
            
            var eventEntity = await connection.Events.FindAsync(eventId);
            
            return eventEntity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to retrieve event with ID {eventId}", ex);
        }
    }
}