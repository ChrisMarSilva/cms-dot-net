namespace Project.Filters.Idempotency;

public class IdempotencyOptions
{
    public const string IdempotencyResponseBodyKey = "idempotency-response-body";

    public string HeaderName { get; set; }

    public int TTLInHours { get; set; }

    public bool EnableWhiteList { get; set; }
}