using Serilog;

namespace Seq.App.Webex.Room.TestConsole;

class Program
{
    static void Main(string[] args)
    {
        using var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .AuditTo.SeqApp<WebexApp>(new Dictionary<string, string>
            {
                [nameof(WebexApp.UsedByAlerts)] = "true",
                [nameof(WebexApp.RoomId)] = "xxx",
                [nameof(WebexApp.AuthorizationBearerToken)] = "yyy",
                [nameof(WebexApp.ApiKey)] = "zzz",
                [nameof(WebexApp.UseProxy)] = "false",
                [nameof(WebexApp.ProxyAddress)] = "aaa",
                [nameof(WebexApp.ProxyPort)] = "bbb",
                [nameof(WebexApp.AuthenticatedProxy)] = "false",
                [nameof(WebexApp.ProxyUsername)] = "ccc",
                [nameof(WebexApp.ProxyPassword)] = "ddd",
            })
            .CreateLogger();
        
        logger.Information("Hello, {Name}!", Environment.UserName);
    }
}