namespace server.Domain.Models.Common;


public class UrlQuery
{
    public int? PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    public string? Filter { get; set; }
    public string? SortBy { get; set; }

    public bool SortDescending { get; set; }

    public bool HasFilter => !string.IsNullOrEmpty(Filter);
    public bool HasSortBy => !string.IsNullOrEmpty(SortBy);

    public int TotalRecords { get; set; }
}