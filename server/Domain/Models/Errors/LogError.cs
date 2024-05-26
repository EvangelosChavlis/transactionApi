namespace server.Domain.Models.Errors;

public class LogError
{
    public Guid Id { get; set; }
    public string Error { get; set; }
    public int StatusCode { get; set; }
    public string Instance { get; set; }
    public string ExceptionType { get; set; }
    public string StackTrace { get; set; }
    public DateTime Timestamp { get; set; }
}