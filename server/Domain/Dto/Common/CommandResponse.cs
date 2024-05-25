namespace server.Domain.Dto.Common;

public class CommandResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }

    public CommandResponse<T> WithData(T data)
    {
        Data = data;
        return this;
    }

    public CommandResponse<T> WithSuccess(bool success)
    {
        Success = success;
        return this;
    }
}