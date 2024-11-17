using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.CustomList;

public class UpdateCustomListDto
{
    [MinLength(3)] public string? Name { get; set; }

    public bool? IsPublic { get; set; }
}