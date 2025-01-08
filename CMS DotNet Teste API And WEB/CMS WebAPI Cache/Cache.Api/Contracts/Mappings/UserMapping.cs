using Cache.Api.Contracts.Requests;
using Cache.Api.Contracts.Responses;
using Cache.Api.Models;

namespace Cache.Api.Contracts.Mappings;

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