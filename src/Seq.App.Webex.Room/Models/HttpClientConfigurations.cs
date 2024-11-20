namespace Seq.App.Webex.Room.Models;

public class HttpClientConfigurations
{
    public string AuthorizationBearerToken { get; set; }
    
    public bool UseProxy { get; set; }
    
    public string ProxyAddress { get; set; }
    
    public int ProxyPort { get; set; }
    
    public bool AuthenticatedProxy { get; set; }
    
    public string ProxyUsername { get; set; }
    
    public string ProxyPassword { get; set; }
}