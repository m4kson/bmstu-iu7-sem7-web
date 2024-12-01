using System.Diagnostics.CodeAnalysis;
using ProdMonitor.Domain.Models;
using ProdMonitor.Domain.Models.Enums;
using ProdMonitor.Web.Controllers.Converters.Enums;
using ProdMonitor.Web.Dto.Users;

namespace ProdMonitor.Web.Controllers.Converters;

public static class UserDtoConverter
{
    [return: NotNullIfNotNull(nameof(user))]
    public static UserDto? ToDto(this User? user)
    {
        if (user is null)
            return null;

        return new UserDto
        (
            id: user.Id,
            name: user.Name,
            surname: user.Surname,
            fathername: user.Patronymic,
            department: user.Department,
            email: user.Email,
            passwordHash: user.PasswordHash,
            passwordSalt: user.PasswordSalt,
            birthDay: user.BirthDay,
            sex: user.Sex.ToDto(),
            role: user.Role.ToDto()
        );
    }

    public static UserFilter ToDomain(this UserFilterDto filterDto)
    {
        return new UserFilter(
            department: filterDto.Department,
            birthDay: filterDto.BirthDate,
            sex: filterDto.Sex?.ToDomain(),
            role: filterDto.Role?.ToDomain(),
            skip: filterDto.Skip,
            limit: filterDto.Limit);
    }

    public static UserUpdateRole ToDomain(this UserUpdateRoleDto updateRoleDto)
    {
        return new UserUpdateRole(updateRoleDto.Role.ToDomain());
    }
}