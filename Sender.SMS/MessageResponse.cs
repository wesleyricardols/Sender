
public class MessageResponse
{
    public Message[] messages { get; set; }
    public string errorCode { get; set; }
    public string error { get; set; }
    public string errorDescription { get; set; }
}

public class Message
{
    public string apiMessageId { get; set; }
    public bool accepted { get; set; }
    public string to { get; set; }
    public string errorCode { get; set; }
    public string error { get; set; }
    public string errorDescription { get; set; }
}
