using ProdMonitor.Web.Dto.Enums;

namespace ProdMonitor.Web.Dto.Users;

public class UserFilterDto
{
    public UserFilterDto() { }

    public UserFilterDto(string? department = null,
        DateOnly? birthDate = null,
        SexTypeDto? sex = null,
        RoleTypeDto? role = null,
        int skip = 0,
        int limit = int.MaxValue)
    {
        Department = department;
        BirthDate = birthDate;
        Sex = sex;
        Role = role;
        Skip = skip;
        Limit = limit;
    }
    
    public string? Department { get; set; }

    public DateOnly? BirthDate { get; set; }

    public SexTypeDto? Sex { get; set; }
    
    public RoleTypeDto? Role { get; set; }
    
    public int Skip { get; set; } = 0;
    
    public int Limit { get; set; } = int.MaxValue;
}