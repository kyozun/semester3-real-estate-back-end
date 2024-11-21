using System.Net;
using semester3_real_estate_back_end.DTO.PropertyType;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Interfaces;

public interface IPropertyTypeRepository
{
    Task<IEnumerable<PropertyType>> GetPropertyTypes(PropertyTypeQuery propertyTypeQuery);
    Task<PropertyType?> GetPropertyTypeById(string propertyTypeId);
    Task<HttpStatusCode> CreatePropertyType(PropertyType propertyType);
    Task<HttpStatusCode> UpdatePropertyType(UpdatePropertyTypeDto updatePropertyTypeDto);
    Task<HttpStatusCode> DeletePropertyType(List<Guid> propertyTypeIds);
    Task<bool> PropertyTypeExistsAsync(string propertyTypeId);
}