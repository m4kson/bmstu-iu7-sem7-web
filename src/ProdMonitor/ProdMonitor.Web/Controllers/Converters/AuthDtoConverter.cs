using ProdMonitor.Domain.Models;
using ProdMonitor.Web.Dto.Auth;
using ProdMonitor.Web.Controllers.Converters.Enums;

namespace ProdMonitor.Web.Controllers.Converters;

public static class AuthDtoConverter
{
    public static RegisterModel ToDomain(this RegisterDto registerDto)
    {
        return new RegisterModel(
            email: registerDto.Email,
            password: registerDto.Password,
            name: registerDto.Name,
            surname: registerDto.Surname,
            patronymic: registerDto.Fathername,
            sex: registerDto.Sex.ToDomain(),
            department: registerDto.Department,
            birthDay: registerDto.BirthDate);
    }
    
    public static LoginModel ToDomain(this LoginDto loginDto)
    {
        return new LoginModel(
            email: loginDto.Login,
            password: loginDto.Password);
    }
}