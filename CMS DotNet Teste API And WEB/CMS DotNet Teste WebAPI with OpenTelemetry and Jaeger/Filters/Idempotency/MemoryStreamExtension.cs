using Microsoft.IO;

namespace Project.Filters.Idempotency;

internal static class MemoryStreamExtension
{
    internal static readonly RecyclableMemoryStreamManager RecyclableMemoryStreamManager = new();
}