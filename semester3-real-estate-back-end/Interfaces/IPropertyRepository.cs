﻿using System.Net;
using semester3_real_estate_back_end.DTO.Property;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Interfaces;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetProperties(PropertyQuery propertyQuery);
    Task<Property?> GetPropertyById(string propertyId);
    Task<Property?> GetDraftPropertyById(string propertyId);
    Task<IEnumerable<Property>> GetDraftProperties(PropertyQuery propertyQuery);

    Task<HttpStatusCode> CreateProperty(CreatePropertyDto createPropertyDto);
    Task<HttpStatusCode> CreateDraftProperty(CreatePropertyDto createPropertyDto);
    Task<HttpStatusCode> UpdateProperty(UpdatePropertyDto updatePropertyDto);
    Task<HttpStatusCode> DeleteProperty(List<Guid> propertyIds);

    Task<string> SaveImageAsync(IFormFile image);

    Task<bool> PropertyExistsAsync(string propertyId);
    
}