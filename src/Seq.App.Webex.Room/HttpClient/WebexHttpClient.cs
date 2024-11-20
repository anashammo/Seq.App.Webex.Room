using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Seq.App.Webex.Room.Models;

namespace Seq.App.Webex.Room.HttpClient;

public class WebexHttpClient : IWebexHttpClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public WebexHttpClient(HttpClientConfigurations configurations)
    {
        if (configurations.UseProxy)
        {
            var proxy = default(WebProxy);
            
            if (configurations.AuthenticatedProxy)
            {
                proxy = new WebProxy($"http://{configurations.ProxyAddress}:{configurations.ProxyPort}")
                {
                    Credentials = new NetworkCredential(configurations.ProxyUsername, configurations.ProxyPassword),
                };
            }
            else
            {
                proxy = new WebProxy($"http://{configurations.ProxyAddress}:{configurations.ProxyPort}");
            }
            
            var httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy
            };
        
            _httpClient = new System.Net.Http.HttpClient(handler: httpClientHandler);
        }
        else
        {
            _httpClient = new System.Net.Http.HttpClient();
        }
        
        _httpClient.DefaultRequestHeaders.Authorization 
            = new AuthenticationHeaderValue("Bearer", configurations.AuthorizationBearerToken);
        
        //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + configurations.AuthorizationBearerToken);
    }

    public async Task SendWebexMessage(WebexMessage message)
    {
        var body = System.Text.Json.JsonSerializer.Serialize(message);

        using (var content = new StringContent(body, Encoding.UTF8, "application/json"))
        {
            var response = await _httpClient.PostAsync("https://webexapis.com/v1/messages", content);

            response.EnsureSuccessStatusCode();
        }
    }
}