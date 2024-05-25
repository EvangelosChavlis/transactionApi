namespace server.Domain.Dto.Common;

public class ListResponse<T>
{
    public T? Data { get; set; }
    public PaginatedList? Pagination { get; set; }

    public ListResponse<T> WithData(T data)
    {
        this.Data = data;
        return this;
    }
}