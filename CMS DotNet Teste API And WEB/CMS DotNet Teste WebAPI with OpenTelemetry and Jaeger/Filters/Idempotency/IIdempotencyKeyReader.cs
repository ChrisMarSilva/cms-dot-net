namespace Project.Filters.Idempotency;

public interface IIdempotencyKeyReader<in TRequest>
{
    string Read(TRequest request);
}