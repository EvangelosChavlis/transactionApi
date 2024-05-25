namespace server.Domain.Models.Common;

public class Envelope<T>
{
    public IList<T> Rows { get; set; }    
    public UrlQuery UrlQuery { get; set; }
}