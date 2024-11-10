using Seq.Apps;
using Seq.Apps.LogEvents;

namespace Seq.App.Webex.Room;


[SeqApp("Webex Room", Description = "Send events and notifications to a Webex configured Room")]
public class WebexApp : SeqApp, ISubscribeToAsync<LogEventData>
{
    [SeqAppSetting]
    public string RoomId { get; set; }
    
    [SeqAppSetting]
    public string AuthorizationBearerToken { get; set; }
    
    [SeqAppSetting]
    public bool UseProxy { get; set; }
    
    [SeqAppSetting]
    public string ProxyAddress { get; set; }
    
    [SeqAppSetting]
    public int ProxyPort { get; set; }
    
    [SeqAppSetting]
    public bool AuthenticatedProxy { get; set; }
    
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
}