using System.Text.Json;
using Seq.Api;
using Seq.Api.Model.Events;
using Seq.Apps;
using Seq.Apps.LogEvents;

namespace Seq.App.Webex.Room;

[SeqApp("Webex Room", Description = "Send events and notifications to a Webex configured Room")]
public class WebexApp : SeqApp, ISubscribeToAsync<LogEventData>
{
    [SeqAppSetting(
        DisplayName = "Webex Room Id",
        IsOptional = false,
        InputType = SettingInputType.Text,
        HelpText = "The Id used to send the events to a Webex room")]
    public string RoomId { get; set; }

    [SeqAppSetting(
        DisplayName = "Authorization Bearer Token",
        IsOptional = false,
        InputType = SettingInputType.Password,
        HelpText = "The Authorization Bearer token of the Bot used to send the events to a Webex room")]
    public string AuthorizationBearerToken { get; set; }

    [SeqAppSetting(
        DisplayName = "Seq API Key",
        IsOptional = false,
        InputType = SettingInputType.Text,
        HelpText = "The API Key used to get event details within an alert using Seq API")]
    public string ApiKey { get; set; }
    
    [SeqAppSetting(
        DisplayName = "Use HTTP Proxy",
        IsOptional = false,
        InputType = SettingInputType.Checkbox,
        HelpText = "If checked, HTTP proxy will be used to connect to the Webex API")]
    public bool UseProxy { get; set; }

    [SeqAppSetting(
        DisplayName = "HTTP Proxy Address",
        IsOptional = true,
        InputType = SettingInputType.Text,
        HelpText = "The HTTP Proxy Address used to connect to the Webex API")] 
    public string ProxyAddress { get; set; }

    [SeqAppSetting(
        DisplayName = "HTTP Proxy Port",
        IsOptional = true,
        InputType = SettingInputType.Integer,
        HelpText = "The HTTP Proxy Port used to connect to the Webex API")] 
    public int ProxyPort { get; set; }

    [SeqAppSetting(
        DisplayName = "Secure HTTP Proxy",
        IsOptional = false,
        InputType = SettingInputType.Checkbox,
        HelpText = "If checked, The HTTP proxy will be used to connect to the Webex API will use username and password")]
    public string AuthenticatedProxy { get; set; }

    [SeqAppSetting(
        DisplayName = "HTTP Proxy Username",
        IsOptional = true,
        InputType = SettingInputType.Text,
        HelpText = "The HTTP Proxy Username used to connect to the Webex API")] 
    public string ProxyUsername { get; set; }

    [SeqAppSetting(
        DisplayName = "HTTP Proxy Password",
        IsOptional = true,
        InputType = SettingInputType.Password,
        HelpText = "The HTTP Proxy Password used to connect to the Webex API")] 
    public string ProxyPassword { get; set; }
    
    [SeqAppSetting(
        DisplayName = "Suppression time (minutes)",
        IsOptional = true,
        HelpText = "Once an event type has been sent to Webex, the time to wait before sending again, The default is zero")]
    public int SuppressionMinutes { get; set; }
    
    private EventTypeSuppressions _suppressions;

    public WebexApp()
    {
    }

    protected override void OnAttached()
    {
    }

    public async Task OnAsync(Event<LogEventData> evt)
    {
        var logger = Log
            .ForContext<WebexApp>()
            .ForContext("Method", "OnAsync");

        try
        {
            _suppressions = _suppressions ?? new EventTypeSuppressions(SuppressionMinutes);
            
            if (_suppressions.ShouldSuppressAt(evt.EventType, DateTime.UtcNow))
            {
                return;
            }
            
            var eventAsJson = JsonSerializer.Serialize(evt, new JsonSerializerOptions());

            var eventAsJsonDocument = JsonDocument.Parse(eventAsJson);

            var root = eventAsJsonDocument.RootElement;

            logger.Information("Event detected: {Event}", eventAsJsonDocument);

            if (root.TryGetProperty("Data", out var data) && data.TryGetProperty("Properties", out var properties) &&
                properties.TryGetProperty("Alert", out var alert))
            {
                var alertData = new Models.Alerts.AlertRoot();
            }
            else
            {
                var eventData = new Models.Events.EventRoot();
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Failed to send event with Id: {EventId}", evt.Id);
        }
    }

    private async Task<EventEntity?> GetEventById(string eventId)
    {
        var logger = Log
            .ForContext<WebexApp>()
            .ForContext("Method", "GetEventById");

        try
        {
            using var connection = new SeqConnection(Host.BaseUri, ApiKey);

            return await connection.Events.FindAsync(eventId);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Failed to retrieve event with Id: {EventId}", eventId);
        }

        return default;
    }
}