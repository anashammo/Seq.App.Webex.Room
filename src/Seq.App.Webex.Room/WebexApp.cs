using System.Text.Json;
using Seq.Api;
using Seq.Api.Model.Events;
using Seq.App.Webex.Room.Models;
using Seq.Apps;
using Seq.Apps.LogEvents;

namespace Seq.App.Webex.Room;

[SeqApp("Webex Room", Description = "Send events and notifications to a Webex configured Room")]
public class WebexApp : SeqApp, ISubscribeToAsync<LogEventData>
{
    [SeqAppSetting] public string RoomId { get; set; }

    [SeqAppSetting] public string AuthorizationBearerToken { get; set; }

    [SeqAppSetting] public string ApiKey { get; set; }

    [SeqAppSetting] public string UseProxy { get; set; }

    [SeqAppSetting] public string ProxyAddress { get; set; }

    [SeqAppSetting] public string ProxyPort { get; set; }

    [SeqAppSetting] public string AuthenticatedProxy { get; set; }

    [SeqAppSetting] public string ProxyUsername { get; set; }

    [SeqAppSetting] public string ProxyPassword { get; set; }

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
            var eventAsJson = JsonSerializer.Serialize(evt, new JsonSerializerOptions());

            var eventAsJsonDocument = JsonDocument.Parse(eventAsJson);

            var root = eventAsJsonDocument.RootElement;

            logger.Information("Event detected: {Event}", eventAsJsonDocument);

            if (root.TryGetProperty("Data", out var dataElement) && dataElement.TryGetProperty("Properties", out var propertiesElement) &&
                propertiesElement.TryGetProperty("Alert", out var alertElement))
            {
                var alertData = CreateAlert(dataElement);
            }
            else
            {
                var eventData = CreateEvent(dataElement);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Failed to send event with Id: {EventId}", evt.Id);
        }
    }

    private Models.Alerts.AlertRoot CreateAlert(JsonElement dataElement)
    {
        var alertData = new Models.Alerts.AlertRoot();

        return alertData;
    }
    
    private Models.Events.EventRoot CreateEvent(JsonElement dataElement)
    {
        var eventData = new Models.Events.EventRoot();

        return eventData;
    }

    private WebexMessage CreateWebexMessage(Models.Alerts.AlertRoot alertData)
    {
        var message = new WebexMessage();

        return message;
    }
    
    private WebexMessage CreateWebexMessage(Models.Events.EventRoot eventData)
    {
        var message = new WebexMessage();

        return message;
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