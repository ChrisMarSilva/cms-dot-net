namespace Project.Filters.Idempotency;

public interface IIdempotencyRegister
{
    int? StatusCode { get; }
    string ContentType { get; }
    string Key { get; }
    bool IsCompleted { get; }
    IReadOnlyList<byte> Value { get; }
    IReadOnlyList<byte> HashOfRequest { get; }
}