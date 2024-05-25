namespace server.Domain.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public string ApplicationName { get; set; }
    public string Email { get; set; }
    public string Filename { get; set; }
    public string Url { get; set; }
    public DateTime Inception { get; set; }
    public string Amount { get; set; }
    public double Allocation { get; set; }
}