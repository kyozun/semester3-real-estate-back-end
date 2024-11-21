using System.ComponentModel.DataAnnotations;

namespace semester3_real_estate_back_end.DTO.Juridical;

public class UpdateJuridicalDto
{
    [Required] public Guid? JuridicalId { get; set; }

    public string? Name { get; set; }
}