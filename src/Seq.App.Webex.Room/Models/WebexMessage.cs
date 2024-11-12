namespace Seq.App.Webex.Room.Models;

public record WebexMessage
{
    public string roomId { get; set; }
    
    public string text { get; set; }
}

