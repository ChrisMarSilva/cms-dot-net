namespace Project.Filters.Idempotency;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IgnoreIdempotencyAttribute : Attribute
{

}