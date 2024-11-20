using Seq.App.Webex.Room.Models;

namespace Seq.App.Webex.Room.HttpClient;

public interface IWebexHttpClient
{
    Task SendWebexMessage(WebexMessage message);
}