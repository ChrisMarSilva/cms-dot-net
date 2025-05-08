using Cache.Contracts.Request;
using Cache.Contracts.Response;
using Cache.Domain.Models;

namespace Cache.App.Api.Adapters;

public static class ContraUserMappingctMapping
{
    public static UserModel MapToUserModel(this UserRequestDto request) =>
        new UserModel(
            name: request.Name,
            email: request.Email,
            password: request.Password);

    public static UserResponseDto MapToResponse(this UserModel userModel) =>
        new UserResponseDto(
            id: userModel.Id,
            name: userModel.Name,
            email: userModel.Email,
            password: userModel.Password,
            dtHrCreated: userModel.DtHrCreated);

    public static IEnumerable<UserResponseDto> MapToResponse(this IEnumerable<UserModel> userModel) =>
        userModel.Select(userModel => userModel.MapToResponse());
}