using System.Diagnostics;

namespace Project.Filters.Idempotency;

public static class IdempotencyTelemetry
{
    public const string SourceName = "Project.Idempotency";

    public static readonly ActivitySource ActivitySource = new(SourceName);
}