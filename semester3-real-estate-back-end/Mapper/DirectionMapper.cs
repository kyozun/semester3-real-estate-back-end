using semester3_real_estate_back_end.DTO.Direction;
using semester3_real_estate_back_end.Models;
using semester4.DTO.Chapter;

namespace semester3_real_estate_back_end.Mapper;

public static class DirectionMapper
{
    public static DirectionDto ConvertToDirectionDto(this Direction direction)
    {
        return new DirectionDto
        {
            Name = direction.Name,
        };
    }


    public static DirectionAndMoreDto ConvertToDirectionAndMoreDto(this Direction direction)
    {
        var dto = new DirectionAndMoreDto
        {
            DirectionId = direction.DirectionId,
            Name = direction.Name,
            Properties = direction.Properties.Select(x => x.ConvertToPropertyDto()).ToList(),
        };

        return dto;
    }

    
    // Convert to Entity
    public static Direction ConvertToDirection(this CreateDirectionDto createDirectionDto)
    {
        var direction = new Direction
        {
            DirectionId = Guid.NewGuid().ToString(),
            Name = createDirectionDto.Name,
        };

        return direction;
    }
}