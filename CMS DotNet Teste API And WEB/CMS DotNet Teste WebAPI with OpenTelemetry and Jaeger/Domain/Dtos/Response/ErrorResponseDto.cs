using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Project.Domain.Dtos.Response;

public class ErrorResponseDto
{
    [JsonPropertyName("Codigo")]
    public string ErrorCode { get; init; }

    [JsonPropertyName("Descricao")]
    public string? Description { get; init; }

    [JsonPropertyName("IdCorrelacao")]
    public string? CorrelationId { get; init; }

    [JsonPropertyName("Erros")]
    [JsonInclude]
    public ICollection<Error>? Errors { get; private set; }

    [JsonConstructor]
    public ErrorResponseDto()
    {

    }

    public ErrorResponseDto(string errorCode, string? description = null, string? correlationId = null)
    {
        ErrorCode = errorCode;
        Description = description;
        CorrelationId = correlationId;
    }

    public ErrorResponseDto(HttpStatusCode errorCode, string? description = null, string? correlationId = null) : this(((int)errorCode).ToString(), description, correlationId)
    {
    }

    public ErrorResponseDto(int errorCode, string? description = null, string? correlationId = null) : this(errorCode.ToString(), description, correlationId)
    {
    }

    public static ErrorResponseDto Begin(string errorCode, string? description = null, string? correlationId = null)
    {
        return new ErrorResponseDto(errorCode, description, correlationId);
    }

    public static ErrorResponseDto Begin(int errorCode, string? description = null, string? correlationId = null)
    {
        return new ErrorResponseDto(errorCode, description, correlationId);
    }

    public static ErrorResponseDto Begin(HttpStatusCode errorCode, string? description = null, string? correlationId = null)
    {
        return new ErrorResponseDto(errorCode, description, correlationId);
    }

    public Error AddError(string field)
    {
        //if (Errors == null)
        //  var collection2 = (Errors = new List<Error>());

        var error = new Error(field, this);
        Errors!.Add(error);
        return error;
    }

    public ErrorResponseDto End()
    {
        return this;
    }
}