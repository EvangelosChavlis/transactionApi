namespace server.Domain.Dto.Common;

public class ItemResponse<T>
{
    public T? Data { get; set; }

    public ItemResponse<T> WithData(T data)
    {
        this.Data = data;
        return this;
    }
}